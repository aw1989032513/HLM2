using ET.EventType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    internal class UpdateChatInfoEvent_RefreshUI : AEventAsync<EventType.UpdateChatInfo>
    {
        protected override async ETTask Run(UpdateChatInfo args)
        {
            args.zongScene.GetComponent<UIComponent>()?.GetDlgLogic<DlgChat>()?.Refresh();
            await ETTask.CompletedTask;
        }
    }
}
