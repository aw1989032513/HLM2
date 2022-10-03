using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET.Demo.Account.Handler
{
    /// <summary>
    /// 账号中心服务器用于登记Gate发送来的accountID 和区服zone
    /// </summary>
    public class G2L_AddLoginRecordHandler : AMActorRpcHandler<Scene, G2L_AddLoginRecord, L2G_AddLoginRecord>
    {
        protected override async ETTask Run(Scene centerScene, G2L_AddLoginRecord request, L2G_AddLoginRecord response, Action reply)
        {
            long accountId= request.AccountId;
            using (await CoroutineLockComponent.Instance.Wait(CoroutineLockType.LoginCenterLock, accountId.GetHashCode())) 
            {
                centerScene.GetComponent<LoginInfoRecordComponent>().Remove(accountId);
                centerScene.GetComponent<LoginInfoRecordComponent>().Add(accountId,request.ServerId);
            }
            reply();
        }
    }
}
