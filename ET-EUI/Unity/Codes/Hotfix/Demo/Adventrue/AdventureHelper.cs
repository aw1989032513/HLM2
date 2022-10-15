using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    public static class AdventureHelper
    {
        public static async ETTask<int> RequestStartGameLevel(Scene zoneScene, int levelId)
        {
            //这里可以请求加载游戏地图。发送C2G_EnterMapHandler


            M2C_StartGameLevel m2C_StartGameLevel = null;
            try
            {
                m2C_StartGameLevel = (M2C_StartGameLevel)await zoneScene.GetComponent<SessionComponent>().Session.Call(new C2M_StartGameLevel() { LevelId = levelId });
            }
            catch (Exception e)
            {
                Log.Error(e.ToString());
                throw;
            }
            if (m2C_StartGameLevel.Error != ErrorCode.ERR_Success)
            {
                Log.Error(m2C_StartGameLevel.Error.ToString());
                return m2C_StartGameLevel.Error;
            }
            return ErrorCode.ERR_Success;
        }

        public static async ETTask <int> RequestEndGameLevel(Scene zoneScene, BattleRoundResult battleRoundResult, int round)
        {
            M2C_EndGameLevel m2CEndGameLevel = null;
            try
            {
                m2CEndGameLevel = (M2C_EndGameLevel)await zoneScene.GetComponent<SessionComponent>().Session.Call(new C2M_EndGameLevel()
                {
                    BattleResult = (int)battleRoundResult,
                    Round = round
                });
            }
            catch (Exception e)
            {
                Log.Error(e.ToString());
                return ErrorCode.ERR_NetWorkError;
            }

            if (m2CEndGameLevel.Error != ErrorCode.ERR_Success)
            {
                Log.Error(m2CEndGameLevel.Error.ToString());
                return m2CEndGameLevel.Error;
            }

            return ErrorCode.ERR_Success;
        }
}
}
