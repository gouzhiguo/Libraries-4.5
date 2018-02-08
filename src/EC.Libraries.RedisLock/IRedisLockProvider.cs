using System;
using EC.Libraries.Framework;

namespace EC.Libraries.RedisLock
{
    /// <summary>
    /// 加锁保证线程安全
    /// </summary>
    public interface IRedisLockProvider : IProxyBaseObject<IRedisLockProvider>
    {
        /// <summary>
        /// 加锁
        /// </summary>
        /// <returns>是否取得锁</returns>
        LockResult Lock(string appId, string id, Action func);
    }
}
