using ET.EventType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    /// <summary>
    /// 收到伤害的时候会触发这个事件
    /// </summary>
    public class ShowDamageValueViewEvent_RefreshHp : AEventAsync<EventType.ShowDamageValueView>
    {
        protected override async ETTask Run(ShowDamageValueView args)
        {
            args.targetUnit.GetComponent<HeadHpViewComponent>().SetHp();
            args.zongScene.GetComponent<FlyDamageValueViewComponent>().SpawnFlyDamage(args.targetUnit.Position, args.damageValue).Coroutine();
            bool isAlive = args.targetUnit.IsAlive();
            await TimerComponent.Instance.WaitAsync(400);

            args.targetUnit?.GetComponent<HeadHpViewComponent>()?.SetVisible(isAlive);
        }
    }
}
