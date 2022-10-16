using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    public static class ForgeHelper
    {
        /// <summary>
        /// 开始生产物品
        /// </summary>
        /// <param name="ZoneScene"></param>
        /// <param name="productionConfigId"></param>
        /// <returns></returns>
        public static async ETTask<int> StartProduction(Scene ZoneScene, int productionConfigId)
        {
            //判断材料是否存在
            if (!ForgeProductionConfigCategory.Instance.Contain(productionConfigId))
            {               
                return ErrorCode.ERR_ProductionConfigIdNotExit;
            }
            //判定生产材料数量是否满足
            var config = ForgeProductionConfigCategory.Instance.Get(productionConfigId);
            int materailCount = UnitHelper.GetMyUnitNumericComponent(ZoneScene.CurrentScene()).GetAsInt(config.ConsumId);
            if (materailCount < config.ConsumeCount)
            {
                return ErrorCode.ERR_MaterailCountLess;
            }

            M2C_StartProduction m2C_StartProduction = null;
           
            try
            {
                m2C_StartProduction = (M2C_StartProduction)await ZoneScene.GetComponent<SessionComponent>().Session.Call(new C2M_StartProduction()
                { ConfigId = productionConfigId });
            }
            catch (Exception e)
            {
                Log.Error(e.ToString());
                return ErrorCode.ERR_NetWorkError;
            }
            if (m2C_StartProduction.Error != ErrorCode.ERR_Success)
            {
                return m2C_StartProduction.Error;
            }
            //增加 或者 更新 生产队列
            ZoneScene.GetComponent<ForgeComponent>().AddOrUpdateProductionQueue(m2C_StartProduction.ProductionProto);
            return ErrorCode.ERR_Success;

        }
        /// <summary>
        /// 请求获取生产好的物品
        /// </summary>
        /// <param name="scene"></param>
        /// <param name="productionId"></param>
        /// <returns></returns>
        public static async Task<int> ReceivedProductionItem(Scene ZoneScene, long productionId)
        {
            //背包
            if (ZoneScene.GetComponent<BagComponent>().IsMaxLoad())
            {
                return ErrorCode.ERR_BagMaxLoad;
            }
            M2C_ReceiveProduction m2C_ReceiveProduction = null;
            try
            {
                m2C_ReceiveProduction=( M2C_ReceiveProduction) await ZoneScene.GetComponent<SessionComponent>().Session.Call(new C2M_ReceiveProduction() { ProductionId = productionId });
            }
            catch (Exception e)
            {
                Log.Error(e.ToString());
                return ErrorCode.ERR_NetWorkError;
            }
            if (m2C_ReceiveProduction.Error != ErrorCode.ERR_Success)
            {
                return m2C_ReceiveProduction.Error;
            }

            //成功
            //更新production
            ZoneScene.GetComponent<ForgeComponent>().AddOrUpdateProductionQueue(m2C_ReceiveProduction.ProductionProto);
            return ErrorCode.ERR_Success;
        }
    }
}
