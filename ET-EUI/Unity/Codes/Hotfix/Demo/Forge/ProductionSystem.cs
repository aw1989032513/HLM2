using System;
using UnityEngine;

namespace ET
{
    public class ProductionAwakeSystem : AwakeSystem<Production, int>
    {
        public override void Awake(Production self, int configId)
        {
            self.ConfigId = configId;
            self.StartTime = 0;
            self.TargetTime = 0;
            self.ProductionState = (int)ProductionState.Received;
        }
    }
    public class ProductionDestorySystem : DestroySystem<Production>
    {
        public override void Destroy(Production self)
        {
            self.ConfigId = 0;
            self.StartTime = 0;
            self.TargetTime = 0;
            self.ProductionState = (int)ProductionState.None;
        }
    }
    public static class ProductionSystem
    {
        public static void DoAwake(this Production self, ProductionProto productionProto)
        {
            if (self == null)
            {
                Log.Error("self is null");
            }
            if (productionProto == null)
            {
                Log.Error("productionProto is null");
            }
            self.Id = productionProto.Id;
            self.ConfigId = productionProto.ConfigId;
            self.ProductionState = productionProto.ProductionState;
            self.StartTime = productionProto.StartTime;
            ///结束时间
            self.TargetTime = productionProto.TargetTime;
        }
        public static bool IsMakingState(this Production self)
        {
            return self.ProductionState == (int)ProductionState.Making;
        }
        public static bool IsMakeTimeOver(this Production self)
        {
            return self.TargetTime < TimeHelper.ServerNow();
        }
        /// <summary>
        /// 制作物品时间
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static string GetRemainingTimeStr(this Production self)
        {
            long RemainTime = self.TargetTime - TimeHelper.ServerNow();
            if (RemainTime <= 0)
            {
                return "0时0分0秒";
            }
            RemainTime /= 1000;//转换为秒

            float h = Mathf.FloorToInt(RemainTime / 3600f);
            float m = Mathf.FloorToInt(RemainTime / 60f - h * 60);
            float s = Mathf.FloorToInt(RemainTime - m * 60f - h * 3600f);
            return h.ToString("0")+"小时" + m.ToString("00") + "分" + s.ToString("00") + "秒";
        }
        public static float GetRemainTimeValue(this Production self)
        {
            long RemainTime = self.TargetTime - TimeHelper.ServerNow();
            if (RemainTime <= 0)
            {
                return 0.0f;
            }
            long totalTIme = self.TargetTime - self.StartTime;

            return RemainTime / (float)totalTIme;
        }
    }
}
