using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    public static class ItemFactory
    {
        public static Item Create(Entity self,ItemInfo iteminfo)
        {
            Item item = self?.AddChild<Item, int>(iteminfo.ItemConfigId);
            //初始化
            item?.DoAwake(iteminfo);
            return item;
        }
    }
}
