using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    public class UnitCacheDestorySystem : DestroySystem<UnitCache>
    {
        public override void Destroy(UnitCache self)
        {
            foreach (var entity in self.cacheComponentDic.Values)
            {
                entity.Dispose();
            }
            self.cacheComponentDic.Clear();
            self.key = null;
        }
    }
    public static class UnitCacheSystem
    {
        public static void AddOrUpdate(this UnitCache self, Entity newEntity)
        {
            if (newEntity == null)
            {
                return;
            }
            if (self.cacheComponentDic.TryGetValue(newEntity.Id,out Entity oldEntity))
            {
                if (newEntity != oldEntity)
                {
                    oldEntity.Dispose();
                }
                self.cacheComponentDic.Remove(newEntity.Id);
            }
            self.cacheComponentDic.Add(newEntity.Id,newEntity);
        }
        public static async ETTask<Entity> Get(this UnitCache self, long unitId)
        {
            Entity entity = null;
            if (! self.cacheComponentDic.TryGetValue(unitId,out entity))
            {
                entity = await DBManagerComponent.Instance.GetZoneDB(self.DomainZone()).Query<Entity>(unitId, self.key);
                if (entity != null)
                {
                    self.AddOrUpdate(entity);
                }
            }
            return entity;
        }
        public static void Delete(this UnitCache self, long id)
        {
            if (self.cacheComponentDic.TryGetValue(id, out Entity entity))
            {
               entity.Dispose();
               self.cacheComponentDic.Remove(id);
            }
        }
    }
}
