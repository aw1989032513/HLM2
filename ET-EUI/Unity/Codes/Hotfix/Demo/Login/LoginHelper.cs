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
                a2C_LoginAccount= (A2C_LoginAccount)await serverAccountSession.Call(new C2A_LoginAccount() { AccountName = account, Password = password });
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
            zonScene.GetComponent<AccountInfoComponent>().token = a2C_LoginAccount.Key;
            zonScene.GetComponent<AccountInfoComponent>().accountId = a2C_LoginAccount.AccounId;
            return ErrorCode.ERR_Success;
        }
    }
}
