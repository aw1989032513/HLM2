
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	public  class DlgTaskViewComponent : Entity,IAwake,IDestroy 
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

		public UnityEngine.UI.LoopVerticalScrollRect E_TasksLoopVerticalScrollRect
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_TasksLoopVerticalScrollRect == null )
     			{
		    		this.m_E_TasksLoopVerticalScrollRect = UIFindHelper.FindDeepChild<UnityEngine.UI.LoopVerticalScrollRect>(this.uiTransform.gameObject,"E_Tasks");
     			}
     			return this.m_E_TasksLoopVerticalScrollRect;
     		}
     	}

		public UnityEngine.UI.Text E_TaskNameText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_TaskNameText == null )
     			{
		    		this.m_E_TaskNameText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"E_Tasks/Content/Item_task/E_TaskName");
     			}
     			return this.m_E_TaskNameText;
     		}
     	}

		public UnityEngine.UI.Text E_TaskDescText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_TaskDescText == null )
     			{
		    		this.m_E_TaskDescText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"E_Tasks/Content/Item_task/E_TaskDesc");
     			}
     			return this.m_E_TaskDescText;
     		}
     	}

		public UnityEngine.UI.Text E_TaskProgressTipText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_TaskProgressTipText == null )
     			{
		    		this.m_E_TaskProgressTipText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"E_Tasks/Content/Item_task/E_TaskProgressTip");
     			}
     			return this.m_E_TaskProgressTipText;
     		}
     	}

		public UnityEngine.UI.Text E_TaskProgressText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_TaskProgressText == null )
     			{
		    		this.m_E_TaskProgressText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"E_Tasks/Content/Item_task/E_TaskProgress");
     			}
     			return this.m_E_TaskProgressText;
     		}
     	}

		public UnityEngine.UI.Text E_TaskRewardTipText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_TaskRewardTipText == null )
     			{
		    		this.m_E_TaskRewardTipText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"E_Tasks/Content/Item_task/E_TaskRewardTip");
     			}
     			return this.m_E_TaskRewardTipText;
     		}
     	}

		public UnityEngine.UI.Text E_TaskRewardCountText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_TaskRewardCountText == null )
     			{
		    		this.m_E_TaskRewardCountText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"E_Tasks/Content/Item_task/E_TaskRewardCount");
     			}
     			return this.m_E_TaskRewardCountText;
     		}
     	}

		public UnityEngine.UI.Button E_ReceiveButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_ReceiveButton == null )
     			{
		    		this.m_E_ReceiveButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"E_Tasks/Content/Item_task/E_Receive");
     			}
     			return this.m_E_ReceiveButton;
     		}
     	}

		public UnityEngine.UI.Image E_ReceiveImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_ReceiveImage == null )
     			{
		    		this.m_E_ReceiveImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_Tasks/Content/Item_task/E_Receive");
     			}
     			return this.m_E_ReceiveImage;
     		}
     	}

		public UnityEngine.UI.Text E_ReceiveTipText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_ReceiveTipText == null )
     			{
		    		this.m_E_ReceiveTipText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"E_Tasks/Content/Item_task/E_Receive/E_ReceiveTip");
     			}
     			return this.m_E_ReceiveTipText;
     		}
     	}

		public void DestroyWidget()
		{
			this.m_E_CloseButton = null;
			this.m_E_CloseImage = null;
			this.m_E_TasksLoopVerticalScrollRect = null;
			this.m_E_TaskNameText = null;
			this.m_E_TaskDescText = null;
			this.m_E_TaskProgressTipText = null;
			this.m_E_TaskProgressText = null;
			this.m_E_TaskRewardTipText = null;
			this.m_E_TaskRewardCountText = null;
			this.m_E_ReceiveButton = null;
			this.m_E_ReceiveImage = null;
			this.m_E_ReceiveTipText = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.Button m_E_CloseButton = null;
		private UnityEngine.UI.Image m_E_CloseImage = null;
		private UnityEngine.UI.LoopVerticalScrollRect m_E_TasksLoopVerticalScrollRect = null;
		private UnityEngine.UI.Text m_E_TaskNameText = null;
		private UnityEngine.UI.Text m_E_TaskDescText = null;
		private UnityEngine.UI.Text m_E_TaskProgressTipText = null;
		private UnityEngine.UI.Text m_E_TaskProgressText = null;
		private UnityEngine.UI.Text m_E_TaskRewardTipText = null;
		private UnityEngine.UI.Text m_E_TaskRewardCountText = null;
		private UnityEngine.UI.Button m_E_ReceiveButton = null;
		private UnityEngine.UI.Image m_E_ReceiveImage = null;
		private UnityEngine.UI.Text m_E_ReceiveTipText = null;
		public Transform uiTransform = null;
	}
}
