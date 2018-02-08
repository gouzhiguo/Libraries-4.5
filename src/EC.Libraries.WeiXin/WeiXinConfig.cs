namespace EC.Libraries.WeiXin
{
    /// <summary>
    /// 微信配置
    /// </summary>
    public class WeiXinConfig
    {
        /// <summary>
        /// 商户账号appid
        /// </summary>
        public string AppId { set; get; }

        /// <summary>
        /// 商户账号appSecret
        /// </summary>
        public string AppSecret { set; get; }

        /// <summary>
        /// 商户号
        /// </summary>
        public string MchId { set; get; }

        /// <summary>
        /// 密钥
        /// </summary>
        public string ApiKey { get; set; }

        /// <summary>
        /// 回调地址
        /// </summary>
        public string RedirectUrl { get; set; }
        
        /// <summary>
        /// 支付回调址
        /// </summary>
        public string NotifyUrl { get; set; }
        
    }
}
