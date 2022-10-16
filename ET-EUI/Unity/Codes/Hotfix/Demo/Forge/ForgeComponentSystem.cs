using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    [Timer(TimerType.MakeQueueOver)]
    public class MakeQueueOverTimer : ATimer<ForgeComponent>
    {
        public override async void Run(ForgeComponent t)
        {
            //红点显示
           await  Game.EventSystem.PublishAsync(new EventType.MakeQueueOver() { zongScene = t.ZoneScene() }) ;
        }
    }
    public class ForgeComponentAwakeSystem : AwakeSystem<ForgeComponent>
    {
        public override void Awake(ForgeComponent self)
        {

        }
    }
    public class ForgeComponentDestorySystem : DestroySystem<ForgeComponent>
    {
        public override void Destroy(ForgeComponent self)
        {
            foreach (var production in self.ProductionsList)
            {
                production?.Dispose();
            }

            ForeachHelper.Foreach<long, long>(self.ProductionTimerDict, (key, value) =>
            {
                TimerComponent.Instance?.Remove(ref value);
            });
            self.ProductionsList.Clear();
            self.ProductionTimerDict.Clear();
        }
    }
    /// <summary>
    /// 生产组件
    /// </summary>
    public static class ForgeComponentSystem
    {
        public static void AddOrUpdateProductionQueue(this ForgeComponent self, ProductionProto productionProto)
        {
            //我们客户端当前已经拥有的Production
            Production production = self.GetProductionById(productionProto.Id);
            if (production == null)
            {
                production = self.AddChild<Production>();
                self.ProductionsList.Add(production);
            }
            //拆解
            production.DoAwake(productionProto);
            //清空定时器任务
            if (self.ProductionTimerDict.TryGetValue(production.Id,out long timeId))
            {
                TimerComponent.Instance.Remove(ref timeId);
                self.ProductionTimerDict.Remove(production.Id);
            }
            //启动定时器任务
            if (production.IsMakingState() && !production.IsMakeTimeOver())
            {
                Log.Debug($"启动一个定时器!!!!:{production.TargetTime}");
                timeId = TimerComponent.Instance.NewOnceTimer(production.TargetTime, TimerType.MakeQueueOver, self);
                self.ProductionTimerDict.Add(production.Id, timeId);
            }
            //制作完之后红点显示
            Game.EventSystem.PublishAsync(new EventType.MakeQueueOver() { zongScene = self.ZoneScene() }).Coroutine();
        }
        /// <summary>
        /// 通过Id拿到Production
        /// </summary>
        /// <param name="self"></param>
        /// <param name="productionProtoId"></param>
        /// <returns></returns>
        public static Production GetProductionById(this ForgeComponent self, long productionProtoId)
        {
            for (int i = 0; i < self.ProductionsList.Count; i++)
            {
                if (self.ProductionsList[i].Id == productionProtoId)
                {
                    return self.ProductionsList[i];
                }
            }
            return null;
        }

        public static bool IsExistMakeQueueOver(this ForgeComponent self)
        {
            bool isCanRecive = false;
            for (int i = 0; i < self.ProductionsList.Count; i++)
            {
                Production production = self.ProductionsList[i];
                if (production.IsMakingState() && production.IsMakeTimeOver())
                {
                    isCanRecive = true;
                    break;
                }
            }
            return isCanRecive;
        }

        public static Production GetProductionByIndex(this ForgeComponent self,int index)
        {
            for (int i = 0; i < self.ProductionsList.Count; i++)
            {
                if (i == index)
                {
                    return self.ProductionsList[index];
                }
            }
            return null ;
        }
        public static int GetMakeingProductionQueueCount(this ForgeComponent self)
        {
            int count = 0;
            for (int i = 0; i < self.ProductionsList.Count; i++)
            {
                Production production = self.ProductionsList[i];
                if (production.ProductionState == (int)ProductionState.Making)
                {
                    ++count;
                }
            }
            return count;
        }
    }
}
