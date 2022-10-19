using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    public class Chat2C_NoticeChatInfoHandler : AMHandler<Chat2C_NoticeChatInfo>
    {
        protected override async ETTask Run(Session session, Chat2C_NoticeChatInfo message)
        {
            ChatInfo chatInfo = session.DomainScene().GetComponent<ChatComponent>().AddChild<ChatInfo>(true);
            chatInfo.name = message.Name;
            chatInfo.message = message.ChatMessage;
            session.DomainScene().GetComponent<ChatComponent>().Add(chatInfo);

            Game.EventSystem.PublishAsync(new EventType.UpdateChatInfo() { zongScene = session.ZoneScene() }).Coroutine() ;
            await ETTask.CompletedTask;
        }
    }
}
