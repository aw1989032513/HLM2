using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    public partial class TaskConfigCategory
    {
        public Dictionary<int, List<int>> BeforeTaskConfigDictionary = new Dictionary<int, List<int>>();
        public override void AfterEndInit()
        {
            base.AfterEndInit();
            foreach (var config in this.list)
            {
                if (!this.BeforeTaskConfigDictionary.ContainsKey(config.TaskBeforeId))
                {
                    this.BeforeTaskConfigDictionary.Add(config.TaskBeforeId, new List<int>());
                }
                this.BeforeTaskConfigDictionary[config.TaskBeforeId].Add(config.Id);
            }

            // 0-1,8,15  1-2 3-4 5-6
        }
        /// <summary>
        /// 通过前置任务ID获得后面的任务ID
        /// </summary>
        /// <param name="beforeConfigId"></param>
        /// <returns></returns>
        public  List<int> GetAfterTaskIdListByBeforeTaskId(int beforeConfigId)
        {
            if (this.BeforeTaskConfigDictionary.TryGetValue(beforeConfigId,out List<int> configIdList))
            {
                return configIdList;
            }
            return null;
        }
    }
}
