using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    public class C2A_GetRolesHandler : AMRpcHandler<C2A_GetRoles, A2C_GetRoles>
    {
        protected override async ETTask Run(Session clientSession, C2A_GetRoles request, A2C_GetRoles response, Action reply)
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
            if (serverToken != request.Token || serverToken ==null)
            {
                response.Error = ErrorCode.ERR_TokenError;
                reply();
                clientSession.Disconnect().Coroutine();
                return;
            }
            using (clientSession.AddComponent<SessionLockingComponent>())
            {
                using (await CoroutineLockComponent.Instance.Wait(CoroutineLockType.CreateRoleLock,request.AccountId))
                {
                    var roleInfos = await DBManagerComponent.Instance.GetZoneDB(clientSession.DomainZone()).Query<RoleInfo>(d
                         => d.AccountId == request.AccountId && d.ServerId == request.ServerId && d.State == (int)RoleInfoState.Normal && d.State == (int)RoleInfoState.Normal);
                    if (roleInfos == null || roleInfos.Count == 0)
                    {
                        reply();
                        return;
                    }

                    foreach (var item in roleInfos)
                    {
                        response.RoleInfo.Add(item.ToMessage());
                        item?.Dispose();
                    }
                    roleInfos.Clear();

                    reply();
                }
            }
        }
    }
}
