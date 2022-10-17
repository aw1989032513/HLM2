using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    public enum ProductionConsumType
    {
        IronStone = 1,  //精铁
        Fur = 2, // 皮毛
    }


    public enum ProductionState
    {
        None = -1,
        Received = 0, //已领取
        Making = 1, //正在制造
    }
#if SERVER
    public class Production : Entity, IAwake, IAwake<int>, IDestroy, ISerializeToEntity
#else
    public class Production:Entity,IAwake,IDestroy
#endif
    {
        public long StartTime = 0;
        /// <summary>
        /// 结束时间
        /// </summary>
        public long TargetTime = 0;
        public int ConfigId = 0;
        public int ProductionState = 0;
    }
}
