﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    public class C2G_EnterGameHandler : AMRpcHandler<C2G_EnterGame, G2C_EnterGame>
    {
        protected override async ETTask Run(Session session, C2G_EnterGame request, G2C_EnterGame response, Action reply)
        {
            if (session.DomainScene().SceneType != SceneType.Gate)
            {
                Log.Error($"请求的Scene错误，当前Scene为：{session.DomainScene().SceneType}");
                session.Dispose();
                return;
            }

            if (session.GetComponent<SessionLockingComponent>() != null)
            {
                response.Error = ErrorCode.ERR_RequestRepeatedly;
                reply();
                return;
            }

            SessionPlayerComponent sessionPlayerComponent= session.GetComponent<SessionPlayerComponent>();
            if (sessionPlayerComponent == null)
            {
                response.Error = ErrorCode.ERR_SessionPlayerError;
                reply();
                return;
            }

            Player player = Game.EventSystem.Get(sessionPlayerComponent.playerInstanceId) as Player;
            if (player == null || player.IsDisposed)
            {
                response.Error = ErrorCode.ERR_NonePlayerError;
                reply();
                return;
            }

            long instanceId = session.InstanceId;
            using (session.AddComponent<SessionLockingComponent>())
            {
                using (await CoroutineLockComponent.Instance.Wait(CoroutineLockType.LoginGate, player.AccountId.GetHashCode()))
                {
                    if (instanceId != session.InstanceId || player.IsDisposed)
                    {
                        response.Error = ErrorCode.ERR_NonePlayerError;
                        reply();
                        return;
                    }
                                                    //如果Session已经在游戏中了  就Return                                          
                    if (session.GetComponent<SessionStateComponent>() !=null && session.GetComponent<SessionStateComponent>().state == SessionState.Game)
                    {
                        response.Error = ErrorCode.ERR_SessionStateError;
                        reply();
                        return;
                    }
                    //如果正在游戏中
                    if (player.PlayerState == PlayerState.Game)
                    {
                        try
                        {
                            IActorResponse reqEnter = await MessageHelper.CallLocationActor(player.UnitId, new G2M_RequestEnterGameState());
                            if (reqEnter.Error == ErrorCode.ERR_Success)//正在游戏中
                            {
                                reply();
                                return;
                            }
                            else
                            {
                                Log.Error("二次登录失败" + reqEnter.Error + "|" + reqEnter.Message);
                                response.Error = ErrorCode.ERR_EnterGameError;
                                await DisconnectHelper.KickPlayer(player,true);
                                reply();
                                session?.Disconnect().Coroutine();
                            }

                        }
                        catch (Exception e)
                        {
                            Log.Error("二次登录失败" +e.ToString());
                            response.Error = ErrorCode.ERR_EnterGameError;
                            await DisconnectHelper.KickPlayer(player, true);
                            reply();
                            session?.Disconnect().Coroutine();
                            throw;
                        }
                        return;
                    }
                    else if (player.PlayerState == PlayerState.Gate)
                    {
                        try
                        {
                            //Gate上的player 复制个影子，叫做unit
                            //从数据库或者缓存中加载出Unity实体及其相关组件
                            (bool isNewPlayer,Unit unit) = await UnitHelper.LoadUnit(player);
                          
                            unit.AddComponent<UnitGateComponent, long>(player.InstanceId);
                           
                            //玩家Unit上线后的初始化操作
                            await UnitHelper.InitUnit(unit, isNewPlayer);

                            //给Unit从Gate服务器Scene移动到Map逻辑服务器上
                            long unitId = unit.Id;
                            player.UnitId = unitId;
                            response.MyId = unit.Id;
                            reply();

                            StartSceneConfig startSceneConfig = StartSceneConfigCategory.Instance.GetBySceneName(session.DomainZone(), "Game");
                            await TransferHelper.Transfer(unit, startSceneConfig.InstanceId, startSceneConfig.Name);

                           
                           

                            SessionStateComponent sessionStateComponent = session.GetComponent<SessionStateComponent>();
                            if (sessionStateComponent ==null)
                            {
                                sessionStateComponent = session.AddComponent<SessionStateComponent>();
                            }
                            sessionStateComponent.state = SessionState.Game;
                            player.PlayerState = PlayerState.Game;
                        }
                        catch (Exception e)
                        {
                            Log.Error($"角色进入游戏逻辑服出现问题 账号Id: {player.AccountId}  角色Id: {player.Id}   异常信息： {e.ToString()}");
                            response.Error = ErrorCode.ERR_EnterGameError;
                            reply();
                            await DisconnectHelper.KickPlayer(player, true);
                            session.Disconnect().Coroutine();
                        }
                    }
                     


                }
            }
                    await ETTask.CompletedTask;

        }
    }
}
