

namespace ET
{
	public static class SessionPlayerComponentSystem
	{
		public class SessionPlayerComponentDestroySystem: DestroySystem<SessionPlayerComponent>
		{
			public override void Destroy(SessionPlayerComponent self)
			{
                // 发送断线消息
                if (! self.isLoginAgain && self.playerInstanceId !=0)//如果是二次登录
                {
					Player player = Game.EventSystem.Get(self.playerInstanceId) as Player;
					DisconnectHelper.KickPlayer(player).Coroutine();
                }

				self.accountId = 0;
				self.playerInstanceId = 0;
				self.PlayerId = 0;
				self.isLoginAgain = false;
			}
		}

		public static Player GetMyPlayer(this SessionPlayerComponent self)
		{
			return self.Domain.GetComponent<PlayerComponent>().Get(self.accountId);
		}
	}
}
