using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
#if SERVER
    public class RoleInfo:Entity,IAwake,ITransfer,IUnitCache
#else
    public class RoleInfo : Entity, IAwake
#endif
    {
        public string Name { get; set; }
        public int  ServerId { get; set; }
        public int State { get; set; }
        public long  AccountId { get; set; }
        public int LastLoginTime { get; set; }
        public int CreateTime { get; set; }
    }
}
