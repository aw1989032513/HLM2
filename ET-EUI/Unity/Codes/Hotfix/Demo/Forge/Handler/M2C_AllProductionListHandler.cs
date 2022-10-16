using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    /// <summary>
    /// 给客户端的ForgeComponent的ProductionsList赋值
    /// </summary>
    public class M2C_AllProductionListHandler : AMHandler<M2C_AllProductionList>
    {
        protected override async ETTask Run(Session session, M2C_AllProductionList message)
        {
            for (int i = 0; i < message.ProductionProto.Count; i++)
            {
                session.ZoneScene().GetComponent<ForgeComponent>().AddOrUpdateProductionQueue(message.ProductionProto[i]);
            }
            await ETTask.CompletedTask;
        }
    }
}
