using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    public class C2A_GetRealmKeyHanlder : AMRpcHandler<C2A_GetRealmKey, A2C_GetRealmKey>
    {
        protected override async ETTask Run(Session clientSession, C2A_GetRealmKey request, A2C_GetRealmKey response, Action reply)
        {
            if (clientSession.DomainScene().SceneType != SceneType.Account)
            {
                Log.Error($"请求的session错误，当前Scene为：{clientSession.DomainScene().SceneType}");
                clientSession.Dispose();
                return;
            }
            if (clientSession.GetComponent<SessionLockingComponent>() != null)
            {
                response.Error = ErrorCode.ERR_RequestRepeatedly;
                reply();
                clientSession.Disconnect().Coroutine();
                return;
            }
            string serverToken = clientSession.DomainScene().GetComponent<TokenComponent>().Get(request.AccountId);
            if (serverToken != request.Token || serverToken == null)
            {
                response.Error = ErrorCode.ERR_TokenError;
                reply();
                clientSession.Disconnect().Coroutine();
                return;
            }

            using (clientSession.AddComponent<SessionLockingComponent>())
            {
                using (await CoroutineLockComponent.Instance.Wait(CoroutineLockType.LoginAccount, request.AccountId))
                {
                    //该服务器上的Realm
                    StartSceneConfig realmStartSceneConfig = RealmGateAddressHelper.GetRealm(request.ServerId);

                    //向Realm请求
                    R2A_GetRealmKey r2A_GetRealmKey = (R2A_GetRealmKey)await MessageHelper.CallActor(realmStartSceneConfig.InstanceId, new A2R_GetRealmKey()
                    {
                        AccountId = request.AccountId,
                    });

                    if (r2A_GetRealmKey.Error != ErrorCode.ERR_Success)
                    {
                        response.Error = r2A_GetRealmKey.Error;
                        reply();
                        clientSession?.Disconnect().Coroutine();
                        return;
                    }

                    response.RealmKey = r2A_GetRealmKey.RealmKey;
                    response.RealmAddress = realmStartSceneConfig.OuterIPPort.ToString();//外网地址外网端口

                    reply();
                    clientSession?.Disconnect().Coroutine();//clientSession 跟Realm断开 ，因为要去找Gate了
                }
            }
            await ETTask.CompletedTask;
        }
    }
}
