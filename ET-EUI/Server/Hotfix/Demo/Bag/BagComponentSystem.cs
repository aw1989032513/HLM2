using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    public class BagComponentDestorySystem : DestroySystem<BagComponent>
    {
        public override void Destroy(BagComponent self)
        {
            foreach (var item in self.ItemsDict)
            {
                item.Value.Dispose();
            }
            self.ItemsDict.Clear();
            self.ItemsMap.Clear();
        }
    }
    /// <summary>
    /// 实现反序列化接口
    /// </summary>
    public class BagComponenIDeserializeSystem : DeserializeSystem<BagComponent>
    {
        public override void Deserialize(BagComponent self)
        {
            foreach (var entity in self.Children.Values)
            {
                self.AddContainer(entity as Item);
            }
        }
    }
    public static class BagComponentSystem
    {
        public static Item GetItemById(this BagComponent self,long itemId)
        {
            if (self.ItemsDict.TryGetValue(itemId,out Item item))
            {
                return item;
            }
            return null;
        }
        /// <summary>
        /// 是否可以增加装备物品
        /// </summary>
        /// <param name="self"></param>
        /// <param name="itemId"></param>
        /// <returns></returns>
        public static bool IsCanAddItemByConfigId(this BagComponent self, int itemId)
        {
            if (!ItemConfigCategory.Instance.Contain(itemId))
            {
                return false;
            }
            if (self.IsMaxLoad())
            {
                return false;
            }
            return true;
        }
        public static bool AddItemByConfigId(this BagComponent self, int configId,int count = 1)
        {
            if (!ItemConfigCategory.Instance.Contain(configId))
            {
                return false;
            }
            if (count <= 0)
            {
                return false;
            }
            for (int i = 0; i < count; i++)
            {
                Item newItem = ItemFactory.Create(self, configId);
                if (!self.AddItem(newItem))
                {
                    Log.Error("添加物品失败！");
                    newItem?.Dispose(); 
                    return false;
                }
            }
            return true;
        }
        /// <summary>
        /// 增加物品
        /// </summary>
        /// <param name="self"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public static bool AddItem(this BagComponent self,Item item)
        {
            if (item == null || item.IsDisposed)
            {
                Log.Error("item is null!");
                return false;
            }
            if (self.IsMaxLoad())
            {
                Log.Error("bag is IsMaxLoad!");
                return false;
            }
            if (!self.AddContainer(item))//添加到字典中
            {
                Log.Error("Add Container is Error!");
                return false;
            }
            if (item.Parent != self)
            {
                //由于Item 继承ISerializeToEntity
                ///把Item放到BagComponent 节点下，并且，调用AddToChildrenDB 方法，把Item放到ChildrenDB（HashSet） 当中
                self.AddChild(item);
            }
            //同步给客户端
            ItemUpdateNoticeHelper.SyncAddItem(self.GetParent<Unit>(), item, self.message);
            return true;
        }
        /// <summary>
        /// 背包最大负重
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static bool IsMaxLoad(this BagComponent self)
        {
            return self.ItemsDict.Count == self.GetParent<Unit>().GetComponent<NumericComponent>()[(int)NumericType.MaxBagCapacity];
            //int temp = self.GetParent<Unit>().GetComponent<NumericComponent>().GetAsInt((int)NumericType.MaxBagCapacity);
            //if (self.ItemsDict.Count >= temp)
            //{
            //    return true;
            //}
            //else
            //{
            //    return false;
            //}
        }
        /// <summary>
        /// 添加到字典中
        /// </summary>
        /// <param name="self"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public static bool AddContainer(this BagComponent self,Item item)
        {
            if (self.ItemsDict.ContainsKey(item.Id))
            {
                return false;
            }
            self.ItemsDict.Add(item.Id, item);
            self.ItemsMap.Add(item.config.Type, item);
            return true;
        }
        public static bool IsItemExist(this BagComponent self, long itemId)
        {
            self.ItemsDict.TryGetValue(itemId, out Item item);
            return item != null && !item.IsDisposed;
        }
        public static void RemoveItem(this BagComponent self, Item item)
        {
            self.RemoveContainer(item);
            ItemUpdateNoticeHelper.SyncRemoveItem(self.GetParent<Unit>(), item, self.message);
            item.Dispose();
        }
        public static void RemoveContainer(this BagComponent self, Item item)
        {
            self.ItemsDict.Remove(item.Id);
            self.ItemsMap.Remove(item.config.Type, item);
        }
        /// <summary>
        /// 从背包中移除Item，但是不释放
        /// </summary>
        /// <param name="self"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public static Item RemoveItemNoDispose(this BagComponent self, Item item)
        {
            self.RemoveContainer(item);
            ItemUpdateNoticeHelper.SyncRemoveItem(self.GetParent<Unit>(), item, self.message);
            return item;
        }
        /// <summary>
        /// 判断此Item是否异常
        /// </summary>
        /// <param name="self"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public static bool IsCanAddItem(this BagComponent self, Item item)
        {
            if (item == null || item.IsDisposed)
            {
                return false;
            }

            if (!ItemConfigCategory.Instance.Contain(item.configId))
            {
                return false;
            }

            if (self.IsMaxLoad())
            {
                return false;
            }

            if (self.ItemsDict.ContainsKey(item.Id))//不能叠加
            {
                return false;
            }

            if (item.Parent == self)
            {
                return false;
            }
            return true;
        }
    }
}
