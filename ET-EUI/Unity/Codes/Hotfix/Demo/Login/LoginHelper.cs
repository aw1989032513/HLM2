using System;


namespace ET
{
    public static class LoginHelper
    {
      
        public static async ETTask<int> Login(Scene zonScene, string address, string account,string password)
        {
            //A指的服务器的Account，C指的Client
            A2C_LoginAccount a2C_LoginAccount = null;
            Session serverAccountSession = null;//服务器Seesion
            try
            {
                serverAccountSession = zonScene.GetComponent<NetKcpComponent>().Create(NetworkHelper.ToIPEndPoint(address));
                password = MD5Helper.StringMD5(password);
                a2C_LoginAccount = (A2C_LoginAccount)await serverAccountSession.Call(new C2A_LoginAccount() { AccountName = account, Password = password });
            }
            catch (Exception e)
            {

                serverAccountSession?.Dispose();
                Log.Error(e.ToString());
                return ErrorCode.ERR_NetWorkError;
            }
            if (a2C_LoginAccount.Error!=ErrorCode.ERR_Success)
            {
                serverAccountSession?.Dispose();
                return a2C_LoginAccount.Error;
            }
            //客户端和服务器Session连接了
            zonScene.AddComponent<SessionComponent>().Session= serverAccountSession;
            zonScene.GetComponent<SessionComponent>().Session.AddComponent<PingComponent>();

            zonScene.GetComponent<AccountInfoComponent>().token = a2C_LoginAccount.Token;
            zonScene.GetComponent<AccountInfoComponent>().accountId = a2C_LoginAccount.AccountId;
            return ErrorCode.ERR_Success;
        }
        /// <summary>
        /// 获取服务器列表
        /// </summary>
        /// <param name="zonScene"></param>
        /// <param name="address"></param>
        /// <param name="account"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static async ETTask<int> GetServerInfos(Scene zonScene)
        {
            A2C_GetServerInfos a2C_GetServerInfos = null;
            try
            {
                a2C_GetServerInfos = (A2C_GetServerInfos)await zonScene.GetComponent<SessionComponent>().Session.Call(new C2A_GetServerInfos()
                {
                    AccountId = zonScene.GetComponent<AccountInfoComponent>().accountId,
                    Token = zonScene.GetComponent<AccountInfoComponent>().token
                });

            }
            catch (Exception e)
            {
                Log.Error(e.ToString());
                return ErrorCode.ERR_NetWorkError; ;
            }
            if (a2C_GetServerInfos.Error!= ErrorCode.ERR_Success)
            {
                return a2C_GetServerInfos.Error;
            }

            //拿到ServerInfosList
            foreach (var item in a2C_GetServerInfos.ServerInfosList)
            {
                //把空的ServerInfo挂在到ServerInfosCompontent的实体下
                ServerInfo serverInfo = zonScene.GetComponent<ServerInfosCompontent>().AddChild<ServerInfo>();
                //初始化ServerInfo
                serverInfo.DoAwake(item);
                zonScene.GetComponent<ServerInfosCompontent>().Add(serverInfo);
            }

            await ETTask.CompletedTask;
            return ErrorCode.ERR_Success;
        }

        public static async ETTask<int> CreateRoles(Scene zonScene, string name)
        {
            A2C_CreateRole a2C_CreateRole = null;
          
            try
            {
                a2C_CreateRole = (A2C_CreateRole)await zonScene.GetComponent<SessionComponent>().Session.Call(new C2A_CreateRole()
                {
                    AccountId = zonScene.GetComponent<AccountInfoComponent>().accountId,
                    Token = zonScene.GetComponent<AccountInfoComponent>().token,
                    Name = name,
                    ServerId =zonScene.GetComponent<ServerInfosCompontent>().currentServerId ,
                }) ;
            }
            catch (Exception e)
            {
                Log.Error(e.ToString());
                return ErrorCode.ERR_NetWorkError;
            }
            if (a2C_CreateRole.Error != ErrorCode.ERR_Success)
            {
                Log.Error(a2C_CreateRole.Error.ToString());
                return a2C_CreateRole.Error;
            }

            //通过，服务器允许创建Role
            RoleInfo newRoleInfo = zonScene.GetComponent<RoleInfosComponent>().AddChild<RoleInfo>();
            newRoleInfo.DoAwake(a2C_CreateRole.RoleInfo);
            zonScene.GetComponent<RoleInfosComponent>().roleInfosList.Add(newRoleInfo);

            await ETTask.CompletedTask;
            return ErrorCode.ERR_Success;
        }

        public static async ETTask<int> GetRoles(Scene zonScene)
        {
            A2C_GetRoles a2C_GetRoles = null;
      
            try
            {
                a2C_GetRoles = (A2C_GetRoles)await zonScene.GetComponent<SessionComponent>().Session.Call(new C2A_GetRoles()
                {
                    AccountId = zonScene.GetComponent<AccountInfoComponent>().accountId,
                    Token = zonScene.GetComponent<AccountInfoComponent>().token,
                    ServerId = zonScene.GetComponent<ServerInfosCompontent>().currentServerId,
                });
            }
            catch (Exception e)
            {
                Log.Error(e.ToString());
                return ErrorCode.ERR_NetWorkError;
            }
            if (a2C_GetRoles.Error != ErrorCode.ERR_Success)
            {
                Log.Error(a2C_GetRoles.Error.ToString());
                return a2C_GetRoles.Error;
            }

            //全部清空
            zonScene.GetComponent<RoleInfosComponent>().roleInfosList.Clear();
            // 重新赋值
            foreach (var item in a2C_GetRoles.RoleInfo)
            {
                RoleInfo roleInfo = zonScene.GetComponent<RoleInfosComponent>().AddChild<RoleInfo>();
                roleInfo.DoAwake(item);
                zonScene.GetComponent<RoleInfosComponent>().roleInfosList.Add(roleInfo);
            }
            return ErrorCode.ERR_Success;
        }

        public static async ETTask<int> DeleteRole(Scene zonScene)
        {
            A2C_DeleteRole a2C_DeleteRole = null;

            try
            {
                a2C_DeleteRole = (A2C_DeleteRole)await zonScene.GetComponent<SessionComponent>().Session.Call(new C2A_DeleteRole()
                {
                    Token = zonScene.GetComponent<AccountInfoComponent>().token,
                    AccountId = zonScene.GetComponent<AccountInfoComponent>().accountId,
                    RoleInfoId = zonScene.GetComponent<RoleInfosComponent>().currentRoleId,
                    ServerId = zonScene.GetComponent<ServerInfosCompontent>().currentServerId,
                });
            }
            catch (Exception e)
            {
                Log.Error(e.ToString());
                return ErrorCode.ERR_NetWorkError;
            }
            if (a2C_DeleteRole.Error != ErrorCode.ERR_Success)
            {
                Log.Error(a2C_DeleteRole.Error.ToString());
                return a2C_DeleteRole.Error;
            }
            int index = zonScene.GetComponent<RoleInfosComponent>().roleInfosList.FindIndex((info) => { return info.Id == a2C_DeleteRole.DeletedRoleInfoId; });
            zonScene.GetComponent<RoleInfosComponent>().roleInfosList.RemoveAt(index);

            await ETTask.CompletedTask;
            return ErrorCode.ERR_Success;
        }

        public static async ETTask<int> GetRealmKey(Scene zoneScene)
        {
            A2C_GetRealmKey a2C_GetRealmKey = null;

            try
            {
                a2C_GetRealmKey = (A2C_GetRealmKey) await zoneScene.GetComponent<SessionComponent>().Session.Call(new C2A_GetRealmKey()
                {
                    Token = zoneScene.GetComponent<AccountInfoComponent>().token,
                    AccountId = zoneScene.GetComponent<AccountInfoComponent>().accountId,
                    ServerId = zoneScene.GetComponent<ServerInfosCompontent>().currentServerId
                });
            }
            catch (Exception e)
            {

                Log.Error(e.ToString());
                return ErrorCode.ERR_NetWorkError;
            }

            if (a2C_GetRealmKey.Error != ErrorCode.ERR_Success)
            {
                Log.Error(a2C_GetRealmKey.Error.ToString());
                return a2C_GetRealmKey.Error;
            }
            //把得到的RealmKey 和 RealmAddress 存到AccountInfoComponent里
            zoneScene.GetComponent<AccountInfoComponent>().realmKey = a2C_GetRealmKey.RealmKey;
            zoneScene.GetComponent<AccountInfoComponent>().realmAddress = a2C_GetRealmKey.RealmAddress;
            //跟account服务器断开链接
            zoneScene.GetComponent<SessionComponent>().Session.Dispose();
            await ETTask.CompletedTask;
            return ErrorCode.ERR_Success;
        }

        public static async ETTask<int> EnterGame(Scene zoneScene)
        {
            string realmAdress = zoneScene.GetComponent<AccountInfoComponent>().realmAddress;
            // 1. 连接Realm，获取分配的Gate
            Session realmsession = zoneScene.GetComponent<NetKcpComponent>().Create(NetworkHelper.ToIPEndPoint(realmAdress));
            R2C_LoginRealm r2C_LoginRealm = null;
            try
            {
                 r2C_LoginRealm = (R2C_LoginRealm)await realmsession.Call(new C2R_LoginRealm()
                {
                    AccountId = zoneScene.GetComponent<AccountInfoComponent>().accountId,
                    RealmTokenKey = zoneScene.GetComponent<AccountInfoComponent>().realmKey,
                }) ;
            }
            catch (Exception e)
            {
                Log.Error(e.ToString());
                realmsession?.Dispose();
                return ErrorCode.ERR_NetWorkError;                
            }
            //断开连接
            realmsession?.Dispose();


            if (r2C_LoginRealm.Error != ErrorCode.ERR_Success)
            {
                Log.Error(r2C_LoginRealm.Error.ToString());
                return r2C_LoginRealm.Error;
            }
            
         
            //如果成功了
            Log.Warning($"GateAddress :  {r2C_LoginRealm.GateAddress}");
            Session gateSession = zoneScene.GetComponent<NetKcpComponent>().Create(NetworkHelper.ToIPEndPoint(r2C_LoginRealm.GateAddress));
            gateSession.AddComponent<PingComponent>();
            zoneScene.GetComponent<SessionComponent>().Session = gateSession;

            // 2. 开始连接Gate
            long currentRoleId = zoneScene.GetComponent<RoleInfosComponent>().currentRoleId;
            G2C_LoginGameGate g2C_LoginGameGate = null;

         
            try
            {
                long accountId = zoneScene.GetComponent<AccountInfoComponent>().accountId;
                g2C_LoginGameGate = (G2C_LoginGameGate)await gateSession.Call(new C2G_LoginGameGate()
                {
                    Account = accountId,
                    Key = r2C_LoginRealm.GateSessionKey,
                    RoleId = currentRoleId,

                })  ;
            }
            catch (Exception e)
            {
                Log.Error(e);
                zoneScene.GetComponent<SessionComponent>().Session.Dispose();
                return ErrorCode.ERR_NetWorkError;
            }
            if (g2C_LoginGameGate.Error != ErrorCode.ERR_Success)
            {
                zoneScene.GetComponent<SessionComponent>().Session.Dispose();
                return g2C_LoginGameGate.Error;
            }

            Log.Debug("登陆gate成功!");


            //3 .角色正式请求进入游戏逻辑服
            G2C_EnterGame g2C_EnterGame = null;
            try
            {
                g2C_EnterGame = (G2C_EnterGame)await gateSession.Call(new C2G_EnterGame()
                {

                });
            }
            catch (Exception e)
            {
                Log.Error(e.ToString());
                zoneScene.GetComponent<SessionComponent>().Session.Dispose();
                return ErrorCode.ERR_NetWorkError;
            }
            if (g2C_EnterGame.Error != ErrorCode.ERR_Success)
            {
                Log.Error(g2C_EnterGame.Error.ToString());
                return g2C_EnterGame.Error;
            }
            Log.Debug("玩家进入游戏成功");

            zoneScene.GetComponent<PlayerComponent>().MyId = g2C_EnterGame.MyId;
            await zoneScene.GetComponent<ObjectWait>().Wait<WaitType.Wait_SceneChangeFinish>();
            return ErrorCode.ERR_Success;
        }
    }
}