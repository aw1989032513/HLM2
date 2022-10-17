using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace ET
{
    public static class DlgMainSystem
    {

        public static void RegisterUIEvent(this DlgMain self)
        {
            self.View.E_RoleButton.AddListenerAsync(() => { return self.OnRoleBtnClickHandler(); });
            self.View.E_BattleButton.AddListener(self.OnBattleButtonClickHandler);
            self.View.E_BagButton.AddListener(self.OnBagButtonClickHandler);
            self.View.E_MakeButton.AddListener(self.OnMakeButtonClickHandler);
            self.View.E_TaskButton.AddListener(self.OnTaskButtonClickHandler);
            RedDotHelper.AddRedDotNodeView(self.ZoneScene(), "Role", self.View.E_RoleButton.gameObject, Vector3.one, new Vector3(75, 55, 0));
            RedDotHelper.AddRedDotNodeView(self.ZoneScene(), "Forge", self.View.E_MakeButton.gameObject, Vector3.one, new Vector3(75, 55, 0));
            RedDotHelper.AddRedDotNodeView(self.ZoneScene(), "Task", self.View.E_TaskButton.gameObject, Vector3.one, new Vector3(75, 55, 0));
        }
        /// <summary>
        /// 任务按钮
        /// </summary>
        /// <param name="self"></param>
        public static void OnTaskButtonClickHandler(this DlgMain self)
        {
            self.ZoneScene().GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_Task);
        }
        public static void OnMakeButtonClickHandler(this DlgMain self)
        {
            self.ZoneScene().GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_Forge);
        }
        public static void OnBattleButtonClickHandler(this DlgMain self)
        {
            self.ZoneScene().GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_Adventure);
        }

        public static void OnUnLoadWindow(this DlgMain self)
        {
            RedDotMonoView redDotMonoView = self.View.E_RoleButton.GetComponent<RedDotMonoView>();
            RedDotHelper.RemoveRedDotView(self.ZoneScene(), "Role", out redDotMonoView);
        }
        public static void ShowWindow(this DlgMain self, Entity contextData = null)
        {
            self.Refresh().Coroutine();
        }
        public static async ETTask Refresh(this DlgMain self)
        {
            Unit unit = UnitHelper.GetMyUnitFromCurrentScene(self.ZoneScene().CurrentScene());
            //SceneChangeHelp 的UnityFactory.Create  已经拿到了NumericComponent
            NumericComponent numericComponent = unit.GetComponent<NumericComponent>();

            self.View.E_RoleLevelText.SetText($"Lv.{numericComponent.GetAsInt((int)NumericType.Level)}");
            self.View.E_GoldText.SetText(numericComponent.GetAsInt((int)NumericType.Gold).ToString());
            self.View.E_ExpText.SetText(numericComponent.GetAsInt((int)NumericType.Exp).ToString());
            await ETTask.CompletedTask;
        }
        public static async ETTask OnRoleBtnClickHandler(this DlgMain self)
        {
            try
            {
                await self.ZoneScene().GetComponent<UIComponent>().ShowWindowAsync(WindowID.WindowID_RoleInfo);
            }
            catch (Exception e)
            {
                Log.Error(e.ToString());
            }
        }
        public static void OnBagButtonClickHandler(this DlgMain self)
        {
            self.ZoneScene().GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_Bag);
        }

    }
}
