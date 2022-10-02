﻿namespace ET
{
	public enum PlayerState
    {
		Disconnect,//断开
		Gate,
		Game
    }


	[ObjectSystem]
	public class PlayerSystem : AwakeSystem<Player, long ,long>
	{
		public override void Awake(Player self, long a ,long roleId)
		{
			self.AccountId = a;
			self.UnitId = roleId;
		}
	}

	public sealed class Player : Entity, IAwake<string>,IAwake<long,long>
	{
		public long AccountId { get;  set; }
		public long SessionInstanceId { get; set; }
		/// <summary>
		/// 用于传送给Map
		/// </summary>
		public long UnitId { get; set; }

		public PlayerState PlayerState { get;  set; }

		public Session ClientSession { get; set; }
	}
}