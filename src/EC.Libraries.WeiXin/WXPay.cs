using System;

using EC.Libraries.Util;
using EC.Libraries.WeiXin.Impl;

namespace EC.Libraries.WeiXin
{
    /// <summary>
    /// 支付
    /// </summary>
    public class WXPay
    {
        /// <summary>
        /// https://pay.weixin.qq.com/wiki/doc/api/jsapi.php?chapter=9_1
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static UnifiedOrderResponse UnifiedOrder(UnifiedOrderRequest request)
        {
            var response = new UnifiedOrderResponse()
            {
                return_code = "FAI"
            };

            string requestUrl = "https://api.mch.weixin.qq.com/pay/unifiedorder";

            WXPayData wxPayData = new WXPayData(null);
            wxPayData.Add("appid", request.appid);          //公众账号ID
            wxPayData.Add("mch_id", request.mch_id);        //商户号
            wxPayData.Add("nonce_str", Util.GetNoncestr()); //随机字符串
            wxPayData.Add("body", request.body);            //订单信息
            wxPayData.Add("out_trade_no", request.order_no); //订单号
            wxPayData.Add("total_fee", Convert.ToInt32(request.total_fee * 100).ToString());//商品金额,以分为单位(money * 100).ToString()
            wxPayData.Add("spbill_create_ip", request.spbill_create_ip); //用户的公网ip，不是商户服务器IP
            wxPayData.Add("notify_url", request.notify_url); //接收财付通通知的URL
            wxPayData.Add("trade_type", request.trade_type); //交易类型
            wxPayData.Add("openid", request.open_id);        //用户的openId
            var sign = wxPayData.CreateMd5Sign("apiKey", request.apiKey);
            wxPayData.Add("sign", sign);                     //签名

            var data = new HttpUtils().DoPost(requestUrl, wxPayData.ParseXML(), false);

            return response;
        }
    }
}
