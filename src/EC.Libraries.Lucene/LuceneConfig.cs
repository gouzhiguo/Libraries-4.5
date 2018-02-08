namespace EC.Libraries.Lucene
{
    using EC.Libraries.Framework;

    /// <summary>
    /// 缓存配置实体
    /// </summary>
    public class LuceneConfig : BaseConfig
    {
        /// <summary>
        /// 连接缓存服务器的Url，若使用Local，则此值无效
        /// </summary>
        public string Path { set; get; }
    }
}
