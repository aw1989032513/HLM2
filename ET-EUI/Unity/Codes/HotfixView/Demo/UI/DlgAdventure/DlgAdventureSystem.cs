using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace ET
{
	public static  class DlgAdventureSystem
	{

		public static void RegisterUIEvent(this DlgAdventure self)
		{
			self.RegisterCloseEvent<DlgAdventure>(self.View.E_CloseButton);
            self.View.E_BattleLevelLoopVerticalScrollRect.AddItemRefreshListener((Transform transform, int index) =>
            { self.OnBattleLevelItemRefresh(transform, index); }
            );
        }

		public static void ShowWindow(this DlgAdventure self, Entity contextData = null)
		{
			self.View.EG_ContentRectTransform.DOScale(new Vector3(0.2f, 0.2f, 0.2f), 0.1f);
			self.View.EG_ContentRectTransform.DOScale(Vector3.one, 0.3f).onComplete += () => { self.Refresh(); };
		}
		public static void OnBattleLevelItemRefresh(this DlgAdventure self,Transform transform,int index)
        {
			Scroll_Item_battleLevel scroll_Item_BattleLevel = self.scrollItemBattleLevelsDic[index].BindTrans(transform);
			BattleLevelConfig config = BattleLevelConfigCategory.Instance.GetConfigByIndex(index);

			Unit unit = UnitHelper.GetMyUnitFromCurrentScene(self.ZoneScene().CurrentScene());

			int unitLevel      = unit.GetComponent<NumericComponent>().GetAsInt((int)NumericType.Level);
			bool isInAdventure = unit.GetComponent<NumericComponent>().GetAsInt((int)NumericType.AdventureState) !=0;

			scroll_Item_BattleLevel.E_GoButton.SetVisible(unitLevel >= config.MiniEnterLevel[0] && !isInAdventure);
			scroll_Item_BattleLevel.E_InAdventureTipText.SetVisible(unitLevel >= config.MiniEnterLevel[0] && isInAdventure);
			scroll_Item_BattleLevel.E_LevelNotEnoughText.SetVisible(unitLevel < config.MiniEnterLevel[0]);
			scroll_Item_BattleLevel.E_LevelNameText.SetText($"{config.Name} Lv.{config.MiniEnterLevel[0]}~Lv.{config.MiniEnterLevel[1]}");
			scroll_Item_BattleLevel.E_GoButton.AddListenerAsync(() =>
			{ return self.OnStartGameLevelClickHandler(config.Id); });
		}
		public static async ETTask OnStartGameLevelClickHandler(this DlgAdventure self, int configId)
        {
            try
            {
				int errorCode = await AdventureHelper.RequestStartGameLevel(self.ZoneScene(), configId);
                if (errorCode != ErrorCode.ERR_Success)
                {
					return;
                }
				self.ZoneScene().GetComponent<UIComponent>().HideWindow(WindowID.WindowID_Adventure);
				self.ZoneScene().CurrentScene().GetComponent<AdventureComponent>().StartAdventure().Coroutine();
			}
            catch (Exception e)
            {
				Log.Error(e.ToString());
                throw;
            }
        }
		public static void HideWindow(this DlgAdventure self)
        {
			self.View.E_BattleLevelLoopVerticalScrollRect.SetVisible(false);
			self.RemoveUIScrollItems(ref self.scrollItemBattleLevelsDic);
		}
		public static void Refresh(this DlgAdventure self)
		{
			int count = BattleLevelConfigCategory.Instance.GetAll().Count;
			self.AddUIScrollItems(ref self.scrollItemBattleLevelsDic, count);
			self.View.E_BattleLevelLoopVerticalScrollRect.SetVisible(true, count);
		}

	}
}
