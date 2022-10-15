using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    public class C2M_UnloadEquipItemHandler : AMActorLocationRpcHandler<Unit, C2M_UnloadEquipItem, M2C_UnloadEquipItem>
    {
        protected override async ETTask Run(Unit unit, C2M_UnloadEquipItem request, M2C_UnloadEquipItem response, Action reply)
        {
            BagComponent bagComponent = unit.GetComponent<BagComponent>();
            EquipmentsComponent equipmentsComponent = unit.GetComponent<EquipmentsComponent>();
            if (bagComponent.IsMaxLoad())
            {
                response.Error = ErrorCode.ERR_BagMaxLoad;
                reply();
                return;
            }
            if (!equipmentsComponent.IsEquipItemByPosition((EquipPosition)request.EquipPosition))
            {
                response.Error = ErrorCode.ERR_UnLoadItemNotExist;
                reply();
                return;
            }

            //从字典中拿出来相应位置的装备
            Item oldEquipItem = equipmentsComponent.GetEquipItemByPosition((EquipPosition)request.EquipPosition);
            if (!bagComponent.IsCanAddItem(oldEquipItem))
            {
                response.Error = ErrorCode.ERR_AddBagItemError;
                reply();
                return;
            }
            ///卸下装备
            oldEquipItem = equipmentsComponent.UnloadEquipItemByPosition((EquipPosition)request.EquipPosition);
            bagComponent.AddItem(oldEquipItem);

            reply();

            await ETTask.CompletedTask;
        }
    }
}
