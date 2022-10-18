using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace ET
{
    [Timer(TimerType.RankUI)]
    public class RankUITimer : ATimer<DlgRank>
    {
        public override void Run(DlgRank t)
        {
			t?.RefreshRankInfo().Coroutine();
		}
    }
    public static  class DlgRankSystem
	{

		public static void RegisterUIEvent(this DlgRank self)
		{
			self.RegisterCloseEvent<DlgRank>(self.View.E_CloseButton);
			self.View.E_RankLoopVerticalScrollRect.AddItemRefreshListener(self.OnRankItemLoopHandler);
		}

		public static void ShowWindow(this DlgRank self, Entity contextData = null)
		{
			self.RefreshRankInfo().Coroutine();
			self.Timer = TimerComponent.Instance.NewRepeatedTimer(5000, TimerType.RankUI, self);
		}
		public static void RefreshCount(this DlgRank self)
        {
			int count = self.ZoneScene().GetComponent<RankComponent>().GetRankCount();
			self.AddUIScrollItems(ref self.ScrollItemRanks, count);
			self.View.E_RankLoopVerticalScrollRect.SetVisible(true, count);
		}
		public static async ETTask RefreshRankInfo(this DlgRank self)
        {
            try
            {
				Scene zoneScene = self.ZoneScene();
				int errorCode = await RankHelper.GetRankInfo(zoneScene);
                if (errorCode != ErrorCode.ERR_Success)
                {
					Log.Error(errorCode.ToString());
					return;
                }
				//刷新界面
				if (!zoneScene.GetComponent<UIComponent>().IsWindowVisible(WindowID.WindowID_Rank))
				{
					return;
				}
				self.RefreshCount();
			}
            catch (Exception e)
            {
				Log.Error(e.ToString());
            }
        }
		/// <summary>
		/// 生成预制体
		/// </summary>
		/// <param name="arg1"></param>
		/// <param name="arg2"></param>
		/// <exception cref="NotImplementedException"></exception>
		public static void OnRankItemLoopHandler(this DlgRank self,Transform transform, int index)
		{
			Scroll_Item_rank scroll_Item_Rank = self.ScrollItemRanks[index].BindTrans(transform);
			RankInfo rankInfo = self.ZoneScene().GetComponent<RankComponent>().GetRankInfoByIndex(index);

			int order = index + 1;
			scroll_Item_Rank.E_RankOrderText.SetText($"第{order}名");
			scroll_Item_Rank.E_NameText.SetText(rankInfo.name);
			scroll_Item_Rank.E_LevelText.SetText("Lv." + rankInfo.count.ToString());
		}
		public static void HideWindow(this DlgRank self, Entity contextData = null)
		{
			TimerComponent.Instance.Remove(ref self.Timer);
		}
	}
}
