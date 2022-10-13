using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    public class AdventureCheckComponent : Entity, IAwake, IDestroy
    {
        public int aniamtionTotalTime = 0;

        public List<long> enemyIdList = new List<long>();
        public List<long> cacheEnemyIdList = new List<long>();

        public SRandom random = null;
    }
}
