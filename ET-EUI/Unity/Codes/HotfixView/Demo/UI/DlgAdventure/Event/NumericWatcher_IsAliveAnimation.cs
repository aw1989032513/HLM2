using ET.EventType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    /// <summary>
    /// 监听unit是否死亡的显示层逻辑
    /// </summary>
    [NumericWatcher((int)NumericType.IsAlive)]
    public class NumericWatcher_IsAliveAnimation : INumericWatcher
    {
        public void Run(NumbericChange args)
        {
            if (!(args.Parent is Unit unit))
            {
                return;
            }
            unit = args.Parent as Unit;
            if (args.New == 0)//如果IsAlive 值为0 代表死亡
            {
                unit?.GetComponent<AnimatorComponent>()?.Play(MotionType.Die);
            }
            else
            {
                unit?.GetComponent<AnimatorComponent>()?.Play(MotionType.Idle);
            }
        }
    }
}
