using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace ET
{
	public static  class DlgRolesSystem
	{

		public static void RegisterUIEvent(this DlgRoles self)
		{
			self.View.E_ConfirmButton.AddListenerAsync(() => { return self.OnConfirmClickHandler(); });
            self.View.E_CreateRoleButton.AddListenerAsync(() => { return self.OnCreateRoleClickHandler(); });
			self.View.E_DeleteRoleButton.AddListenerAsync(() => { return self.OnDeleteRoleClickHandler(); });
			self.View.E_RolesLoopVerticalScrollRect.AddItemRefreshListener((Transform transform, int index) => { self.OnRoleListRefreshHandler(transform, index); });
		}

		public static void ShowWindow(this DlgRoles self, Entity contextData = null)
		{
			Log.Debug($"当前窗口类型{WindowID.WindowID_Roles}");
			self.RefreshRoleItems();
		}
		public static void OnRoleListRefreshHandler(this DlgRoles self, Transform transform, int index)
		{
			Scroll_Item_role scrollItemRole = self.ScrollItemRolesDic[index].BindTrans(transform);
			RoleInfo info = self.ZoneScene().GetComponent<RoleInfosComponent>().roleInfosList[index];

			scrollItemRole.E_RoleImage.color = info.Id == self.ZoneScene().GetComponent<RoleInfosComponent>().currentRoleId ? Color.green : Color.gray;

			scrollItemRole.E_RoleNameText.SetText(info.Name);
			scrollItemRole.E_RoleButton.AddListener(() => { self.OnRoleItemClickHandler(info.Id); });
		}
		public static void OnRoleItemClickHandler(this DlgRoles self, long roleId)
		{
			self.ZoneScene().GetComponent<RoleInfosComponent>().currentRoleId = roleId;
			self.View.E_RolesLoopVerticalScrollRect.RefillCells();
		}
		public static async ETTask OnCreateRoleClickHandler(this DlgRoles self)
		{
			string name = self.View.E_RoleNameInputField.text;

			if (string.IsNullOrEmpty(name))
			{
				Log.Error("Name is null");
				return;
			}
            try
            {
				int errorCode = await LoginHelper.CreateRoles(self.ZoneScene(), name);
				if (errorCode != ErrorCode.ERR_Success)
                {
					Log.Error(errorCode.ToString());
					return;
                }
				self.RefreshRoleItems();
			}
            catch (Exception e)
            {
				Log.Error(e.ToString());
			}
		}
		public static void RefreshRoleItems(this DlgRoles self)
        {
			int count = self.ZoneScene().GetComponent<RoleInfosComponent>().roleInfosList.Count;
			self.AddUIScrollItems(ref self.ScrollItemRolesDic, count);
			self.View.E_RolesLoopVerticalScrollRect.SetVisible(true, count);
        }
		/// <summary>
		/// 进入游戏
		/// </summary>
		/// <param name="self"></param>
		/// <returns></returns>
		public static async ETTask OnConfirmClickHandler(this DlgRoles self)
        {
		

			if (self.ZoneScene().GetComponent<RoleInfosComponent>().currentRoleId == 0)
            {
				Log.Error("请选择需要进入的角色");
				return;
			}
            try
            {
				int errorCode = await LoginHelper.GetRealmKey(self.ZoneScene());
                if (errorCode != ErrorCode.ERR_Success)
                {
					Log.Error(errorCode.ToString());
					return;
                }

				errorCode = await LoginHelper.EnterGame(self.ZoneScene());
                if (errorCode != ErrorCode.ERR_Success)
                {
					Log.Error(errorCode.ToString());
					return;
                }
				self?.ZoneScene()?.GetComponent<UIComponent>()?.CloseWindow(WindowID.WindowID_Roles);
			}
            catch (Exception e)
            {
				Log.Error(e.ToString());
            }
			await ETTask.CompletedTask;
        }

		public static async ETTask OnDeleteRoleClickHandler(this DlgRoles self)
		{
            if (self.ZoneScene().GetComponent<RoleInfosComponent>().currentRoleId == 0)
            {
				Log.Error("请先选择要删除的角色");
				return;
            }
            try
            {
				int errorCode = await LoginHelper.DeleteRole(self.ZoneScene());
				if (errorCode != ErrorCode.ERR_Success)
				{
					Log.Error(errorCode.ToString());
					return;
				}
				self.RefreshRoleItems();
			}
            catch (Exception e)
            {
				Log.Error(e.ToString());
            }
			await ETTask.CompletedTask;
		}

	}
}
