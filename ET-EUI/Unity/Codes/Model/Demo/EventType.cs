using UnityEngine;

namespace ET
{
    namespace EventType
    {
        public struct AppStart
        {
        }

        public struct SceneChangeStart
        {
            public Scene ZoneScene;
        }
        
        
        public struct SceneChangeFinish
        {
            public Scene ZoneScene;
            public Scene CurrentScene;
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

        public struct PingChange
        {
            public Scene ZoneScene;
            public long Ping;
        }
        
        public struct AfterCreateZoneScene
        {
            public Scene ZoneScene;
        }
        
        public struct AfterCreateCurrentScene
        {
            public Scene CurrentScene;
        }
        
        public struct AfterCreateLoginScene
        {
            public Scene LoginScene;
        }

        public struct AppStartInitFinish
        {
            public Scene ZoneScene;
        }

        public struct LoginFinish
        {
            public Scene ZoneScene;
        }

        public struct LoadingBegin
        {
            public Scene Scene;
        }

        public struct LoadingFinish
        {
            public Scene Scene;
        }

        public struct EnterMapFinish
        {
            public Scene ZoneScene;
        }

        public struct AfterUnitCreate
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
        }
        public struct AdventureRoundReset
        {
            public Scene ZoneScene;
        }
        public struct AdventureBattleRoundView
        {
            public Scene zongScene;
            public Unit  attackUnit;
            public Unit  monsterUnit;
        }
        /// <summary>
        /// 战斗结束
        /// </summary>
        public struct AdventureBattleOver
        {
            public Scene zongScene;
            public Unit  winUnit;
        }
        /// <summary>
        /// 战报
        /// </summary>
        public struct AdventureBattleReport
        {
            public Scene zongScene;
            public BattleRoundResult battleRoundResult;
            public int round;
        }
    }
}