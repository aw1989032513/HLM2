using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    /// <summary>
    /// 发送消息给服务器
    /// </summary>
    public static class ItemApplyHelper
    {
        public static async Task<int> SellBagItem(Scene ZoneScene,long itemId)
        {
            Item item = ItemHelper.GetItem(ZoneScene, itemId, ItemContainerType.Bag);
            if (item == null)
            {
                return ErrorCode.ERR_ItemNotExist;
            }
            M2C_SellItem m2C_SellItem = null;
            try
            {
                m2C_SellItem = (M2C_SellItem)await ZoneScene.GetComponent<SessionComponent>().Session.Call(new C2M_SellItem() { ItemId = itemId });
            }
            catch (Exception e)
            {
                Log.Error(e.ToString());
                return ErrorCode.ERR_NetWorkError;
            }
            return m2C_SellItem.Error;
        }
        public static async ETTask<int> EquipItem(Scene ZoneScene, long itemId)
        {
            Item item = ItemHelper.GetItem(ZoneScene, itemId, ItemContainerType.Bag);

            if (item == null)
            {
                return ErrorCode.ERR_ItemNotExist;
            }

            M2C_EquipItem m2CEquipItem = null;

            try
            {
                m2CEquipItem = (M2C_EquipItem)await ZoneScene.GetComponent<SessionComponent>().Session.Call(new C2M_EquipItem() { ItemId = itemId });
            }
            catch (Exception e)
            {
                Log.Error(e.ToString());
                return ErrorCode.ERR_NetWorkError;
            }

            return m2CEquipItem.Error;
        }
        /// <summary>
        /// 卸载装备
        /// </summary>
        /// <param name="ZoneScene"></param>
        /// <param name="itemId"></param>
        /// <returns></returns>
        public static async ETTask<int> UnloadEquipItem(Scene ZoneScene , long itemId)
        {
            Item item = ItemHelper.GetItem(ZoneScene, itemId, ItemContainerType.RoleInfo);

            if (item == null)
            {
                return ErrorCode.ERR_ItemNotExist;
            }
            M2C_UnloadEquipItem m2C_UnloadEquipItem = null;
            try
            {
                m2C_UnloadEquipItem = (M2C_UnloadEquipItem)await ZoneScene.GetComponent<SessionComponent>().Session.Call(new C2M_UnloadEquipItem() { EquipPosition = item.config.EquipPosition });
            }
            catch (Exception e)
            {
                Log.Error(e.ToString());
                return ErrorCode.ERR_NetWorkError;
            }

            return m2C_UnloadEquipItem.Error;
        }
    }
}
