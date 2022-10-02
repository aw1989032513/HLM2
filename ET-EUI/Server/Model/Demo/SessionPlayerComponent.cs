namespace ET
{
	public class SessionPlayerComponent : Entity, IAwake, IDestroy
	{
		public long PlayerId;
		public long playerInstanceId;
		public long accountId;
		/// <summary>
		/// 是否再次登录
		/// </summary>
		public bool isLoginAgain = false;

	}
}