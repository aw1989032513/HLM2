using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    public class RankComponent: Entity, IAwake, IDestroy
    {
        public List<RankInfo> rankInfosList = new List<RankInfo>();
    }
}
