using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    /// <summary>
    /// 类似心跳包，
    /// </summary>
    public class AccountCheckOutTimeComponent:Entity,IAwake<long>,IDestroy
    {
        public long timer;
        public long accountID;
    }
}
