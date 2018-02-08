namespace EC.Libraries.RedisLock
{
    /// <summary>
    /// 线程同步结果
    /// </summary>
    public class LockResult
    {
        /// <summary>
        /// 状态
        /// </summary>
        public bool Status { get; set; }

        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; set; }
    }

    /// <summary>
    /// 线程同步结果
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class LockResult<T> : LockResult
    {
        /// <summary>
        /// 方法的返回数据
        /// </summary>
        public T Data { get; set; }
    }
}
