using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    public class TasksComponentAwakeSystem : AwakeSystem<TasksComponent>
    {
        public override void Awake(TasksComponent self)
        {
            self.Awake();
        }
    }
    public class TasksComponentDserSystem : DeserializeSystem<TasksComponent>
    {
        public override void Deserialize(TasksComponent self)
        {
            foreach (Entity item in self.Children.Values)
            {
                TaskInfo taskInfo = item as TaskInfo;
                self.TaskInfoDict.Add(taskInfo.configId, taskInfo);
                if (!taskInfo.IsTaskState(TaskState.Received))
                {
                    self.CurrentTaskSet.Add(taskInfo.configId);
                }
            }
        }
    }
    public class TasksComponentDestroySystem : DestroySystem<TasksComponent>
    {
        public override void Destroy(TasksComponent self)
        {
            foreach (var taskInfo in self.TaskInfoDict.Values)
            {
                taskInfo?.Dispose();
            }
            self.TaskInfoDict.Clear();
            self.CurrentTaskSet.Clear();
        }
    }
    public static class TasksComponentSystem
    {
        public static void Awake(this TasksComponent self)
        {
            if (self.TaskInfoDict.Count == 0)
            {
                //初始化任务信息
                self.UpdateAfterTaskInfo(beforeTaskConfigId: 0, isNoticeClient: false);
            }
        }
        /// <summary>
        /// 初始化任务信息，更新后续任务信息
        /// </summary>
        /// <param name="self"></param>
        /// <param name="beforeTaskConfigId"></param>
        /// <param name="isNoticeClient">是否需要发送给客户端</param>
        public static void UpdateAfterTaskInfo(this TasksComponent self, int beforeTaskConfigId, bool isNoticeClient = true)
        {
            self.CurrentTaskSet.Remove(beforeTaskConfigId);
            // beforeTask 之后的任务ID
            var taskConfigIdList = TaskConfigCategory.Instance.GetAfterTaskIdListByBeforeTaskId(beforeTaskConfigId);
            if (taskConfigIdList == null)
            {
                return;
            }
            foreach (var taskConfigId in taskConfigIdList)
            {
                self.CurrentTaskSet.Add(taskConfigId);//1，8,15三个任务
                //任务初始进度值
                int count = self.GetTaskInitProgressCount(taskConfigId);
                self.AddOrUpdateTaskInfo(taskConfigId, count, isNoticeClient);
            }
        }
      /// <summary>
      /// 初始任务进度值(等级和0)
      /// </summary>
      /// <param name="self"></param>
      /// <param name="taskConfigId"></param>
        public static int GetTaskInitProgressCount(this TasksComponent self, int taskConfigId)
        {
            var config = TaskConfigCategory.Instance.Get(taskConfigId);
            if (config.TaskActionType == (int)TaskActionType.UpLevel)//如果是等级的话
            {
                return self.GetParent<Unit>().GetComponent<NumericComponent>().GetAsInt((int)NumericType.Level);
            }
            else
            {
                return 0;
            }
        }
        public static void AddOrUpdateTaskInfo(this TasksComponent self , int taskConfigId,int count, bool isNoticeClient = true)
        {
            if (!self.TaskInfoDict.TryGetValue(taskConfigId,out TaskInfo taskInfo))
            {
                taskInfo = self.AddChild<TaskInfo,int>(taskConfigId);
                self.TaskInfoDict.Add(taskConfigId, taskInfo);
            }
            //更新进度
            taskInfo.UpdateProgress(count);
            //检查更新进度后能否完成目标
            taskInfo.CheckCompleteTask();
            if (isNoticeClient)//需要通知客户端
            {
                TaskNoticeHelper.SyncTaskInfo(self.GetParent<Unit>(), taskInfo, self.M2CUpdateTaskInfo);
            }
        }
        /// <summary>
        /// 触发任务进程
        /// </summary>
        /// <param name="self"></param>
        /// <param name="taskActionType"></param>
        /// <param name="value"></param>
        /// <param name="targetId"></param>
        public static void TriggerTaskAction(this TasksComponent self, TaskActionType taskActionType,int value,int targetId = 0)
        {
            foreach (var taskId in self.CurrentTaskSet)
            {
                TaskConfig taskConfig = TaskConfigCategory.Instance.Get(taskId);
                if (taskConfig.TaskActionType == (int)taskActionType && taskConfig.TaskTargetId ==targetId)
                {
                    self.AddOrUpdateTaskInfo(taskId, value);
                }
            }
        }
        /// <summary>
        /// 得到任务奖励
        /// </summary>
        /// <param name="self"></param>
        /// <param name="taskConfigId"></param>
        public static int TryReceiveTaskReward(this TasksComponent self,int taskConfigId)
        {
            //配置表有么
            if (!TaskConfigCategory.Instance.Contain(taskConfigId))
            {
                return ErrorCode.ERR_NoTaskConfigExist;

            }
            self.TaskInfoDict.TryGetValue(taskConfigId, out TaskInfo taskInfo);
            //字典中有么
            if (taskInfo == null || taskInfo.IsDisposed)
            {
                return ErrorCode.ERR_NoTaskInfoNoExist;
            }
            //前置任务没有领取奖励
            if (!self.IsBeforeTaskReceived(taskConfigId)) 
            {
                return ErrorCode.ERR_BeforeTaskNoOver;
            }
            if (!taskInfo.IsTaskState(TaskState.Complete))
            {
                return ErrorCode.ERR_TaskNoCompleted;
            }
            //该任务是否已经被领取了奖励
            if (taskInfo.IsTaskState(TaskState.Received))
            {
                return ErrorCode.ERR_TaskRewarded;
            }
            return ErrorCode.ERR_Success;
        }
        /// <summary>
        /// 前置任务接了么，完成了吗
        /// </summary>
        /// <param name="self"></param>
        /// <param name="taskConfigId"></param>
        /// <returns></returns>
        public static bool IsBeforeTaskReceived(this TasksComponent self,int taskConfigId)
        {
          
            var config = TaskConfigCategory.Instance.Get(taskConfigId);
            if (config.TaskBeforeId == 0) //如果是初始任务
            {
                return true;
            }
            if (! self.TaskInfoDict.TryGetValue(config.TaskBeforeId,out TaskInfo beforeTaskInfo))
            {
                return false;
            }
            //前置任务没有领取了奖励
            if (!beforeTaskInfo.IsTaskState(TaskState.Received))
            {
                return false;
            }
            return true;                      
        }
        /// <summary>
        /// 奖励领取后刷新状态
        /// </summary>
        /// <param name="self"></param>
        /// <param name="unit"></param>
        /// <param name="taskConfigId"></param>
        public static void ReceiveTaskRewardState(this TasksComponent self,Unit unit,int taskConfigId)
        {
            if (!self.TaskInfoDict.TryGetValue(taskConfigId, out TaskInfo taskInfo))
            {
                Log.Error($"TaskInfo Error :{taskConfigId}");
                return;
            }
            taskInfo.SetTaskState(TaskState.Received);
            //通知客户端
            TaskNoticeHelper.SyncTaskInfo(unit, taskInfo, self.M2CUpdateTaskInfo);
            self.UpdateAfterTaskInfo(taskConfigId);
        }
    }
}
