using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    public static class ItemHelper
    {
        public static void RandomQuality(this Item item)
        {
            int rate = RandomHelper.RandomNumber(0, 10000);
            if (rate < 4000)
            {
                item.quality = (int)ItemQuality.General;
            }
            else if (rate < 7000)
            {
                item.quality = (int)ItemQuality.Good;
            }
            else if (rate < 8500)
            {
                item.quality = (int)ItemQuality.Excellent;
            }
            else if (rate < 9500)
            {
                item.quality = (int)ItemQuality.Epic;
            }
            else if (rate < 10000)
            {
                item.quality = (int)ItemQuality.Legend;
            }
        }
    }
}
