using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EC.Libraries.Lock
{
    internal class LockProvider : ILockProvider
    {
        private string lockKey = "";

        /// <summary>
        /// 操作加锁保证线程安全
        /// </summary>
        /// <param name="syncFunc">线程同步方法</param>
        /// <returns></returns>
        public LockResult Lock(string appId, string syncId, Action syncFunc)
        {
            lockKey = appId + "_" + syncId;

            var response = new LockResult()
            {
                Status = false
            };

            var redlock = new Redlock("".Split(','));

            Lock lockObj;

            redlock.Lock(lockKey)
            //return LockObj.Lock(appId, syncId,syncFunc);
        }

        /// <summary>
        /// 操作加锁保证线程安全
        /// </summary>
        /// <param name="syncFunc">线程同步方法</param>
        /// <returns></returns>
        public LockResult<T> Lock<T>(string appId, string syncId, Func<T> syncFunc)
        {
            //return LockObj.Lock<T>(appId, syncId, syncFunc);
            return null;
        }

        public void Dispose()
        {
        }
    }
}
