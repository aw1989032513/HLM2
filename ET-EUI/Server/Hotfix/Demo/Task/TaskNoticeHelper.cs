using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    public static class TaskNoticeHelper
    {
        public static void SyncTaskInfo(Unit unit,TaskInfo taskInfo, M2C_UpdateTaskInfo updateTaskInfo)
        {
            updateTaskInfo.TaskInfoProto = taskInfo.ToMessage();
            MessageHelper.SendToClient(unit, updateTaskInfo);
        }
        public static void SyncAllTaskInfo(Unit unit)
        {
            TasksComponent tasksComponent = unit.GetComponent<TasksComponent>();
            M2C_AllTaskInfoList m2C_AllTaskInfoList = new M2C_AllTaskInfoList();
            foreach (var item in tasksComponent.TaskInfoDict.Values)
            {
                m2C_AllTaskInfoList.TaskInfoProto.Add(item.ToMessage());
            }
            MessageHelper.SendToClient(unit, m2C_AllTaskInfoList);
        }
    }
}
