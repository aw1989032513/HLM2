using System.Collections.Generic;
using System.Linq;

namespace ET
{
	/// <summary>
	/// 控制player的Add remove Get
	/// </summary>
	public class PlayerComponent : Entity, IAwake, IDestroy
	{
		private readonly Dictionary<long, Player> idPlayers = new Dictionary<long, Player>();
		
		public void Add(Player player)
		{
			this.idPlayers.Add(player.AccountId, player);
		}
		/// <summary>
		/// 根据AccountID 获取Player
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public Player Get(long id)
		{
			this.idPlayers.TryGetValue(id, out Player gamer);
			return gamer;
		}

		public void Remove(long id)
		{
			this.idPlayers.Remove(id);
		}

		public int Count
		{
			get
			{
				return this.idPlayers.Count;
			}
		}

		public Player[] GetAll()
		{
			return this.idPlayers.Values.ToArray();
		}
	}
}