using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    public class ServerInfoManagerComponent: Entity,IAwake,IDestroy,ILoad
    {
        public List<ServerInfo> serverInfoList=new List<ServerInfo>();
    }
}
