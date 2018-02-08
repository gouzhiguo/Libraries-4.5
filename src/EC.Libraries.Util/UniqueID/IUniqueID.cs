namespace EC.Libraries.Util.UniqueID
{
    /// <summary>
    /// 生成唯一ID接口
    /// </summary>
    /// <remarks>2015-06-02 杨合余 添加</remarks>
    internal interface IUniqueID
    {
        /// <summary>
        /// 生成UID
        /// </summary>
        /// <returns></returns>
        long NextId();
    }
}
