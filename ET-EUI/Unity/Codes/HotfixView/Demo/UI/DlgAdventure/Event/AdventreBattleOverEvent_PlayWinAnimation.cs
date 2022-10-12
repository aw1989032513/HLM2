using ET.EventType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    public class AdventreBattleOverEvent_PlayWinAnimation : AEventAsync<EventType.AdventureBattleOver>
    {
        protected override async ETTask Run(AdventureBattleOver args)
        {
            args.winUnit?.GetComponent<AnimatorComponent>()?.Play(MotionType.Win);
            await ETTask.CompletedTask;
        }
    }
}
