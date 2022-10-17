using UnityEngine;

namespace ET
{
	namespace EventType
	{
		public struct AppStart
		{
		}
		
		public struct ChangePosition
		{
			public Unit Unit;
			public Vector3 OldPos;
		}

		public struct ChangeRotation
		{
			public Unit Unit;
		}

		public struct MoveStart
		{
			public Unit Unit;
		}

		public struct MoveStop
		{
			public Unit Unit;
		}

		public struct UnitEnterSightRange
		{
			public AOIEntity A;
			public AOIEntity B;
		}

		public struct UnitLeaveSightRange
		{
			public AOIEntity A;
			public AOIEntity B;
		}
		public struct ChangeEquipItem
		{
			public Unit unit;
			public Item item;
			public EquipOp equipOp;
		}
		public struct MakeProdutionOver
        {
			public Unit unit;
			public int productionConfigId;
		}
		public struct BattleWin
		{
			public Unit unit;
			/// <summary>
			/// 关卡ID
			/// </summary>
			public int levelId;
		}
	}
}