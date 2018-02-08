using System;
using EC.Libraries.Framework;

namespace EC.Libraries.RedisLock
{
    /// <summary>
    ///Lock事务锁管理
    /// </summary>
    internal class RedisLockProvider : IRedisLockProvider
    {
        /// <summary>
        /// Key
        /// </summary>
        private string lockKey = "";

        /// <summary>
        /// 锁对象
        /// </summary>
        private static object _lock = new object();

        /// <summary>
        /// Redis配置
        /// </summary>
        private static LockConfig _lockConfig = null;

        /// <summary>
        /// 获取所需的基础调用实体
        /// </summary>
        public IRedisLockProvider Instance
        {
            get { return this; }
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="config"></param>
        public void Initialize(BaseConfig config = null)
        {
            lock (_lock)
            {
                if (config != null) _lockConfig = config as LockConfig;

                if (_lockConfig == null)
                {
                    _lockConfig = Config.GetConfig<LockConfig>();

                    if (_lockConfig == null) throw new Exception("缺少LockConfig配置");
                }
            }
        }

        /// <summary>
        /// 加锁
        /// </summary>
        /// <returns>是否取得锁</returns>
        public LockResult Lock(string appId, string id, Action func)
        {
            var lockRespose = new LockResult()
            {
                Status = false
            };

            lockKey = appId + "_" + id;

            var redisLock = new Redlock(_lockConfig.Url.Split(','));

            //锁对象
            Lock lockObject;

            var locked = redisLock.Lock(lockKey, new TimeSpan(0, 0, 10), out lockObject);
            if (!locked)
            {
                lockRespose.Status = true;
                lockRespose.Message = "获取锁失败";
                return lockRespose;
            }
            try
            {
                //执行线程安全方法
                if (func != null)
                    func();
            }
            finally
            {
                redisLock.Unlock(lockObject);
            }
            return lockRespose;
        }

        public void Dispose()
        {
        }
    }
}
