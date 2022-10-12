using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    public class AdventureCheckComponent : Entity, IAwake, IDestroy
    {
        public Dictionary<int,int>enemyHpDic = new Dictionary<int,int>();
        /// <summary>
        /// 总回合数
        /// </summary>
        public int round = 0;
        /// <summary>
        /// 动画总共时间
        /// </summary>
        public int animationTotalTime = 0;
        /// <summary>
        /// 怪物总伤害量
        /// </summary>
        public int monsterTotalDamage = 0;
        /// <summary>
        /// 玩家打出的伤害
        /// </summary>
        public int unitTotalDamage = 0;
        public int totalMonsterHp = 0;
    }
}
