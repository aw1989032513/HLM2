using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    public class M2C_AllTaskInfoListHandler : AMHandler<M2C_AllTaskInfoList>
    {
        protected override async ETTask Run(Session session, M2C_AllTaskInfoList message)
        {
            foreach (var item in message.TaskInfoProto)
            {
                session.ZoneScene().GetComponent<TasksComponent>().AddOrUpdateTaskInfo(item);
            }
            await ETTask.CompletedTask;
        }
    }
}
