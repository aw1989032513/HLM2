using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;

namespace ET
{
#if SERVER
    public class TasksComponent:Entity,IAwake,IDestroy,ITransfer, IUnitCache,IDeserialize
#else 
    public class TasksComponent : Entity, IAwake, IDestroy
#endif
    {
        /// <summary>
        /// KEY：ConfigID
        /// </summary>
#if SERVER
[BsonIgnore]
#endif
        public SortedDictionary<int, TaskInfo> TaskInfoDict = new SortedDictionary<int, TaskInfo>();
#if !SERVER
        public List<TaskInfo> TaskInfoList = new List<TaskInfo>();
#endif
        /// <summary>
        /// 任务ID  触发任务进程作用
        /// </summary>
#if SERVER
        [BsonIgnore]
        public HashSet<int> CurrentTaskSet = new HashSet<int>();
#endif

#if SERVER
        [BsonIgnore]
        public M2C_UpdateTaskInfo M2CUpdateTaskInfo = new M2C_UpdateTaskInfo();
#endif
    }
}
