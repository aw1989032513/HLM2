using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    public static class RankHelper
    {
        public static async ETTask<int> GetRankInfo(Scene ZoneScene)
        {
            Rank2C_GetRanksInfo rank2C_GetRanksInfo = new Rank2C_GetRanksInfo();
            try
            {
                rank2C_GetRanksInfo = (Rank2C_GetRanksInfo)await ZoneScene.GetComponent<SessionComponent>().Session.Call(new C2Rank_GetRanksInfo()
                {
                });
            }
            catch (Exception e)
            {
                Log.Error(e.ToString());
                return ErrorCode.ERR_NetWorkError;
            }
            if (rank2C_GetRanksInfo.Error != ErrorCode.ERR_Success)
            {
                return rank2C_GetRanksInfo.Error;
            }
            //成功的话
            //清空List
            ZoneScene.GetComponent<RankComponent>().ClearAll();
            for (int i = 0; i < rank2C_GetRanksInfo.RankInfoProto.Count; i++)
            {
                ZoneScene.GetComponent<RankComponent>().Add(rank2C_GetRanksInfo.RankInfoProto[i]);
            }
            return rank2C_GetRanksInfo.Error;
        }
    }
}
