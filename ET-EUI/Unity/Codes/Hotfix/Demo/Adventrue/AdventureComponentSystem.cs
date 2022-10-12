using System;
using System.Collections.Generic;
using ET.EventType;
using UnityEngine;

namespace ET
{
    [Timer(TimerType.BattleRound)]
    public class AdventureBattleRoundTimer : ATimer<AdventureComponent>
    {
        public override void Run(AdventureComponent t)
        {
            t?.PlayOneBattleRound().Coroutine();
        }
    }
    public static class AdventureComponentSystem
    {
        public static async ETTask StartAdventure(this AdventureComponent self)
        {
            self.ResetAdventure();
            await self.CreateAdventureEnemy();
            //0.5秒后，启动战斗，只会执行一次
            self.battleTimer = TimerComponent.Instance.NewOnceTimer(500, TimerType.BattleRound, self);
        }
        /// <summary>
        /// 创建关卡敌人
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static async ETTask CreateAdventureEnemy(this AdventureComponent self)
        {
            //拿到任务当前的关卡levelId
            Unit unit = UnitHelper.GetMyUnitFromCurrentScene(self.DomainScene().CurrentScene());
            int levelId = unit.GetComponent<NumericComponent>().GetAsInt((int)NumericType.AdventureState);

            BattleLevelConfig battleLevelConfig = BattleLevelConfigCategory.Instance.Get(levelId);
            for (int i = 0; i < battleLevelConfig.MonsterIds.Length; i++)
            {
                Unit monsterUnit = await UnitFactory.CreateMonster(self.DomainScene().CurrentScene(), battleLevelConfig.MonsterIds[i]);
                monsterUnit.Position = new Vector3(1.5f, -2 + i, 0);
                self.enemyIdList.Add(monsterUnit.Id);
            }
        }
        /// <summary>
        /// 刷新关卡状态,角色状态
        /// </summary>
        /// <param name="self"></param>
        public static void ResetAdventure(this AdventureComponent self)
        {
            for (int i = 0; i < self.enemyIdList.Count; i++)
            {
                self.ZoneScene().CurrentScene().GetComponent<UnitComponent>().Remove(self.enemyIdList[i]);
            }

            TimerComponent.Instance?.Remove(ref self.battleTimer);
            self.battleTimer = 0;
            self.round = 0;
            self.enemyIdList.Clear();
            self.aliveEnemyIdList.Clear();

            Unit unit = UnitHelper.GetMyUnitFromCurrentScene(self.ZoneScene().CurrentScene());
            int maxHp = unit.GetComponent<NumericComponent>().GetAsInt((int)NumericType.MaxHp);
            unit.GetComponent<NumericComponent>().Set((int)NumericType.Hp, maxHp);
            unit.GetComponent<NumericComponent>().Set((int)NumericType.IsAlive, 1);

            Game.EventSystem.PublishAsync(new EventType.AdventureRoundReset() { ZoneScene = self.ZoneScene() }).Coroutine();
        }
        /// <summary>
        /// 开始回合战斗
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static async ETTask PlayOneBattleRound(this AdventureComponent self)
        {
            Unit unit = UnitHelper.GetMyUnitFromCurrentScene(self.ZoneScene().CurrentScene());
            if (self.round % 2 == 0)
            {
                //玩家回合
                Unit monsterUnit = self.GetTargetMonsterUnit();
                Game.EventSystem.PublishAsync(new EventType.AdventureBattleRoundView()
                {
                    zongScene = self.ZoneScene().CurrentScene(),
                    attackUnit = unit,
                    monsterUnit = monsterUnit
                }).Coroutine();
                //await Game.EventSystem.PublishAsync(new EventType.AdventureBattleRound()
                //{
                //    ZoneScene = self.ZoneScene(),
                //    AttackUnit = unit,
                //    TargetUnit = monsterUnit
                //});
                await TimerComponent.Instance.WaitAsync(1000);
            }
            else
            {
                //敌人回合
                for (int i = 0; i < self.enemyIdList.Count; i++)
                {
                    if (!unit.IsAlive())
                    {
                        break;
                    }

                    Unit monsterUnit = self.ZoneScene().CurrentScene().GetComponent<UnitComponent>().Get(self.enemyIdList[i]);

                    if (!monsterUnit.IsAlive())
                    {
                        continue;
                    }

                    Game.EventSystem.PublishAsync(new EventType.AdventureBattleRoundView()
                    {
                        zongScene = self.ZoneScene(),
                        attackUnit = monsterUnit,
                        monsterUnit = unit
                    }).Coroutine();

                    //await Game.EventSystem.PublishAsync(new EventType.AdventureBattleRound()
                    //{
                    //    ZoneScene = self.ZoneScene(),
                    //    AttackUnit = monsterUnit,
                    //    TargetUnit = unit
                    //});

                    await TimerComponent.Instance.WaitAsync(1000);
                }
            }
            self.BattleRoundOver();
        }
        /// <summary>
        /// 战斗结束
        /// </summary>
        /// <param name="self"></param>
        public static void BattleRoundOver(this AdventureComponent self)
        {
            ++self.round;
            BattleRoundResult battleRoundResult = self.GetBattleRoundResult();
            Log.Debug("当前回合结果:" + battleRoundResult);
            switch (battleRoundResult)
            {
                case BattleRoundResult.KeepBattle:
                    //继续开始一回合
                    self.battleTimer = TimerComponent.Instance.NewOnceTimer(TimeHelper.ServerNow() + 500, TimerType.BattleRound, self);
                    break;
                case BattleRoundResult.WinBattle:
                    Unit unit = UnitHelper.GetMyUnitFromCurrentScene(self.ZoneScene().CurrentScene());
                    Game.EventSystem.PublishAsync(new EventType.AdventureBattleOver() { zongScene = self.ZoneScene(), winUnit = unit }).Coroutine();
                    break;
                case BattleRoundResult.LoseBattle:
                    for (int i = 0; i < self.enemyIdList.Count; i++)
                    {
                        Unit monsterUint = self.ZoneScene().CurrentScene().GetComponent<UnitComponent>().Get(self.enemyIdList[i]);
                        if (!monsterUint.IsAlive())
                        {
                            continue;
                        }
                        Game.EventSystem.PublishAsync(new EventType.AdventureBattleOver() { zongScene = self.ZoneScene(), winUnit = monsterUint }).Coroutine();
                    }
                    break;          
            }

            //战报
            Game.EventSystem.PublishAsync(new EventType.AdventureBattleReport()
            {
                zongScene = self.ZoneScene(),
                battleRoundResult = battleRoundResult,
                round = self.round
            }).Coroutine() ;
        }
        /// <summary>
        /// 战斗结果
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static BattleRoundResult GetBattleRoundResult(this AdventureComponent self)
        {
            Unit unit = UnitHelper.GetMyUnitFromCurrentScene(self.ZoneScene().CurrentScene());
            if (!unit.IsAlive())
                {
                return BattleRoundResult.LoseBattle;
                 }
            Unit monterUnit = self.GetTargetMonsterUnit();
            if (monterUnit == null)
            {
                return BattleRoundResult.WinBattle;
            }
            return BattleRoundResult.KeepBattle;
        }
        /// <summary>
        /// 拿到当前Alive =1 的怪物MonsterUnit
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
         public static Unit GetTargetMonsterUnit(this AdventureComponent self)
        {
            self.aliveEnemyIdList.Clear();
            for (int i = 0; i < self.enemyIdList.Count; i++)
            {
                Unit monsterUnit = self.ZoneScene().CurrentScene().GetComponent<UnitComponent>().Get(self.enemyIdList[i]);

                if (monsterUnit.IsAlive())
                {
                    self.aliveEnemyIdList.Add(monsterUnit.Id);
                }
            }
            if (self.aliveEnemyIdList.Count <= 0)
            {
                return null;
            }
            return self.ZoneScene().CurrentScene().GetComponent<UnitComponent>().Get(self.aliveEnemyIdList[0]);
        }
    }
}
