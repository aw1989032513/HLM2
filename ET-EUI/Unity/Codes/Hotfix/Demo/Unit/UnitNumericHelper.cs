using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    public static class UnitNumericHelper
    {
        public static bool IsAlive(this Unit unit)
        {
            if (unit.IsDisposed || unit == null)
            {
                return false;
            }
            if (unit.GetComponent<NumericComponent>() == null)
            {
                return false;
            }
            return unit.GetComponent<NumericComponent>().GetAsInt((int)NumericType.IsAlive) == 1;
        }
        public static void SetAlive(this Unit unit, bool isAlive)
        {
            if (unit == null || unit.IsDisposed)
            {
                return;
            }

            NumericComponent numericComponent = unit.GetComponent<NumericComponent>();
            if (null == numericComponent)
            {
                return;
            }

            numericComponent.Set((int)NumericType.IsAlive, isAlive ? 1 : 0);
        }
    }
}
