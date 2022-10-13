using ET.EventType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ET
{
    /// <summary>
    /// 显示层逻辑层处理攻击
    /// </summary>
    internal class AdventureBattleRoundView_PlayAnimation : AEventAsync<EventType.AdventureBattleRoundView>
    {
        protected override async ETTask Run(AdventureBattleRoundView args)
        {
            if (!args.attackUnit.IsAlive() || !args.monsterUnit.IsAlive())
            {
                return;
            }

            args.attackUnit?.GetComponent<AnimatorComponent>().Play(MotionType.Attack);
            args.monsterUnit?.GetComponent<AnimatorComponent>().Play(MotionType.Hurt);
            args.monsterUnit.GetComponent<GameObjectComponent>().SpriteRenderer.color = Color.red;

            long instanceId = args.monsterUnit.InstanceId;

            await TimerComponent.Instance.WaitAsync(300);
            if (instanceId != args.monsterUnit.InstanceId)
            {
                return;
            }
            args.monsterUnit.GetComponent<GameObjectComponent>().SpriteRenderer.color = Color.white;

            await ETTask.CompletedTask;
        }
    }
}
