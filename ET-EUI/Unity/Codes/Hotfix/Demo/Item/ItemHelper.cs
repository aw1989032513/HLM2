using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    public static class ItemHelper
    {
        /// <summary>
        /// 增加Item
        /// </summary>
        /// <param name="ZoneScene"></param>
        /// <param name="item"></param>
        /// <param name="itemContainerType"></param>
        public static void AddItem(Scene ZoneScene, Item item, ItemContainerType itemContainerType)
        {
            if (itemContainerType == ItemContainerType.Bag)
            {
                ZoneScene.GetComponent<BagComponent>().AddItem(item);
            }
            else if (itemContainerType == ItemContainerType.RoleInfo)
            {
                ZoneScene.GetComponent<EquipmentsComponent>().AddEquipItem(item);
            }
        }
        public static void Clear(Scene ZoneScene, ItemContainerType itemContainerType)
        {
            if (itemContainerType == ItemContainerType.Bag)
            {
                ZoneScene?.GetComponent<BagComponent>()?.Clear();
            }
            else if (itemContainerType == ItemContainerType.RoleInfo)
            {
               ZoneScene?.GetComponent<EquipmentsComponent>()?.Clear();
            }
        }
        public static Item GetItem(Scene ZoneScene, long itemId, ItemContainerType itemContainerType)
        {
            if (itemContainerType == ItemContainerType.Bag)
            {
                return ZoneScene.GetComponent<BagComponent>().GetItemById(itemId);
            }
            else if (itemContainerType == ItemContainerType.RoleInfo)
            {
                return ZoneScene.GetComponent<EquipmentsComponent>().GetItemById(itemId);
            }

            return null;
        }
        public static void RemoveItemById(Scene ZoneScene, long itemId, ItemContainerType itemContainerType)
        {
            Item item = GetItem(ZoneScene, itemId, itemContainerType);
            if (item == null)
            {
                Log.Error($"通过Id获取Item失败{itemId}");
            }
            if (itemContainerType == ItemContainerType.Bag)
            {
                ZoneScene.GetComponent<BagComponent>().RemoveItem(item);
            }
            else if (itemContainerType == ItemContainerType.RoleInfo)
            {
                
                ZoneScene.GetComponent<EquipmentsComponent>().UnloadEquipItem(item);
            }
        }
    }
}
