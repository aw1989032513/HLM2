using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using ProtoBuf;

namespace ET
{
    [ProtoContract]
    [Config]
    public partial class ForgeProductionConfigCategory : ProtoObject
    {
        public static ForgeProductionConfigCategory Instance;
		
        [ProtoIgnore]
        [BsonIgnore]
        private Dictionary<int, ForgeProductionConfig> dict = new Dictionary<int, ForgeProductionConfig>();
		
        [BsonElement]
        [ProtoMember(1)]
        private List<ForgeProductionConfig> list = new List<ForgeProductionConfig>();
		
        public ForgeProductionConfigCategory()
        {
            Instance = this;
        }
		
        public override void EndInit()
        {
            foreach (ForgeProductionConfig config in list)
            {
                config.EndInit();
                this.dict.Add(config.Id, config);
            }            
            this.AfterEndInit();
        }
		
        public ForgeProductionConfig Get(int id)
        {
            this.dict.TryGetValue(id, out ForgeProductionConfig item);

            if (item == null)
            {
                throw new Exception($"配置找不到，配置表名: {nameof (ForgeProductionConfig)}，配置id: {id}");
            }

            return item;
        }
		
        public bool Contain(int id)
        {
            return this.dict.ContainsKey(id);
        }

        public Dictionary<int, ForgeProductionConfig> GetAll()
        {
            return this.dict;
        }

        public ForgeProductionConfig GetOne()
        {
            if (this.dict == null || this.dict.Count <= 0)
            {
                return null;
            }
            return this.dict.Values.GetEnumerator().Current;
        }
    }

    [ProtoContract]
	public partial class ForgeProductionConfig: ProtoObject, IConfig
	{
		[ProtoMember(1)]
		public int Id { get; set; }
		[ProtoMember(2)]
		public int ItemConfigId { get; set; }
		[ProtoMember(3)]
		public int ProductionTime { get; set; }
		[ProtoMember(4)]
		public int ConsumId { get; set; }
		[ProtoMember(5)]
		public int ConsumeCount { get; set; }
		[ProtoMember(6)]
		public int NeedLevel { get; set; }

	}
}
