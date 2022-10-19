using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    public class ChatComponentDestorySystem : DestroySystem<ChatComponent>
    {
        public override void Destroy(ChatComponent self)
        {
            foreach (var item in self.ChatInfoUnitsDict.Values)
            {
                item?.Dispose();
            }
            self.ChatInfoUnitsDict.Clear();
        }
    }
    public static class ChatComponentSystem
    {
        public static void Add(this ChatComponent self,ChatInfo chatInfoUnit)
        {
            if (self.ChatInfoUnitsDict.ContainsKey(chatInfoUnit.Id))
            {
                Log.Error($"chatInfoUnit is exist! ： {chatInfoUnit.Id}");
                return;
            }
            self.ChatInfoUnitsDict.Add(chatInfoUnit.Id, chatInfoUnit);
        }
        public static ChatInfo Get(this ChatComponent self, long  UnitId)
        {
            self.ChatInfoUnitsDict.TryGetValue(UnitId, out ChatInfo chatInfo);
            return chatInfo;
        }
        public static void Remove(this ChatComponent self, long chatInfoUnitId)
        {
            if (self.ChatInfoUnitsDict.TryGetValue(chatInfoUnitId, out ChatInfo chatInfo))
            {
                self.ChatInfoUnitsDict.Remove(chatInfoUnitId);
                self.ChatInfoUnitsDict[chatInfoUnitId]?.Dispose();
            } 
        }
    }
}
