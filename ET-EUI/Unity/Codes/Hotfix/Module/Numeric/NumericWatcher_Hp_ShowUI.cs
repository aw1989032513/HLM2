﻿using ET.EventType;

namespace ET
{
    /// <summary>
    /// 监视hp数值变化，改变血条值
    /// </summary>
    [NumericWatcher((int)NumericType.Hp)]
    public class NumericWatcher_Hp_ShowUI : INumericWatcher
    {
        public void Run(NumbericChange args)
        {
            
        }
    }
}
