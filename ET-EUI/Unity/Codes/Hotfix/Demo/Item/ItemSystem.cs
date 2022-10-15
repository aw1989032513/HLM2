using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    public class ItemAwakeSystem : AwakeSystem<Item, int>
    {
        public override void Awake(Item self, int configId)
        {
            self.configId = configId;
        }
    }
    public class ItemDestorySystem : DestroySystem<Item>
    {
        public override void Destroy(Item self)
        {
            self.quality = 0;
            self.configId = 0;
        }
    }
    public static class ItemSystem
    {
        /// <summary>
        /// 将服务器传来的itemInfo 赋值给 Item
        /// </summary>
        /// <param name="self"></param>
        /// <param name="itemInfo"></param>
        public static void DoAwake(this Item self, ItemInfo itemInfo)
        {
            self.Id = itemInfo.ItemId;
            self.configId = itemInfo.ItemConfigId;
            self.quality = itemInfo.ItemQuality;

            if (itemInfo.EquipInfo != null)
            {
                EquipInfoComponent equipInfoComponent = self.GetComponent<EquipInfoComponent>();

                if (equipInfoComponent == null)
                {
                    equipInfoComponent = self.AddComponent<EquipInfoComponent>();
                }
                equipInfoComponent.FromMessage(itemInfo.EquipInfo);
            }
        }
    }
}
