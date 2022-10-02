using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    /// <summary>
    /// Account服务器向Realm 拿到realm的Address 和 realmKey
    /// </summary>
    public class A2R_GetRealmKeyHandler : AMActorRpcHandler<Scene, A2R_GetRealmKey, R2A_GetRealmKey>
    {
        protected override async ETTask Run(Scene realmScene, A2R_GetRealmKey request, R2A_GetRealmKey response, Action reply)
        {
            if (realmScene.SceneType != SceneType.Realm)
            {
                Log.Error($"请求的Scene错误，当前Scene为：{realmScene.SceneType}");
                response.Error = ErrorCode.ERR_RequestSceneTypeError;
                reply();
                return;
            }

            string token = TimeHelper.ServerNow().ToString() + RandomHelper.RandInt64().ToString();
            realmScene.GetComponent<TokenComponent>().Remove(request.AccountId);
            realmScene.GetComponent<TokenComponent>().Add(request.AccountId,token);
            response.RealmKey = token;
            reply();
            await ETTask.CompletedTask;
        }
    }
}
