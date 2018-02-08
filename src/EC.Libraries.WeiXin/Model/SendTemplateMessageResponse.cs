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
    public class SendTemplateMessageResponse
    {
        /// <summary>
        /// 错误编号
        /// </summary>
        public string errcode { get; set; }

        /// <summary>
        /// 错误信息
        /// </summary>
        public string errmsg { get; set; }

        /// <summary>
        /// 公众账号apiKey
        /// </summary>
        public string msgid { get; set; }
    }
}
