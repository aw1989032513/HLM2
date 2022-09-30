using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    /// <summary>
    /// 账号服务登记中心   登记  登录服发来的AccountID
    /// </summary>
    public class A2L_LoginAccountRequestHandler : AMActorRpcHandler<Scene, A2L_LoginAccountRequest, L2A_LoginAccountResponse>
    {
        protected override async ETTask Run(Scene scene, A2L_LoginAccountRequest request, L2A_LoginAccountResponse response, Action reply)
        {

            long accountID = request.AccountId;
            using (await CoroutineLockComponent.Instance.Wait(CoroutineLockType.LoginCenterLock, accountID.GetHashCode())) 
            {
                if (!scene.GetComponent<LoginInfoRecordComponent>().IsExist(accountID))
                {
                    reply();
                    return;
                }

                int zone = scene.GetComponent<LoginInfoRecordComponent>().Get(accountID);
                StartSceneConfig gateConfig = RealmGateAddressHelper.GetGate(zone,accountID);


                //账号登记中心，通知Gate网关，T玩家下线
                var g2lDisconnectGateUnit = (G2L_DisconnectGateUnit)await MessageHelper.CallActor(gateConfig.InstanceId, new L2G_DisconnectGateUnit() { AccountId = accountID });
                response.Error = g2lDisconnectGateUnit.Error;

                reply();
            }
        }
    }
}
