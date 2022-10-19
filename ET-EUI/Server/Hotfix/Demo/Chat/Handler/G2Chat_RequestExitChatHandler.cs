using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    /// <summary>
    /// 退出聊天服
    /// </summary>
    public class G2Chat_RequestExitChatHandler:AMActorRpcHandler<ChatInfo,G2Chat_RequestExitChat, Chat2G_RequestExitChat>
    {
        protected override async ETTask Run(ChatInfo chatInfo, G2Chat_RequestExitChat request, Chat2G_RequestExitChat response, Action reply)
        {
            ChatComponent chatComponent = chatInfo.DomainScene().GetComponent<ChatComponent>();
            chatComponent.Remove(chatInfo.Id);

            reply();
            await ETTask.CompletedTask;

        }

      
    }
}
