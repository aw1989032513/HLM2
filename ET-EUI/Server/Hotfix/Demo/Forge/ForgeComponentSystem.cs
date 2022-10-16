using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    public class ForgeComponentDeserialSystem : DeserializeSystem<ForgeComponent>
    {
        public override void Deserialize(ForgeComponent self)
        {
            foreach (var entity in self.Children.Values)
            {
                self.ProductionsList.Add(entity as Production);
            }
        }
    }
    public static class ForgeComponentSystem
    {
        public static bool IsExistFreeQueue(this ForgeComponent self)
        {
            if (self.ProductionsList.Count < 2)
            {
                return true;
            }
            else
            {
                Production production = self.GetFreeProduction();
                if (production != null)
                {
                    return true;
                }
            }
            return false;
        }
        public static Production GetFreeProduction(this ForgeComponent self)
        {
            for (int i = 0; i < self.ProductionsList.Count; i++)
            {
                if (self.ProductionsList[i].ProductionState == (int)ProductionState.Received)
                {
                    return self.ProductionsList[i];
                }
            }
            return null;
        }
        /// <summary>
        /// 开始生产
        /// </summary>
        /// <param name="self"></param>
        public static Production StartProduction(this ForgeComponent self,int configId)
        {
            Production production = self.GetFreeProduction();
            if (production == null)
            {
                production = self.AddChild<Production>();
                self.ProductionsList.Add(production);
            }
            //初始化
            production.ConfigId = configId;
            production.ProductionState = (int)ProductionState.Making;
            production.StartTime = TimeHelper.ServerNow();
            production.TargetTime = TimeHelper.ServerNow() + (ForgeProductionConfigCategory.Instance.Get(configId).ProductionTime * 1000); ;
            return production;
        }
        public static bool IsExistProductionOverQueue(this ForgeComponent self,long productionId)
        {
            for (int i = 0; i < self.ProductionsList.Count; i++)
            {
                if (self.ProductionsList[i].Id == productionId  &&
                    self.ProductionsList[i].ProductionState == (int)ProductionState.Making &&
                    self.ProductionsList[i].TargetTime <= TimeHelper.ServerNow())
                {
                    return true;
                }
            }
           return false;
        }
        public static Production GetProductionById(this ForgeComponent self, long productionId)
        {
            for (int i = 0; i < self.ProductionsList.Count; i++)
            {
                if (productionId == self.ProductionsList[i].Id)
                {
                    return self.ProductionsList[i];
                }
            }
            return null;
        }
    }
}
