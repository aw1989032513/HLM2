using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    [Timer(TimerType.AccountSessionCheckOutTime)]
    public class AccountSessionCheckOutTimer : ATimer<AccountCheckOutTimeComponent>
    {
        public override void Run(AccountCheckOutTimeComponent self)
        {
            try
            {
                self.DeleteSession();
            }
            catch (Exception e)
            {
                Log.Error(e.ToString());
                throw;
            }
        }
    }

    public class AccountCheckOutTimeComponentAwakeSystem : AwakeSystem<AccountCheckOutTimeComponent, long>
    {
        public override void Awake(AccountCheckOutTimeComponent self, long accountID)
        {
            self.accountID = accountID;
            TimerComponent.Instance.Remove(ref self.timer);
            self.timer = TimerComponent.Instance.NewOnceTimer(TimeHelper.ServerNow() + 600000, TimerType.AccountSessionCheckOutTime, self);
        }
    }

    public class AccountCheckOutTimeComponentDestorySystem : DestroySystem<AccountCheckOutTimeComponent>
    {
        public override void Destroy(AccountCheckOutTimeComponent self)
        {
            self.accountID = 0;
            TimerComponent.Instance.Remove(ref self.timer);
        }
    }
    public static class AccountCheckOutTimeComponentSystem
    {

        public static void DeleteSession(this AccountCheckOutTimeComponent self)
        {
            Session session = self.GetParent<Session>();
            long sessionInstancID = session.DomainScene().GetComponent<AccountSessionsComponent>().Get(self.accountID);
            if (sessionInstancID == session.InstanceId)
            {
                session.DomainScene().GetComponent<AccountSessionsComponent>().Remove(self.accountID);
            }
            session?.Send(new A2C_Disconnect() { Error = 1 });
            session?.Disconnect().Coroutine();
        }
    }
}
