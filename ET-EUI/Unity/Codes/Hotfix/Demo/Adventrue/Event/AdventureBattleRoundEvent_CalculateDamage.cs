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
    public class AdventureBattleRoundEvent_CalculateDamage : AEventAsync<EventType.AdventureBattleRound>
    {
        protected override async ETTask Run(AdventureBattleRound args)
        {
            if (!args.attackUnit.IsAlive() || !args.monsterUnit.IsAlive())
            {
                return;
            }
            SRandom random = args.zongScene.CurrentScene().GetComponent<AdventureComponent>().random;

            int damage = DamageCalcuateHelper.CalcuateDamageValue(args.attackUnit, args.monsterUnit, ref random);
            int hp = args.monsterUnit.GetComponent<NumericComponent>().GetAsInt((int)NumericType.Hp) - damage;


            if (hp <=0)
            {
                hp = 0;
                args.monsterUnit.SetAlive(false);
            }
            args.monsterUnit.GetComponent<NumericComponent>().Set((int)NumericType.Hp, hp);
            Log.Debug($"***********************{args.monsterUnit.Type}剩余血量：{hp}***********************");

            Game.EventSystem.PublishAsync(new EventType.ShowDamageValueView()
            {
                zongScene = args.zongScene,
                targetUnit = args.monsterUnit,
                damageValue = damage
            }).Coroutine();


      
            await ETTask.CompletedTask;
        }
    }
}
