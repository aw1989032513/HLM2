using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    public class EquipInfoComponentDestorySystem : DestroySystem<EquipInfoComponent>
    {
        public override void Destroy(EquipInfoComponent self)
        {
            self.isInited = false;
            self.score = 0;
            for (int i = 0; i < self.entryList.Count; i++)
            {
                self.entryList[i]?.Dispose();
            }
            self.entryList.Clear();
        }
    }
    public static class EquipInfoComponentSystem
    {
        public static void FromMessage(this EquipInfoComponent self, EquipInfoProto equipInfoProto)
        {
            self.score = equipInfoProto.Score;
            //先清空
            for (int i = 0; i < self.entryList.Count; i++)
            {
                self.entryList[i]?.Dispose();
            }
            self.entryList.Clear();
            //重新赋值
            for (int i = 0; i < equipInfoProto.AttributeEntryProtoList.Count; i++)
            {
                AttributeEntry attributeEntry = self.AddChild<AttributeEntry>();
                attributeEntry.FromMessage(equipInfoProto.AttributeEntryProtoList[i]);
                self.entryList.Add(attributeEntry);
            }
            self.isInited = true;
        }
    }
}
