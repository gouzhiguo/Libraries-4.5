using System;

namespace EC.Libraries.Solr
{
    /// <summary>
    /// Solr查询对象
    /// </summary>
    public class SolrRequest<T>
    {
        private Func<SolrRequest<T>, SolrResponse<T>> _provider;

        public SolrRequest(Func<SolrRequest<T>, SolrResponse<T>> provider)
        {
            _provider = provider;
            this.Start = 0;
            this.Rows = 10;
        }

        /// <summary>
        /// 查询条件字符串
        /// </summary>
        public string Query { get; private set; }

        /// <summary>
        /// 返回字段，以逗号分隔
        /// </summary>
        public string Fields { get; private set; }

        /// <summary>
        /// 高亮设置
        /// </summary>
        public string HighLights { get; private set; }

        /// <summary>
        /// 返回记录行数
        /// </summary>
        public int Rows { get; private set; }

        /// <summary>
        /// 返回记录开始位置
        /// </summary>
        public int Start { get; private set; }

        /// <summary>
        /// 排序字段字符串
        /// </summary>
        public string OrderByResult { get; private set; }

        /// <summary>
        /// 取得指定行数记录
        /// </summary>
        /// <param name="take">返回行数</param>
        /// <returns>请求对象</returns>
        public SolrRequest<T> Take(int take)
        {
            this.Rows = take;
            return this;
        }

        /// <summary>
        /// 路过指定行数记录
        /// </summary>
        /// <param name="skip">跳过行数</param>
        /// <returns>请求对象</returns>
        public SolrRequest<T> Skip(int skip)
        {
            this.Start = skip;
            return this;
        }

        /// <summary>
        /// 查询条件
        /// </summary>
        /// <param name="expression">条件表达式</param>
        /// <returns>请求对象</returns>
        public SolrRequest<T> Where(Expression<Func<T, bool>> expression)
        {
            if (!string.IsNullOrEmpty(Query)) Query += " AND ";
            Query += SolrExpressionCompiler.Compile(expression);
            return this;
        }

        /// <summary>
        /// 选择返回字段
        /// </summary>
        /// <param name="selector">选择表达式，可以使用匿名对象</param>
        /// <returns>请求对象</returns>
        public SolrRequest<T> Select(Expression<Func<T, object>> selector)
        {
            this.Fields = string.Join(",", SolrExpressionCompiler.CompileSelector(selector));
            return this;
        }

        /// <summary>
        /// 选择高亮字段
        /// </summary>
        /// <param name="selector">选择表达式，可以使用匿名对象</param>
        /// <param name="highLightElement">高亮包含元素，默认em</param>
        /// <param name="uniqueKey">Scheam中配置的主键属性名称，默认ID，注意需要区分大小写</param>
        /// <returns>请求对象</returns>
        public SolrRequest<T> HighLight(Expression<Func<T, object>> selector, string highLightElement = "em")
        {
            this.HighLights = string.Format("hl=on&hl.fl={0}&hl.simple.post=</{1}>&hl.simple.pre=<{1}>",
                string.Join(",", SolrExpressionCompiler.CompileSelector(selector)),
                highLightElement);

            return this;
        }

        /// <summary>
        /// 升序排序
        /// </summary>
        /// <param name="keySelector">排序表达式，可以使用匿名对象</param>
        /// <returns>请求对象</returns>
        public SolrRequest<T> OrderBy(Expression<Func<T, object>> keySelector)
        {
            OrderByResult = string.Join(",", SolrExpressionCompiler.CompileSelector(keySelector).Select(p => p + " ASC"));

            return this;
        }

        /// <summary>
        /// 降序排序
        /// </summary>
        /// <param name="keySelector">排序表达式，可以使用匿名对象</param>
        /// <returns>请求对象</returns>
        public SolrRequest<T> OrderByDescending(Expression<Func<T, object>> keySelector)
        {
            OrderByResult = string.Join(",", SolrExpressionCompiler.CompileSelector(keySelector).Select(p => p + " DESC"));

            return this;
        }

        /// <summary>
        /// 二次升序排序
        /// </summary>
        /// <param name="keySelector">排序表达式，可以使用匿名对象</param>
        /// <returns>请求对象</returns>
        public SolrRequest<T> ThenBy(Expression<Func<T, object>> keySelector)
        {
            if (!string.IsNullOrEmpty(OrderByResult)) OrderByResult += ",";

            OrderByResult += string.Join(",", SolrExpressionCompiler.CompileSelector(keySelector).Select(p => p + " ASC"));

            return this;
        }

        /// <summary>
        /// 二次降序排序
        /// </summary>
        /// <param name="keySelector">排序表达式，可以使用匿名对象</param>
        /// <returns>请求对象</returns>
        public SolrRequest<T> ThenByDescending(Expression<Func<T, object>> keySelector)
        {
            if (!string.IsNullOrEmpty(OrderByResult)) OrderByResult += ",";

            OrderByResult += string.Join(",", SolrExpressionCompiler.CompileSelector(keySelector).Select(p => p + " DESC"));

            return this;
        }

        /// <summary>
        /// 分页结果，默认返回前10条
        /// </summary>
        /// <param name="pageIndex">页索引</param>
        /// <param name="pageSize">页大小</param>
        /// <returns>请求结果</returns>
        public IPageList<T> Page(int pageIndex = 1, int pageSize = 10)
        {
            this.Start = (pageIndex - 1) * pageSize;
            this.Rows = pageSize;

            var result = _provider(this);

            return new PageList<T>(result.Response.Docs, pageIndex, pageSize, result.Response.NumFound);
        }

        /// <summary>
        /// 分页结果，默认返回前10条
        /// <para>通过out参数形式返回Response详细信息</para>
        /// </summary>
        /// <param name="response">Response详细信息对象，包括高亮、查询头信息等等</param>
        /// <param name="pageIndex">页索引</param>
        /// <param name="pageSize">页大小</param>
        /// <returns>请求结果</returns>
        public IPageList<T> Page(out SolrResponse<T> response, int pageIndex = 1, int pageSize = 10)
        {
            this.Start = (pageIndex - 1) * pageSize;
            this.Rows = pageSize;

            var result = _provider(this);

            response = result;

            return new PageList<T>(result.Response.Docs, pageIndex, pageSize, result.Response.NumFound);
        }
    }
}
