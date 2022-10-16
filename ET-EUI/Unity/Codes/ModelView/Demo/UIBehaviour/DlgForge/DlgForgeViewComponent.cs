
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	public  class DlgForgeViewComponent : Entity,IAwake,IDestroy 
	{
		public ES_MakeQueue ES_MakeQueueOne
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_es_makequeueone == null )
     			{
		    	   Transform subTrans = UIFindHelper.FindDeepChild<Transform>(this.uiTransform.gameObject,"BackGround/LayoutGroup/MakeQueueGroup/ES_MakeQueueOne");
		    	   this.m_es_makequeueone = this.AddChild<ES_MakeQueue, Transform>(subTrans);
     			}
     			return this.m_es_makequeueone;
     		}
     	}

		public ES_MakeQueue ES_MakeQueueTwo
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_es_makequeuetwo == null )
     			{
		    	   Transform subTrans = UIFindHelper.FindDeepChild<Transform>(this.uiTransform.gameObject,"BackGround/LayoutGroup/MakeQueueGroup/ES_MakeQueueTwo");
		    	   this.m_es_makequeuetwo = this.AddChild<ES_MakeQueue, Transform>(subTrans);
     			}
     			return this.m_es_makequeuetwo;
     		}
     	}

		public UnityEngine.UI.LoopVerticalScrollRect E_ProductionLoopVerticalScrollRect
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_ProductionLoopVerticalScrollRect == null )
     			{
		    		this.m_E_ProductionLoopVerticalScrollRect = UIFindHelper.FindDeepChild<UnityEngine.UI.LoopVerticalScrollRect>(this.uiTransform.gameObject,"BackGround/LayoutGroup/ProductionGroup/E_Production");
     			}
     			return this.m_E_ProductionLoopVerticalScrollRect;
     		}
     	}

		public ES_EquipItem ES_EquipItem
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_es_equipitem == null )
     			{
		    	   Transform subTrans = UIFindHelper.FindDeepChild<Transform>(this.uiTransform.gameObject,"BackGround/LayoutGroup/ProductionGroup/E_Production/Content/Item_production/ES_EquipItem");
		    	   this.m_es_equipitem = this.AddChild<ES_EquipItem,Transform>(subTrans);
     			}
     			return this.m_es_equipitem;
     		}
     	}

		public UnityEngine.UI.Text E_ItemNameText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_ItemNameText == null )
     			{
		    		this.m_E_ItemNameText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"BackGround/LayoutGroup/ProductionGroup/E_Production/Content/Item_production/E_ItemName");
     			}
     			return this.m_E_ItemNameText;
     		}
     	}

		public UnityEngine.UI.Button E_MakeButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_MakeButton == null )
     			{
		    		this.m_E_MakeButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"BackGround/LayoutGroup/ProductionGroup/E_Production/Content/Item_production/E_Make");
     			}
     			return this.m_E_MakeButton;
     		}
     	}

		public UnityEngine.UI.Image E_MakeImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_MakeImage == null )
     			{
		    		this.m_E_MakeImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"BackGround/LayoutGroup/ProductionGroup/E_Production/Content/Item_production/E_Make");
     			}
     			return this.m_E_MakeImage;
     		}
     	}

		public UnityEngine.UI.Text E_ConsumeTypeText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_ConsumeTypeText == null )
     			{
		    		this.m_E_ConsumeTypeText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"BackGround/LayoutGroup/ProductionGroup/E_Production/Content/Item_production/ConsumeLayout/E_ConsumeType");
     			}
     			return this.m_E_ConsumeTypeText;
     		}
     	}

		public UnityEngine.UI.Text E_ConsumeCountText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_ConsumeCountText == null )
     			{
		    		this.m_E_ConsumeCountText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"BackGround/LayoutGroup/ProductionGroup/E_Production/Content/Item_production/ConsumeLayout/E_ConsumeCount");
     			}
     			return this.m_E_ConsumeCountText;
     		}
     	}

		public UnityEngine.UI.Text E_IronStoneCountText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_IronStoneCountText == null )
     			{
		    		this.m_E_IronStoneCountText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"BackGround/LayoutGroup/BottomGroup/E_IronStoneCount");
     			}
     			return this.m_E_IronStoneCountText;
     		}
     	}

		public UnityEngine.UI.Text E_FurCountText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_FurCountText == null )
     			{
		    		this.m_E_FurCountText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"BackGround/LayoutGroup/BottomGroup/E_FurCount");
     			}
     			return this.m_E_FurCountText;
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

		public void DestroyWidget()
		{
			this.m_es_makequeueone?.Dispose();
			this.m_es_makequeueone = null;
			this.m_es_makequeuetwo?.Dispose();
			this.m_es_makequeuetwo = null;
			this.m_E_ProductionLoopVerticalScrollRect = null;
			this.m_es_equipitem?.Dispose();
			this.m_es_equipitem = null;
			this.m_E_ItemNameText = null;
			this.m_E_MakeButton = null;
			this.m_E_MakeImage = null;
			this.m_E_ConsumeTypeText = null;
			this.m_E_ConsumeCountText = null;
			this.m_E_IronStoneCountText = null;
			this.m_E_FurCountText = null;
			this.m_E_CloseButton = null;
			this.m_E_CloseImage = null;
			this.uiTransform = null;
		}

		private ES_MakeQueue m_es_makequeueone = null;
		private ES_MakeQueue m_es_makequeuetwo = null;
		private UnityEngine.UI.LoopVerticalScrollRect m_E_ProductionLoopVerticalScrollRect = null;
		private ES_EquipItem m_es_equipitem = null;
		private UnityEngine.UI.Text m_E_ItemNameText = null;
		private UnityEngine.UI.Button m_E_MakeButton = null;
		private UnityEngine.UI.Image m_E_MakeImage = null;
		private UnityEngine.UI.Text m_E_ConsumeTypeText = null;
		private UnityEngine.UI.Text m_E_ConsumeCountText = null;
		private UnityEngine.UI.Text m_E_IronStoneCountText = null;
		private UnityEngine.UI.Text m_E_FurCountText = null;
		private UnityEngine.UI.Button m_E_CloseButton = null;
		private UnityEngine.UI.Image m_E_CloseImage = null;
		public Transform uiTransform = null;
	}
}
