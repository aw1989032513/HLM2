namespace ET
{
	/// <summary>
	/// 挂在到GateSeesion
	/// </summary>
	public class SessionPlayerComponent : Entity, IAwake, IDestroy
	{
		public long PlayerId;
		public long playerInstanceId;
		public long accountId;
		/// <summary>
		/// 顶号登陆或者二次登录状态
		/// </summary>
		public bool isLoginAgain = false;

	}
}