using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    /// <summary>
    /// 客户端通知Gate进入游戏Map
    /// </summary>
    public class C2G_LoginGameGateHandler : AMRpcHandler<C2G_LoginGameGate, G2C_LoginGameGate>
    {
        protected override async ETTask Run(Session gateSession, C2G_LoginGameGate request, G2C_LoginGameGate response, Action reply)
        {
            if (gateSession.DomainScene().SceneType != SceneType.Gate)
            {
                Log.Error($"请求的Scene错误，当前Scene为：{gateSession.DomainScene().SceneType}");
                gateSession.Dispose();  
                return;
            }
            //移除它
            gateSession.RemoveComponent<SessionAcceptTimeoutComponent>();
            if (gateSession.GetComponent<SessionLockingComponent>() != null)
            {
                response.Error = ErrorCode.ERR_RequestRepeatedly;
                reply();
                return;
            }

            Scene scene = gateSession.DomainScene();
            string tokenKey = scene.GetComponent<GateSessionKeyComponent>().Get(request.Account);

            if (tokenKey == null || !tokenKey.Equals(request.Key))
            {
                response.Error = ErrorCode.ERR_ConnectGateKeyError;
                response.Message = "Gate key验证失败!";
                reply();
                gateSession.Disconnect().Coroutine();
                return;
            }
            scene.GetComponent<GateSessionKeyComponent>().Remove(request.Account);
            long instanceId = gateSession.InstanceId;
            using (gateSession.AddComponent<SessionLockingComponent>())
            {
                using (await CoroutineLockComponent.Instance.Wait(CoroutineLockType.LoginGate, request.Account.GetHashCode()))
                {
                    //这里防止协成锁逻辑受到 异步逻辑的影响
                    if (instanceId != gateSession.InstanceId)
                    {
                        return;
                    }

                    //通知AccountCenter服务器，记录本次登录的服务器Zone  （为了以后顶号做准备，顶号断开Gate服）
                    StartSceneConfig loginCenterConfig = StartSceneConfigCategory.Instance.LoginCenterConfig;
                    L2G_AddLoginRecord l2G_AddLoginRecord = (L2G_AddLoginRecord)await MessageHelper.CallActor(loginCenterConfig.InstanceId, new G2L_AddLoginRecord()
                    {
                        AccountId = request.Account,
                        ServerId = scene.Zone
                    }) ;
                    if (l2G_AddLoginRecord.Error != ErrorCode.ERR_Success)
                    {
                        response.Error = l2G_AddLoginRecord.Error;
                        reply();
                        gateSession.Disconnect().Coroutine();
                        return;
                    }

                    //更新Seesion状态
                    SessionStateComponent sessionStateComponent = gateSession.GetComponent<SessionStateComponent>();
                    if (sessionStateComponent == null)
                    {
                        sessionStateComponent = gateSession.AddComponent<SessionStateComponent>();
                    }
                    sessionStateComponent.state = SessionState.Normal;

                    Player player = scene.GetComponent<PlayerComponent>().Get(request.Account);
                    if (player == null)
                    {
                        //添加一个新的GateUnit
                        player = scene.GetComponent<PlayerComponent>().AddChildWithId<Player, long, long>(request.RoleId, request.Account, request.RoleId);
                        player.PlayerState = PlayerState.Gate;
                        scene.GetComponent<PlayerComponent>().Add(player);
                        gateSession.AddComponent<MailBoxComponent, MailboxType>(MailboxType.GateSession);
                    }
                    else
                    {
                        //如果玩家存在，那么就移除这个计时下线组件
                        player.RemoveComponent<PlayerOfflineOutTimeComponent>();
                    }

                    gateSession.AddComponent<SessionPlayerComponent>().PlayerId = player.Id;
                    gateSession.GetComponent<SessionPlayerComponent>().playerInstanceId = player.InstanceId;
                    gateSession.GetComponent<SessionPlayerComponent>().accountId = player.AccountId;
                    player.ClientSession = gateSession;
                }
            }

            reply();
        }
    }
}
