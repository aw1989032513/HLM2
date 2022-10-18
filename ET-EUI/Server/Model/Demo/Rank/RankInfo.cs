using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    public class RankInfo : Entity, IAwake, IDestroy
    {
        public long unitId;
        public string name;
        /// <summary>
        /// 等级
        /// </summary>
        public int count;
    }
}
