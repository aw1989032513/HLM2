using System.Collections.Generic;
using UnityEngine;

namespace ET
{
    public static class UnitHelper
    {
        /// <summary>
        /// 地图传送创建Unit并且赋值属性
        /// </summary>
        /// <param name="unit"></param>
        /// <returns></returns>
        public static UnitInfo CreateUnitInfo(Unit unit)
        {
            UnitInfo unitInfo = new UnitInfo();
            NumericComponent nc = unit.GetComponent<NumericComponent>();
            unitInfo.UnitId = unit.Id;
            unitInfo.ConfigId = unit.ConfigId;
            unitInfo.Type = (int)unit.Type;
            Vector3 position = unit.Position;
            unitInfo.X = position.x;
            unitInfo.Y = position.y;
            unitInfo.Z = position.z;
            Vector3 forward = unit.Forward;
            unitInfo.ForwardX = forward.x;
            unitInfo.ForwardY = forward.y;
            unitInfo.ForwardZ = forward.z;
            #region
            //MoveComponent moveComponent = unit.GetComponent<MoveComponent>();
            //if (moveComponent != null)
            //{
            //    if (!moveComponent.IsArrived())
            //    {
            //        unitInfo.MoveInfo = new MoveInfo();
            //        for (int i = moveComponent.N; i < moveComponent.Targets.Count; ++i)
            //        {
            //            Vector3 pos = moveComponent.Targets[i];
            //            unitInfo.MoveInfo.X.Add(pos.x);
            //            unitInfo.MoveInfo.Y.Add(pos.y);
            //            unitInfo.MoveInfo.Z.Add(pos.z);
            //        }
            //    }
            //}
            #endregion
            foreach ((int key, long value) in nc.NumericDic)
            {
                unitInfo.Ks.Add(key);
                unitInfo.Vs.Add(value);
            }

            return unitInfo;
        }
        #region AOI
        // 获取看见unit的玩家，主要用于广播
        public static Dictionary<long, AOIEntity> GetBeSeePlayers(this Unit self)
        {
            return self.GetComponent<AOIEntity>().GetBeSeePlayers();
        }
        /// <summary>
        /// 进入视野通知
        /// </summary>
        /// <param name="unit"></param>
        /// <param name="sendUnit"></param>
        public static void NoticeUnitAdd(Unit unit, Unit sendUnit)
        {
            M2C_CreateUnits createUnits = new M2C_CreateUnits();
            createUnits.Units.Add(CreateUnitInfo(sendUnit));
            MessageHelper.SendToClient(unit, createUnits);
        }
        /// <summary>
        /// 离开视野
        /// </summary>
        /// <param name="unit"></param>
        /// <param name="sendUnit"></param>
        public static void NoticeUnitRemove(Unit unit, Unit sendUnit)
        {
            M2C_RemoveUnits removeUnits = new M2C_RemoveUnits();
            removeUnits.Units.Add(sendUnit.Id);
            MessageHelper.SendToClient(unit, removeUnits);
        }
        #endregion
        public static async ETTask<(bool,Unit)> LoadUnit(Player player)
        {
            GateMapComponent gateMapComponent = player.AddComponent<GateMapComponent>();
            gateMapComponent.Scene = await SceneFactory.Create(gateMapComponent, "GateMap", SceneType.Map);

            //从缓存组建中取得
            Unit unit = await UnitCacheHelper.GetUnitCache(gateMapComponent.Scene, player.UnitId);

            bool isNewUnit = unit == null;
            if (isNewUnit)
            {
                //创建一个Unit
                unit = UnitFactory.Create(gateMapComponent.Scene, player.UnitId, UnitType.Player);
                List<RoleInfo> roleInfos = await DBManagerComponent.Instance.GetZoneDB(player.DomainZone()).Query<RoleInfo>(d => d.Id == player.UnitId);
                unit.AddComponent(roleInfos[0]); //挂载

                UnitCacheHelper.AddOrUpdateUnitAllCache(unit);
            }
            return (isNewUnit, unit);
        }

        public static async ETTask InitUnit(Unit unit,bool isNew)
        {
            //unit.GetComponent<NumericComponent>().SetNoEvent(NumericType.BattleRandomSeed, TimeHelper.ServerNow());
            await ETTask.CompletedTask;
        }
    }
}