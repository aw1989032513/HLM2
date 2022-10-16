using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;

namespace ET
{
#if SERVER
    public class ForgeComponent:Entity,IAwake,IDestroy,IDeserialize,ITransfer,IUnitCache
#else
    public class ForgeComponent:Entity,IAwake,IDestroy
#endif
    {
        /// <summary>
        /// KEY：ProductionId，VALUE:TimeType
        /// </summary>
#if SERVER
        [BsonIgnore]
#endif
        public Dictionary<long, long> ProductionTimerDict = new Dictionary<long, long>();
#if SERVER
        [BsonIgnore]
#endif
        //生产队列
        public List<Production> ProductionsList = new List<Production>();
    }
}
