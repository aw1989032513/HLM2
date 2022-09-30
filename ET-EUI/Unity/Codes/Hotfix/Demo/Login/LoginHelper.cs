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
                    ServerId=1,
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


            await ETTask.CompletedTask;
            return ErrorCode.ERR_Success;
        }
    }
}
