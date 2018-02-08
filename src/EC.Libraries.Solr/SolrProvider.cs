using EC.Libraries.Framework;

namespace EC.Libraries.Solr
{
    /// <summary>
    /// Solr查询提供类
    /// </summary>
    public class SolrProvider : ISolrProvider
    {
        /// <summary>
        /// 锁对象
        /// </summary>
        private static object _lock = new object();

        /// <summary>
        /// Solr配置
        /// </summary>
        private static SolrConfig _solrConfig = null;

        /// <summary>
        /// 获取所需的基础调用实体
        /// </summary>
        public ISolrProvider Instance
        {
            get { return this; }
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="config"></param>
        public void Initialize(BaseConfig config = null)
        {
        }

        public void Dispose()
        {
            
        }
    }
}
