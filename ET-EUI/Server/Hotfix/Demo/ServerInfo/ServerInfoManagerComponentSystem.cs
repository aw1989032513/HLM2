namespace ET
{
    public class ServerInfoManagerComponentAwakeSystem : AwakeSystem<ServerInfoManagerComponent>
    {
        public override void Awake(ServerInfoManagerComponent self)
        {
            self.Awake().Coroutine();
        }
    }

    public class ServerInfoManagerComponentDestorySystem : DestroySystem<ServerInfoManagerComponent>
    {
        public override void Destroy(ServerInfoManagerComponent self)
        {
            foreach (var item in self.serverInfoList)
            {
                item?.Dispose();
            }
            self.serverInfoList.Clear();
        }
    }
    /// <summary>
    /// 热重载周期函数
    /// </summary>
    public class ServerInfoManagerComponentLoadSystem : LoadSystem<ServerInfoManagerComponent>
    {
        public override void Load(ServerInfoManagerComponent self)
        {
            self.Awake().Coroutine();
        }
    }
    public static class ServerInfoManagerComponentSystem
    {
        public static async ETTask Awake(this ServerInfoManagerComponent self)
        {
            var serverInfoList = await DBManagerComponent.Instance.GetZoneDB(self.DomainZone()).Query<ServerInfo>(d
                   => true);
            if (serverInfoList == null || serverInfoList.Count <= 0)
            {
                Log.Error("serverInfo 数据库是空的");
                self.serverInfoList.Clear();
                //拿到表所有的信息
                var serverInfoCfg = ServerInfoConfigCategory.Instance.GetAll();
                foreach (var info in serverInfoCfg.Values)
                {
                    ServerInfo newServerInfo = self.AddChildWithId<ServerInfo>(info.Id);
                    newServerInfo.serverName = info.ServerName;
                    newServerInfo.status = (int)ServerStatus.Normal;

                    self.serverInfoList.Add(newServerInfo);
                    await DBManagerComponent.Instance.GetZoneDB(self.DomainZone()).Save(newServerInfo);
                }
                return;
            }

            self.serverInfoList.Clear();
            foreach (var serverInfo in serverInfoList)
            {
                self.AddChild(serverInfo);
                self.serverInfoList.Add(serverInfo);
            }
            await ETTask.CompletedTask;
        }
    }
}
