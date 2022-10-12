using ET.EventType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    /// <summary>
    /// 逻辑层处理攻击
    /// </summary>
    public class AdventureBattleRoundEvent_CalculateDamage : AEventAsync<EventType.AdventureBattleRoundView>
    {
        protected override async ETTask Run(AdventureBattleRoundView args)
        {
            if (!args.attackUnit.IsAlive() || !args.monsterUnit.IsAlive())
            {
                return;
            }
            int damage = args.attackUnit.GetComponent<NumericComponent>().GetAsInt((int)NumericType.DamageValue);
            int hp = args.monsterUnit.GetComponent<NumericComponent>().GetAsInt((int)NumericType.Hp);

            hp -= damage;

            if (hp <=0)
            {
                args.monsterUnit.SetAlive(false);
            }
            args.monsterUnit.GetComponent<NumericComponent>().Set((int)NumericType.Hp, hp);

            Log.Debug($"***********************{args.attackUnit.Type}攻击造成伤害：{damage}***********************");
            Log.Debug($"***********************{args.monsterUnit.Type}剩余血量：{hp}***********************");
            await ETTask.CompletedTask;
        }
    }
}
