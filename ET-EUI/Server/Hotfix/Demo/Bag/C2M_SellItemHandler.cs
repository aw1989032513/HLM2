using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    public class C2M_SellItemHandler : AMActorLocationRpcHandler<Unit, C2M_SellItem, M2C_SellItem>
    {
        protected override async ETTask Run(Unit unit, C2M_SellItem request, M2C_SellItem response, Action reply)
        {
            BagComponent bagComponent = unit.GetComponent<BagComponent>();
            if (!bagComponent.IsItemExist(request.ItemId))
            {
                response.Error = ErrorCode.ERR_ItemNotExist;
                reply();
                return;
            }
            Item bagItem = bagComponent.GetItemById(request.ItemId);
            int addGold = bagItem.config.SellBasePrice;
            bagComponent.RemoveItem(bagItem);
            unit.GetComponent<NumericComponent>()[(int)NumericType.Gold] += addGold;
            reply();
            await ETTask.CompletedTask;
        }
    }
}
