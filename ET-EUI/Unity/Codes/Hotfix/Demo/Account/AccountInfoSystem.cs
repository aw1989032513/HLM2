using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    public class AccountInfoDestorySystem:DestroySystem<AccountInfoComponent>
    {
        public override void Destroy(AccountInfoComponent self)
        {
            self.token = string.Empty;
            self.accountId = 0;
        }
    }
    public static class AccountInfoSystem
    {
    }
}
