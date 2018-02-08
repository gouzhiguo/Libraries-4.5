using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Collections;

namespace EC.Libraries.WeiXin
{
    public class WXPayData
    {
        /// <summary>
        /// 请求的参数
        /// </summary>
        public Hashtable hashtable { get; set; }

        /// <summary>
        /// HttpContext
        /// </summary>
        protected HttpContext HttpContext;

        public WXPayData(HttpContext httpContext)
        {
            hashtable = new Hashtable();
            HttpContext = httpContext ?? HttpContext.Current;
        }

        /// <summary>
        /// 设置参数值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Add(string key, string value)
        {
            if (!string.IsNullOrWhiteSpace(key))
            {
                if (hashtable.Contains(value))
                {
                    hashtable.Remove(value);
                }
                hashtable.Add(key, value);
            }
        }

        /// <summary>
        /// 输出XML
        /// </summary>
        /// <returns></returns>
        public string ParseXML()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<xml>");
            foreach (string k in hashtable.Keys)
            {
                string v = (string)hashtable[k];
                if (v != null && Regex.IsMatch(v, @"^[0-9.]$"))
                {

                    sb.Append("<" + k + ">" + v + "</" + k + ">");
                }
                else
                {
                    sb.Append("<" + k + "><![CDATA[" + v + "]]></" + k + ">");
                }

            }
            sb.Append("</xml>");
            return sb.ToString();
        }

        /// <summary>
        /// 创建md5摘要,规则是:按参数名称a-z排序,遇到空值的参数不参加签名
        /// </summary>
        /// <param name="key">参数名</param>
        /// <param name="value">参数值</param>
        /// key和value通常用于填充最后一组参数
        /// <returns></returns>
        public virtual string CreateMd5Sign(string key, string value)
        {
            StringBuilder sb = new StringBuilder();

            ArrayList akeys = new ArrayList(hashtable.Keys);
            akeys.Sort(ASCIISort.Create());

            foreach (string k in akeys)
            {
                string v = (string)hashtable[k];
                if (null != v && "".CompareTo(v) != 0 && "sign".CompareTo(k) != 0 && "key".CompareTo(k) != 0)
                {
                    sb.Append(k + "=" + v + "&");
                }
            }

            sb.Append(key + "=" + value);

            string sign = WebUtil.GetMD5(sb.ToString(), Encoding.GetEncoding("UTF-8")).ToUpper();

            return sign;
        }
    }
}
