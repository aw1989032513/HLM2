using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    public class ChatComponent : Entity, IAwake, IDestroy
    {
        /// <summary>
        /// KEY UnitId
        /// </summary>
        public Dictionary<long,ChatInfo> ChatInfoUnitsDict = new Dictionary<long,ChatInfo>();
    }
}
