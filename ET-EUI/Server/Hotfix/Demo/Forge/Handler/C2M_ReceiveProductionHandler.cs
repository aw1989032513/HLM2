using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    public class C2M_ReceiveProductionHandler : AMActorLocationRpcHandler<Unit, C2M_ReceiveProduction, M2C_ReceiveProduction>
    {
        protected override async ETTask Run(Unit unit, C2M_ReceiveProduction request, M2C_ReceiveProduction response, Action reply)
        {
            ForgeComponent forgeComponent = unit.GetComponent<ForgeComponent>();
            BagComponent bagComponent = unit.GetComponent<BagComponent>();
            //判断背包能装下不
            if (bagComponent.IsMaxLoad())
            {
                response.Error = ErrorCode.ERR_BagMaxLoad;
                reply();
                return;
            }
            //还在生产中
            if (!forgeComponent.IsExistProductionOverQueue(request.ProductionId))
            {
                response.Error = ErrorCode.ERR_NoMakeQueueOver;
                reply();
                return;
            }

            Production production = forgeComponent.GetProductionById(request.ProductionId);
            if (production == null)
            {
                Log.Error("production is null");
            }

            //拿到配置表
            var config = ForgeProductionConfigCategory.Instance.Get(production.ConfigId);
            //放到背包中
            if (!BagHelper.AddItemByConfigId(unit, config.ItemConfigId))
            {
                response.Error = ErrorCode.ERR_AddBagItemError;
                reply();
                return;
            }
            Game.EventSystem.PublishAsync(new EventType.MakeProdutionOver() { unit = unit, productionConfigId = production.ConfigId }).Coroutine();
            production.Reset();

            response.ProductionProto = production.ToMessage();
            reply();

            await ETTask.CompletedTask;
        }
    }
}
