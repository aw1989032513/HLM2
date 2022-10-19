using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    public class C2Chat_SendChatInfoHandler : AMActorRpcHandler<ChatInfo, C2Chat_SendChatInfo, Chat2C_SendChatInfo>
    {
        protected override async ETTask Run(ChatInfo chatInfo, C2Chat_SendChatInfo request, Chat2C_SendChatInfo response, Action reply)
        {
            if (string.IsNullOrEmpty(request.ChatMessage))
            {
                response.Error = ErrorCode.ERR_ChatMessageEmpty;
                reply();
                return;
            }

            ChatComponent chatComponent = chatInfo.DomainScene().GetComponent<ChatComponent>();
            foreach (var otherUnit in chatComponent.ChatInfoUnitsDict.Values)
            {
                MessageHelper.SendActor(otherUnit.gateSessionActorId, new Chat2C_NoticeChatInfo()
                {
                    Name = chatInfo.name,ChatMessage = request.ChatMessage
                });
            }

            reply();
            await ETTask.CompletedTask;
        }
    }
}
