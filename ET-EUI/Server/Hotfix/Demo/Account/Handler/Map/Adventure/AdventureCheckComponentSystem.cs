using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    public class AdventureCheckComponentDestorySystem : DestroySystem<AdventureCheckComponent>
    {
        public override void Destroy(AdventureCheckComponent self)
        {
            foreach (var monsterId in self.cacheEnemyIdList)
            {
                self.DomainScene().GetComponent<UnitComponent>().Remove(monsterId);
            }
            self.cacheEnemyIdList.Clear();
            self.enemyIdList.Clear();
            self.aniamtionTotalTime = 0;
            self.random = null;
        }
    }
    public static class AdventureCheckComponentSystem
    {
        public static void ResetAdventureInfo(this AdventureCheckComponent self)
        {
            self.aniamtionTotalTime = 0;
            NumericComponent numericComponent = self.GetParent<Unit>().GetComponent<NumericComponent>();
            numericComponent.SetNoEvent((int)NumericType.Hp, numericComponent.GetAsInt((int)NumericType.MaxHp));
            numericComponent.SetNoEvent((int)NumericType.IsAlive, 1);
        }
        public static bool CheckBattleWinResult(this AdventureCheckComponent self, int battleRound)
        {
            try
            {
                self.ResetAdventureInfo();
                self.SetBattleRandomSeed();
                self.CreateBattleMonsterUnit();
               
                //模拟对战
                bool isSimulationNormal = self.SimulationBattle(battleRound);
                if (!isSimulationNormal)
                {
                    Log.Error("模拟对战失败");
                    return false;
                }

                //判定角色是否存活
                if (!self.GetParent<Unit>().IsAlive())
                {
                    Log.Error("玩家挂了");
                    return false;
                }
                if (self.GetFirstAliveEnemy() != null)
                {
                    Log.Error("玩家没打死所有怪");
                    return false;
                }
                //判定动画时间
                NumericComponent numericComponent = self.GetParent<Unit>().GetComponent<NumericComponent>();
                long playAnimTime = TimeHelper.ServerNow() - numericComponent.GetAsLong((int)NumericType.AdventureStartTime);
                if (playAnimTime < self.aniamtionTotalTime)
                {
                    Log.Error("动画时间不足");
                    return false;
                }
                return true;
            }
            finally
            {
                self.ResetAdventureInfo();
            }
        }

        public static bool SimulationBattle(this AdventureCheckComponent self, int battleRound)
        {
            //开始模拟对战
            for (int i = 0; i < battleRound; i++)
            {
                if (i % 2 == 0)
                {
                    //玩家回合
                    Unit monsterUnit = self.GetFirstAliveEnemy();
                    if (monsterUnit == null)
                    {
                        Log.Error($"monsterUnit is null:{monsterUnit}");
                        return false;
                    }
                    self.aniamtionTotalTime += 1000;
                    self.CalcuateDamageHpValue(self.GetParent<Unit>(), monsterUnit);
                }
                else  //敌人回合
                {
                     //玩家是否死亡
                    if (!self.GetParent<Unit>().IsAlive())
                    {
                        return false;
                    }

                    for (int j = 0; j < self.enemyIdList.Count; j++)
                    {
                        Unit monsterUnit = self.DomainScene().GetComponent<UnitComponent>().Get(self.enemyIdList[j]);
                        if (!monsterUnit.IsAlive())
                        {
                            continue;
                        }
                        self.aniamtionTotalTime += 1000;

                        self.CalcuateDamageHpValue(monsterUnit, self.GetParent<Unit>());
                    }
                }
            }
            return true;
        }
        /// <summary>
        /// 获取第一个存活的怪物
        /// </summary>
        /// <param name="self"></param>
        /// <param name="levelId"></param>
        /// <returns></returns>
        public static Unit GetFirstAliveEnemy(this AdventureCheckComponent self)
        {
            for (int i = 0; i < self.enemyIdList.Count; i++)
            {
                Unit monsterUnit = self.DomainScene().GetComponent<UnitComponent>().Get(self.enemyIdList[i]);
                if (monsterUnit.IsAlive())
                {
                    return monsterUnit;
                }
            }
            return null;
        }
        /// <summary>
        /// 设置随机战斗种子
        /// </summary>
        public static void SetBattleRandomSeed(this AdventureCheckComponent self)
        {
          int seed =  self.GetParent<Unit>().GetComponent<NumericComponent>().GetAsInt((int)NumericType.BattleRandomSeed);
            if (self.random == null)
            {
                self.random = new SRandom((uint)seed);
            }
            else
            {
                self.random.SetRandomSeed((uint)seed);
            }
        }
        /// <summary>
        /// 创建当前关卡怪物
        /// </summary>
        /// <param name="self"></param>
        public static void CreateBattleMonsterUnit(this AdventureCheckComponent self)
        {
           int leveid= self.GetParent<Unit>().GetComponent<NumericComponent>().GetAsInt((int)NumericType.AdventureState);
           BattleLevelConfig battleLevelConfig = BattleLevelConfigCategory.Instance.Get(leveid);
           int monsterCount = battleLevelConfig.MonsterIds.Length - self.cacheEnemyIdList.Count;
            //生成怪物
            for (int i = 0; i < monsterCount; i++)
            {
                Unit monsterUnit = UnitFactory.CreateMonster(self.DomainScene(),1002);
                self.cacheEnemyIdList.Add(monsterUnit.Id);
            }

            //复用怪物Unit
            self.enemyIdList.Clear();
            for (int i = 0; i < battleLevelConfig.MonsterIds.Length; i++)
            {
                Unit monsterUnit = self.DomainScene().GetComponent<UnitComponent>().Get(self.cacheEnemyIdList[i]);
                UnitConfig unitConfig = UnitConfigCategory.Instance.Get(battleLevelConfig.MonsterIds[i]);
                //由于 monsterUnit.ConfigId是空 的，所以赋值下
                monsterUnit.ConfigId = unitConfig.Id;

                NumericComponent numericComponent = monsterUnit.GetComponent<NumericComponent>();
                numericComponent.SetNoEvent((int)NumericType.MaxHp, monsterUnit.Config.MaxHP);
                numericComponent.SetNoEvent((int)NumericType.Hp, monsterUnit.Config.MaxHP);
                numericComponent.SetNoEvent((int)NumericType.DamageValue, monsterUnit.Config.DamageValue);
                numericComponent.SetNoEvent((int)NumericType.IsAlive, 1);
                self.enemyIdList.Add(monsterUnit.Id);
            }
        }
        /// <summary>
        /// 计算伤害值
        /// </summary>
        /// <param name="self"></param>
        public static void CalcuateDamageHpValue(this AdventureCheckComponent self,Unit attackUnit, Unit targeUnit)
        {
            int Hp = targeUnit.GetComponent<NumericComponent>().GetAsInt((int)NumericType.Hp);
            Hp = Hp - DamageCalcuateHelper.CalcuateDamageValue(attackUnit, targeUnit, ref self.random);
            if (Hp <= 0)
            {
                Hp = 0;
                //保存敌人状态
                targeUnit.GetComponent<NumericComponent>().SetNoEvent((int)NumericType.IsAlive, 0);
            }
            targeUnit.GetComponent<NumericComponent>().SetNoEvent((int)NumericType.Hp, Hp);
        }
     }
}
