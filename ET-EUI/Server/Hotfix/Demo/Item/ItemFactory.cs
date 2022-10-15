﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    public static class ItemFactory
    {
        public static Item Create(Entity parent, int configId) 
        {
            if (!ItemConfigCategory.Instance.Contain(configId))
            {
                Log.Error($"当前所创建的物品id 不存在: {configId}");
                return null;
            }
            Item item = parent.AddChild<Item, int>(configId);
            //场景物品掉落
            item.RandomQuality();
            AddComponentByItemType(item);
            return item;
        }
        public static void AddComponentByItemType(Item item)
        {
            switch ((ItemType)item.config.Type)
            {
                case ItemType.Weapon:
                case ItemType.Armor:
                case ItemType.Ring:
                    {
                        item.AddComponent<EquipInfoComponent>();
                    }
                    break;
                case ItemType.Prop:
                    {

                    }
                    break;
            }
        }
    }
}
