using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    public class UnitDBSaveComponent:Entity,IAwake,IDestroy
    {
        /// <summary>
        /// Unit 身上的组件
        /// HashSet 特性：里面的值必须是唯一的
        /// </summary>
        public HashSet<Type> EntityChangeTypeSet 
        { 
            get;
        } 
            = new HashSet<Type>();

        public long Timer;
    }
}
