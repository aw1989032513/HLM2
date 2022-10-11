using System.Collections.Generic;

namespace ET
{
	public  class DlgRoleInfo :Entity,IAwake, IUILogic
	{

		public DlgRoleInfoViewComponent View { get => this.Parent.GetComponent<DlgRoleInfoViewComponent>();}

		public Dictionary<int, Scroll_Item_attribute> ScrollItemAttributes;

	}
}
