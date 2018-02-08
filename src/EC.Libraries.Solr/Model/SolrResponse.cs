using System.Collections.Generic;

namespace EC.Libraries.Solr
{
    /// <summary>
    /// Solr返回对象
    /// </summary>
    public class SolrResponse
    {
        /// <summary>
        /// 查询响应头信息对象
        /// </summary>
        public Header ResponseHeader { get; set; }

        /// <summary>
        /// 错误对象信息，Newtonsoft对象
        /// </summary>
        public Error Error { get; set; }

        /// <summary>
        /// 结果状态
        /// </summary>
        public bool Status
        {
            get
            {
                return ResponseHeader != null || ResponseHeader.Status == 0;
            }
        }
    }

    /// <summary>
    /// 查询返回对象
    /// </summary>
    /// <typeparam name="T">查询结果对象类型</typeparam>
    public class SolrResponse<T> : SolrResponse
    {
        /// <summary>
        /// 查询响应信息，包括总记录数和结果集合
        /// </summary>
        public Body<T> Response { get; set; }

        /// <summary>
        /// 查询高亮信息，Newtonsoft对象
        /// </summary>
        public JObject HighLighting { get; set; }

        private Dictionary<string, Dictionary<string, string>> _highLight;

        /// <summary>
        /// 高亮信息格式化为字典
        /// <para>Key为对象主键</para>
        /// <para>Value为设置高亮字段的数组结果</para>
        /// </summary>
        public Dictionary<string, Dictionary<string, string>> HighLight
        {
            get
            {
                if (_highLight == null)
                {
                    _highLight = new Dictionary<string, Dictionary<string, string>>();
                    if (HighLighting != null)
                    {
                        foreach (var item in HighLighting)
                        {
                            var value = new Dictionary<string, string>();
                            foreach (JProperty inner in item.Value)
                            {
                                value.Add(inner.Name, inner.Value[0].ToString());
                            }
                            _highLight.Add(item.Key, value);
                        }
                    }
                }

                return _highLight;
            }
        }
    }

    /// <summary>
    /// 查询响应头信息
    /// </summary>
    public class Header
    {
        /// <summary>
        /// 查询状态，当值为0是为正确结果
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 查询响应时间
        /// </summary>
        public int QTime { get; set; }
    }

    /// <summary>
    /// 查询响应内容
    /// </summary>
    /// <typeparam name="T">返回结果对象类型</typeparam>
    public class Body<T>
    {
        /// <summary>
        /// 查询记录总数
        /// </summary>
        public int NumFound { get; set; }

        /// <summary>
        /// 查询结果集合
        /// </summary>
        public IEnumerable<T> Docs { get; set; }
    }

    /// <summary>
    /// 错误消息对象
    /// </summary>
    public class Error
    {
        /// <summary>
        /// 元数据
        /// </summary>
        public string[] MetaData { get; set; }
        /// <summary>
        /// 错误消息
        /// </summary>
        public string Msg { get; set; }
        /// <summary>
        /// 错误代码
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// 重写格式化输出
        /// </summary>
        /// <returns>错误消息输出字符串</returns>
        public override string ToString()
        {
            return string.Format("Code:{0}\r\nError Message:{1}\r\nMetaData:{2}",
                this.Code,
                this.Msg,
                ((this.MetaData != null && this.MetaData.Length > 0) ? string.Join(",", this.MetaData) : "NULL"));
        }
    }
}
