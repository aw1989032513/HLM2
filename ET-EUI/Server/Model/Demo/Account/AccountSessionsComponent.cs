using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    /// <summary>
    /// 控制账号和Seesion的连接组件
    /// </summary>
    public class AccountSessionsComponent:Entity,IAwake,IDestroy
    {
        /// <summary>
        /// accountId    sessionInstanceId
        /// </summary>
        public Dictionary<long,long> accountSeesionDic=new Dictionary<long,long>();
    }
}
