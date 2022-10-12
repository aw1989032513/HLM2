using UnityEngine;

namespace ET
{
    public static class UnitFactory
    {
        /// <summary>
        /// //将服务器发下来的数值，存放到客户端的NumericComponent,并且创建Unit
        /// </summary>
        /// <param name="currentScene"></param>
        /// <param name="unitInfo"></param>
        /// <returns></returns>
        public static Unit Create(Scene currentScene, UnitInfo unitInfo)
        {
	        UnitComponent unitComponent = currentScene.GetComponent<UnitComponent>();
	        Unit unit = unitComponent.AddChildWithId<Unit, int>(unitInfo.UnitId, unitInfo.ConfigId);
	        unitComponent.Add(unit);
	        
	        //unit.Position = new Vector3(unitInfo.X, unitInfo.Y, unitInfo.Z);
	        //unit.Forward = new Vector3(unitInfo.ForwardX, unitInfo.ForwardY, unitInfo.ForwardZ);
	        
	        NumericComponent numericComponent = unit.AddComponent<NumericComponent>();
	        for (int i = 0; i < unitInfo.Ks.Count; ++i)
	        {
				//将服务器发下来的数值，存放到客户端的NumericComponent
				numericComponent.Set((int)(NumericType)unitInfo.Ks[i], unitInfo.Vs[i]);
	        }
            #region     MoveComponent
            //unit.AddComponent<MoveComponent>();
            //if (unitInfo.MoveInfo != null)
            //{
            // if (unitInfo.MoveInfo.X.Count > 0)
            // {
            //  using (ListComponent<Vector3> list = ListComponent<Vector3>.Create())
            //  {
            //   list.Add(unit.Position);
            //   for (int i = 0; i < unitInfo.MoveInfo.X.Count; ++i)
            //   {
            //    list.Add(new Vector3(unitInfo.MoveInfo.X[i], unitInfo.MoveInfo.Y[i], unitInfo.MoveInfo.Z[i]));
            //   }

            //   unit.MoveToAsync(list).Coroutine();
            //  }
            // }
            //}
            #endregion

            unit.AddComponent<ObjectWait>();

	       // unit.AddComponent<XunLuoPathComponent>();
	        
	        Game.EventSystem.Publish(new EventType.AfterUnitCreate() {Unit = unit});
            return unit;
        }
        public static async ETTask<Unit> CreateMonster(Scene currentScene, int configId)
        {
            UnitComponent unitComponent = currentScene.GetComponent<UnitComponent>();
            Unit monsterUnit = unitComponent.AddChildWithId<Unit, int>(IdGenerater.Instance.GenerateId(), configId);
            unitComponent.Add(monsterUnit);

            //添加数值组件
            NumericComponent numericComponent = monsterUnit.AddComponent<NumericComponent>();
            numericComponent.SetNoEvent((int)NumericType.IsAlive, 1);
            numericComponent.SetNoEvent((int)NumericType.DamageValue, monsterUnit.Config.DamageValue);
            numericComponent.SetNoEvent((int)NumericType.MaxHp, monsterUnit.Config.MaxHP);
            numericComponent.SetNoEvent((int)NumericType.Hp, monsterUnit.Config.MaxHP);

            monsterUnit.AddComponent<ObjectWait>();

            await Game.EventSystem.PublishAsync(new EventType.AfterUnitCreate() { Unit = monsterUnit });
            return monsterUnit;
        }
    }
}
