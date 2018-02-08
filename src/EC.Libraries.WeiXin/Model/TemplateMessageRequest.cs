using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EC.Libraries.WeiXin.Model
{
    /// <summary>
    /// 公众号模板消息
    /// </summary>
    public class TemplateMessageRequest
    {
        public TemplateMessageRequest()
        {
            topcolor = "#FF0000";
        }
        /// <summary>
        /// 接收者微信OpenId
        /// </summary>
        public string touser { get; set; }
        /// <summary>
        /// 模板Id
        /// </summary>
        public string template_id { get; set; }
        /// <summary>
        /// 跳转url
        /// </summary>
        public string url { get; set; }
        /// <summary>
        /// 顶部颜色
        /// </summary>
        public string topcolor { get; set; }
        /// <summary>
        /// 具体模板数据
        /// </summary>
        public object data { get; set; }

    }
}
