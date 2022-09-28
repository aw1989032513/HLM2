using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    /// <summary>
    /// 登录账号中心服
    /// </summary>
    public class LoginInfoRecordComponent:Entity,IAwake,IDestroy
    {
        public Dictionary<long, int> AccountLoginInfoDict = new Dictionary<long, int>();
    }
}
