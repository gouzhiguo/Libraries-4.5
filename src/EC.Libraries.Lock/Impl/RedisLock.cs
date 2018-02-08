using System;
using System.Collections.Concurrent;
using System.Configuration;
using System.Threading;
using EC.Libraries.Framework;
using StackExchange.Redis;

namespace EC.Libraries.Lock
{
    /// <summary>
    /// 分布式锁
    /// </summary>
    /// <remarks>2014-08-19 杨合余 添加</remarks>
    internal class RedisLock : ILock
    {
        private string distributedLockKey="";
        private string redisServerList = "";
        /// <summary>
        /// 初始化单机锁
        /// </summary>
        public RedisLock(LockConfig config)
        {

            if (!string.IsNullOrEmpty(config.RedisServerList))
            {
                redisServerList = config.RedisServerList;
            }
            else
            {
                var librariesConfig = ConfigurationManager.GetSection("LibrariesConfig") as LibrariesConfig;
                if (librariesConfig != null)
                {
                    var cacheConfig = librariesConfig.GetObjByXml<CacheConfig>("CacheConfig");

                    if (cacheConfig == null || string.IsNullOrEmpty(cacheConfig.Url))
                        throw new Exception("缺少缓存配置CacheConfig");
                    redisServerList = cacheConfig.Url;
                }
            }
        }

        /// <summary>
        /// 加锁
        /// </summary>
        /// <returns>是否取得锁</returns>
        public SyncResult Lock(string appId, string syncId, Action syncFunc)
        {
           distributedLockKey = appId + "_" + syncId;
           var res = new SyncResult();

            var dlm = new Redlock.CSharp.Redlock(redisServerList.Split(','));

            Redlock.CSharp.Lock lockObject;
            var locked = dlm.Lock(distributedLockKey, new TimeSpan(0, 0, 10), out lockObject);
            if (!locked)
            {
                res.IsError = true;
                res.Message = "获取锁失败";
                return res;
            }
            try
            {
                //执行线程安全方法
                if (syncFunc != null)
                     syncFunc();
            }
            finally
            {
                dlm.Unlock(lockObject);
            }
          return res;
        }

        public SyncResult<T> Lock<T>(string appId, string syncId, Func<T> syncFunc)
        {
            var res = new SyncResult<T>();
            distributedLockKey = appId + "_" + syncId;

            var dlm = new Redlock.CSharp.Redlock(redisServerList.Split(','));

            Redlock.CSharp.Lock lockObject;
            var locked = dlm.Lock(distributedLockKey, new TimeSpan(0, 0, 10), out lockObject);
            if (!locked)
            {
                res.IsError = true;
                res.Message = "获取锁失败";
                return res;
            }
            try
            {
                //执行线程安全方法
                if (syncFunc != null)
                    res.Data=syncFunc();
            }
            finally
            {
                //if (_type == LockType.DistributedLock)
                dlm.Unlock(lockObject);
            }
            return res;
    }
    }
}
