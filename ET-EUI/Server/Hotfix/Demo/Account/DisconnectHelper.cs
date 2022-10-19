﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    public static class DisconnectHelper
    {
        public static async ETTask Disconnect(this Session self)
        {
            if (self.IsDisposed || self == null)
            {
                return;
            }
            long sessionID = self.InstanceId;
            await TimerComponent.Instance.WaitAsync(1000);
            if (sessionID != self.InstanceId)
            {
                return;
            }
            self.Dispose();
        }
        /// <summary>
        /// 踢下线
        /// </summary>
        /// <param name="player"></param>
        /// <param name="isException">是否异常</param>
        /// <returns></returns>
        public static async ETTask KickPlayer(Player player,bool isException = false)
        {
            if (player == null || player.IsDisposed)
            {
                return;
            }
            long instanceId = player.InstanceId;
            using (await CoroutineLockComponent.Instance.Wait(CoroutineLockType.LoginGate,player.AccountId.GetHashCode()))
            {
                //为了防止多线程的，异步加载打乱逻辑
                if (player.IsDisposed || instanceId != player.InstanceId)
                {
                    return ;
                }
                if (!isException)
                {
                    switch (player.PlayerState)
                    {
                        case PlayerState.Disconnect:
                            break;
                        case PlayerState.Gate:
                            break;
                        case PlayerState.Game:
                            //通知游戏逻辑服下线Unit角色逻辑，并将数据存入数据库
                            var m2GRequestExitGame = (M2G_RequestExitGame)await MessageHelper.CallLocationActor(player.UnitId, new G2M_RequestExitGame());
                            //通知移除账号角色登录信息
                            long LoginCenterConfigSceneId=  StartSceneConfigCategory.Instance.LoginCenterConfig.InstanceId;
                            var L2G_RemoveLoginRecord = (L2G_RemoveLoginRecord)await MessageHelper.CallActor(LoginCenterConfigSceneId,
                                new G2L_RemoveLoginRecord()
                                {
                                    AccountId = player.AccountId,
                                    ServerId = player.DomainZone()
                                }) ;
                            //聊天服下线Unit
                            var chat2GRequestExitChat = (Chat2G_RequestExitChat)await MessageHelper.CallActor(player.ChatInfoInstanceId, new G2Chat_RequestExitChat());
                            break;
                        default:
                            break;
                    }
                    player.PlayerState = PlayerState.Disconnect;
                    player.DomainScene().GetComponent<PlayerComponent>()?.Remove(player.AccountId);
                    player?.Dispose();
                    await TimerComponent.Instance.WaitAsync(300);
                }
            }
        }
    }
}
