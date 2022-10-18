using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    public class C2Rank_GetRanksInfoHandler : AMActorRpcHandler<Scene, C2Rank_GetRanksInfo, Rank2C_GetRanksInfo>
    {
        protected override async ETTask Run(Scene scene, C2Rank_GetRanksInfo request, Rank2C_GetRanksInfo response, Action reply)
        {
            RankComponent rankComponent = scene.GetComponent<RankComponent>();
            foreach (var item in rankComponent.SortedRankInfoList)
            {
                response.RankInfoProto.Add(item.Key.ToMessage());
            }

            reply();
            await ETTask.CompletedTask;
        }
    }
}
