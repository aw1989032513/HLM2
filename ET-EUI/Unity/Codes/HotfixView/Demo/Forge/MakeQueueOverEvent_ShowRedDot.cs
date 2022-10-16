using ET.EventType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    /// <summary>
    /// 生产装备结束事件
    /// </summary>
    internal class MakeQueueOverEvent_ShowRedDot : AEventAsync<EventType.MakeQueueOver>
    {
        protected override async ETTask Run(MakeQueueOver args)
        {
            //是否成产完了
            bool isExist = args.zongScene.GetComponent<ForgeComponent>().IsExistMakeQueueOver();
            if (isExist)
            {
                //红点显示
                RedDotHelper.ShowRedDotNode(args.zongScene, "Forge");
            }
            else
            {
                if (RedDotHelper.IsLogicAlreadyShow(args.zongScene, "Forge"))
                {
                    RedDotHelper.HideRedDotNode(args.zongScene, "Forge");
                }               
            }
            await ETTask.CompletedTask;
        }
    }
}
