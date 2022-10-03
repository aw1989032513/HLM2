using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    public class R2G_GetLoginGateKeyHanlder : AMActorRpcHandler<Scene, R2G_GetLoginGateKey, G2R_GetLoginGateKey>
    {
        protected override async ETTask Run(Scene gateScene, R2G_GetLoginGateKey request, G2R_GetLoginGateKey response, Action reply)
        {
            if (gateScene.SceneType != SceneType.Gate)
            {
                Log.Error($"请求的Scene错误，当前Scene为：{gateScene.SceneType}");
                response.Error = ErrorCode.ERR_RequestSceneTypeError;
                reply();
                return;
            }
            string key = RandomHelper.RandInt64().ToString()+TimeHelper.ServerNow().ToString();
            gateScene.GetComponent<GateSessionKeyComponent>().Remove(request.AccountId);
            gateScene.GetComponent<GateSessionKeyComponent>().Add(request.AccountId, key);

            response.GateSessionKey = key;
            reply();
            await ETTask.CompletedTask;
        }
    }
}
