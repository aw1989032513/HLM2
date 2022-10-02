using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    /// <summary>
    /// 客户端Client 拿着 realm的address向 realm 请求登录  
    ///  realm  拿着AccountID 向 Gate请求 拿到GateSessionKey 和GateAddress
    /// </summary>
    public class C2R_LoginRealmHandler : AMRpcHandler<C2R_LoginRealm, R2C_LoginRealm>
    {
        protected override async ETTask Run(Session realmSession, C2R_LoginRealm request, R2C_LoginRealm response, Action reply)
        {
            if (realmSession.DomainScene().SceneType != SceneType.Realm)
            {
                Log.Error($"请求的Scene错误，当前Scene为：{realmSession.DomainScene().SceneType}");
                realmSession.Dispose(); 
                return;
            }
            Scene realmDomianScene = realmSession.DomainScene();
            if (realmSession.GetComponent<SessionLockingComponent>() != null)
            {
                response.Error = ErrorCode.ERR_RequestRepeatedly;
                reply();
                realmSession?.Disconnect().Coroutine();
                return;
            }

            string token = realmSession.DomainScene().GetComponent<TokenComponent>().Get(request.AccountId);
            if (token == null || token != request.RealmTokenKey)
            {
                response.Error = ErrorCode.ERR_TokenError;
                reply();
                realmSession?.Disconnect().Coroutine();
                return;
            }
            realmDomianScene.GetComponent<TokenComponent>().Remove(request.AccountId);
            using (realmSession.AddComponent<SessionLockingComponent>())
            {
                using (await CoroutineLockComponent.Instance.Wait((int)CoroutineLockType.LoginRealm, request.AccountId))
                {
                    //取固定分配一个Gate
                    StartSceneConfig config = RealmGateAddressHelper.GetGate(realmDomianScene.Zone, request.AccountId);//配置表里的zone

                    // 向gate请求一个key,客户端可以拿着这个key连接gate
                    G2R_GetLoginGateKey g2R_GetLoginGateKey = (G2R_GetLoginGateKey)await MessageHelper.CallActor(config.InstanceId, new R2G_GetLoginGateKey()
                    {
                        AccountId = request.AccountId,
                    });
                    if (g2R_GetLoginGateKey.Error != ErrorCode.ERR_Success)
                    {
                        response.Error = g2R_GetLoginGateKey.Error;
                        reply();
                        return;
                    }
                    response.GateSessionKey = g2R_GetLoginGateKey.GateSessionKey;
                    response.GateAddress = config.OuterIPPort.ToString();

                    reply();
                    realmSession?.Disconnect().Coroutine();
                }
            }
        }
    }
}
