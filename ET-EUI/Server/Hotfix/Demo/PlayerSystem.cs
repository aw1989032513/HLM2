using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    public class PlayerDestorySystem : DestroySystem<Player>
    {
        public override void Destroy(Player self)
        {
            self.AccountId = 0;
            self.UnitId = 0;
            self.ChatInfoInstanceId = 0;
            self.PlayerState = PlayerState.Disconnect;
            self.ClientSession?.Dispose();
        }
    }
    public class PlayerAwakeSystem : AwakeSystem<Player, long, long>
    {
        public override void Awake(Player self, long a, long roleId)
        {
            self.AccountId = a;
            self.UnitId = roleId;
        }
    }
    public static class PlayerSystem
    {

    }
}
