using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    public class AccountSessionsComponentDestorySystem : DestroySystem<AccountSessionsComponent>
    {
        public override void Destroy(AccountSessionsComponent self)
        {
            self.accountSeesionDic.Clear();
        }
    }
    public static class AccountSessionsComponentSystem
    {
        public static long Get(this AccountSessionsComponent self, long accountId)
        {
            if (!self.accountSeesionDic.TryGetValue(accountId, out long instanceId))
            {
                return 0;
            }
            return instanceId;
        }
        public static void Add(this AccountSessionsComponent self, long accountId, long sessionInstanceId)
        {
            if (self.accountSeesionDic.ContainsKey(accountId))
            {
                self.accountSeesionDic[accountId] = sessionInstanceId;
                return;
            }
            self.accountSeesionDic.Add(accountId, sessionInstanceId);
        }
        public static void Remove(this AccountSessionsComponent self, long accountId)
        {
            if (self.accountSeesionDic.ContainsKey(accountId))
            {
                self.accountSeesionDic.Remove(accountId);
            }
        }

    }
}
