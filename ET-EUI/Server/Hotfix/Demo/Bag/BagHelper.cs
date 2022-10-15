using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    public static class BagHelper
    {
        /// <summary>
        /// 给Unit发装备
        /// </summary>
        /// <param name="unit"></param>
        /// <param name="configId"></param>
        /// <returns></returns>
        public static bool AddItemByConfigId(Unit unit,int configId)
        {
            BagComponent bagComponent = unit.GetComponent<BagComponent>();
            if (bagComponent == null)
            {
                Log.Error("bagComponent is null");
                return false;
            }
            if (!bagComponent.IsCanAddItemByConfigId(configId))
            {
                return false;
            }
            return bagComponent.AddItemByConfigId(configId);
        }
    }
}
