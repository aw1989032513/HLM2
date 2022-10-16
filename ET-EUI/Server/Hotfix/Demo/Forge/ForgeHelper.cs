using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    public static class ForgeHelper
    {
        /// <summary>
        /// 通知客户端同步下打造
        /// </summary>
        /// <param name="unit"></param>
        public static void SyncAllProduction(Unit unit)
        {
            ForgeComponent forgeComponent = unit.GetComponent<ForgeComponent>();
            M2C_AllProductionList m2C_AllProductionList = new M2C_AllProductionList();
            for (int i = 0; i < forgeComponent.ProductionsList.Count; i++)
            {
                m2C_AllProductionList.ProductionProto.Add(forgeComponent.ProductionsList[i].ToMessage());
            }
            MessageHelper.SendToClient(unit, m2C_AllProductionList);
        }
    }
}
