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
            self.ResetAdventureInfo();
        }
    }
    public static class AdventureCheckComponentSystem
    {
        public static void ResetAdventureInfo(this AdventureCheckComponent self)
        {
            self.round = 0;
            self.animationTotalTime = 0;
            self.monsterTotalDamage = 0;
            self.unitTotalDamage = 0;
            self.totalMonsterHp = 0;
            self.enemyHpDic.Clear();
        }
        public static bool CheckBattleWinResult(this AdventureCheckComponent self, int battleRound)
        {
            try
            {
                self.ResetAdventureInfo();
                NumericComponent numericComponent = self.GetParent<Unit>().GetComponent<NumericComponent>();
                int levelId = numericComponent.GetAsInt((int)NumericType.AdventureState);
                //模拟对战
                bool isSimulationNormal = self.SimulationBattle(levelId, battleRound);
                if (!isSimulationNormal)
                {
                    Log.Error("模拟对战失败");
                    return false;
                }

                //判定角色是否存活
                if (self.monsterTotalDamage >= numericComponent.GetAsInt((int)NumericType.MaxHp))
                {
                    Log.Error("玩家挂了");
                    return false;
                }
                if (self.unitTotalDamage < self.totalMonsterHp)
                {
                    Log.Error("玩家没打死所有怪");
                    return false;
                }
                //判定动画时间
                long playAnimTime = TimeHelper.ServerNow() - numericComponent.GetAsLong((int)NumericType.AdventureStartTime);
                if (playAnimTime < self.animationTotalTime)
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

        public static bool SimulationBattle(this AdventureCheckComponent self, int levelId,int battleRound)
        {
            //创建怪物信息
            BattleLevelConfig battleLevelConfig = BattleLevelConfigCategory.Instance.Get(levelId);
            for (int i = 0; i < battleLevelConfig.MonsterIds.Length; i++)
            {
                UnitConfig unitConfig = UnitConfigCategory.Instance.Get(battleLevelConfig.MonsterIds[i]);
                self.enemyHpDic.Add(i, unitConfig.MaxHP);
                self.totalMonsterHp += unitConfig.MaxHP;
            }

            //开始模拟对战
            for (int i = 0; i < battleRound; i++)
            {
                if (self.round % 2 == 0)
                {
                    //玩家回合
                    int targetIndex = self.GetFirstAliveEnemyIndex(levelId);
                    if (targetIndex < 0 )
                    {
                        Log.Error($"targetIndex error:{targetIndex}");
                        return false;
                    }
                    int unitDamage = self.GetParent<Unit>().GetComponent<NumericComponent>().GetAsInt((int)NumericType.DamageValue);
                    self.enemyHpDic[targetIndex] -= unitDamage;
                    self.unitTotalDamage += unitDamage;
                    self.animationTotalTime += 1000;
                }
                else
                {
                    //敌人回合
                    for (int j = 0; j < battleLevelConfig.MonsterIds.Length; j++)
                    {
                        if (self.enemyHpDic[j] <= 0)
                        {
                            continue;
                        }

                        self.monsterTotalDamage += UnitConfigCategory.Instance.Get(battleLevelConfig.MonsterIds[j]).DamageValue;
                        self.animationTotalTime += 1000;
                    }
                }
                ++self.round;
            }
            return true;
        }
        public static int GetFirstAliveEnemyIndex(this AdventureCheckComponent self,int levelId)
        {
            BattleLevelConfig battleLevelConfig = BattleLevelConfigCategory.Instance.Get(levelId);
            for (int i = 0; i < battleLevelConfig.MonsterIds.Length; i++)
            {
                if (self.enemyHpDic[i] > 0)
                {
                    return i;
                }
            }
            return -1;
        }
     }
}
