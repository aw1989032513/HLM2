using ET.EventType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    public class UpdateTaskInfoEvent_ShowRedDot : AEventAsync<EventType.UpdateTaskInfo>
    {
        protected override async ETTask Run(UpdateTaskInfo args)
        {
            bool isExist = args.zongScene.GetComponent<TasksComponent>().IsExistTaskComplete();
            if (isExist)
            {
                RedDotHelper.ShowRedDotNode(args.zongScene, "Task");
            }
            else
            {
                if (RedDotHelper.IsLogicAlreadyShow(args.zongScene, "Task"))
                {
                    RedDotHelper.HideRedDotNode(args.zongScene, "Task");
                }
            }
            args.zongScene.GetComponent<UIComponent>()?.GetDlgLogic<DlgTask>()?.Refresh();
            await ETTask.CompletedTask;
        }
    }
}
