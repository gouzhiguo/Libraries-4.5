using System.Diagnostics;
using System.Threading;
using EC.Libraries.Framework;
using EC.Libraries.RedisLock;
using EC.Libraries.Util;
using EC.Libraries.WeiXin.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EC.Libraries.WeiXin;

namespace EC.Libraries.UnitTest
{
    [TestClass]
    public class WeiXinTest
    {
        /// <summary>
        /// Redis分布式锁
        /// </summary>
        [TestMethod]
        public void TestMethod1()
        {
            using (var cliect = ClientProxy.GetInstance<IRedisLockProvider>())
            {
                var r = cliect.Lock("Order", "420", () =>
                {

                    Debug.WriteLine(Thread.CurrentThread.Name + " 获得锁!");
                    //Thread.Sleep(1200+i);
                    //Thread.Sleep(1000);
                });
                if (r.Status)
                {
                    Debug.WriteLine(Thread.CurrentThread.Name + " 获得锁成功!");
                }
            }
        }

        /// <summary>
        /// 统一下单
        /// </summary>
        [TestMethod]
        public void TestUnifiedOrder()
        {
            //var xml = "<xml><return_code><![CDATA[FAIL]]></return_code><return_msg><![CDATA[签名错误]]></return_msg></xml>";
            //var ss = SerializationUtil.GetStrForXmlDoc(xml, "return_code");
            WeiXinProvider wx = new WeiXinProvider();

            var result =wx.UnifiedOrder(new UnifiedOrderRequest()
            {
                appid = "wx9535283296d186c3",
                mch_id = "1491518502",
                body = "test",
                order_no = "11111112212",
                total_fee = new decimal(0.01),
                spbill_create_ip = "127.0.0.1",
                notify_url = "test",
                trade_type = trade_type.MWEB.ToString(),
                open_id = "o121v0wIoFtd8yWZiaezCVTU_Pqg",
                apiKey = "0bad12ce2b775367347d0b3a4bacd698"
            });

            Assert.IsNotNull(result);
        }

        /// <summary>
        /// 刷卡支付(公司微信信息、测试注意)
        /// </summary>
        [TestMethod]
        public void BarCodePay()
        {
            //<xml>
            //    <return_code><![CDATA[SUCCESS]]></return_code>
            //    <return_msg><![CDATA[OK]]></return_msg>
            //    <appid><![CDATA[wx9e1b2a5db4a6b3a7]]></appid>
            //    <mch_id><![CDATA[1459826002]]></mch_id>
            //    <nonce_str><![CDATA[f7c396ad36ae4746956dd5b505c565b7]]></nonce_str>
            //    <sign><![CDATA[6420AE0DDA685C4DA578159D0E0328A4]]></sign>
            //    <result_code><![CDATA[SUCCESS]]></result_code>
            //    <openid><![CDATA[oC39E1Zc6gflywBufl5zorgjioCc]]></openid>
            //    <is_subscribe><![CDATA[N]]></is_subscribe>
            //    <trade_type><![CDATA[MICROPAY]]></trade_type>
            //    <bank_type><![CDATA[CFT]]></bank_type>
            //    <total_fee>1</total_fee>
            //    <fee_type><![CDATA[CNY]]></fee_type>
            //    <transaction_id><![CDATA[4200000062201801111947971302]]></transaction_id>
            //    <out_trade_no><![CDATA[11111112212]]></out_trade_no>
            //    <attach><![CDATA[]]></attach>
            //    <time_end><![CDATA[20180111123141]]></time_end>
            //    <cash_fee>1</cash_fee>
            //    <cash_fee_type><![CDATA[CNY]]></cash_fee_type>
            //</xml>


            //<xml>
            //    <return_code><![CDATA[SUCCESS]]></return_code>
            //    <return_msg><![CDATA[OK]]></return_msg>
            //    <appid><![CDATA[wx9e1b2a5db4a6b3a7]]></appid>
            //    <mch_id><![CDATA[1459826002]]></mch_id>
            //    <nonce_str><![CDATA[EVoTOEBNCSmWsWRy]]></nonce_str>
            //    <sign><![CDATA[F12634F35DE62614179AFDEC24EEA9B2]]></sign>
            //    <result_code><![CDATA[FAIL]]></result_code>
            //    <err_code><![CDATA[ORDERPAID]]></err_code>
            //    <err_code_des><![CDATA[该订单已支付]]></err_code_des>
            //</xml>

            WeiXinProvider wx = new WeiXinProvider();
            var result = wx.BarCodePay(new BarCodePayRequest()
            {
                appid = "wx9e1b2a5db4a6b3a7",
                mch_id = "1459826002",
                body = "test",
                total_fee = new decimal(0.01),
                out_trade_no = "11111112212",
                spbill_create_ip = "127.0.0.1",
                auth_code = "134562262665136203",
                apiKey = "e24cb7d4adb546d5baadbb71efddABCD"

            });
            Assert.IsNotNull(result);
        }

        /// <summary>
        /// 刷卡支付查询订单(公司微信信息、测试注意)
        /// </summary>
        [TestMethod]
        public void BarCodePayQuery()
        {
            
            //<xml>
            //    <return_code><![CDATA[SUCCESS]]></return_code>
            //    <return_msg><![CDATA[OK]]></return_msg>
            //    <appid><![CDATA[wx9e1b2a5db4a6b3a7]]></appid>
            //    <mch_id><![CDATA[1459826002]]></mch_id>
            //    <nonce_str><![CDATA[bTkubY48VZTTCBzp]]></nonce_str>
            //    <sign><![CDATA[399ED36CB33B32D1AC79D6FC6A8AB896]]></sign>
            //    <result_code><![CDATA[SUCCESS]]></result_code>
            //    <openid><![CDATA[oC39E1Zc6gflywBufl5zorgjioCc]]></openid>
            //    <is_subscribe><![CDATA[N]]></is_subscribe>
            //    <trade_type><![CDATA[MICROPAY]]></trade_type>
            //    <bank_type><![CDATA[CFT]]></bank_type>
            //    <total_fee>1</total_fee>
            //    <fee_type><![CDATA[CNY]]></fee_type>
            //    <transaction_id><![CDATA[4200000062201801111947971302]]></transaction_id>
            //    <out_trade_no><![CDATA[11111112212]]></out_trade_no>
            //    <attach><![CDATA[]]></attach>
            //    <time_end><![CDATA[20180111123141]]></time_end>
            //    <trade_state><![CDATA[SUCCESS]]></trade_state>
            //    <cash_fee>1</cash_fee>
            //</xml>

            //<xml>
            //    <return_code><![CDATA[SUCCESS]]></return_code>
            //    <return_msg><![CDATA[OK]]></return_msg>
            //    <appid><![CDATA[wx9e1b2a5db4a6b3a7]]></appid>
            //    <mch_id><![CDATA[1459826002]]></mch_id>
            //    <nonce_str><![CDATA[i2tiigFzGX50YdwC]]></nonce_str>
            //    <sign><![CDATA[669530C7CCC5D742F6E707636FFF44E1]]></sign>
            //    <result_code><![CDATA[FAIL]]></result_code>
            //    <err_code><![CDATA[ORDERNOTEXIST]]></err_code>
            //    <err_code_des><![CDATA[order not exist]]></err_code_des>
            //</xml>
            WeiXinProvider wx = new WeiXinProvider();
            var result = wx.Orderquery(new OrderQueryRequest()
            {
                appid = "wx9e1b2a5db4a6b3a7",
                mch_id = "1459826002",
                out_trade_no = "111111122120",
                apiKey = "e24cb7d4adb546d5baadbb71efddABCD"

            });
            Assert.IsNotNull(result);
        }

        /// <summary>
        /// 企业打款
        /// </summary>
        [TestMethod]
        public void TestUniTransfers()
        {
            //<xml>
            //    <return_code><![CDATA[SUCCESS]]></return_code>
            //    <return_msg><![CDATA[CA_ERROR]]></return_msg>
            //    <mchid><![CDATA[1491518502]]></mchid>
            //    <result_code><![CDATA[FAIL]]></result_code>
            //    <err_code><![CDATA[CA_ERROR]]></err_code>
            //    <err_code_des><![CDATA[CA证书出错，请登录微信支付商户平台下载证书]]></err_code_des>
            //</xml>
            WeiXinProvider wx = new WeiXinProvider();

            var result = wx.Transfers(new TransfersRequest()
            {
                mch_appid = "wx9535283296d186c3",
                mchid = "1491518502",
                nonce_str = "1213546623546",
                partner_trade_no = "1111111222",
                openid = "o121v0wIoFtd8yWZiaezCVTU_Pqg",
                check_name = "苟治国",
                amount = new decimal(0.01),
                desc = "开发测试",
                spbill_create_ip = "192.168.0.1",
                apiKey = "0bad12ce2b775367347d0b3a4bacd698",
                isUseCert = true,
                cert = @"C:\cert\apiclient_cert.p12",
                password = "1491518502"
            });

            Assert.IsNotNull(result);
        }


        /// <summary>
        /// 申请退款
        /// </summary>
        [TestMethod]
        public void TestRefund()
        {
            //<xml>
            //    <return_code><![CDATA[FAIL]]></return_code>
            //    <return_msg><![CDATA[certificate not match]]></return_msg>
            //</xml>


            //<xml>
            //    <return_code><![CDATA[SUCCESS]]></return_code>
            //    <return_msg><![CDATA[OK]]></return_msg>
            //    <appid><![CDATA[wx9535283296d186c3]]></appid>
            //    <mch_id><![CDATA[1491518502]]></mch_id>
            //    <nonce_str><![CDATA[9XlZiG7Wv6O4axbi]]></nonce_str>
            //    <sign><![CDATA[8CFEF31C32B31FA4D1BC305767F937E8]]></sign>
            //    <result_code><![CDATA[SUCCESS]]></result_code>
            //    <transaction_id><![CDATA[4200000084201801144101832407]]></transaction_id>
            //    <out_trade_no><![CDATA[4b2722ea4dff4408911be0f77b53a2c9]]></out_trade_no>
            //    <out_refund_no><![CDATA[4b2722ea4dff4408911be0f77b53a2c9]]></out_refund_no>
            //    <refund_id><![CDATA[50000705522018011403107858953]]></refund_id>
            //    <refund_channel><![CDATA[]]></refund_channel>
            //    <refund_fee>1</refund_fee>
            //    <coupon_refund_fee>0</coupon_refund_fee>
            //    <total_fee>1</total_fee>
            //    <cash_fee>1</cash_fee>
            //    <coupon_refund_count>0</coupon_refund_count>
            //    <cash_refund_fee>1</cash_refund_fee>
            //</xml>

            WeiXinProvider wx = new WeiXinProvider();

            var result = wx.Refund(new RefundRequest()
            {
                appid = "wx9535283296d186c3",
                mch_id = "1491518502",
                nonce_str = "11111112212",
                //out_trade_no = "82",
                out_refund_no = "4b2722ea4dff4408911be0f77b53a2c9",
                refund_fee = new decimal(0.01),
                total_fee = new decimal(0.01),
                transaction_id = "4200000084201801144101832407",
                apiKey = "0bad12ce2b775367347d0b3a4bacd698",
                isUseCert = true,
                cert = @"C:/cert/apiclient_cert.p12",
                password = "1491518502"
            });

            Assert.IsNotNull(result);
        }

        /// <summary>
        /// 加密码公钥
        /// </summary>
        [TestMethod]
        public void GetPublickey()
        {
            //<xml>
            //    <return_code><![CDATA[SUCCESS]]></return_code>
            //    <return_msg><![CDATA[OK]]></return_msg>
            //    <result_code><![CDATA[SUCCESS]]></result_code>
            //    <mch_id><![CDATA[1491518502]]></mch_id>
            //    <pub_key><![CDATA[-----BEGIN RSA PUBLIC KEY-----
            //    MIIBCgKCAQEAwOY++Z1QD1sirm4FQz8nTU5NcDecRTAhumjlrchfvxowMf9FK/Ad
            //    NhbzF/IB0wSmb9+Z+8Ct9kYihW5L/ETek0s+Qdg0eFcZ4pqiGI9iStQ/57IZANE7
            //    7AUaKWWD1d81FJ6Xxb1KiNv/Jly1bSRowfFmpbDW0P9dATJLcSDJ2OTQZWfqj4/U
            //    Loyp04p530egiLsN/RPAClohCnu/zUjLrsBhi/8xUNwCbTRgEf9uFg4WnRblBcF+
            //    WTFPIFko4iAMBuso36ayw1YEokPTSDhs4pHj7JhOj5JFoVqgOh4UMhJZRGvvkS9v
            //    WXho4WWuWnyC8RfBUBbbr8YbOXFfB2Gm+wIDAQAB
            //    -----END RSA PUBLIC KEY-----
            //    ]]></pub_key>
            //</xml>
            WeiXinProvider wx = new WeiXinProvider();

            var result = wx.GetPublickey(new PublickeyRequest()
            {
                mch_id = "1491518502",
                nonce_str = "11111112212",
                isUseCert = true,
                apiKey = "0bad12ce2b775367347d0b3a4bacd698",
                cert = @"C:/cert/apiclient_cert.p12",
                password = "1491518502"
            });
            Assert.IsNotNull(result);
        }

        /// <summary>
        /// 获取AccessToken
        /// </summary>
        [TestMethod]
        public void TestGetAccessToken()
        {
            //var xml = "<xml><return_code><![CDATA[FAIL]]></return_code><return_msg><![CDATA[签名错误]]></return_msg></xml>";
            //var ss = SerializationUtil.GetStrForXmlDoc(xml, "return_code");
            WeiXinProvider wx = new WeiXinProvider();

            var response = wx.GetAccessToken("wx9535283296d186c3", "543e75c11909f438c02870ecbab85f5d");

            Assert.IsNotNull(response);
        }

        /// <summary>
        /// 创建菜单
        /// </summary>
        [TestMethod]
        public void TestCreateMenu()
        {
            WeiXinProvider wx = new WeiXinProvider();

            var menu = @" {  
                     ""button"":[
                      {  
                           ""name"":""千年大计"",  
                           ""sub_button"":[  
                            {  
                               ""type"":""view"",  
                               ""name"":""公司介绍"",  
                                ""url"":""http://www.1000n.com/about.html""  
                            },  
                            {  
                               ""type"":""view"",  
                               ""name"":""软件下载"",  
                               ""url"":""http://www.1000n.com""  
                            },  
                            {  
                               ""type"":""view"",  
                               ""name"":""企业文化"",  
                               ""url"":""http://www.1000n.com""  
                            }]  
                       },{  
                           ""name"":""会员中心"",  
                           ""sub_button"":[  
                            {  
                               ""type"":""view"",  
                               ""name"":""新手指南"",  
                                ""url"":""http://www.1000n.com/help.html""  
                            },  
                            {  
                               ""type"":""view"",  
                               ""name"":""代理中心"",  
                               ""url"":""http://weixin.1000n.com""  
                            },  
                            {  
                               ""type"":""view"",  
                               ""name"":""我的二维码"",  
                               ""url"":""http://www.1000n.com""  
                            }]  
                       },{  
                           ""name"":""联系我们"",  
                           ""sub_button"":[  
                            {  
                               ""type"":""view"",  
                               ""name"":""商务合作"",  
                                ""url"":""http://www.1000n.com/hezuo.html""  
                            },  
                            {  
                               ""type"":""view"",  
                               ""name"":""联系我们"",  
                               ""url"":""http://www.1000n.com/lianxi.html""  
                            },  
                            {  
                               ""type"":""view"",  
                               ""name"":""在线客服"",  
                               ""url"":""http://www.1000n.com""  
                            }]  
                       }]  
                }  
                ";
            var access_token = "6_0nbes5c9s2s4DWigMABnj2UwVED1H0evlZnMDCDUp8DB82TnsMdPur-jWA6QX0zd2OD3sgr8BUk0p0_eaTNJYCYNfvE2AqhUklOI23m8erGYV_puw0xRIJP3tighdhh-_7mh-a0KPhJ6Q8STZZZhADACCC";
            var createMenuResponse = wx.CreateMenu(access_token, menu);

            //var accessTokenResponse = wx.GetAccessToken("wx9535283296d186c3", "543e75c11909f438c02870ecbab85f5d");
            //if (accessTokenResponse.return_code.Equals(return_code.SUCCESS.ToString()) && accessTokenResponse.result_code.Equals(result_code.SUCCESS.ToString()))
            //{
//                var menu = @"
//                {
//                     ""button"":[
//                      {
//                           ""name"":""千年大计"",
//                           ""sub_button"":[
//                            {
//                               ""type"":""view"",
//                               ""name"":""公司介绍"",
//                               ""url"":""http://www.1000n.com/about.html""
//                            },
//                            {
//                               ""type"":""view"",
//                               ""name"":""软件下载"",
//                               ""url"":""http://www.1000n.com""
//                            },
//                            {
//                               ""type"":""view"",
//                               ""name"":""企业文化"",
//                               ""key"":""http://www.1000n.com""
//                            }]
//                      }
//                ]}";

                
            //}

        }

        /// <summary>
        /// 获取jsapiTicket
        /// </summary>
        [TestMethod]
        public void TestGetJSApiTicket()
        {

            WeiXinProvider wx = new WeiXinProvider();

            var response = wx.GetAccessToken("wx9535283296d186c3", "543e75c11909f438c02870ecbab85f5d");

            if (response.return_code.Equals(return_code.SUCCESS.ToString()) && response.result_code.Equals(result_code.SUCCESS.ToString()))
            {
                var jsapiTicketResponse = wx.GetTicket("wx9535283296d186c3", response.access_token, "http://www.baidu.com");
                Assert.IsNotNull(jsapiTicketResponse);
            }
            
        }

        /// <summary>
        /// 授权
        /// </summary>
        [TestMethod]
        public void GetAuthorize()
        {
            WeiXinProvider wx = new WeiXinProvider();

            var result = wx.GetAuthorize("wx9535283296d186c3", "http://www.baidu.com", "000");

            Assert.IsNotNull(result);
        }

        /// <summary>
        /// 支付结果通知 https://pay.weixin.qq.com/wiki/doc/api/jsapi.php?chapter=9_7
        /// </summary>
        [TestMethod]
        public void OrderNotify()
        {
            //<xml>
            //    <appid><![CDATA[wx9535283296d186c3]]></appid>
            //    <bank_type><![CDATA[CFT]]></bank_type>
            //    <cash_fee><![CDATA[1]]></cash_fee>
            //    <fee_type><![CDATA[CNY]]></fee_type>
            //    <is_subscribe><![CDATA[Y]]></is_subscribe>
            //    <mch_id><![CDATA[1491518502]]></mch_id>
            //    <nonce_str><![CDATA[5D078CEBD641FB4524960067EDE27592]]></nonce_str>
            //    <openid><![CDATA[o121v0wIoFtd8yWZiaezCVTU_Pqg]]></openid>
            //    <out_trade_no><![CDATA[4b3439afdf364d71b56d4b9594f9709d]]></out_trade_no>
            //    <result_code><![CDATA[SUCCESS]]></result_code>
            //    <return_code><![CDATA[SUCCESS]]></return_code>
            //    <sign><![CDATA[7FA9A764835E91BFC909D021734B5F86]]></sign>
            //    <time_end><![CDATA[20180114204939]]></time_end>
            //    <total_fee>1</total_fee>
            //    <trade_type><![CDATA[MWEB]]></trade_type>
            //    <transaction_id><![CDATA[4200000093201801144137104667]]></transaction_id>
            //</xml>
            WeiXinProvider wx = new WeiXinProvider();

            wx.OrderNotify(new NotifyRequest()
            {
                apiKey = "0bad12ce2b775367347d0b3a4bacd698"
            });
        }

        /// <summary>
        /// 企业付款到银行卡https://pay.weixin.qq.com/wiki/doc/api/tools/mch_pay.php?chapter=24_2
        /// </summary>
        [TestMethod]
        public void PayBank()
        {
            WeiXinProvider wx = new WeiXinProvider();
            //<xml>
            //    <return_code><![CDATA[SUCCESS]]></return_code>
            //    <return_msg><![CDATA[OK]]></return_msg>
            //    <result_code><![CDATA[SUCCESS]]></result_code>
            //    <mch_id><![CDATA[1491518502]]></mch_id>
            //    <pub_key><![CDATA[-----BEGIN RSA PUBLIC KEY-----
            //    MIIBCgKCAQEAwOY++Z1QD1sirm4FQz8nTU5NcDecRTAhumjlrchfvxowMf9FK/Ad
            //    NhbzF/IB0wSmb9+Z+8Ct9kYihW5L/ETek0s+Qdg0eFcZ4pqiGI9iStQ/57IZANE7
            //    7AUaKWWD1d81FJ6Xxb1KiNv/Jly1bSRowfFmpbDW0P9dATJLcSDJ2OTQZWfqj4/U
            //    Loyp04p530egiLsN/RPAClohCnu/zUjLrsBhi/8xUNwCbTRgEf9uFg4WnRblBcF+
            //    WTFPIFko4iAMBuso36ayw1YEokPTSDhs4pHj7JhOj5JFoVqgOh4UMhJZRGvvkS9v
            //    WXho4WWuWnyC8RfBUBbbr8YbOXFfB2Gm+wIDAQAB
            //    -----END RSA PUBLIC KEY-----
            //    ]]></pub_key>
            //</xml>
            var publickeyResult = wx.GetPublickey(new PublickeyRequest()
            {
                mch_id = "1491518502",
                nonce_str = "11111112212",
                isUseCert = true,
                apiKey = "0bad12ce2b775367347d0b3a4bacd698",
                cert = @"C:/cert/apiclient_cert.p12",
                password = "1491518502"
            });

            if (publickeyResult.return_code.Equals("SUCCESS") && publickeyResult.result_code.Equals("SUCCESS"))
            {

                var pubKey = @"MIIBCgKCAQEAwOY++Z1QD1sirm4FQz8nTU5NcDecRTAhumjlrchfvxowMf9FK/Ad
NhbzF/IB0wSmb9+Z+8Ct9kYihW5L/ETek0s+Qdg0eFcZ4pqiGI9iStQ/57IZANE7
7AUaKWWD1d81FJ6Xxb1KiNv/Jly1bSRowfFmpbDW0P9dATJLcSDJ2OTQZWfqj4/U
Loyp04p530egiLsN/RPAClohCnu/zUjLrsBhi/8xUNwCbTRgEf9uFg4WnRblBcF+
WTFPIFko4iAMBuso36ayw1YEokPTSDhs4pHj7JhOj5JFoVqgOh4UMhJZRGvvkS9v
WXho4WWuWnyC8RfBUBbbr8YbOXFfB2Gm+wIDAQAB";

                var enc_bank_no = EncryptionUtil.encryptData("622909433778622718", pubKey, "UTF-8");
                var enc_true_name = EncryptionUtil.encryptData("苟治国", pubKey, "UTF-8");

                var result = wx.PayBank(new PayBankRequest()
                {
                    mch_id = "1491518502",
                    nonce_str = "11111112212",
                    amount = 1,
                    bank_code = "1009",
                    account_type = 1,
                    bank_note = "开发测试",
                    desc = "test",
                    enc_bank_no = enc_bank_no,
                    enc_true_name = enc_true_name,
                    partner_trade_no = "1212121221278",
                    isUseCert = true,
                    apiKey = "0bad12ce2b775367347d0b3a4bacd698",
                    cert = @"C:/cert/apiclient_cert.p12",
                    password = "1491518502"
                });

                Assert.IsNotNull(result);
            }
            
        }
    }
}