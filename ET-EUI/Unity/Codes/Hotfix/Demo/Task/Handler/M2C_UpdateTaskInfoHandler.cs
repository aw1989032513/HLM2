using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    internal class M2C_UpdateTaskInfoHandler : AMHandler<M2C_UpdateTaskInfo>
    {
        protected override async ETTask Run(Session session, M2C_UpdateTaskInfo message)
        {
          session.ZoneScene().GetComponent<TasksComponent>().AddOrUpdateTaskInfo(message.TaskInfoProto);
            await ETTask.CompletedTask;
        }
    }
}
