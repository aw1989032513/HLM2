using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    public class Other2UnitCache_GetUnitHandler : AMActorRpcHandler<Scene, Other2UnitCache_GetUnit, UnitCache2Other_GetUnit>
    {
        protected override async ETTask Run(Scene scene, Other2UnitCache_GetUnit request, UnitCache2Other_GetUnit response, Action reply)
        {
            UnitCacheComponent unitCacheComponent = scene.GetComponent<UnitCacheComponent>();   
            Dictionary<string, Entity> dictionary = MonoPool.Instance.Fetch(typeof(Dictionary<string ,Entity>)) as Dictionary<string, Entity>;
            try
            {
                if (request.ComponentNameList.Count == 0)
                {
                    dictionary.Add(nameof(Unit), null);//加入Unit的名字，值为null  ，，这里因为第一个肯定是Unit的实体Entity，下面才是Unit的身上组件component
                    foreach (string s in unitCacheComponent.unitCacheKeyList)
                    {
                        dictionary.Add(s, null);
                    }
                }
                else
                {
                    foreach (string  s in request.ComponentNameList)
                    {
                        dictionary.Add(s, null);//只需要他的名字
                    }
                }
                //赋值
                foreach (var key in dictionary.Keys)
                {
                    Entity entity = await unitCacheComponent.Get(request.UnitId, key);
                    dictionary[key] = entity;
                }
                response.ComponentNameList.AddRange(dictionary.Keys);
                response.EntityList.AddRange(dictionary.Values);
            }
            finally
            {
                dictionary.Clear();
                MonoPool.Instance.Recycle(dictionary);
            }
            reply();
            await ETTask.CompletedTask;
        }
    }
}
