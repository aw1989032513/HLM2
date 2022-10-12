using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using ProtoBuf;

namespace ET
{
    [ProtoContract]
    [Config]
    public partial class BattleLevelConfigCategory : ProtoObject
    {
        public static BattleLevelConfigCategory Instance;
		
        [ProtoIgnore]
        [BsonIgnore]
        private Dictionary<int, BattleLevelConfig> dict = new Dictionary<int, BattleLevelConfig>();
		
        [BsonElement]
        [ProtoMember(1)]
        private List<BattleLevelConfig> list = new List<BattleLevelConfig>();
		
        public BattleLevelConfigCategory()
        {
            Instance = this;
        }
		
        public override void EndInit()
        {
            foreach (BattleLevelConfig config in list)
            {
                config.EndInit();
                this.dict.Add(config.Id, config);
            }            
            this.AfterEndInit();
        }
		
        public BattleLevelConfig Get(int id)
        {
            this.dict.TryGetValue(id, out BattleLevelConfig item);

            if (item == null)
            {
                throw new Exception($"配置找不到，配置表名: {nameof (BattleLevelConfig)}，配置id: {id}");
            }

            return item;
        }
		
        public bool Contain(int id)
        {
            return this.dict.ContainsKey(id);
        }

        public Dictionary<int, BattleLevelConfig> GetAll()
        {
            return this.dict;
        }

        public BattleLevelConfig GetOne()
        {
            if (this.dict == null || this.dict.Count <= 0)
            {
                return null;
            }
            return this.dict.Values.GetEnumerator().Current;
        }
    }

    [ProtoContract]
	public partial class BattleLevelConfig: ProtoObject, IConfig
	{
		[ProtoMember(1)]
		public int Id { get; set; }
		[ProtoMember(2)]
		public int[] MonsterIds { get; set; }
		[ProtoMember(4)]
		public int[] MiniEnterLevel { get; set; }
		[ProtoMember(5)]
		public int RewardExp { get; set; }

	}
}
