using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
#if SERVER
    public class Item : Entity,IAwake<int>,IDestroy,ISerializeToEntity
#else
    public class Item:Entity,IAwake<int>,IDestroy
#endif
    {
        /// <summary>
        /// 物品配置ID
        /// </summary>
        public int configId = 0;
        /// <summary>
        /// 物品品质
        /// </summary>
        public int quality = 0;
#if SERVER
        [BsonIgnore]
#endif
        //物品配置数据
        public ItemConfig config => ItemConfigCategory.Instance.Get(configId);
    }
}
