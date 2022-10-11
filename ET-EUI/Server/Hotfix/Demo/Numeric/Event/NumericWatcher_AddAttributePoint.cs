using ET.EventType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    [NumericWatcher((int)NumericType.Spirit)]
    [NumericWatcher((int)NumericType.Agile)]
    [NumericWatcher((int)NumericType.PhysicalStrength)]
    [NumericWatcher((int)NumericType.Power)]
    public class NumericWatcher_AddAttributePoint : INumericWatcher
    {
        public void Run(NumbericChange args)
        {
            if (! (args.Parent is Unit unit))
            {
                return;
            }
            //1点力量 伤害值+5
            if (args.NumericType == (int)NumericType.Power)
            {
                unit.GetComponent<NumericComponent>()[(int)NumericType.DamageValueAdd] += 5;
            }
            // 1 点体力 增加1% 生命值
            if (args.NumericType == (int)NumericType.PhysicalStrength)
            {
                unit.GetComponent<NumericComponent>()[(int)NumericType.HpPct] += 1*10000;  
            }
            //敏捷+1点  闪避概率加0.1%
            if (args.NumericType == (int)NumericType.Agile)
            {
                unit.GetComponent<NumericComponent>()[(int)NumericType.DodgeFinalAdd] += 1 * 1000;
            }

            //精神+1点 最大法力值 +1%
            if (args.NumericType == (int)NumericType.Spirit)
            {
                unit.GetComponent<NumericComponent>()[(int)NumericType.MaxMpFinalPct] += 1 * 10000;
            }
        }
    }
}
