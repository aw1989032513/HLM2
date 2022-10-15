using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    public class EquipmentsComponentDestroy : DestroySystem<EquipmentsComponent>
    {
        public override void Destroy(EquipmentsComponent self)
        {
            foreach (var item in self.EquipItemsDic.Values)
            {
                item?.Dispose();
            }
            self.EquipItemsDic.Clear();
            self.message = null;
        }
    }
    /// <summary>
    /// 反序列化
    /// </summary>
    public class EquipmentsComponentDeserializeSystem : DeserializeSystem<EquipmentsComponent>
    {
        public override void Deserialize(EquipmentsComponent self)
        {
            foreach (var entity in self.Children.Values)
            {
                Item item = entity as Item;
                self.EquipItemsDic.Add(item.config.EquipPosition, item);
            }
        }
    }
    public static  class EquipmentsComponentSystem
    {
        /// <summary>
        /// 装备位置处是否有装备
        /// </summary>
        /// <returns></returns>
        public static bool IsEquipItemByPosition(this EquipmentsComponent self, EquipPosition equipPosition)
        {
            self.EquipItemsDic.TryGetValue((int)equipPosition, out Item item);
            return item != null && !item.IsDisposed;
        }
        /// <summary>
        /// 装备物体
        /// </summary>
        /// <param name="self"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public static bool EquipItem(this EquipmentsComponent self, Item item)
        {
            if (!self.EquipItemsDic.ContainsKey(item.config.EquipPosition))
            {
                self.AddChild(item);
                self.EquipItemsDic.Add(item.config.EquipPosition, item);
                Game.EventSystem.Publish(new EventType.ChangeEquipItem()
                {
                    unit = self.GetParent<Unit>(),
                    item = item,
                    equipOp = EquipOp.Load
                });
                ItemUpdateNoticeHelper.SyncAddItem(self.GetParent<Unit>(), item, self.message);
                return true;
            }
            return false;
        }
        /// <summary>
        /// 从字典中拿出来相应位置的装备
        /// </summary>
        /// <param name="self"></param>
        /// <param name="equipPosition"></param>
        /// <returns></returns>
        public static Item GetEquipItemByPosition(this EquipmentsComponent self, EquipPosition equipPosition)
        {
            if (!self.EquipItemsDic.TryGetValue((int)equipPosition,out Item item))
            {
                return null;
            }
            return item;
        }
        /// <summary>
        /// 卸下装备
        /// </summary>
        /// <param name="self"></param>
        /// <param name="equipPosition"></param>
        /// <returns></returns>
        public static Item UnloadEquipItemByPosition(this EquipmentsComponent self, EquipPosition equipPosition)
        {
            if (self.EquipItemsDic.TryGetValue((int)equipPosition,out Item item))
            {
                self.EquipItemsDic.Remove((int)equipPosition);
                //但是此时Item还挂载在EquipmentsComponent身上，我们要移动到BagComponent身上

                //事件，更新数值
                Game.EventSystem.Publish(new EventType.ChangeEquipItem() { unit = self.GetParent<Unit>(), item = item, equipOp = EquipOp.Unload });
               //通知客户端卸下装备
                ItemUpdateNoticeHelper.SyncRemoveItem(self.GetParent<Unit>(), item, self.message);
            }
            return item;
        }
    }
}
