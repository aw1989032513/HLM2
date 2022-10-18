using ET.EventType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    [NumericWatcher((int)NumericType.Level)]
    public class NumericWatcher_UpLevel : INumericWatcher
    {
        public void Run(NumbericChange args)
        {
            if (!(args.Parent is Unit unit))
            {
                return;
            }
            unit = args.Parent as Unit;
            //触发任务行为
            unit.GetComponent<TasksComponent>().TriggerTaskAction(TaskActionType.UpLevel, (int)args.New);
            //排行榜
            RankHelper.AddOrUpdateLevelRank(unit);
        }
    }
}
