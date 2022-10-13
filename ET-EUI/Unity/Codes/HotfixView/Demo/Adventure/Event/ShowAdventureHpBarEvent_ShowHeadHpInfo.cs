using ET.EventType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    public class ShowAdventureHpBarEvent_ShowHeadHpInfo : AEventAsync<EventType.ShowAdventureHpBar>
    {
        protected override async ETTask Run(ShowAdventureHpBar args)
        {
            args.unit.GetComponent<HeadHpViewComponent>().SetVisible(args.isShow);
            args.unit.GetComponent<HeadHpViewComponent>().SetHp();
            await ETTask.CompletedTask;
        }
    }
}
