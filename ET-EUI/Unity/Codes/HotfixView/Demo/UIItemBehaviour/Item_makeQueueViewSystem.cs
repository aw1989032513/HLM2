
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ObjectSystem]
	public class Scroll_Item_makeQueueDestroySystem : DestroySystem<Scroll_Item_makeQueue> 
	{
		public override void Destroy( Scroll_Item_makeQueue self )
		{
			self.DestroyWidget();
		}
	}
}
