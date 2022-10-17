using ET.EventType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    public class BattleWinEvent_TaskUpdate : AEventAsync<EventType.BattleWin>
    {
        protected override async ETTask Run(BattleWin args)
        {
            args.unit.GetComponent<TasksComponent>().TriggerTaskAction(TaskActionType.Adventrue, 1, args.levelId);
            await ETTask.CompletedTask;
        }
    }
}
