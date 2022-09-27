using System;

namespace ET
{
    public static class DBManagerComponentSystem
    {
        [ObjectSystem]
        public class DBManagerComponentAwakeSystem: AwakeSystem<DBManagerComponent>
        {
            public override void Awake(DBManagerComponent self)
            {
                DBManagerComponent.Instance = self;
            }
        }

        [ObjectSystem]
        public class DBManagerComponentDestroySystem: DestroySystem<DBManagerComponent>
        {
            public override void Destroy(DBManagerComponent self)
            {
                DBManagerComponent.Instance = null;
            }
        }
        /// <summary>
        /// 根据session.DomainZone来拿到该服的数据库
        /// </summary>
        /// <param name="self"></param>
        /// <param name="zone">session.DomainZone() 一服，二服，三服，</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static DBComponent GetZoneDB(this DBManagerComponent self, int zone)
        {
            DBComponent dbComponent = self.DBComponents[zone];
            if (dbComponent != null)
            {
                return dbComponent;
            }

            StartZoneConfig startZoneConfig = StartZoneConfigCategory.Instance.Get(zone);
            if (startZoneConfig.DBConnection == "")
            {
                throw new Exception($"zone: {zone} not found mongo connect string");
            }

            dbComponent = self.AddChild<DBComponent, string, string, int>(startZoneConfig.DBConnection, startZoneConfig.DBName, zone);
            self.DBComponents[zone] = dbComponent;
            return dbComponent;
        }
    }
}