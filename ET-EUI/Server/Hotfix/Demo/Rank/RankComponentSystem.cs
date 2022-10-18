using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    public class RankComponentDestorySystem : DestroySystem<RankComponent>
    {
        public override void Destroy(RankComponent self)
        {
            foreach (var rankInfo in self.RankInfosDic.Values)
            {
                rankInfo?.Dispose();
            }
            foreach (var rankInfo in self.SortedRankInfoList.Keys)
            {
                rankInfo?.Dispose();
            }
            self.RankInfosDic.Clear();
            self.SortedRankInfoList.Clear();
        }
    }
    public static class RankComponentSystem
    {
        /// <summary>
        /// 操作数据库，从数据库获得排行榜信息
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static async Task LoadRankInfo(this RankComponent self)
        {
            var rankInfoList = await DBManagerComponent.Instance.GetZoneDB(self.DomainZone()).Query<RankInfo>
                (d => true, collection: "RankComponent");
            foreach (var rankInfo in rankInfoList)
            {
                self.AddChild(rankInfo);
                self.RankInfosDic.Add(rankInfo.unitId, rankInfo);
                self.SortedRankInfoList.Add(rankInfo, rankInfo.unitId);
            }
        }
        public static  void AddOrUpdate(this RankComponent self, RankInfo newRankInfo)
        {
            if (self.RankInfosDic.ContainsKey(newRankInfo.unitId))
            {
                RankInfo oldRankInfo = self.RankInfosDic[newRankInfo.unitId];
                if (oldRankInfo.count == newRankInfo.count)
                {
                    return;
                }
                //全部移除
                self.RankInfosDic.Remove(oldRankInfo.unitId);
                self.SortedRankInfoList.Remove(oldRankInfo);
                DBManagerComponent.Instance.GetZoneDB(self.DomainZone()).Remove<RankInfo>(oldRankInfo.unitId, oldRankInfo.Id, "RankComponent").Coroutine();           
            }

            self.AddChild(newRankInfo);
            self.RankInfosDic.Add(newRankInfo.unitId,newRankInfo);
            self.SortedRankInfoList.Add(newRankInfo, newRankInfo.unitId);
            DBManagerComponent.Instance.GetZoneDB(self.DomainZone()).Save<RankInfo>(newRankInfo.unitId, newRankInfo, "RankComponent").Coroutine();

        }

    }
}
