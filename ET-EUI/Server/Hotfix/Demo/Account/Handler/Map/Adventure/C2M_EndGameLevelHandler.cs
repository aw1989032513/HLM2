using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    /// <summary>
    /// 关卡结束战报处理
    /// </summary>
    public class C2M_EndGameLevelHandler : AMActorLocationRpcHandler<Unit, C2M_EndGameLevel, M2C_EndGameLevel>
    {
        protected override async ETTask Run(Unit unit, C2M_EndGameLevel request, M2C_EndGameLevel response, Action reply)
        {
            //检测关卡信息是否正常
            NumericComponent numericComponent = unit.GetComponent<NumericComponent>();

            int levelId = numericComponent.GetAsInt((int)NumericType.AdventureState);
            if (levelId == 0 || !BattleLevelConfigCategory.Instance.Contain(levelId))
            {
                response.Error = ErrorCode.ERR_AdventureLevelIdError;
                reply();
                return;
            }
            //检测上传的回合数是否正常
            if (request.Round <= 0)
            {
                response.Error = ErrorCode.ERR_AdventureRoundError;
                reply();
                return;
            }

            //战斗失败直接进入垂死状态
            if (request.BattleResult == (int)BattleRoundResult.LoseBattle)
            {
                numericComponent.Set((int)NumericType.DyingState, 1);
                numericComponent.Set((int)NumericType.AdventureState, 0);
                reply();
                return;
            }
            if (request.BattleResult != (int)BattleRoundResult.WinBattle)
            {
                response.Error = ErrorCode.ERR_AdventureResultError;
                reply();
                return;
            }


            //检测战斗胜利结果是否正常
            if (!unit.GetComponent<AdventureCheckComponent>().CheckBattleWinResult(request.Round))
            {
                response.Error = ErrorCode.ERR_AdventureWinResultError;
                reply();
                return;
            }
            numericComponent.Set((int)NumericType.AdventureState, 0);
            reply();

            //下发奖励todo
            //模拟下发装备
            for (int i = 0; i < 30; i++)
            {
                if (!BagHelper.AddItemByConfigId(unit,RandomHelper.RandomNumber(1002,1018)))
                {
                    Log.Error("增加背包物品失败");
                }
            }
            //增加经验
            numericComponent[(int)NumericType.Exp] += BattleLevelConfigCategory.Instance.Get(levelId).RewardExp;
            numericComponent[(int)NumericType.IronStone] += 3600;
            numericComponent[(int)NumericType.Fur] += 3600;
            await ETTask.CompletedTask;
        }
    }
}
