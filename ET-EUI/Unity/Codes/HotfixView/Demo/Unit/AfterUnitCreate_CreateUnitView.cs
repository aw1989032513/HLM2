using UnityEngine;

namespace ET
{
    public class AfterUnitCreate_CreateUnitView: AEvent<EventType.AfterUnitCreate>
    {
        protected override async ETTask Run(EventType.AfterUnitCreate args)
        {
            // Unit View层
            await ResourcesComponent.Instance.LoadBundleAsync($"{args.Unit.Config.PrefabName}.unity3d");
            GameObject bundleGameObject = (GameObject)ResourcesComponent.Instance.GetAsset($"{args.Unit.Config.PrefabName}.unity3d", args.Unit.Config.PrefabName);
            GameObject go = UnityEngine.Object.Instantiate(bundleGameObject);
            go.transform.SetParent(GlobalComponent.Instance.Unit, true);

            args.Unit.AddComponent<GameObjectComponent>().GameObject = go;
            args.Unit.GetComponent<GameObjectComponent>().SpriteRenderer = go.GetComponent<SpriteRenderer>();
            args.Unit.AddComponent<AnimatorComponent>();
           // args.Unit.AddComponent<HeadHpViewComponent>();

            args.Unit.Position = args.Unit.Type == UnitType.Player ? new Vector3(-1.5f, 0, 7) : new Vector3(1.5f, RandomHelper.RandomNumber(-1, 1), 7);

            await ETTask.CompletedTask;
        }
    }
}