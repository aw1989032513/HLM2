using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    /// <summary>
    /// 倒计时下线组件
    /// </summary>
    public class PlayerOfflineOutTimeComponent:Entity,IAwake,IDestroy
    {
        public long timer;
    }
}
