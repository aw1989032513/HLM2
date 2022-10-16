using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    public partial class ForgeProductionConfigCategory
    {
        /// <summary>
        /// 根据等级获得可以同时打造几个商品
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        public int GetProductionConfigCount(int level)
        {
            int count = 0;
            foreach (var config in this.list)
            {
                if (config.NeedLevel <= level)
                {
                    ++count;
                }
            }
            return count;   
        }
        /// <summary>
        /// 根据等级和Index 获得对应的配置表
        /// </summary>
        /// <param name="unitLevel"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public ForgeProductionConfig GetProductionByLevelIndex(int unitLevel, int index)
        {
            int tempIndex = 0;
            foreach (var config in this.list)
            {
                if (config.NeedLevel <= unitLevel && index == tempIndex)
                {
                    return config;
                }
                if (config.NeedLevel <= unitLevel)
                {
                    ++tempIndex;
                }
            }
            return null;
        }
    }
}
