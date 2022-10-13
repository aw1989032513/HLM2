using System.Collections.Generic;
using UnityEngine;

namespace ET
{
    public class FlyDamageValueViewComponent:Entity,IAwake,IDestroy
    {
        /// <summary>
        /// 正在飘动伤害的移动物体
        /// </summary>
        public HashSet<GameObject> FlyingDamageHashSet = new HashSet<GameObject>();
    }
}
