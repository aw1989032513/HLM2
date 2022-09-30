using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    public static class RoleInfoSystem
    {
        public static void DoAwake(this RoleInfo self,RoleInfoProto roleInfoProto)
        {
            self.Id=roleInfoProto.Id;
            self.Name = roleInfoProto.Name; 
            self.ServerId = roleInfoProto.ServerId; 
            self.State = roleInfoProto.State;   
            self.AccountId = roleInfoProto.AccountId;
            self.LastLoginTime =(int) roleInfoProto.LastLoginTime;
            self.CreateTime = (int)roleInfoProto.CreateTime;
        }

        public static RoleInfoProto ToMessage(this RoleInfo self)
        {
            return new RoleInfoProto()
            {
                Id = self.Id,
                Name = self.Name,
                ServerId = self.ServerId,
                State = self.State,
                AccountId = self.AccountId,
                LastLoginTime =(int)self.LastLoginTime,
                CreateTime =(int)self.CreateTime,

            };
        }
    }
}
