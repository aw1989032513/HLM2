namespace ET
{
	public enum PlayerState
    {
		Disconnect,//断开
		Gate,
		Game
    }



	public sealed class Player : Entity, IAwake<string>,IAwake<long,long>,IDestroy
	{
		public long AccountId { get;  set; }
		/// <summary>
		/// 用于传送给Map
		/// </summary>
		public long UnitId { get; set; }

		public PlayerState PlayerState { get;  set; }

		public Session ClientSession { get; set; }
		public long ChatInfoInstanceId { get; set; }
	}
}