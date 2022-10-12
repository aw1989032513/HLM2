using ET.EventType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    /// <summary>
    /// 刚进Map服务器时
    /// </summary>
    public class SceneChangeFinish_StartAdventure : AEventAsync<EventType.SceneChangeFinish>
    {
        protected override async ETTask Run(SceneChangeFinish args)
        {
            Unit unit = UnitHelper.GetMyUnitFromCurrentScene(args.CurrentScene);

            //是否在关卡状态
            if (unit.GetComponent<NumericComponent>().GetAsInt((int)NumericType.AdventureState) == 0)
            {
                return;
            }
            await TimerComponent.Instance.WaitAsync(3000);

            args.CurrentScene.GetComponent<AdventureComponent>().StartAdventure().Coroutine();
            await ETTask.CompletedTask;
        }
    }
}
