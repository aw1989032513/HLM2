using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    /// <summary>
    /// 装备的穿戴和卸下
    /// </summary>
    public class M2C_ItemUpdateOpInfoHandler : AMHandler<M2C_ItemUpdateOpInfo>
    {
        protected override async ETTask Run(Session session, M2C_ItemUpdateOpInfo message)
        {
            if (message.Op == (int)ItemOp.Add)
            {
                Item item = ItemFactory.Create(session.ZoneScene(), message.ItemInfo);
                ItemHelper.AddItem(session.ZoneScene(), item, (ItemContainerType)message.ContainerType);
            }
            else if (message.Op == (int)ItemOp.Remove)
            {
                ItemHelper.RemoveItemById(session.ZoneScene(), message.ItemInfo.ItemId, (ItemContainerType)message.ContainerType);
            }
            await ETTask.CompletedTask;
        }
    }
}
