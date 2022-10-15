using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace ET
{
	public static  class DlgItemPopUpSystem
	{

		public static void RegisterUIEvent(this DlgItemPopUp self)
		{
			self.RegisterCloseEvent<DlgItemPopUp>(self.View.E_CloseButton);
			///刷新
			self.View.E_EntrysLoopVerticalScrollRect.AddItemRefreshListener(self.OnEntryLoopHandler);
			//装上装备
			self.View.E_EquipButton.AddListenerAsync(self.OnEquipItemHandler);
			//卸下装备
			self.View.E_UnEquipButton.AddListenerAsync(self.OnUnloadEquipItemHandler);
			self.View.E_SellButton.AddListenerAsync(self.OnSellItemHandler);
		}

		public static void ShowWindow(this DlgItemPopUp self, Entity contextData = null)
		{
		}
		public static void HideWindow(this DlgItemPopUp self)
		{
			self.RemoveUIScrollItems(ref self.ScrollItemEntries);
		}
		/// <summary>
		/// 刷新信息
		/// </summary>
		/// <param name="self"></param>
		/// <param name="item"></param>
		/// <param name="itemContainerType"></param>
		public static void RefreshInfo(this DlgItemPopUp self, Item item, ItemContainerType itemContainerType)
		{
			self.ItemId = item.Id;
			self.ItemContainerType = itemContainerType;

			self.View.E_IconImage.overrideSprite = IconHelper.LoadIconSprite("Icons", item.config.Icon);
			self.View.E_QualityImage.color = item.ItemQualityColor();
			self.View.E_NameText.text = item.config.Name;
			self.View.E_DescText.text = item.config.Desc;
			self.View.E_PriceText.text = item.config.SellBasePrice.ToString();

			if (item.config.Type == (int)ItemType.Prop)//道具的话
			{
				self.View.E_EquipButton.SetVisible(false);
				self.View.E_UnEquipButton.SetVisible(false);
				self.View.E_SellButton.SetVisible(false);
				self.View.E_EntrysLoopVerticalScrollRect.SetVisible(true, 0);
				self.View.E_SellButton.SetVisible(itemContainerType == ItemContainerType.Bag);
			}
			else
			{
                self.View.E_ScoreText.text = item.GetComponent<EquipInfoComponent>().score.ToString();
                int count = item.GetComponent<EquipInfoComponent>().entryList.Count;
                self.AddUIScrollItems(ref self.ScrollItemEntries, count);
                self.View.E_EntrysLoopVerticalScrollRect.SetVisible(true, count);

                self.View.E_EquipButton.SetVisible(itemContainerType == ItemContainerType.Bag);
				self.View.E_UnEquipButton.SetVisible(itemContainerType == ItemContainerType.RoleInfo);
				self.View.E_SellButton.SetVisible(itemContainerType == ItemContainerType.Bag);
			}
		}

		public static void OnEntryLoopHandler(this DlgItemPopUp self, Transform transform, int index)
        {
			Scroll_Item_entry scrollItemEntry = self.ScrollItemEntries[index].BindTrans(transform);
			Item item = ItemHelper.GetItem(self.ZoneScene(), self.ItemId, self.ItemContainerType);
            AttributeEntry entry = item.GetComponent<EquipInfoComponent>().entryList[index];
            scrollItemEntry.E_EntryNameText.text = PlayerNumericConfigCategory.Instance.Get(entry.Key).Name + ":";
            bool isPrcent = PlayerNumericConfigCategory.Instance.Get(entry.Key).isPrecent > 0;
            scrollItemEntry.E_EntryValueText.text = "+" + (isPrcent ? $"{(entry.Value / 10000.0f).ToString("0.00")}%" : entry.Value.ToString());
        }
		public static async ETTask OnEquipItemHandler(this DlgItemPopUp self)
        {
            try
            {
				int errorCode = await ItemApplyHelper.EquipItem(self.ZoneScene(), self.ItemId);

				if (errorCode != ErrorCode.ERR_Success)
				{
					Log.Error(errorCode.ToString());
					return;
				}
				self.ZoneScene().GetComponent<UIComponent>().HideWindow(WindowID.WindowID_ItemPopUp);
				self.ZoneScene().GetComponent<UIComponent>().GetDlgLogic<DlgBag>().Refresh();
			}
            catch (Exception e)
            {
				Log.Error(e.ToString());
            }


		}
		/// <summary>
		/// 卸下装备
		/// </summary>
		/// <param name="self"></param>
		/// <returns></returns>
		public static async ETTask OnUnloadEquipItemHandler(this DlgItemPopUp self)
		{
            try
            {
				int errorCode = await ItemApplyHelper.UnloadEquipItem(self.ZoneScene(), self.ItemId);
				if (errorCode != ErrorCode.ERR_Success)
				{
					Log.Error(errorCode.ToString());
					return;
				}
				//成功之后
				self.ZoneScene().GetComponent<UIComponent>().HideWindow(WindowID.WindowID_ItemPopUp);
				//刷新人物属性UI
				self.ZoneScene().GetComponent<UIComponent>().GetDlgLogic<DlgRoleInfo>().RefreshEquipShowItems();
				self.ZoneScene().GetComponent<UIComponent>().GetDlgLogic<DlgRoleInfo>().Refresh();
			}
			catch (Exception e)
			{
				Log.Error(e.ToString());
			}
			await ETTask.CompletedTask;
		}
		public static async ETTask OnSellItemHandler(this DlgItemPopUp self)
        {
            try
            {
				int errorCode = await ItemApplyHelper.SellBagItem(self.ZoneScene(), self.ItemId);
				if (errorCode != ErrorCode.ERR_Success)
				{
					Log.Error(errorCode.ToString());
					return;
				}
				self.ZoneScene().GetComponent<UIComponent>().HideWindow(WindowID.WindowID_ItemPopUp);
				self.ZoneScene().GetComponent<UIComponent>().GetDlgLogic<DlgBag>().Refresh();
			}
            catch (Exception e)
            {
				Log.Error(e.ToString());
			}
			await ETTask.CompletedTask;
		}
	}
}
