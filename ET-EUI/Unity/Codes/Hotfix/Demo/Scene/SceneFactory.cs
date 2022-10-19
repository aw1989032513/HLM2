namespace ET
{
    public static class SceneFactory
    {
        public static Scene CreateZoneScene(int zone, string name, Entity parent)
        {
            Scene zoneScene = EntitySceneFactory.CreateScene(Game.IdGenerater.GenerateInstanceId(), zone, SceneType.Zone, name, parent);
            zoneScene.AddComponent<ZoneSceneFlagComponent>();
            zoneScene.AddComponent<NetKcpComponent, int>(SessionStreamDispatcherType.SessionStreamDispatcherClientOuter);
			zoneScene.AddComponent<CurrentScenesComponent>();
            zoneScene.AddComponent<ObjectWait>();
            zoneScene.AddComponent<PlayerComponent>();
            //背包
            zoneScene.AddComponent<BagComponent>();
            zoneScene.AddComponent<EquipmentsComponent>();
            //打造
            zoneScene.AddComponent<ForgeComponent>();
            //任务
            zoneScene.AddComponent<TasksComponent>();
            //排行
            zoneScene.AddComponent<RankComponent>();
            //聊天
            zoneScene.AddComponent<ChatComponent>();
            zoneScene.AddComponent<AccountInfoComponent>();
            zoneScene.AddComponent<ServerInfosCompontent>();
            zoneScene.AddComponent<RoleInfosComponent>();
            Game.EventSystem.Publish(new EventType.AfterCreateZoneScene() {ZoneScene = zoneScene});
            return zoneScene;
        }
        
        public static Scene CreateCurrentScene(long id, int zone, string name, CurrentScenesComponent currentScenesComponent)
        {
            Scene currentScene = EntitySceneFactory.CreateScene(id, zone, SceneType.Current, name, currentScenesComponent);
            currentScenesComponent.Scene = currentScene;
            
            Game.EventSystem.Publish(new EventType.AfterCreateCurrentScene() {CurrentScene = currentScene});
            return currentScene;
        }
        
        
    }
}