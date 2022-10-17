using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using ProtoBuf;

namespace ET
{
    [ProtoContract]
    [Config]
    public partial class TaskConfigCategory : ProtoObject
    {
        public static TaskConfigCategory Instance;
		
        [ProtoIgnore]
        [BsonIgnore]
        private Dictionary<int, TaskConfig> dict = new Dictionary<int, TaskConfig>();
		
        [BsonElement]
        [ProtoMember(1)]
        private List<TaskConfig> list = new List<TaskConfig>();
		
        public TaskConfigCategory()
        {
            Instance = this;
        }
		
        public override void EndInit()
        {
            foreach (TaskConfig config in list)
            {
                config.EndInit();
                this.dict.Add(config.Id, config);
            }            
            this.AfterEndInit();
        }
		
        public TaskConfig Get(int id)
        {
            this.dict.TryGetValue(id, out TaskConfig item);

            if (item == null)
            {
                throw new Exception($"配置找不到，配置表名: {nameof (TaskConfig)}，配置id: {id}");
            }

            return item;
        }
		
        public bool Contain(int id)
        {
            return this.dict.ContainsKey(id);
        }

        public Dictionary<int, TaskConfig> GetAll()
        {
            return this.dict;
        }

        public TaskConfig GetOne()
        {
            if (this.dict == null || this.dict.Count <= 0)
            {
                return null;
            }
            return this.dict.Values.GetEnumerator().Current;
        }
    }

    [ProtoContract]
	public partial class TaskConfig: ProtoObject, IConfig
	{
		[ProtoMember(1)]
		public int Id { get; set; }
		[ProtoMember(4)]
		public int TaskActionType { get; set; }
		[ProtoMember(5)]
		public int TaskTargetId { get; set; }
		[ProtoMember(6)]
		public int TaskTargetCount { get; set; }
		[ProtoMember(7)]
		public int TaskBeforeId { get; set; }
		[ProtoMember(8)]
		public int RewardGoldCount { get; set; }

	}
}
