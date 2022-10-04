using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    public class UnitCacheComponent:Entity ,IAwake,IDestroy
    {
        /// <summary>
        /// 通过Entity的名字   获取UnitCache
        /// </summary>
        public Dictionary<string,UnitCache> unitCacheDic = new Dictionary<string, UnitCache>();
        /// <summary>
        /// Entity名字，跟UnitCache中的Key一个值
        /// </summary>
        public List<string> unitCacheKeyList = new List<string>();
    }
}
