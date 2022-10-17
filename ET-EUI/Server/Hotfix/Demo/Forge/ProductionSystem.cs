using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    public class ProductionAwakeSystem : AwakeSystem<Production, int>
    {
        public override void Awake(Production self, int a)
        {
            self.ConfigId = a;
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
        public static ProductionProto ToMessage(this Production self)
        {
            ProductionProto productionProto = new ProductionProto();
            productionProto.Id = self.Id;
            productionProto.ConfigId = self.ConfigId;
            productionProto.StartTime = self.StartTime;
            productionProto.TargetTime = self.TargetTime;
            productionProto.ProductionState = self.ProductionState;
            return productionProto; 
        }
        /// <summary>
        /// 释放掉
        /// </summary>
        /// <param name="self"></param>
        public static void Reset(this Production self)
        {
            self.ConfigId = 0;
            self.ProductionState = (int)ProductionState.Received;
            self.TargetTime = 0;
            self.StartTime = 0;
        }
    }
}
