using System.Collections.Generic;

namespace ET
{
	public  class DlgAdventure :Entity,IAwake, IUILogic
	{

		public DlgAdventureViewComponent View { get => this.Parent.GetComponent<DlgAdventureViewComponent>();}

		public Dictionary<int, Scroll_Item_battleLevel> scrollItemBattleLevelsDic;

	}
}
