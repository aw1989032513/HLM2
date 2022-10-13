namespace ET
{
    public static class UnitHelper
    {
        public static Unit GetMyUnitFromZoneScene(Scene zoneScene)
        {
            PlayerComponent playerComponent = zoneScene.GetComponent<PlayerComponent>();
            Scene currentScene = zoneScene.GetComponent<CurrentScenesComponent>().Scene;
            return currentScene.GetComponent<UnitComponent>().Get(playerComponent.MyId);
        }
        /// <summary>
        /// 当前场景拿到Unit
        /// </summary>
        /// <param name="currentScene"></param>
        /// <returns></returns>
        public static Unit GetMyUnitFromCurrentScene(Scene currentScene)
        {
            //currentScene.Parent.是CurrentScenesComponent
            //CurrentScenesComponent 的Parent 是zoneScene
            PlayerComponent playerComponent = currentScene.Parent.Parent.GetComponent<PlayerComponent>();
            if (playerComponent == null)
            {
                Log.Error($"{currentScene}身上没有PlayerComponent");
                return null;
            }
            return currentScene.GetComponent<UnitComponent>().Get(playerComponent.MyId);
        }
        public static NumericComponent GetMyUnitNumericComponent(Scene currentScene)
        {
            PlayerComponent playerComponent = currentScene?.ZoneScene()?.GetComponent<PlayerComponent>();
            if (playerComponent == null)
            {
                Log.Error($"{currentScene}身上没有PlayerComponent");
                return null;
            }
            return currentScene.GetComponent<UnitComponent>()?.Get(playerComponent.MyId)?.GetComponent<NumericComponent>();
        }
    }
}