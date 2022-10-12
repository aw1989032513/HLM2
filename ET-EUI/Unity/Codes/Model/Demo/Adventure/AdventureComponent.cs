using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    public class AdventureComponent:Entity,IAwake,IDestroy
    {
        public long battleTimer = 0;
        /// <summary>
        /// 本轮闯关回合数
        /// </summary>
        public int round = 0;
        public List<long> enemyIdList =new List<long>();
        public List<long> aliveEnemyIdList = new List<long>();
        public SRandom random = null;
    }
}
