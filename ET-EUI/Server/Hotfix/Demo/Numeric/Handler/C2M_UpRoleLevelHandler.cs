using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    public class C2M_UpRoleLevelHandler : AMActorLocationRpcHandler<Unit, C2M_UpRoleLevel, M2C_UpRoleLevel>
    {
        protected override async ETTask Run(Unit unit, C2M_UpRoleLevel request, M2C_UpRoleLevel response, Action reply)
        {
            NumericComponent numericComponent = unit.GetComponent<NumericComponent>();
            int level = numericComponent.GetAsInt((int)NumericType.Level);
            PlayerLevelConfig playerLevelConfig = PlayerLevelConfigCategory.Instance.Get(level);
            int exp = numericComponent.GetAsInt((int)NumericType.Exp);
            if (exp < playerLevelConfig.NeedExp)
            {
                response.Error =  ErrorCode.ERR_ExpNotEnughError;
                reply();
                return;
            }
            long newExp = exp - playerLevelConfig.NeedExp;
            numericComponent[(int)NumericType.Exp] = newExp;
            numericComponent[(int)NumericType.Level] = level + 1;
            numericComponent[(int)NumericType.AttributePoint] += 5;

            reply();
            await ETTask.CompletedTask;
        }
    }
}
