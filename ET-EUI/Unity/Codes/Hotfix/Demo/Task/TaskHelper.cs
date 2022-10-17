﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    public static class TaskHelper
    {
        public static async ETTask<int> GetTaskReward(Scene ZoneScene, int taskConfigId)
        {
            TaskInfo taskInfo = ZoneScene.GetComponent<TasksComponent>().GetTaskInfoByConfigId(taskConfigId);
            if (taskInfo == null || taskInfo.IsDisposed)
            {
                return ErrorCode.ERR_NoClientTaskInfoExist;
            }
            if (!taskInfo.IsTaskState(TaskState.Complete))
            {
                return ErrorCode.ERR_TaskClientNoCompleted;
            }
            M2C_ReceiveTaskReward m2CReciveTaskReward = null;
            try
            {
                m2CReciveTaskReward = (M2C_ReceiveTaskReward)await ZoneScene.GetComponent<SessionComponent>().Session.Call(new C2M_ReceiveTaskReward() { TaskConfigId = taskConfigId });
            }
            catch (Exception e)
            {
                Log.Error(e.ToString());
                return ErrorCode.ERR_NetWorkError;
            }

            return m2CReciveTaskReward.Error;
        }
    }
}
