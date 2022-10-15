using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;

namespace ET
{

#if SERVER
    public class EquipmentsComponent : Entity, IAwake, IDestroy, ITransfer, IDeserialize, IUnitCache
#else
    public class EquipmentsComponent : Entity,IAwake,IDestroy
#endif
    {
        /// <summary>
        ///  //KEY:装备位置
        /// </summary>
#if SERVER
        [BsonIgnore]
#endif      
        public Dictionary<int, Item> EquipItemsDic = new Dictionary<int, Item>();

#if SERVER
        [BsonIgnore]
        public M2C_ItemUpdateOpInfo message = new M2C_ItemUpdateOpInfo() { ContainerType = (int)ItemContainerType.RoleInfo };
#endif

    }
}