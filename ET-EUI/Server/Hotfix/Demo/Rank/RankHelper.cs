using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    public static class RankHelper
    {
        public static void AddOrUpdateLevelRank(Unit unit)
        {
            using (RankInfo rankInfo = unit.DomainScene().AddChild<RankInfo>())
            {
                rankInfo.unitId = unit.Id;
                rankInfo.name = unit.GetComponent<RoleInfo>().Name;
                rankInfo.count= unit.GetComponent<NumericComponent>().GetAsInt((int)NumericType.Level);
                //Map服务器通知Rrank服务器
                Map2Rank_AddOrUpdateRankInfo message = new Map2Rank_AddOrUpdateRankInfo();
                message.RankInfo = rankInfo;
                long instanceId = StartSceneConfigCategory.Instance.GetBySceneName(unit.DomainZone(), "Rank").InstanceId;
                MessageHelper.SendActor(instanceId, message);
            }
        }
    }
}
