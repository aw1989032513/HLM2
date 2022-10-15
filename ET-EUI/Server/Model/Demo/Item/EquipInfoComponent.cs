using System;
using System.Collections.Generic;
#if SERVER
using MongoDB.Bson.Serialization.Attributes;
#endif

namespace ET
{
    /// <summary>
    /// 装备信息组件
    /// </summary>
#if SERVER
    public class EquipInfoComponent : Entity, IAwake, IDestroy,ISerializeToEntity,IDeserialize
#else
    public class EquipInfoComponent:Entity,IAwake,IDestroy
#endif
    {
        /// <summary>
        /// 装备属性是否生成过
        /// </summary>
        public bool isInited = false;
        /// <summary>
        /// 装备评分
        /// </summary>
        public int score = 0;
        /// <summary>
        /// 装备词条列表
        /// </summary>
#if SERVER
        [BsonIgnore]
#endif
        public List<AttributeEntry> EntryList = new List<AttributeEntry>();
    }
}
