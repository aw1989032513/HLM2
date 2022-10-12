
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	public  class DlgAdventureViewComponent : Entity,IAwake,IDestroy 
	{
		public UnityEngine.RectTransform EG_ContentRectTransform
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EG_ContentRectTransform == null )
     			{
		    		this.m_EG_ContentRectTransform = UIFindHelper.FindDeepChild<UnityEngine.RectTransform>(this.uiTransform.gameObject,"EG_Content");
     			}
     			return this.m_EG_ContentRectTransform;
     		}
     	}

		public UnityEngine.UI.LoopVerticalScrollRect E_BattleLevelLoopVerticalScrollRect
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_BattleLevelLoopVerticalScrollRect == null )
     			{
		    		this.m_E_BattleLevelLoopVerticalScrollRect = UIFindHelper.FindDeepChild<UnityEngine.UI.LoopVerticalScrollRect>(this.uiTransform.gameObject,"EG_Content/BackGround/E_BattleLevel");
     			}
     			return this.m_E_BattleLevelLoopVerticalScrollRect;
     		}
     	}

		public UnityEngine.UI.Text E_LevelNameText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_LevelNameText == null )
     			{
		    		this.m_E_LevelNameText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"EG_Content/BackGround/E_BattleLevel/Content/Item_battleLevel/E_LevelName");
     			}
     			return this.m_E_LevelNameText;
     		}
     	}

		public UnityEngine.UI.Button E_GoButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_GoButton == null )
     			{
		    		this.m_E_GoButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"EG_Content/BackGround/E_BattleLevel/Content/Item_battleLevel/E_Go");
     			}
     			return this.m_E_GoButton;
     		}
     	}

		public UnityEngine.UI.Image E_GoImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_GoImage == null )
     			{
		    		this.m_E_GoImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"EG_Content/BackGround/E_BattleLevel/Content/Item_battleLevel/E_Go");
     			}
     			return this.m_E_GoImage;
     		}
     	}

		public UnityEngine.UI.Text E_GoTextText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_GoTextText == null )
     			{
		    		this.m_E_GoTextText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"EG_Content/BackGround/E_BattleLevel/Content/Item_battleLevel/E_Go/E_GoText");
     			}
     			return this.m_E_GoTextText;
     		}
     	}

		public UnityEngine.UI.Text E_LevelNotEnoughText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_LevelNotEnoughText == null )
     			{
		    		this.m_E_LevelNotEnoughText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"EG_Content/BackGround/E_BattleLevel/Content/Item_battleLevel/E_LevelNotEnough");
     			}
     			return this.m_E_LevelNotEnoughText;
     		}
     	}

		public UnityEngine.UI.Text E_InAdventureTipText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_InAdventureTipText == null )
     			{
		    		this.m_E_InAdventureTipText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"EG_Content/BackGround/E_BattleLevel/Content/Item_battleLevel/E_InAdventureTip");
     			}
     			return this.m_E_InAdventureTipText;
     		}
     	}

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
		    		this.m_E_CloseButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"EG_Content/E_Close");
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
		    		this.m_E_CloseImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"EG_Content/E_Close");
     			}
     			return this.m_E_CloseImage;
     		}
     	}

		public void DestroyWidget()
		{
			this.m_EG_ContentRectTransform = null;
			this.m_E_BattleLevelLoopVerticalScrollRect = null;
			this.m_E_LevelNameText = null;
			this.m_E_GoButton = null;
			this.m_E_GoImage = null;
			this.m_E_GoTextText = null;
			this.m_E_LevelNotEnoughText = null;
			this.m_E_InAdventureTipText = null;
			this.m_E_CloseButton = null;
			this.m_E_CloseImage = null;
			this.uiTransform = null;
		}

		private UnityEngine.RectTransform m_EG_ContentRectTransform = null;
		private UnityEngine.UI.LoopVerticalScrollRect m_E_BattleLevelLoopVerticalScrollRect = null;
		private UnityEngine.UI.Text m_E_LevelNameText = null;
		private UnityEngine.UI.Button m_E_GoButton = null;
		private UnityEngine.UI.Image m_E_GoImage = null;
		private UnityEngine.UI.Text m_E_GoTextText = null;
		private UnityEngine.UI.Text m_E_LevelNotEnoughText = null;
		private UnityEngine.UI.Text m_E_InAdventureTipText = null;
		private UnityEngine.UI.Button m_E_CloseButton = null;
		private UnityEngine.UI.Image m_E_CloseImage = null;
		public Transform uiTransform = null;
	}
}
