using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    public class AccountInfoComponent:Entity,IAwake, IDestroy
    {
        public string token;
        public long accountId;
        /// <summary>
        /// 客户端跟Realm服务器链接的Key
        /// </summary>
        public string realmKey;
        /// <summary>
        /// 客户端跟Realm服务器链接的Realm地址
        /// </summary>
        public string realmAddress;
    }
}
