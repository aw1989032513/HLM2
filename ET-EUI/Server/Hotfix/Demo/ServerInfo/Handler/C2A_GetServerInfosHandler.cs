using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET 
{
    public class C2A_GetServerInfosHandler : AMRpcHandler<C2A_GetServerInfos, A2C_GetServerInfos>
    {
        protected override async ETTask Run(Session clientSession, C2A_GetServerInfos request, A2C_GetServerInfos response, Action reply)
        {
            if (clientSession.DomainScene().SceneType != SceneType.Account)
            {
                Log.Error($"请求的Scene错误，当前的Scene为：{ clientSession.DomainScene().SceneType}");
                clientSession.Dispose();
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
            //返回区服信息
            foreach (var item in clientSession.DomainScene().GetComponent<ServerInfoManagerComponent>().serverInfoList)
            {
                response.ServerInfosList.Add(item.ToMessage());
            }
            reply();

            await ETTask.CompletedTask;
        }
    }
}
