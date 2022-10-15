﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    public static class Scroll_Item_bagItemSystem
    {
        public static void Refresh(this Scroll_Item_bagItem self, long id)
        {
            Item item = self.ZoneScene().GetComponent<BagComponent>().GetItemById(id);
            self.E_IconImage.overrideSprite = IconHelper.LoadIconSprite("Icons", item.config.Icon);
            self.E_QualityImage.color = item.ItemQualityColor();
            self.E_SelectButton.AddListenerWithId(self.OnShowItemEntryPopUpHandler, id);

        }
        /// <summary>
        /// 点击装备
        /// </summary>
        /// <param name="self"></param>
        /// <param name="Id"></param>
        public static void OnShowItemEntryPopUpHandler(this Scroll_Item_bagItem self, long Id)
        {
            self.ZoneScene().GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_ItemPopUp);
            Item item = self.ZoneScene().GetComponent<BagComponent>().GetItemById(Id);
            self.ZoneScene().GetComponent<UIComponent>().GetDlgLogic<DlgItemPopUp>().RefreshInfo(item, ItemContainerType.Bag);
        }
    }
}
