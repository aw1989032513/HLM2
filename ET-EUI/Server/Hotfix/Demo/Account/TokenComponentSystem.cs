using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    public static class TokenComponentSystem
    {
        /// <summary>
        /// Add
        /// </summary>
        /// <param name="self"></param>
        /// <param name="key">AccountId</param>
        /// <param name="token"> token</param>
        public static void Add(this TokenComponent self,long key,string token)
        {
            self.tokenDictionary.Add(key,token);
            TimeOutRemoveToken(self,key,token).Coroutine();
        }
        /// <summary>
        /// Get
        /// </summary>
        /// <param name="self"></param>
        /// <param name="key">AccountId</param>
        /// <returns></returns>
        public static string Get(this TokenComponent self, long key)
        {
            string value = null;
            self.tokenDictionary.TryGetValue(key,out value);
            return value;
        }
        public static void Remove(this TokenComponent self, long key)
        {
            if (self.tokenDictionary.ContainsKey(key))
            {
                self.tokenDictionary.Remove(key);
            }
        }
        /// <summary>
        /// 超过十分钟就移除令牌
        /// </summary>
        /// <param name="self"></param>
        /// <param name="key">AccountId</param>
        /// <param name="token"></param>
        /// <returns></returns>
        private static async ETTask TimeOutRemoveToken(this TokenComponent self,long key,string token)
        {
            await TimerComponent.Instance.WaitAsync(600000);  
            string tempToken=Get(self,key);
            if (!string.IsNullOrEmpty(tempToken) && tempToken==token)
            {
                self.Remove(key);
            }
        }
    }
}
