﻿using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace ET
{
	public static  class DlgChatSystem
	{

		public static void RegisterUIEvent(this DlgChat self)
		{
			self.RegisterCloseEvent<DlgChat>(self.View.E_CloseButton);
			self.View.E_SendButton.AddListenerAsync(self.OnSendMessageClickHandler);
			self.View.E_ChatLoopVerticalScrollRect.AddItemRefreshListener(self.OnChatItemLoopHandler);
		}

		public static void ShowWindow(this DlgChat self, Entity contextData = null)
		{
			self.Refresh();
		}
		public static void HideWindow(this DlgChat self)
		{
			self.RemoveUIScrollItems(ref self.ScrollItemChats);
		}
		public static void OnChatItemLoopHandler(this DlgChat self, Transform transform, int index)
		{ 
			Scroll_Item_chat scroll_Item_Chat = self.ScrollItemChats[index].BindTrans(transform);
			ChatInfo chatInfo = self.ZoneScene().GetComponent<ChatComponent>().GetChatMessageByIndex(index);

			scroll_Item_Chat.E_NameText.SetText(chatInfo.name + ":");
			scroll_Item_Chat.E_ChatText.SetText(chatInfo.message);
		}
		public static async ETTask OnSendMessageClickHandler(this DlgChat self)
        {
            try
            {
				int errorCode = await ChatHelper.SendMessage(self.ZoneScene(), self.View.E_MessageInputField.text);
				if (errorCode != ErrorCode.ERR_Success)
				{
                    if (errorCode == ErrorCode.ERR_ChatMessageEmpty)
                    {
						self.View.E_MessageInputField.text = "空消息";
						return;
					}
					Log.Error(errorCode.ToString());
					return;
				}

				self.Refresh();
			}
            catch (Exception e)
            {
				Log.Error(e.ToString());
			}
        }
		public static void Refresh(this DlgChat self)
        {
			//数量
			int count = self.ZoneScene().GetComponent<ChatComponent>().GetChatMessageCount();
			self.AddUIScrollItems(ref self.ScrollItemChats, count);
			self.View.E_ChatLoopVerticalScrollRect.SetVisible(true, count);
			self.View.E_ChatLoopVerticalScrollRect.RefillCellsFromEnd();
		}

	}
}
