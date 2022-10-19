using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    /// <summary>
    /// ChatInfo.id == player.unitId
    /// </summary>
    public  class ChatInfo:Entity,IAwake,IDestroy
    {
        public long gateSessionActorId;
        public string name;
    }
}
