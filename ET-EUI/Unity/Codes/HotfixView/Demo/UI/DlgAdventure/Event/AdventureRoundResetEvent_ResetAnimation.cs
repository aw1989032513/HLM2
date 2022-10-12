using ET.EventType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    /// <summary>
    /// 回合数重置
    /// </summary>
    public class AdventureRoundResetEvent_ResetAnimation : AEvent<EventType.AdventureRoundReset>
    {
        protected override async ETTask Run(AdventureRoundReset args)
        {
            Unit unit = UnitHelper.GetMyUnitFromCurrentScene(args.ZoneScene.CurrentScene());
            unit?.GetComponent<AnimatorComponent>()?.Play(MotionType.Idle);
            await ETTask.CompletedTask;
        }
    }
}
