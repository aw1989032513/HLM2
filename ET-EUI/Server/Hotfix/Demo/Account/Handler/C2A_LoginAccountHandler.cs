using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ET
{
    public class C2A_LoginAccountHandler : AMRpcHandler<C2A_LoginAccount, A2C_LoginAccount>
    {
        protected override async ETTask Run(Session accountSession, C2A_LoginAccount request, A2C_LoginAccount response, Action reply)
        {
            //判断是否是AccoutnScene
            if (accountSession.DomainScene().SceneType!=SceneType.Account)
            {
                Log.Error($"请求的ScentType错误，当前SceneType为：{accountSession.DomainScene().SceneType}");
                // 断开链接
                accountSession.Dispose();
                return;
            }
            //连接通过，移除该Component，不添加词句，5秒后就断开连接了
            accountSession.RemoveComponent<SessionAcceptTimeoutComponent>();

            //第二次玩家登录请求，会被阻断，防止玩家疯狂点击登录
            if (accountSession.GetComponent<SessionLockingComponent>()!=null)
            {
                response.Error = ErrorCode.ERR_RequestRepeatedly;
                reply();
                accountSession.Disconnect().Coroutine();
                return;
            }

            if (string.IsNullOrEmpty(request.Password)|| string.IsNullOrEmpty(request.AccountName))
            {
                response.Error = ErrorCode.ERR_LoginInfoError;
                reply();//意思就是服务器会向客户端发送一条消息，发送错误码
                accountSession.Disconnect().Coroutine();
                return;
            }
            //账号
            //必须大小写和数字组合而成
            if (!Regex.IsMatch(request.AccountName.Trim(), @"^(?=.*[0-9].*)(?=.*[A-Z].*)(?=.*[a-z].*).{6,15}$"))
            {
                response.Error = ErrorCode.ERR_LoginInfoError;
                reply();//意思就是服务器会向客户端发送一条消息，发送错误码
                accountSession.Disconnect().Coroutine();
                return;
            }
            //密码
            if (!Regex.IsMatch(request.Password.Trim(), @"^[A-Za-z0-9]+$"))
            {
                response.Error = ErrorCode.ERR_LoginInfoError;
                reply();//意思就是服务器会向客户端发送一条消息，发送错误码
                accountSession.Disconnect().Coroutine();
                return;
            }
            //if (session.GetComponent<AccountsZone>() == null)
            //{
            //    session.AddComponent<AccountsZone>();
            //}

            if (accountSession.GetComponent<RoleInfosZone>() == null)
            {
                accountSession.AddComponent<RoleInfosZone>();
            }


            //第一次进来会添加这个组件SessionLockingComponent
            using (accountSession.AddComponent<SessionLockingComponent>())
            {
                //协成锁
                using (await CoroutineLockComponent.Instance.Wait(CoroutineLockType.LoginAccount,request.AccountName.Trim().GetHashCode()))
                {
                    #region DB查询
                    Account account = null;
                    var accountInfoList = await DBManagerComponent.Instance.GetZoneDB(accountSession.DomainZone()).Query<Account>(
                        d => d.accountName.Equals(request.AccountName.Trim()));
                    if (accountInfoList.Count > 0 && accountInfoList!=null)//账号存在
                    {
                        account = accountInfoList[0];
                        //已经存在，就不需要Addchild<Account>
                        accountSession.AddChild(account);
                        if (account.accountType == (int)AccountType.BlackList)
                        {
                            response.Error = ErrorCode.ERR_LoginBlackListError;
                            reply();
                            accountSession.Disconnect().Coroutine();
                            account?.Dispose();
                            return;
                        }
                        if (account.Password != request.Password)
                        {
                            response.Error = ErrorCode.ERR_LoginInfoError;
                            reply();
                            accountSession.Disconnect().Coroutine();
                            account?.Dispose();
                            return;
                        }
                    }
                    else
                    {
                        ///AddChild
                        ///会创建这个实体，并且让他Awake
                        ///fales 代表没有，需要创建，true 则从对象池拿出这个Entity
                        account = accountSession.AddChild<Account>(false);
                        account.accountName = request.AccountName.Trim();
                        account.Password = request.Password.Trim();
                        account.CreateTime = TimeHelper.ServerNow();
                        account.accountType = (int)AccountType.General;
                        //save 必须是实体
                        await DBManagerComponent.Instance.GetZoneDB(accountSession.DomainZone()).Save<Account>(account);
                    }
                    #endregion

                    #region  用户到账号中心登记自己的账号信息，并且T掉相同账号的 
                    StartSceneConfig startSceneConfig = StartSceneConfigCategory.Instance.GetBySceneName(accountSession.DomainZone(), "LoginCenter");
                    long loginCenterInstanceId = startSceneConfig.InstanceId;
                    var loginAccountResponse = (L2A_LoginAccountResponse)await ActorMessageSenderComponent.Instance.Call(loginCenterInstanceId, new A2L_LoginAccountRequest() { AccountId = account.Id });

                    if (loginAccountResponse.Error != ErrorCode.ERR_Success)
                    {
                        response.Error = loginAccountResponse.Error;

                        reply();
                        accountSession?.Disconnect().Coroutine();
                        account?.Dispose();
                        return;
                    }
                    #endregion

                    #region 踢别人下线
                    long accountSeesionInstanceId = accountSession.DomainScene().GetComponent<AccountSessionsComponent>().Get(account.InstanceId);
                    Session otherSession=Game.EventSystem.Get(accountSeesionInstanceId) as Session;//由于Session继承Entity
                    otherSession?.Send(new A2C_Disconnect() { Error = 0 });
                    otherSession?.Disconnect().Coroutine();
                    //自己上线
                    accountSession.DomainScene().GetComponent<AccountSessionsComponent>().Add(account.InstanceId, accountSession.InstanceId);
                    accountSession.AddComponent<AccountCheckOutTimeComponent,long >(account.Id);
                    #endregion


                    //令牌
                    string token = TimeHelper.ServerNow().ToString() + RandomHelper.RandomNumber(int.MinValue, int.MaxValue).ToString();
                    accountSession.DomainScene().GetComponent<TokenComponent>().Remove(account.Id);
                    accountSession.DomainScene().GetComponent<TokenComponent>().Add(account.Id, token);

                    response.AccountId = account.Id;
                    response.Token = token;

                    reply();
                    account?.Dispose();
                }
            }
        }
    }
}
