using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    public static class SeverInfoSystem
    {
        public static void DoAwake(this ServerInfo self,ServerInfoProto serverInfoProto)
        {
            self.Id = serverInfoProto.Id;
            self.serverName = serverInfoProto.ServerName;
            self.status = serverInfoProto.Status;
        }
        /// <summary>
        /// 赋值
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static ServerInfoProto ToMessage(this ServerInfo self)
        {
            return new ServerInfoProto() { Id = (int)self.Id, ServerName = self.serverName, Status = self.status };
        }
    }
}
