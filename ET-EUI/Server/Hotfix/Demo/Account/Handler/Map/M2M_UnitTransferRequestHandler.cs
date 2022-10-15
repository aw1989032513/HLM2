using System;
using UnityEngine;

namespace ET
{
	/// <summary>
	/// 从Gate网关传送到Map逻辑服务器
	/// </summary>
	[ActorMessageHandler]
	public class M2M_UnitTransferRequestHandler : AMActorRpcHandler<Scene, M2M_UnitTransferRequest, M2M_UnitTransferResponse>
	{
		protected override async ETTask Run(Scene mapScene, M2M_UnitTransferRequest request, M2M_UnitTransferResponse response, Action reply)
		{
			await ETTask.CompletedTask;
			UnitComponent unitComponent = mapScene.GetComponent<UnitComponent>();
			Unit unit = request.Unit;

			//因为Unit需要反序列化，所以需要AddChild
			unitComponent.AddChild(unit);
			unitComponent.Add(unit);

			///遍历所有的unit身上 的Entity组件，重新添加
			foreach (Entity entity in request.Entitys)
			{
				unit.AddComponent(entity);
			}
		
			
			unit.AddComponent<MailBoxComponent>();
			
			// 通知客户端创建My Unit
			M2C_CreateMyUnit m2CCreateUnits = new M2C_CreateMyUnit();
			//UnitHelper 拿到UnitInfo
			m2CCreateUnits.Unit = UnitHelper.CreateUnitInfo(unit);
			MessageHelper.SendToClient(unit, m2CCreateUnits);

			//通知客户端同步下背包
			ItemUpdateNoticeHelper.SyncAllBagItems(unit);
			ItemUpdateNoticeHelper.SyncAllEquipItems(unit);

			// 加入数值变化监听组件
			unit.AddComponent<NumericNoticeComponent>();
			//加入延迟存入数据库方法
			unit.AddComponent<UnitDBSaveComponent>();
			//加入 闯关检查组件
			unit.AddComponent<AdventureCheckComponent>();
			// 加入aoi组件
			unit.AddComponent<AOIEntity, int, Vector3>(9 * 1000, unit.Position);

			response.NewInstanceId = unit.InstanceId;
			
			reply();
		}
	}
}