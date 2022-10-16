using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    public static class ES_MakeQueueSystem
    {
        public static void  Refresh(this ES_MakeQueue self,Production production)
        {
            if (production == null || !production.IsMakingState())
            {
                self.uiTransform.SetVisible(false);
                return;
            }
            self.uiTransform.SetVisible(true);

            int itemConfigId = ForgeProductionConfigCategory.Instance.Get(production.ConfigId).ItemConfigId;
            //设置图标
            self.ES_EquipItem.RefreshShowItem(itemConfigId);

            bool isCanReceive = production.IsMakeTimeOver() && production.IsMakingState();
            //制作时间需要多久
            self.E_MakeTimeText.SetText(production.GetRemainingTimeStr());
            self.E_LeaftTimeSlider.value = production.GetRemainTimeValue();

            self.E_LeaftTimeSlider.SetVisible(!isCanReceive);
            self.E_MakeTimeText.SetVisible(!isCanReceive);
            self.E_MakeTipText.SetVisible (!isCanReceive);
            self.E_MakeOverTipText.SetVisible(isCanReceive);
            self.E_ReceiveButton.SetVisible(isCanReceive);
            //button
            self.E_ReceiveButton.AddListenerAsync(() =>
            { return self.OnReceiveButtonHandler(production.Id); });
        }

        public static async ETTask OnReceiveButtonHandler(this ES_MakeQueue self,long productionId)
        {
            //收集按钮
            try
            {
                int errorCode = await ForgeHelper.ReceivedProductionItem(self.ZoneScene(), productionId);
                if (errorCode != ErrorCode.ERR_Success)
                {
                    Log.Error(errorCode.ToString());
                    return;
                }
                //刷新生产队列
                self.ZoneScene().GetComponent<UIComponent>().GetDlgLogic<DlgForge>().RefreshMakeQueue();
            }
            catch (Exception e)
            {
                Log.Error(e.ToString());
            }
        }
    }
}
