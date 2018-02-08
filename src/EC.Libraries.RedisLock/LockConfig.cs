namespace EC.Libraries.RedisLock
{
    using EC.Libraries.Framework;

    /// <summary>
    /// 缓存锁配置实体
    /// </summary>
    public class LockConfig : BaseConfig
    {
        /// <summary>
        /// 连接缓存服务器的Url，若使用Local，则此值无效
        /// </summary>
        public string Url { set; get; }
    }
}
