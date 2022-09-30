using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    /// <summary>
    /// 存储拿到ServerInfosList
    /// </summary>
    public class ServerInfosCompontent:Entity,IAwake,IDestroy
    {
        public List<ServerInfo> ServerInfosList=new List<ServerInfo>();
    }
}
