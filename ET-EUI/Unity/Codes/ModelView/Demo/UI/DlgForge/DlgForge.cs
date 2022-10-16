using System;
using System.Collections.Generic;
using UnityEngine;

namespace ET
{
	public  class DlgForge :Entity,IAwake,IUILogic
	{

		public DlgForgeViewComponent View { get => this.Parent.GetComponent<DlgForgeViewComponent>();}
		public Dictionary<int, Scroll_Item_production> scrollItemProductionsDic;


        public long MakeQueueTimer = 0;

     
    }
}
