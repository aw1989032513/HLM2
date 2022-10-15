using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    public class AttributeEntryAwakeSystem : AwakeSystem<AttributeEntry>
    {
        public override void Awake(AttributeEntry self)
        {
            self.Id = 0;
            self.Key = 0;
            self.Value = 0;
            self.Type = EntryType.Common;
        }
    }
    public static  class AttributeEntrySystem
    {
        public static void FromMessage(this AttributeEntry self,AttributeEntryProto attributeEntryProto)
        {
            self.Id = attributeEntryProto.Id;
            self.Key = attributeEntryProto.Key;
            self.Value = attributeEntryProto.Value;
            self.Type = (EntryType)attributeEntryProto.EntryType;
        }
    }
}
