using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    public class RankInfoDestorySystem : DestroySystem<RankInfo>
    {
        public override void Destroy(RankInfo self)
        {
            self.unitId = 0;
            self.name = String.Empty;
            self.count = 0;
        }
    }
    public static class RankInfoSystem
    {
        public static void FromMessage(this RankInfo self, RankInfoProto rankInfoProto)
        {
            self.Id = rankInfoProto.Id;
            self.unitId = rankInfoProto.UnitId;
            self.name = rankInfoProto.Name;
            self.count = rankInfoProto.Count;
        }

        public static RankInfoProto ToMessage(this RankInfo self)
        {
            RankInfoProto rankInfoProto = new RankInfoProto();
            rankInfoProto.Id = self.Id;
            rankInfoProto.UnitId = self.unitId;
            rankInfoProto.Name = self.name;
            rankInfoProto.Count = self.count;
            return rankInfoProto;
        }
    }
}
