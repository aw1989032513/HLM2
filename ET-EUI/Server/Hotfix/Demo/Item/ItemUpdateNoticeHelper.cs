using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
   /// <summary>
   /// Item更新通知客户端
   /// </summary>
    public static class ItemUpdateNoticeHelper
    {
        /// <summary>
        /// 增加物品
        /// </summary>
        /// <param name="unit"></param>
        /// <param name="item"></param>
        /// <param name="message"></param>
        public static void SyncAddItem(Unit unit,Item item , M2C_ItemUpdateOpInfo message)
        {
            //打包Item 
            message.ItemInfo = item.ToMessageItemInfo();
            message.Op = (int)ItemOp.Add;
            MessageHelper.SendToClient(unit, message);
        }
        /// <summary>
        /// 通知客户端 更新背包Items
        /// </summary>
        public static void SyncAllBagItems(Unit unit)
        {
            M2C_AllItemsList m2C_AllItemsList = new M2C_AllItemsList() { ContainerType = (int)ItemContainerType.Bag};
            BagComponent bagComponent = unit.GetComponent<BagComponent>();
            foreach (var item in bagComponent.ItemsDict.Values)
            {
                m2C_AllItemsList.ItemInfoList.Add(item.ToMessageItemInfo());
            }
            MessageHelper.SendToClient(unit, m2C_AllItemsList);
        }
        /// <summary>
        /// 让客户端同步玩家身上装备信息
        /// </summary>
        /// <param name="unit"></param>
        public static void SyncAllEquipItems(Unit unit)
        {
            M2C_AllItemsList m2CAllItemsList = new M2C_AllItemsList() { ContainerType = (int)ItemContainerType.RoleInfo }; ;
            EquipmentsComponent equipmentsComponent = unit.GetComponent<EquipmentsComponent>();
            foreach (var item in equipmentsComponent.EquipItemsDic.Values)
            {
                m2CAllItemsList.ItemInfoList.Add(item.ToMessageItemInfo());
            }
            MessageHelper.SendToClient(unit, m2CAllItemsList);
        }
        public static void SyncRemoveItem(Unit unit, Item item, M2C_ItemUpdateOpInfo message)
        {
            message.ItemInfo = item.ToMessageItemInfo(false);
            message.Op = (int)ItemOp.Remove;
            MessageHelper.SendToClient(unit, message);
        }
    }
}
