﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    public static class BagComponentSystem
    {
        public static Item GetItemById(this BagComponent self, long itemId)
        {
            if (self.ItemsDict.TryGetValue(itemId, out Item item))
            {
                return item;
            }
            return null;
        }
        public static int GetItemCountByItemType(this BagComponent self, ItemType itemType)
        {
            if (!self.ItemsMap.ContainsKey((int)itemType))
            {
                return 0;
            }
            return self.ItemsMap[(int)itemType].Count;
        }
        public static void AddItem(this BagComponent self, Item item)
        {
            if (self == null || self.IsDisposed)
            {
                Log.Error(ErrorCode.ERR_ItemNotExist.ToString()+"SELF");
            }
            self.AddChild(item);
            self.ItemsDict.Add(item.Id, item);
            self.ItemsMap.Add(item.config.Type, item);
        }
        public static void Clear(this BagComponent self)
        {
            foreach (var item in self.ItemsDict)
            {
                item.Value.Dispose();
            }
            self.ItemsDict.Clear();
            self.ItemsMap.Clear();
        }
        public static void RemoveItem(this BagComponent self, Item item)
        {
            if (item == null)
            {
                Log.Error("bag item is null");
                return;
            }

            self.ItemsDict.Remove(item.Id);
            self.ItemsMap.Remove(item.config.Type, item);
            item?.Dispose();
        }
    }
}