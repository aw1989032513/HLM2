using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using ProtoBuf;

namespace ET
{
    [ProtoContract]
    [Config]
    public partial class PlayerNumericConfigCategory : ProtoObject
    {
        public static PlayerNumericConfigCategory Instance;
		
        [ProtoIgnore]
        [BsonIgnore]
        private Dictionary<int, PlayerNumericConfig> dict = new Dictionary<int, PlayerNumericConfig>();
		
        [BsonElement]
        [ProtoMember(1)]
        private List<PlayerNumericConfig> list = new List<PlayerNumericConfig>();
		
        public PlayerNumericConfigCategory()
        {
            Instance = this;
        }
		
        public override void EndInit()
        {
            foreach (PlayerNumericConfig config in list)
            {
                config.EndInit();
                this.dict.Add(config.Id, config);
            }            
            this.AfterEndInit();
        }
		
        public PlayerNumericConfig Get(int id)
        {
            this.dict.TryGetValue(id, out PlayerNumericConfig item);

            if (item == null)
            {
                throw new Exception($"配置找不到，配置表名: {nameof (PlayerNumericConfig)}，配置id: {id}");
            }

            return item;
        }
		
        public bool Contain(int id)
        {
            return this.dict.ContainsKey(id);
        }

        public Dictionary<int, PlayerNumericConfig> GetAll()
        {
            return this.dict;
        }

        public PlayerNumericConfig GetOne()
        {
            if (this.dict == null || this.dict.Count <= 0)
            {
                return null;
            }
            return this.dict.Values.GetEnumerator().Current;
        }
    }

    [ProtoContract]
	public partial class PlayerNumericConfig: ProtoObject, IConfig
	{
		[ProtoMember(1)]
		public int Id { get; set; }
		[ProtoMember(3)]
		public long BaseValue { get; set; }
		[ProtoMember(4)]
		public int isNeedShow { get; set; }
		[ProtoMember(5)]
		public int isAddPoint { get; set; }

	}
}
