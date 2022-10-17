using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    /// <summary>
    /// 领取奖励
    /// </summary>
    public class C2M_ReceiveTaskRewardHandler : AMActorLocationRpcHandler<Unit, C2M_ReceiveTaskReward, M2C_ReceiveTaskReward>
    {
        protected override async ETTask Run(Unit unit, C2M_ReceiveTaskReward request, M2C_ReceiveTaskReward response, Action reply)
        {
            TasksComponent tasksComponent = unit.GetComponent<TasksComponent>();

            int errorCode =  tasksComponent.TryReceiveTaskReward(request.TaskConfigId);
            if (errorCode != ErrorCode.ERR_Success)
            {
                response.Error = errorCode;
                reply();
                return;
            }
            //刷新领取任务状态
            tasksComponent.ReceiveTaskRewardState(unit, request.TaskConfigId);
            unit.GetComponent<NumericComponent>()[(int)NumericType.Gold] += TaskConfigCategory.Instance.Get(request.TaskConfigId).RewardGoldCount;
            reply();
            await ETTask.CompletedTask;
        }
    }
}
