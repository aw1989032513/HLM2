using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    public class G2M_RequestEnterGameStateSystem : AMActorLocationRpcHandler<Unit, G2M_RequestEnterGameState, M2G_RequestEnterGameState>
    {
        /// <summary>
        /// 此方法当Unit为空时，会自动抛出错误码 ，当unit不为空的时候， 会走这个Run方法
        /// </summary>
        /// <param name="unit"></param>
        /// <param name="request"></param>
        /// <param name="response"></param>
        /// <param name="reply"></param>
        /// <returns></returns>
        protected override async ETTask Run(Unit unit, G2M_RequestEnterGameState request, M2G_RequestEnterGameState response, Action reply)
        {
            reply();
            await ETTask.CompletedTask;
        }
    }
}
