using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    public enum TaskState
    {
        None = -1,
        Doing = 0,  //正在进行的状态
        Complete = 1,  //任务完成的状态
        Received = 2,  //已领取奖励的状态
    }
    public enum TaskActionType
    {
        UpLevel = 1,
        MakeItem = 2,
        Adventrue = 3,
    }
    public enum TaskProgressType
    {
        Add = 1, //增加
        Sub = 2, //减少
        Update = 3, //赋值
    }
#if SERVER
    public class TaskInfo : Entity,IAwake,IAwake<int>,IDestroy,ISerializeToEntity
#else
    public class TaskInfo:Entity,IAwake,IAwake<int>,IDestroy
#endif
    {
        public int configId = 0;
        public int taskState = 0;
        public int taskPogress = 0;
    }
}
