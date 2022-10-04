namespace ET
{
	public  class DlgLoading :Entity,IAwake
	{

		public DlgLoadingViewComponent View { get => this.Parent.GetComponent<DlgLoadingViewComponent>();} 

		 

	}
}
