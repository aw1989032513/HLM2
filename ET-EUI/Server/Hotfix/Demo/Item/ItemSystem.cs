using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    public class ItemAwakeSystem : AwakeSystem<Item,int>
    {
        public override void Awake(Item self,int configId)
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
        /// 初始化
        /// </summary>
        /// <param name="self"></param>
        /// <param name="isAllInfo">是否将Item身上的所有组件信息传送给客户端</param>
        /// <returns></returns>
        public static ItemInfo ToMessageItemInfo(this Item self, bool isAllInfo = true)
        {
            ItemInfo itemInfo = new ItemInfo();
            itemInfo.ItemId = self.Id;
            itemInfo.ItemConfigId = self.configId;
            itemInfo.ItemQuality = self.quality;
            //是否将Item身上的所有组件信息传送给客户端
            if (!isAllInfo)
            {
                return itemInfo;
            }
            EquipInfoComponent equipInfoComponent = self.GetComponent<EquipInfoComponent>();
            if (equipInfoComponent != null)
            {
                itemInfo.EquipInfo = equipInfoComponent.ToMessageEquipInfoProto();
            }
            return itemInfo;
        }
    }
}
