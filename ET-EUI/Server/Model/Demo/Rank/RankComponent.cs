using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    public class RankComponent : Entity, IAwake, IDestroy
    {
        /// <summary>
        /// 已经排好序的列表
        /// </summary>
        [BsonIgnore]
        public SortedDictionary<RankInfo, long> SortedRankInfoList = new SortedDictionary<RankInfo, long>(new RankInfoCompare());
        /// <summary>
        /// KEY:RankInfo.unitId
        /// </summary>
        [BsonIgnore]
        public Dictionary<long,RankInfo> RankInfosDic = new Dictionary<long,RankInfo>();
    }
    public class RankInfoCompare : IComparer<RankInfo>
    {
        public int Compare(RankInfo a, RankInfo b)
        {
            int result = b.count - a.count;

           //如果向减不为0，用count来拍
            if (result != 0)
            {
                return result;
            }
            //如果向减为0 那么就用ID 来排序
            if (a.Id < b.Id)
            {
                return 1;
            }
            if (a.Id > b.Id)
            {
                return -1;
            }
            return 0;
        }
    }
}
