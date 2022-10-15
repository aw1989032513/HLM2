using System.Collections.Generic;

namespace ET
{
	public  class DlgItemPopUp :Entity,IAwake,IUILogic
	{

		public DlgItemPopUpViewComponent View { get => this.Parent.GetComponent<DlgItemPopUpViewComponent>();}
		public Dictionary<int, Scroll_Item_entry> ScrollItemEntries;
		/// <summary>
		/// 要显示的物品ID
		/// </summary>
		public long ItemId = 0;
		public ItemContainerType ItemContainerType = ItemContainerType.Bag;
	}
}
