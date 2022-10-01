using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    public class RoleInfosComponentAwakeSystem : AwakeSystem<RoleInfosComponent>
    {
        public override void Awake(RoleInfosComponent self)
        {
            throw new NotImplementedException();
        }
    }
    public class RoleInfosComponentDestorySystem : DestroySystem<RoleInfosComponent>
    {
        public override void Destroy(RoleInfosComponent self)
        {
            foreach (var item in self.roleInfosList)
            {
                item?.Dispose();
            }
            self.roleInfosList.Clear();
            self.currentRoleId = 0;
        }
    }
    public static class RoleInfosComponentSystem
    {
    }
}
