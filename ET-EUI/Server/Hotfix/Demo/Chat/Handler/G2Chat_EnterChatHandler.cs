using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    public class G2Chat_EnterChatHandler : AMActorRpcHandler<Scene, G2Chat_EnterChat, Chat2G_EnterChat>
    {
        protected override async ETTask Run(Scene scene, G2Chat_EnterChat request, Chat2G_EnterChat response, Action reply)
        {
            ChatComponent chatComponent = scene.GetComponent<ChatComponent>();
            ChatInfo chatInfo = chatComponent.Get(request.UnitId);
            if (chatInfo != null && !chatInfo.IsDisposed)
            {
                chatInfo.name = request.Name;
                chatInfo.gateSessionActorId = request.GateSessionActorId;

                response.ChatInfoUnitInstanceId = chatInfo.InstanceId;
                reply();
                return;
            }
            else
            {
                chatInfo = chatComponent.AddChildWithId<ChatInfo>(request.UnitId);
                chatInfo.AddComponent<MailBoxComponent>();

                chatInfo.name = request.Name;
                chatInfo.gateSessionActorId = request.GateSessionActorId;
                response.ChatInfoUnitInstanceId = chatInfo.InstanceId;
                chatComponent.Add(chatInfo);
                reply();
            }
 
            await ETTask.CompletedTask;
        }
    }
}
