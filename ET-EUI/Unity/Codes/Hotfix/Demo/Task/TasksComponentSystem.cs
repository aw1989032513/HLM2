using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    public class TasksComponentDestroySystem : DestroySystem<TasksComponent>
    {
        public override void Destroy(TasksComponent self)
        {
            foreach (var item in self.TaskInfoList)
            {
                item?.Dispose();
            }
            foreach (var item in self.TaskInfoDict.Values)
            {
                item?.Dispose();
            }
            self.TaskInfoList.Clear();
            self.TaskInfoDict.Clear();  
        }
    }
    public static class TasksComponentSystem
    {
        public static void AddOrUpdateTaskInfo(this TasksComponent self , TaskInfoProto taskInfoProto)
        {
            TaskInfo taskInfo = self.GetTaskInfoByConfigId(taskInfoProto.ConfigId);
            if (taskInfo == null)
            {
                taskInfo = self.AddChild<TaskInfo,int>(taskInfoProto.ConfigId);
                self.TaskInfoDict.Add(taskInfoProto.ConfigId, taskInfo);
            }
            taskInfo.FromMessage(taskInfoProto);
            //红点事件
            Game.EventSystem.PublishAsync(new EventType.UpdateTaskInfo() { zongScene = self.ZoneScene() }).Coroutine();
        }
        public static TaskInfo GetTaskInfoByConfigId(this TasksComponent self,int configId)
        {
            self.TaskInfoDict.TryGetValue(configId, out TaskInfo taskInfo);
            return taskInfo;
        }
        /// <summary>
        /// 获取任务信息数量
        /// </summary>
        /// <param name="self"></param>
        public static int GetTaskInfoCount(this TasksComponent self)
        {
            //将之前的内容删除
            self.TaskInfoList.Clear();
            self.TaskInfoList = self.TaskInfoDict.Values.Where( a =>
                 !a.IsTaskState(TaskState.Received)).ToList();
            self.TaskInfoList.Sort((a, b) => { return b.taskState - a.taskState; });//因为taskState已经领取了是2，没领取是1，这个方法是已经领取的排在前面
            return self.TaskInfoList.Count;
        }
        /// <summary>
        /// 根据预制体Index获得信息
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static TaskInfo GetTaskInfoByIndex(this TasksComponent self,int index)
        {
            if (index <0 || index >= self.TaskInfoList.Count)
            {
                return null;
            }
            return self.TaskInfoList[index];
        }
        public static bool IsExistTaskComplete(this TasksComponent self)
        {
            foreach (var taskInfo in self.TaskInfoDict.Values)
            {
                if (taskInfo.IsTaskState(TaskState.Complete))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
