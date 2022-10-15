using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    public class EquipInfoComponentAwakeSystem : AwakeSystem<EquipInfoComponent>
    {
        public override void Awake(EquipInfoComponent self)
        {
            self.GenerateEntries();
        }
    }
    public class EquipInfoComponentDestroySystem : DestroySystem<EquipInfoComponent>
    {
        public override void Destroy(EquipInfoComponent self)
        {
            self.isInited = false;
            self.score = 0;
            foreach (var item in self.EntryList)
            {
                item?.Dispose();
            }
            self.EntryList.Clear();
        }
    }
    /// <summary>
    /// 反序列化接口
    /// </summary>
    public class EquipInfoComponentDeserializeSystem : DeserializeSystem<EquipInfoComponent>
    {
        public override void Deserialize(EquipInfoComponent self)
        {
            foreach (var entity in self.Children.Values)
            {
                self.EntryList.Add(entity as AttributeEntry);
            }
        }
    }
    public static class EquipInfoComponentSystem
    {
        public static EquipInfoProto ToMessageEquipInfoProto(this EquipInfoComponent self)
        {
            EquipInfoProto equipInfoProto = new EquipInfoProto();
            equipInfoProto.Id = self.Id;
            equipInfoProto.Score = self.score;
            for (int i = 0; i < self.EntryList.Count; i++)
            {
                equipInfoProto.AttributeEntryProtoList.Add(self.EntryList[i].ToMessageAttributeEntryProto());
            }
            return equipInfoProto;
        }
        /// <summary>
        /// 生成随机属性词条
        /// </summary>
        /// <param name="self"></param>
        public static void GenerateEntries(this EquipInfoComponent self)
        {
            if (self.isInited)
            {
                return;
            }
            self.isInited = true;
            self.CreateEntry();
           
        }
        public static void CreateEntry(this EquipInfoComponent self)
        {
            ItemConfig itemConfig = self.GetParent<Item>().config;
            EntryRandomConfig entryRandomConfig = EntryRandomConfigCategory.Instance.Get(itemConfig.EntryRandomId);

            //创建普通词条
            int entryCount = RandomHelper.RandomNumber(entryRandomConfig.EntryRandMinCount + self.GetParent<Item>().quality,
                entryRandomConfig.EntryRandMaxCount + self.GetParent<Item>().quality);
            for (int i = 0; i < entryCount; i++)
            {
                EntryConfig entryConfig = EntryConfigCategory.Instance.GetRandomEntryConfigByLevel((int)EntryType.Common, entryRandomConfig.EntryLevel);
                if (entryConfig == null)
                {
                    continue;
                }
                AttributeEntry attributeEntry = self.AddChild<AttributeEntry>();
                attributeEntry.Type = EntryType.Common;
                attributeEntry.Key  = entryConfig.AttributeType;//NumericType的属性类型
                attributeEntry.Value = RandomHelper.RandomNumber(entryConfig.AttributeMinValue, entryConfig.AttributeMaxValue + self.GetParent<Item>().quality);
                self.EntryList.Add(attributeEntry);
                self.score += entryConfig.EntryScore;//词条评分
            }
            //创建特殊词条
            entryCount = RandomHelper.RandomNumber(entryRandomConfig.SpecialEntryRandMinCount, entryRandomConfig.SpecialEntryRandMaxCount);
            for (int i = 0; i < entryCount; i++)
            {
                EntryConfig entryConfig = EntryConfigCategory.Instance.GetRandomEntryConfigByLevel((int)EntryType.Special, entryRandomConfig.SpecialEntryLevel);
                if (entryConfig == null)
                {
                    continue;
                }
                AttributeEntry attributeEntry = self.AddChild<AttributeEntry>();
                attributeEntry.Type = EntryType.Special;
                attributeEntry.Key = entryConfig.AttributeType;
                attributeEntry.Value = RandomHelper.RandomNumber(entryConfig.AttributeMinValue, entryConfig.AttributeMaxValue);
                self.EntryList.Add(attributeEntry);
                self.score += entryConfig.EntryScore;
            }
        }
    }
}
