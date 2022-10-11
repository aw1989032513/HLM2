
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	public  class Scroll_Item_makeQueue : Entity,IAwake,IDestroy 
	{
		private bool isCacheNode = true;
		public void SetCacheMode(bool isCache)
		{
			this.isCacheNode = isCache;
		}

		public Scroll_Item_makeQueue BindTrans(Transform trans)
		{
			this.uiTransform = trans;
			return this;
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
		    	   Transform subTrans = UIFindHelper.FindDeepChild<Transform>(this.uiTransform.gameObject,"ES_EquipItem");
		    	   this.m_es_equipitem = this.AddChild<ES_EquipItem,Transform>(subTrans);
     			}
     			return this.m_es_equipitem;
     		}
     	}

		public UnityEngine.UI.Slider E_LeaftTimeSlider
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if (this.isCacheNode)
     			{
     				if( this.m_E_LeaftTimeSlider == null )
     				{
		    			this.m_E_LeaftTimeSlider = UIFindHelper.FindDeepChild<UnityEngine.UI.Slider>(this.uiTransform.gameObject,"E_LeaftTime");
     				}
     				return this.m_E_LeaftTimeSlider;
     			}
     			else
     			{
		    		return UIFindHelper.FindDeepChild<UnityEngine.UI.Slider>(this.uiTransform.gameObject,"E_LeaftTime");
     			}
     		}
     	}

		public UnityEngine.UI.Text E_MakeTipText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if (this.isCacheNode)
     			{
     				if( this.m_E_MakeTipText == null )
     				{
		    			this.m_E_MakeTipText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"E_MakeTip");
     				}
     				return this.m_E_MakeTipText;
     			}
     			else
     			{
		    		return UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"E_MakeTip");
     			}
     		}
     	}

		public UnityEngine.UI.Text E_MakeTimeText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if (this.isCacheNode)
     			{
     				if( this.m_E_MakeTimeText == null )
     				{
		    			this.m_E_MakeTimeText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"E_MakeTime");
     				}
     				return this.m_E_MakeTimeText;
     			}
     			else
     			{
		    		return UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"E_MakeTime");
     			}
     		}
     	}

		public UnityEngine.UI.Button E_ReciveButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if (this.isCacheNode)
     			{
     				if( this.m_E_ReciveButton == null )
     				{
		    			this.m_E_ReciveButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"E_Recive");
     				}
     				return this.m_E_ReciveButton;
     			}
     			else
     			{
		    		return UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"E_Recive");
     			}
     		}
     	}

		public UnityEngine.UI.Image E_ReciveImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if (this.isCacheNode)
     			{
     				if( this.m_E_ReciveImage == null )
     				{
		    			this.m_E_ReciveImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_Recive");
     				}
     				return this.m_E_ReciveImage;
     			}
     			else
     			{
		    		return UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_Recive");
     			}
     		}
     	}

		public UnityEngine.UI.Text E_MakeOverTipText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if (this.isCacheNode)
     			{
     				if( this.m_E_MakeOverTipText == null )
     				{
		    			this.m_E_MakeOverTipText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"E_MakeOverTip");
     				}
     				return this.m_E_MakeOverTipText;
     			}
     			else
     			{
		    		return UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"E_MakeOverTip");
     			}
     		}
     	}

		public void DestroyWidget()
		{
			this.m_es_equipitem?.Dispose();
			this.m_es_equipitem = null;
			this.m_E_LeaftTimeSlider = null;
			this.m_E_MakeTipText = null;
			this.m_E_MakeTimeText = null;
			this.m_E_ReciveButton = null;
			this.m_E_ReciveImage = null;
			this.m_E_MakeOverTipText = null;
			this.uiTransform = null;
		}

		private ES_EquipItem m_es_equipitem = null;
		private UnityEngine.UI.Slider m_E_LeaftTimeSlider = null;
		private UnityEngine.UI.Text m_E_MakeTipText = null;
		private UnityEngine.UI.Text m_E_MakeTimeText = null;
		private UnityEngine.UI.Button m_E_ReciveButton = null;
		private UnityEngine.UI.Image m_E_ReciveImage = null;
		private UnityEngine.UI.Text m_E_MakeOverTipText = null;
		public Transform uiTransform = null;
	}
}
