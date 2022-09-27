using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    public enum AccountType
    {
        General=0,
        BlackList=1,
    }
    public class Account: Entity,IAwake
    {
        public string accountName;
        public string Password;
        public long CreateTime;
        public int accountType;
    }
}
