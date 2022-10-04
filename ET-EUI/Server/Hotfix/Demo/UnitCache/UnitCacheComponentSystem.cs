using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    public class UnitCacheComponentAwakeSystem : AwakeSystem<UnitCacheComponent>
    {
        public override void Awake(UnitCacheComponent self)
        {
            self.unitCacheKeyList.Clear();
            foreach (Type  type in Game.EventSystem.GetTypes().Values)//游戏运行中所有的游戏类型
            {
                if (type != typeof(IUnitCache) && typeof (IUnitCache).IsAssignableFrom(type))//IsAssignableFrom:标识 “当前Class 是否是给定的 Class 的超类或者超接口”。是 返回true，否则返回false
                {
                    self.unitCacheKeyList.Add(type.Name);
                }
            }
            foreach (string key in self.unitCacheKeyList)
            {
                UnitCache unitCache = self.AddChild<UnitCache>();
                unitCache.key = key;
                self.unitCacheDic.Add(key, unitCache);
            }
        }
    }
    public class UnitCacheComponentDestorySystem : DestroySystem<UnitCacheComponent>
    {
        public override void Destroy(UnitCacheComponent self)
        {
            foreach (var item in self.unitCacheDic.Values)
            {
                item?.Dispose();
            }
            self.unitCacheDic.Clear();
        }
    }
    public static class UnitCacheComponentSystem
    {
        public static async ETTask AddOrUpdate(this UnitCacheComponent self,long id, ListComponent<Entity> entityList)
        {
            using (ListComponent<Entity> List =ListComponent<Entity>.Create())
            {
                foreach (Entity entity in entityList)
                {
                    string key = entity.GetType().Name;
                    if (! self.unitCacheDic.TryGetValue(key,out UnitCache unitCache))
                    {
                        unitCache = self.AddChild<UnitCache>();
                        unitCache.key = key;
                        self.unitCacheDic.Add(key, unitCache);
                    }
                    unitCache.AddOrUpdate(entity);
                    List.Add(entity);
                }
                if (List.Count > 0)
                {
                    await DBManagerComponent.Instance.GetZoneDB(self.DomainZone()).Save(id, List);
                }
            }
        }
        public static async ETTask<Entity> Get(this UnitCacheComponent self,long unitId,string unitNameOrComponentName)
        {
            if (! self.unitCacheDic.TryGetValue(unitNameOrComponentName,out UnitCache unitCache))
            {
                unitCache = self.AddChild<UnitCache>();
                unitCache.key = unitNameOrComponentName;
                self.unitCacheDic.Add(unitNameOrComponentName, unitCache);
            }
            return await unitCache.Get(unitId);
        }

        public static void  Delete(this UnitCacheComponent self, long unitId)
        {
            foreach (UnitCache unitCache in self.unitCacheDic.Values)
            {
                unitCache.Delete(unitId);
            }
        }
    }
}
