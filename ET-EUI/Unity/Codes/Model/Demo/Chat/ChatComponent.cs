using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    public class ChatComponent:Entity,IAwake,IDestroy
    {
        public Queue<ChatInfo> ChatMessageQueue = new Queue<ChatInfo>(100);
    }
}
