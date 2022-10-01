using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace ET
{
    public static class DlgServerSystem
    {

        public static void RegisterUIEvent(this DlgServer self)
        {
            self.View.E_ConfirmButton.AddListenerAsync(() =>
            {
                return self.OnConfirmClickHandler();
            }
            );
            //循环列表
            self.View.E_ServerListLoopVerticalScrollRect.AddItemRefreshListener((Transform transform, int index) =>
            {
                self.OnScrollItemRefreshHandler(transform, index);
            });

        }
        public static void ShowWindow(this DlgServer self, Entity contextData = null)
        {
            int count=self.ZoneScene().GetComponent<ServerInfosCompontent>().ServerInfosList.Count;
            self.AddUIScrollItems(ref self.scrollItemServerTestsList, count);
            self.View.E_ServerListLoopVerticalScrollRect.SetVisible(true, count);
        }
        public static void HideWindow(this DlgServer self)
        {
            self.RemoveUIScrollItems(ref self.scrollItemServerTestsList);
        }
        /// <summary>
        /// 选择服务器
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static async ETTask OnConfirmClickHandler(this DlgServer self)
        {
            bool isSelect = self.ZoneScene().GetComponent<ServerInfosCompontent>().currentServerId != 0;
            if (!isSelect)
            {
                Log.Error("请先选择区服");
                return;
            }

            try
            {
                int errorCode = await LoginHelper.GetRoles(self.ZoneScene());
                if (errorCode != ErrorCode.ERR_Success)
                {
                    Log.Error(errorCode.ToString());
                    return;
                }

                self.ZoneScene().GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_Roles);
                self.ZoneScene().GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_Server);

            }
            catch (Exception e)
            {
                Log.Error(e.ToString());
               
            }
        }
        /// <summary>
        /// 循环列表刷新
        /// </summary>
        /// <param name="self"></param>
        /// <param name="transform">热更新用，不然会报错</param>
        /// <param name="index"></param>
        public static void OnScrollItemRefreshHandler(this DlgServer self, Transform transform, int index)
        {
            Scroll_Item_serverTest serverTest=self.scrollItemServerTestsList[index].BindTrans(transform);
            ServerInfo info =self.ZoneScene().GetComponent<ServerInfosCompontent>().ServerInfosList[index];
            serverTest.E_SelectImage.color = info.Id == self.ZoneScene().GetComponent<ServerInfosCompontent>().currentServerId ? Color.red : Color.yellow;
            serverTest.E_serverTestTipText.SetText(info.serverName);
            serverTest.E_SelectButton.AddListener(() => { self.OnSelectServerItemHandler(info.Id); });
        }
       /// <summary>
       /// 选中服务器
       /// </summary>
       /// <param name="self"></param>
       /// <param name="serverId"></param>
        public static void OnSelectServerItemHandler(this DlgServer self, long serverId)
        {
            self.ZoneScene().GetComponent<ServerInfosCompontent>().currentServerId = int.Parse(serverId.ToString());
            Log.Debug($"当前选择的服务器ID是:{serverId}");
            self.View.E_ServerListLoopVerticalScrollRect.RefillCells();
        }
    }
}
