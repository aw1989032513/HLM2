using ET.EventType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    /// <summary>
    /// 打造任务完成
    /// </summary>
    public class MakeProductionOverEvent_TaskUpdate : AEventAsync<EventType.MakeProdutionOver>
    {      
        protected override async ETTask Run(MakeProdutionOver a)
        {
            a.unit.GetComponent<TasksComponent>().TriggerTaskAction(TaskActionType.MakeItem, 1, a.productionConfigId);
            await ETTask.CompletedTask;
        }
    }
}
