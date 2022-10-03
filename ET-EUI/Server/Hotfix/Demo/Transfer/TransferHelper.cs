namespace ET
{
    public static class TransferHelper
    {
        public static async ETTask Transfer(Unit unit, long sceneInstanceId, string sceneName)
        {
            // 通知客户端开始切场景
            M2C_StartSceneChange m2CStartSceneChange = new M2C_StartSceneChange() {SceneInstanceId = sceneInstanceId, SceneName = sceneName};
            MessageHelper.SendToClient(unit, m2CStartSceneChange);
            
            M2M_UnitTransferRequest request = new M2M_UnitTransferRequest();
            request.Unit = unit;
            foreach (Entity entity in unit.Components.Values)
            {
                if (entity is ITransfer)//是否继承ITransfer这个接口
                {
                    request.Entitys.Add(entity);
                }
            }
            // 删除Mailbox,因为传送到Map服务器之后，会重新添加上
            unit.RemoveComponent<MailBoxComponent>();
            
            // location加锁
            long oldInstanceId = unit.InstanceId;
            await LocationProxyComponent.Instance.Lock(unit.Id, unit.InstanceId);
            M2M_UnitTransferResponse response = await ActorMessageSenderComponent.Instance.Call(sceneInstanceId, request) as M2M_UnitTransferResponse;
            await LocationProxyComponent.Instance.UnLock(unit.Id, oldInstanceId, response.NewInstanceId);
            unit.Dispose();//unit 断开Gate服务器
        }
    }
}