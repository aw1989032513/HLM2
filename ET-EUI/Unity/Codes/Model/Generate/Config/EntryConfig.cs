using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using ProtoBuf;

namespace ET
{
    [ProtoContract]
    [Config]
    public partial class EntryConfigCategory : ProtoObject
    {
        public static EntryConfigCategory Instance;
		
        [ProtoIgnore]
        [BsonIgnore]
        private Dictionary<int, EntryConfig> dict = new Dictionary<int, EntryConfig>();
		
        [BsonElement]
        [ProtoMember(1)]
        private List<EntryConfig> list = new List<EntryConfig>();
		
        public EntryConfigCategory()
        {
            Instance = this;
        }
		
        public override void EndInit()
        {
            foreach (EntryConfig config in list)
            {
                config.EndInit();
                this.dict.Add(config.Id, config);
            }            
            this.AfterEndInit();
        }
		
        public EntryConfig Get(int id)
        {
            this.dict.TryGetValue(id, out EntryConfig item);

            if (item == null)
            {
                throw new Exception($"配置找不到，配置表名: {nameof (EntryConfig)}，配置id: {id}");
            }

            return item;
        }
		
        public bool Contain(int id)
        {
            return this.dict.ContainsKey(id);
        }

        public Dictionary<int, EntryConfig> GetAll()
        {
            return this.dict;
        }

        public EntryConfig GetOne()
        {
            if (this.dict == null || this.dict.Count <= 0)
            {
                return null;
            }
            return this.dict.Values.GetEnumerator().Current;
        }
    }

    [ProtoContract]
	public partial class EntryConfig: ProtoObject, IConfig
	{
		[ProtoMember(1)]
		public int Id { get; set; }
		[ProtoMember(2)]
		public int EntryType { get; set; }
		[ProtoMember(3)]
		public int EntryLevel { get; set; }
		[ProtoMember(4)]
		public int EntryScore { get; set; }
		[ProtoMember(5)]
		public int AttributeType { get; set; }
		[ProtoMember(6)]
		public int AttributeMinValue { get; set; }
		[ProtoMember(7)]
		public int AttributeMaxValue { get; set; }

	}
}
