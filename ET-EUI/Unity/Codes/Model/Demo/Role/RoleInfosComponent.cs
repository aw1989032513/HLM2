using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    public  class RoleInfosComponent:Entity,IAwake,IDestroy
    {
        public List<RoleInfo> roleInfosList = new List<RoleInfo>();
        /// <summary>
        /// 进入游戏的角色ID
        /// </summary>
        public long currentRoleId = 0;
    }
}
