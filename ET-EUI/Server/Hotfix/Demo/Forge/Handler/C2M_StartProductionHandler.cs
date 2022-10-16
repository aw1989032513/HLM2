using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    public class C2M_StartProductionHandler : AMActorLocationRpcHandler<Unit, C2M_StartProduction, M2C_StartProduction>
    {
        protected override async ETTask Run(Unit unit, C2M_StartProduction request, M2C_StartProduction response, Action reply)
        {
            if (! ForgeProductionConfigCategory.Instance.Contain(request.ConfigId))
            {
                response.Error = ErrorCode.ERR_ProductionConfigIdNotExit;
                reply();
                return;
            }

            ForgeComponent forgeComponent = unit.GetComponent<ForgeComponent>();
            if (forgeComponent == null)
            { Log.Error("Unit 身上的forgeComponent is null"); }

            //是否有空闲的制造队列
            if (!forgeComponent.IsExistFreeQueue())
            {
                response.Error = ErrorCode.ERR_NoMakeFreeQueue;
                reply();
                return;
            }
            //制造材料是否充足
            var config = ForgeProductionConfigCategory.Instance.Get(request.ConfigId);
            int materialCount = unit.GetComponent<NumericComponent>().GetAsInt(config.ConsumId);
            if (materialCount < config.ConsumeCount)
            {
                response.Error = ErrorCode.ERR_MaterailCountLess;
                reply();
                return;
            }
            NumericType temp = (NumericType)config.ConsumId;
            //消耗材料
            unit.GetComponent<NumericComponent>()[config.ConsumId] -= config.ConsumId;
            //开始成产
            Production production = forgeComponent.StartProduction(request.ConfigId);
            //开始装
            response.ProductionProto = production.ToMessage();

            reply();
            await ETTask.CompletedTask;
        }
    }
}
