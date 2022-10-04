namespace ET
{
	public  class DlgMain :Entity,IAwake
	{

		public DlgMainViewComponent View { get => this.Parent.GetComponent<DlgMainViewComponent>();} 

		 

	}
}
