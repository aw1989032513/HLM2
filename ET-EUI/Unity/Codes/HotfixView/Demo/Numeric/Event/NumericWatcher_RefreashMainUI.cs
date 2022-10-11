using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    [NumericWatcher((int)NumericType.Level)]
    [NumericWatcher((int)NumericType.Gold)]
    [NumericWatcher((int)NumericType.Exp)]
    public class NumericWatcher_RefreashMainUI : INumericWatcher
    {
        public void Run(EventType.NumbericChange args)
        {
           args.Parent.ZoneScene().GetComponent<UIComponent>().GetDlgLogic<DlgMain>()?.Refresh();
        }     
    }
}
