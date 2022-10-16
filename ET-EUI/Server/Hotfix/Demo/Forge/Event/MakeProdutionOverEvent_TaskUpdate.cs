using ET.EventType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    public class MakeProdutionOverEvent_TaskUpdate : AEventAsync<EventType.MakeProdutionOver>
    {
        protected override async ETTask Run(MakeProdutionOver a)
        {
            //args.Unit.GetComponent<TasksComponent>().TriggerTaskAction(TaskActionType.MakeItem, count: 1, targetId: args.ProductionConfigId);
            await ETTask.CompletedTask;
        }
    }
}
