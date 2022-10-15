using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using ProtoBuf;

namespace ET
{
    [ProtoContract]
    [Config]
    public partial class EntryRandomConfigCategory : ProtoObject
    {
        public static EntryRandomConfigCategory Instance;
		
        [ProtoIgnore]
        [BsonIgnore]
        private Dictionary<int, EntryRandomConfig> dict = new Dictionary<int, EntryRandomConfig>();
		
        [BsonElement]
        [ProtoMember(1)]
        private List<EntryRandomConfig> list = new List<EntryRandomConfig>();
		
        public EntryRandomConfigCategory()
        {
            Instance = this;
        }
		
        public override void EndInit()
        {
            foreach (EntryRandomConfig config in list)
            {
                config.EndInit();
                this.dict.Add(config.Id, config);
            }            
            this.AfterEndInit();
        }
		
        public EntryRandomConfig Get(int id)
        {
            this.dict.TryGetValue(id, out EntryRandomConfig item);

            if (item == null)
            {
                throw new Exception($"配置找不到，配置表名: {nameof (EntryRandomConfig)}，配置id: {id}");
            }

            return item;
        }
		
        public bool Contain(int id)
        {
            return this.dict.ContainsKey(id);
        }

        public Dictionary<int, EntryRandomConfig> GetAll()
        {
            return this.dict;
        }

        public EntryRandomConfig GetOne()
        {
            if (this.dict == null || this.dict.Count <= 0)
            {
                return null;
            }
            return this.dict.Values.GetEnumerator().Current;
        }
    }

    [ProtoContract]
	public partial class EntryRandomConfig: ProtoObject, IConfig
	{
		[ProtoMember(1)]
		public int Id { get; set; }
		[ProtoMember(2)]
		public int EntryRandMinCount { get; set; }
		[ProtoMember(3)]
		public int EntryRandMaxCount { get; set; }
		[ProtoMember(4)]
		public int SpecialEntryRandMinCount { get; set; }
		[ProtoMember(5)]
		public int SpecialEntryRandMaxCount { get; set; }
		[ProtoMember(6)]
		public int EntryLevel { get; set; }
		[ProtoMember(7)]
		public int SpecialEntryLevel { get; set; }

	}
}
