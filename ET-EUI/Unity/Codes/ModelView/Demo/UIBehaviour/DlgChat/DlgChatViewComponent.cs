
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	public  class DlgChatViewComponent : Entity,IAwake,IDestroy 
	{
		public UnityEngine.UI.Button E_CloseButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_CloseButton == null )
     			{
		    		this.m_E_CloseButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"BackGround/E_Close");
     			}
     			return this.m_E_CloseButton;
     		}
     	}

		public UnityEngine.UI.Image E_CloseImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_CloseImage == null )
     			{
		    		this.m_E_CloseImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"BackGround/E_Close");
     			}
     			return this.m_E_CloseImage;
     		}
     	}

		public UnityEngine.UI.LoopVerticalScrollRect E_ChatLoopVerticalScrollRect
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_ChatLoopVerticalScrollRect == null )
     			{
		    		this.m_E_ChatLoopVerticalScrollRect = UIFindHelper.FindDeepChild<UnityEngine.UI.LoopVerticalScrollRect>(this.uiTransform.gameObject,"ChatBackGround/E_Chat");
     			}
     			return this.m_E_ChatLoopVerticalScrollRect;
     		}
     	}

		public UnityEngine.UI.Text E_ChatText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_ChatText == null )
     			{
		    		this.m_E_ChatText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"ChatBackGround/E_Chat/Content/Item_chat/E_Chat");
     			}
     			return this.m_E_ChatText;
     		}
     	}

		public UnityEngine.UI.Text E_NameText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_NameText == null )
     			{
		    		this.m_E_NameText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"ChatBackGround/E_Chat/Content/Item_chat/E_Name");
     			}
     			return this.m_E_NameText;
     		}
     	}

		public UnityEngine.UI.InputField E_MessageInputField
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_MessageInputField == null )
     			{
		    		this.m_E_MessageInputField = UIFindHelper.FindDeepChild<UnityEngine.UI.InputField>(this.uiTransform.gameObject,"E_Message");
     			}
     			return this.m_E_MessageInputField;
     		}
     	}

		public UnityEngine.UI.Image E_MessageImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_MessageImage == null )
     			{
		    		this.m_E_MessageImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_Message");
     			}
     			return this.m_E_MessageImage;
     		}
     	}

		public UnityEngine.UI.Button E_SendButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_SendButton == null )
     			{
		    		this.m_E_SendButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"E_Send");
     			}
     			return this.m_E_SendButton;
     		}
     	}

		public UnityEngine.UI.Image E_SendImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_SendImage == null )
     			{
		    		this.m_E_SendImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_Send");
     			}
     			return this.m_E_SendImage;
     		}
     	}

		public void DestroyWidget()
		{
			this.m_E_CloseButton = null;
			this.m_E_CloseImage = null;
			this.m_E_ChatLoopVerticalScrollRect = null;
			this.m_E_ChatText = null;
			this.m_E_NameText = null;
			this.m_E_MessageInputField = null;
			this.m_E_MessageImage = null;
			this.m_E_SendButton = null;
			this.m_E_SendImage = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.Button m_E_CloseButton = null;
		private UnityEngine.UI.Image m_E_CloseImage = null;
		private UnityEngine.UI.LoopVerticalScrollRect m_E_ChatLoopVerticalScrollRect = null;
		private UnityEngine.UI.Text m_E_ChatText = null;
		private UnityEngine.UI.Text m_E_NameText = null;
		private UnityEngine.UI.InputField m_E_MessageInputField = null;
		private UnityEngine.UI.Image m_E_MessageImage = null;
		private UnityEngine.UI.Button m_E_SendButton = null;
		private UnityEngine.UI.Image m_E_SendImage = null;
		public Transform uiTransform = null;
	}
}
