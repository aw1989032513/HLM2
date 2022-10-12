using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    public class C2M_StartGameLevelHandler : AMActorLocationRpcHandler<Unit, C2M_StartGameLevel, M2C_StartGameLevel>
    {
        protected override async ETTask Run(Unit unit, C2M_StartGameLevel request, M2C_StartGameLevel response, Action reply)
        {
            NumericComponent numericComponent = unit.GetComponent<NumericComponent>();

            if (numericComponent.GetAsInt((int)NumericType.AdventureState) != 0)
            {
                response.Error = ErrorCode.ERR_AlreadyAdventureState;
                reply();
                return;
            }

            if (numericComponent.GetAsInt((int)NumericType.DyingState) != 0)
            {
                response.Error = ErrorCode.ERR_AdventureInDying;
                reply();
                return;
            }

            if (!BattleLevelConfigCategory.Instance.Contain(request.LevelId))
            {
                response.Error = ErrorCode.ERR_AdventureErrorLevel;
                reply();
                return;
            }
            BattleLevelConfig config = BattleLevelConfigCategory.Instance.Get(request.LevelId);
            if (numericComponent[(int)NumericType.Level] < config.MiniEnterLevel[0])
            {
                response.Error = ErrorCode.ERR_AdventureLevelNotEnough;
                reply();
                return;
            }

            //设置关卡状态AdventureState为levelId，因为关卡状态设为0 表示不在关卡状态
            //同时还能从AdventureState获得正在哪个关卡中
            numericComponent.Set((int)NumericType.AdventureState, request.LevelId);
            numericComponent.Set((int)NumericType.AdventureStartTime, TimeHelper.ServerNow());
            //设置本次战斗的随机种子，保证客户端的战斗中的每次随机产生的数能在服务器端复现
            numericComponent.Set((int)NumericType.BattleRandomSeed, RandomHelper.RandUInt32());
            reply();

            await ETTask.CompletedTask;
        }
    }
}
