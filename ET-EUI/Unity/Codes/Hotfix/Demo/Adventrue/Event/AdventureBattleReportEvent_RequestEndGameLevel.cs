using ET.EventType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    public class AdventureBattleReportEvent_RequestEndGameLevel : AEventAsync<EventType.AdventureBattleReport>
    {
        protected override async ETTask Run(AdventureBattleReport args)
        {
            if (args.battleRoundResult == BattleRoundResult.KeepBattle)
            {
                return;
            }

            int errCode = await AdventureHelper.RequestEndGameLevel(args.zongScene, args.battleRoundResult, args.round);

            if (errCode != ErrorCode.ERR_Success)
            {
                return;
            }


            await TimerComponent.Instance.WaitAsync(3000);

            args.zongScene?.CurrentScene()?.GetComponent<AdventureComponent>()?.ShowAdventureHpBarInfo(false);
            args.zongScene?.CurrentScene()?.GetComponent<AdventureComponent>()?.ResetAdventure();

            await ETTask.CompletedTask;
        }
    }
}
