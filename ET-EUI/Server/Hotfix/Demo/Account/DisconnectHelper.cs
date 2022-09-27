using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    public static class DisconnectHelper
    {
        public static async ETTask Disconnect(this Session self)
        {
            if (self.IsDisposed || self == null)
            {
                return;
            }
            long sessionID = self.InstanceId;
            await TimerComponent.Instance.WaitAsync(1000);
            if (sessionID != self.InstanceId)
            {
                return;
            }
            self.Dispose();
        }
    }
}
