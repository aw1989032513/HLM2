using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    public  class ServerInfosCompontentDestorySystem : DestroySystem<ServerInfosCompontent>
    {
        public override void Destroy(ServerInfosCompontent self)
        {
            foreach (var item in self.ServerInfosList)
            {
                item?.Dispose();
            }
            self.ServerInfosList.Clear();
        }
    }
    public static class ServerInfosCompontentSystem
    {
        public static void Add(this ServerInfosCompontent self,ServerInfo serverInfo)
        {
            self.ServerInfosList.Add(serverInfo); 
        }
    }
}
