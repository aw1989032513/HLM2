using System;
using UnityEngine;

namespace ET
{
    public static class UnitFactory
    {
        public static Unit Create(Scene scene, long id, UnitType unitType)
        {
            UnitComponent unitComponent = scene.GetComponent<UnitComponent>();
            switch (unitType)
            {
                case UnitType.Player:
                    {
                        Unit unit = unitComponent.AddChildWithId<Unit, int>(id, 1001);
                        //unit.AddComponent<MoveComponent>();
                        //unit.Position = new Vector3(-10, 0, -10);

                        NumericComponent numericComponent = unit.AddComponent<NumericComponent>();
                        foreach (var config in PlayerNumericConfigCategory.Instance.GetAll())
                        {
                            if (config.Value.BaseValue == 0)
                            {
                                continue;
                            }
                            //如果表的ID 小于3000 代表都有加成属性推导
                            if (config.Key < 3000)
                            {
                                int bas = config.Key * 10 + 1;
                                numericComponent.SetNoEvent(bas, config.Value.BaseValue);
                            }
                            else
                            {
                                //大于3000的值直接使用
                                numericComponent.SetNoEvent(config.Key, config.Value.BaseValue);
                            }
                        }

                        unit.AddComponent<BagComponent>();
                        unit.AddComponent<EquipmentsComponent>();
                        unit.AddComponent<ForgeComponent>();
                        unitComponent.Add(unit);
                        // 加入aoi
                        //  unit.AddComponent<AOIEntity, int, Vector3>(9 * 1000, unit.Position);
                        return unit;
                    }
                default:
                    throw new Exception($"not such unit type: {unitType}");
            }
        }
        public static Unit CreateMonster(Scene scene, int configId)
        {
            UnitComponent unitComponent = scene.GetComponent<UnitComponent>();
            Unit unit = unitComponent.AddChildWithId<Unit, int>(IdGenerater.Instance.GenerateId(), configId);

            NumericComponent numericComponent = unit.AddComponent<NumericComponent>();
            numericComponent.SetNoEvent((int)NumericType.MaxHp, unit.Config.MaxHP);
            numericComponent.SetNoEvent((int)NumericType.Hp, unit.Config.MaxHP);
            numericComponent.SetNoEvent((int)NumericType.DamageValue, unit.Config.DamageValue);
            numericComponent.SetNoEvent((int)NumericType.IsAlive, 1);

            unitComponent.Add(unit);
            return unit;
        }
     }
}