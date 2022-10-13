using DG.Tweening;
using TMPro;
using UnityEditor.UI;
using UnityEngine;

namespace ET
{
    public class FlyDamageValueViewComponentAwakeSystem : AwakeSystem<FlyDamageValueViewComponent>
    {
        public override void Awake(FlyDamageValueViewComponent self)
        {
            self.Awake().Coroutine();
        }
    }
    public class FlyDamageValueViewComponentDestroySystem : DestroySystem<FlyDamageValueViewComponent>
    {
        public override void Destroy(FlyDamageValueViewComponent self)
        {
            ForeachHelper.Foreach<GameObject>(self.FlyingDamageHashSet, (o) =>
            {
                o.transform.DOKill();//停止Dotween 
                GameObject.Destroy(o);
            });
            self.FlyingDamageHashSet.Clear();
        }
    }
    public static class FlyDamageValueViewComponentSystem
    {
        public static async ETTask Awake(this FlyDamageValueViewComponent self)
        {
            await ResourcesComponent.Instance.LoadBundleAsync("flyDamageValue.unity3d");
            GameObject prefabGameObject = (GameObject)ResourcesComponent.Instance.GetAsset("flyDamageValue.unity3d", "flyDamageValue");
            await GameObjectPoolHelper.InitPoolFormGamObjectAsync(prefabGameObject, 3);
        }

        public static async ETTask SpawnFlyDamage(this FlyDamageValueViewComponent self, Vector3 startPos, long DamageValue)
        {
            GameObject flyDamageValueGameObject = GameObjectPoolHelper.GetObjectFromPool("flyDamageValue");
            flyDamageValueGameObject.transform.SetParent(GlobalComponent.Instance.Unit);//Canvas界面的UnitRoot根节点下面
            self.FlyingDamageHashSet.Add(flyDamageValueGameObject);
            flyDamageValueGameObject.SetActive(true);

            flyDamageValueGameObject.GetComponentInChildren<TextMeshPro>().text = DamageValue <= 0 ? "Miss" : $"-{DamageValue}";
            flyDamageValueGameObject.transform.position = startPos;

            flyDamageValueGameObject.transform.DOMoveY(startPos.y + 1.5f, 0.8f).onComplete = () =>
            {
                flyDamageValueGameObject.SetActive(false);
                self.FlyingDamageHashSet.Remove(flyDamageValueGameObject);
                //放入对象池中
                GameObjectPoolHelper.ReturnObjectToPool(flyDamageValueGameObject);
            };
            await ETTask.CompletedTask;
        }
    }
}
