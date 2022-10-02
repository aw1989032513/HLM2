using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    public class C2A_CreateRoleHandler : AMRpcHandler<C2A_CreateRole, A2C_CreateRole>
    {
        protected override async ETTask Run(Session clientSession, C2A_CreateRole request, A2C_CreateRole response, Action reply)
        {
            if (clientSession.DomainScene().SceneType != SceneType.Account)
            {
                Log.Error($"请求的Scene错误，当前的Scene为：{ clientSession.DomainScene().SceneType}");
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


            string token = clientSession.DomainScene().GetComponent<TokenComponent>().Get(request.AccountId);

            if (token == null || token != request.Token)
            {
                response.Error = ErrorCode.ERR_TokenError;
                reply();
                clientSession?.Disconnect().Coroutine();
                return;
            }

            if (string.IsNullOrEmpty(request.Name))
            {
                response.Error = ErrorCode.ERR_RoleNameIsNull;
                reply();
                //这里可以通知客户端弹提示框，名字不能为空
                return;
            }

            using (clientSession.AddComponent<SessionLockingComponent>())//防止客户端一直请求创建角色
            {
                //查询数据库要用协成锁
                using (await CoroutineLockComponent.Instance.Wait(CoroutineLockType.CreateRoleLock, request.AccountId))
                {
                    var roleInfos = await DBManagerComponent.Instance.GetZoneDB(clientSession.DomainZone()).Query<RoleInfo>(d
                          => d.Name == request.Name && d.ServerId == request.ServerId);

                    if (roleInfos != null && roleInfos.Count > 0)//该服已经有相同名字了
                    {
                        response.Error = ErrorCode.ERR_RoleNameSame;
                        reply();
                        return;
                    }
                    //往数据库添加新的角色信息
                    RoleInfo newRoleInfo = clientSession.GetComponent<RoleInfosZone>().AddChildWithId<RoleInfo>(IdGenerater.Instance.GenerateUnitId(request.ServerId));
                    newRoleInfo.Name = request.Name;
                    newRoleInfo.ServerId = request.ServerId;
                    newRoleInfo.State = (int)RoleInfoState.Normal;
                    newRoleInfo.AccountId = request.AccountId;
                    newRoleInfo.CreateTime = (int)TimeHelper.ServerNow();
                    newRoleInfo.LastLoginTime = 0;
                    await DBManagerComponent.Instance.GetZoneDB(clientSession.DomainZone()).Save<RoleInfo>(newRoleInfo);

                    //回复客户端消息
                    response.RoleInfo = newRoleInfo.ToMessage();

                    reply();

                    newRoleInfo?.Dispose();
                }
            }
            

        }
    }
}
