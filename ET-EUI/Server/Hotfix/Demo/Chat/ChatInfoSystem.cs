using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    public class ChatInfoDestorySystem : DestroySystem<ChatInfo>
    {
        public override void Destroy(ChatInfo self)
        {
            self.name = null;
            self.gateSessionActorId = 0;
        }
    }
    public static class ChatInfoSystem
    {
       

    }
}
