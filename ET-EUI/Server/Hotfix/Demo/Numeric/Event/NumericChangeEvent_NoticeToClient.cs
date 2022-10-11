using ET.EventType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    
    public class NumericChangeEvent_NoticeToClient : AEvent<EventType.NumbericChange>
    {
        protected override async ETTask Run(NumbericChange a)
        {
            if (!(a.Parent is Unit unit))
            {
                return;
            }
            //if (unit.Type != UnitType.Player)
            //{
            //    return;
            //}
            unit.GetComponent<NumericNoticeComponent>()?.NoticeImmediately(a);
            await ETTask.CompletedTask;
        }
    }
}
