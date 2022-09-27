using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    /// <summary>
    /// 令牌组件，管理所有的令牌
    /// </summary>
    public class TokenComponent:Entity,IAwake
    {
        public readonly Dictionary<long ,string> tokenDictionary = new Dictionary<long ,string>();
    }
}
