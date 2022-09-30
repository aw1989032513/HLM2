using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    public class RoleInfo:Entity,IAwake
    {
        public string Name { get; set; }
        public int  ServerId { get; set; }
        public int State { get; set; }
        public long  AccountId { get; set; }
        public int LastLoginTime { get; set; }
        public int CreateTime { get; set; }
    }
}
