﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    public class TaskInfoAwkaeSystem : AwakeSystem<TaskInfo, int>
    {
        public override void Awake(TaskInfo self, int configId)
        {
            self.configId = configId;
            self.taskPogress = 0;
            self.taskState = (int)TaskState.Doing;
        }
    }
    public class TaskInfoDestorySystem : DestroySystem<TaskInfo>
    {
        public override void Destroy(TaskInfo self)
        {
            self.configId = 0;
            self.taskPogress = 0;
            self.taskState = (int)TaskState.None;
        }
    }
    public static class TaskInfoSystem
    {
        /// <summary>
        /// 装
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static TaskInfoProto ToMessage(this TaskInfo self)
        {
            TaskInfoProto TaskInfoProto = new TaskInfoProto();
            TaskInfoProto.ConfigId     = self.configId;
            TaskInfoProto.TaskProgress = self.taskPogress;
            TaskInfoProto.TaskState    = self.taskState;
            return TaskInfoProto;
        }
        /// <summary>
        /// 拆
        /// </summary>
        /// <param name="self"></param>
        /// <param name="taskInfoProto"></param>
        /// <returns></returns>
        public static void FromMessage(this TaskInfo self, TaskInfoProto taskInfoProto)
        {
            self.configId = taskInfoProto.ConfigId;
            self.taskPogress = taskInfoProto.TaskProgress;
            self.taskState = taskInfoProto.TaskState;
        }
        public static bool IsTaskState(this TaskInfo self, TaskState taskState)
        {
            return self.taskState == (int)taskState;
        }
        public static void SetTaskState(this TaskInfo self, TaskState taskState)
        {
            self.taskState = (int)taskState;
        }
        /// <summary>
        /// 更新数值
        /// </summary>
        /// <param name="count">数值</param>
        public static void UpdateProgress(this TaskInfo self, int count)
        {
            var taskConfigActionType = TaskConfigCategory.Instance.Get(self.configId).TaskActionType;
            var config = TaskActionConfigCategory.Instance.Get(taskConfigActionType);
            switch (config.TaskProgressType)
            {
                case (int)TaskProgressType.Add:
                    self.taskPogress += count;
                    break;
                case (int)TaskProgressType.Sub:
                    self.taskPogress -= count;
                    break;
                case (int)TaskProgressType.Update:
                    self.taskPogress = count;
                    break;
            }
        }
        /// <summary>
        /// 检查是否完成了
        /// </summary>
        /// <param name="self"></param>
        public static void CheckCompleteTask(this TaskInfo self)
        {
            if (!self.IsCompleteProgress() || !self.IsTaskState(TaskState.Doing))
            {
                return;
            }
            self.taskState = (int)TaskState.Complete;
        }
        /// <summary>
        /// 任务目标数量 是否达成
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static bool IsCompleteProgress(this TaskInfo self)
        {
            return self.taskPogress >= TaskConfigCategory.Instance.Get(self.configId).TaskTargetCount;  //任务目标数量
        }
    }
}
