using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    public class C2M_EquipItemHandler : AMActorLocationRpcHandler<Unit, C2M_EquipItem, M2C_EquipItem>
    {
        protected override async ETTask Run(Unit unit, C2M_EquipItem request, M2C_EquipItem response, Action reply)
        {
            BagComponent bagComponent = unit.GetComponent<BagComponent>();
            EquipmentsComponent equipmentsComponent = unit.GetComponent<EquipmentsComponent>();
            if (!bagComponent.IsItemExist(request.ItemId))
            {
                response.Error = ErrorCode.ERR_ItemNotExist;
                reply();
                return;
            }
            Item bagItem = bagComponent.GetItemById(request.ItemId);
            var equipPosition = (EquipPosition)bagItem.config.EquipPosition;
            //从背包中移除，但是不释放
            bagItem = bagComponent.RemoveItemNoDispose(bagItem);
            //装备栏物品
            Item oldEquipItem = equipmentsComponent.GetEquipItemByPosition(equipPosition);
            if (oldEquipItem != null)//说明装备栏有物品了
            {
                //判断装备栏之前的物品能放到背包中么
                if (!bagComponent.IsCanAddItem(oldEquipItem))
                {
                    //刚才从背包中移除，但是不释放的bagItem放回来
                    bagComponent.AddChild(bagItem);
                    response.Error = ErrorCode.ERR_AddBagItemError;
                    reply();
                    return;
                }
                else
                {
                    //卸下来，放到背包中
                    oldEquipItem = equipmentsComponent.UnloadEquipItemByPosition(equipPosition);
                    bagComponent.AddItem(oldEquipItem);
                }
            }
            //装备物体
            if (!equipmentsComponent.EquipItem(bagItem))
            {
                response.Error = ErrorCode.ERR_EquipItemError;
                reply();
                return;
            }

            reply();
            await ETTask.CompletedTask;
        }
    }
}
