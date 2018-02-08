using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EC.Libraries.WeiXin.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class SendTemplateMessageRequest
    {
        /// <summary>
        /// accessToken
        /// </summary>
        public string AccessToken { get; set; }
        /// <summary>
        /// 接收信息者的微信openId
        /// </summary>
        public string OpenId { get; set; }
        /// <summary>
        /// 选择编码
        /// </summary>
        public string TemplateId { get; set; }
        /// <summary>
        /// 数据
        /// </summary>
        public object Data { get; set; }
        /// <summary>
        /// 微信链接地址
        /// </summary>
        public string Url { get; set; }
    }
}
