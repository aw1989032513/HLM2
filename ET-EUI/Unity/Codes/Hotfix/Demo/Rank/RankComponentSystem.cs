using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    public static class RankComponentSystem
    {
        public static RankInfo GetRankInfoByIndex(this RankComponent self,int index)
        {
            if (index < 0 || index >= self.rankInfosList.Count)
            {
                return null;
            }
            return self.rankInfosList[index];
        }
        public static int GetRankCount(this RankComponent self)
        {
            return self.rankInfosList.Count;
        }
        public static void  Add(this RankComponent self, RankInfoProto rankInfoProto)
        {
            RankInfo rankInfo = self.AddChild<RankInfo>(true);
            rankInfo.FromMessage(rankInfoProto);
            self.rankInfosList.Add(rankInfo);
        }
        public static void ClearAll(this RankComponent self)
        {
            for (int i = 0; i < self.rankInfosList.Count; i++)
            {
                self.rankInfosList[i].Dispose();
            }
            self.rankInfosList.Clear();
        }
    }
}
