using System;
using EC.Libraries.Framework;

namespace EC.Libraries.Lock
{
    /// <summary>
    /// 锁接口
    /// </summary>
    public interface ILockProvider : IProxyBaseObject<ILockProvider>
    {
        LockResult Lock(string appId, string syncId, Action syncFunc);

        /// <summary>
        /// 加锁
        /// </summary>
        /// <returns>是否取的锁</returns>
        LockResult<T> Lock<T>(string appId, string syncId, Func<T> syncFunc);
    }
}
