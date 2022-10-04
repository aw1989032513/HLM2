using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    public interface IUnitCache
    {

    }
    public class UnitCache:Entity ,IAwake,IDestroy
    {
        /// <summary>
        /// Entity的名字
        /// </summary>
        public string key;
        /// <summary>
        ///key： unit的ID  ：unitId
        /// </summary>
        public Dictionary<long, Entity> cacheComponentDic = new Dictionary<long, Entity> ();
    }
}
