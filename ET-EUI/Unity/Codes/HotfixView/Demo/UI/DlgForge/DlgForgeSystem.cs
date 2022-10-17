using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace ET
{
    [Timer(TimerType.MakeQueueUI)]
    public class MakeQueueUITimer : ATimer<DlgForge>
    {
        public override void Run(DlgForge t)
        {
			t?.RefreshMakeQueue();
		}
    }
    public static  class DlgForgeSystem
	{

		public static void RegisterUIEvent(this DlgForge self)
		{
			self.RegisterCloseEvent<DlgForge>(self.View.E_CloseButton);
			//刷新列表
			self.View.E_ProductionLoopVerticalScrollRect.AddItemRefreshListener(self.OnProductionRefreshHandler);
		}

		public static void ShowWindow(this DlgForge self, Entity contextData = null)
		{
			self.Refresh();
		}
		public static void Refresh(this DlgForge self)
        {
			self.RefreshMakeQueue();
			self.RefreshProduction();
			self.RefreshMaterailCount();
		}
		/// <summary>
		/// 生产队列
		/// </summary>
		/// <param name="self"></param>
		public static void RefreshMakeQueue(this DlgForge self)
		{
			//最多2条打造信息
			Production production = self.ZoneScene().GetComponent<ForgeComponent>().GetProductionByIndex(0);
			self.View.ES_MakeQueueOne.Refresh(production);

			production = self.ZoneScene().GetComponent<ForgeComponent>().GetProductionByIndex(1);
			self.View.ES_MakeQueueTwo.Refresh(production);

			TimerComponent.Instance.Remove(ref self.MakeQueueTimer);
			//正在进行生产的队列个数
			int count = self.ZoneScene().GetComponent<ForgeComponent>().GetMakeingProductionQueueCount();
			if (count > 0)
			{
				//1秒之后启动定时器任务，
				TimerComponent.Instance.NewOnceTimer(TimeHelper.ServerNow() + 1000, TimerType.MakeQueueUI, self);
			}
		}
		/// <summary>
		/// 商品列表
		/// </summary>
		/// <param name="self"></param>
		private static void RefreshProduction(this DlgForge self)
		{
			int unitLevel = UnitHelper.GetMyUnitNumericComponent(self.ZoneScene().CurrentScene()).GetAsInt((int)NumericType.Level);
			//根据等级获得可以生产商品的数量
			int count = ForgeProductionConfigCategory.Instance.GetProductionConfigCount(unitLevel);
			//根据数量开始生成
			self.AddUIScrollItems(ref self.scrollItemProductionsDic, count);
			//显示出来,会触发循环列表的刷新函数
			self.View.E_ProductionLoopVerticalScrollRect.SetVisible(true, count);
		}
		/// <summary>
		/// 材料数量
		/// </summary>
		/// <param name="self"></param>
		private static void RefreshMaterailCount(this DlgForge self)
		{
			NumericComponent numericComponent = UnitHelper.GetMyUnitNumericComponent(self.ZoneScene().CurrentScene());
			int tempIronStone = numericComponent.GetAsInt((int)NumericType.IronStone);
			self.View.E_IronStoneCountText.SetText((tempIronStone >0 ? tempIronStone : 0).ToString());
			int tempFru = numericComponent.GetAsInt((int)NumericType.Fur);
			self.View.E_FurCountText.SetText((tempFru > 0 ?tempFru:0).ToString());
		}
		/// <summary>
		/// 刷新列表
		/// </summary>
		/// <param name="self"></param>
		/// <param name="transform"></param>
		/// <param name="index"></param>
		public static void OnProductionRefreshHandler(this DlgForge self,Transform transform, int index)
		{
			//绑定
			Scroll_Item_production scroll_Item_Production = self.scrollItemProductionsDic[index].BindTrans(transform);
			NumericComponent numericComponent = UnitHelper.GetMyUnitNumericComponent(self.ZoneScene().CurrentScene());
			int unitLevel = numericComponent.GetAsInt((int)NumericType.Level);
			//当前等级刷新的Index下面对应限时商品的配置表
			var config = ForgeProductionConfigCategory.Instance.GetProductionByLevelIndex(unitLevel, index);

			//拿到各个Index的配置表，开始赋值
			scroll_Item_Production.ES_EquipItem.RefreshShowItem(config.ItemConfigId);
			scroll_Item_Production.E_ItemNameText.SetText(ItemConfigCategory.Instance.Get(config.ItemConfigId).Name);
			scroll_Item_Production.E_ConsumeTypeText.SetText(config.ConsumId == (int)NumericType.IronStone ? "精铁" : "皮革");
			scroll_Item_Production.E_ConsumeCountText.SetText(config.ConsumeCount.ToString());
			//制作按钮
			int materialCount = numericComponent.GetAsInt(config.ConsumId);
			scroll_Item_Production.E_MakeButton.interactable = materialCount >= config.ConsumeCount;
			scroll_Item_Production.E_MakeButton.AddListenerAsync(() =>
			{ return self.OnStartProductionHandler(config.Id); });
		}
		/// <summary>
		/// 按钮开始生产
		/// </summary>
		/// <param name="self"></param>
		/// <param name="productionConfigId"></param>
		/// <returns></returns>
		public static async ETTask OnStartProductionHandler(this DlgForge self, int productionConfigId)
		{
            try
            {
				//向服务器发送打造消息
				int errorCode =  await ForgeHelper.StartProduction(self.ZoneScene(), productionConfigId);
				if (errorCode != ErrorCode.ERR_Success)
				{
					Log.Error(errorCode.ToString());
					return;
				}
				//成功之后刷新界面
				self.Refresh();
			}
            catch (Exception e)
            {
				Log.Error(e.ToString());
            }
		}

	}
}
