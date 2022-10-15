using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    public class EquipmentsComponentDestrory : DestroySystem<EquipmentsComponent>
    {
        public override void Destroy(EquipmentsComponent self)
        {
            self.Clear();
        }
    }
    public static class EquipmentsComponentSystem
    {
        public static void Clear(this EquipmentsComponent self)
        {
            ForeachHelper.Foreach(self.EquipItemsDic, (index, item) =>
            {
                item?.Dispose();
            });
            self.EquipItemsDic.Clear();
        }
        public static Item GetItemById(this EquipmentsComponent self, long itemId)
        {
            if (self.Children.TryGetValue(itemId, out Entity entity))
            {
                return entity as Item;
            }

            return null;
        }
        public static Item GetItemByPosition(this EquipmentsComponent self, EquipPosition equipPosition)
        {
            if (self.EquipItemsDic.TryGetValue((int)equipPosition, out Item item))
            {
                return item;
            }

            return null;
        }
        public static void AddEquipItem(this EquipmentsComponent self,Item item)
        {
            if (self.EquipItemsDic.TryGetValue(item.config.EquipPosition, out Item equipItem))
            {
                Log.Error($"Already EquipItem in Postion{(EquipPosition)item.config.EquipPosition}");
                return;
            }

            self.AddChild(item);
            self.EquipItemsDic.Add(item.config.EquipPosition, item);
        }
        public static bool IsEquipItemByPosition(this EquipmentsComponent self, EquipPosition equipPosition)
        {
            return self.EquipItemsDic.ContainsKey((int)equipPosition);
        }

        public static bool UnloadEquipItem(this EquipmentsComponent self, Item item)
        {
            if (self == null || self.IsDisposed)
            {
                Log.Debug("self is null");
            }
            if (item == null || item.IsDisposed)
            {
                Log.Debug("item is null");
            }
            self.EquipItemsDic.Remove(item.config.EquipPosition);
            item?.Dispose();
            return true;
        }
    }
}
