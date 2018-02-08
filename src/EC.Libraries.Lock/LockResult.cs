namespace EC.Libraries.Lock
{
    #region 基础结果返回

    /// <summary>
    /// 基础结果返回
    /// </summary>
    public class LockResult
    {
        /// <summary>
        /// 状态
        /// </summary>
        public bool Status { get; set; }

        /// <summary>
        /// 状态代码
        /// </summary>
        public string StatusCode { get; set; }

        /// <summary>
        /// 状态消息
        /// </summary>
        public string Message { get; set; }
    }

    #endregion

    #region 数据结果返回

    /// <summary>
    /// 数据结果返回
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class LockResult<T> : LockResult
    {
        /// <summary>
        /// 数据
        /// </summary>
        public T Data { get; set; }
    }

    #endregion

    
}
