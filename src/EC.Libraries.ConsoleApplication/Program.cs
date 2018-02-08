using System;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace EC.Libraries.ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            // Lambda 表达式   
            //Func<int,int,int> sum = (a,b) => a + b;

            //Console.WriteLine(sum(1,1));
            

            //创建匿名方法  
            //Func<int,int,int> delegateSum = delegate(int x,int y) { return x+y; };
            //Console.WriteLine(delegateSum(1,1));
            //Console.ReadLine();
            //Func<string, int> strLength = (string str) => { return str.Length; };

            //dc7c2d713267f999491d2c7a523350a9
            //tt123456(e6dcddde9f2e9ffbc85763ddeb5ee3f1)

            string offset = HttpUtility.UrlEncode("7e12260a3799476d928afee1880ad9ec".ToLower(), Encoding.UTF8);

            //var pass = GetMD5(HttpUtility.UrlEncode("tt123456", Encoding.UTF8));


            var newPass = GetEncryption("7e12260a3799476d928afee1880ad9ec", "tt123456");

            Console.WriteLine(newPass);
            Console.ReadLine();
        }

        private static void CreateLandlordProxy()
        {
            //sssProxyGenerator proxyGenerator = new ProxyGenerator();
        }

        /// <summary>
        ///  MD5加密
        /// </summary>
        /// <param name="input">密码</param>
        /// <returns>加密后符串</returns>
        private static string Md5(string input)
        {
            var sb = new StringBuilder();
            var buffer = MD5.Create().ComputeHash(Encoding.GetEncoding("UTF-8").GetBytes(input));
            for (int i = 0; i < buffer.Length; i++)
            {
                sb.Append(buffer[i].ToString("x2"));
            }
            return sb.ToString();
        }

        /// <summary>
        /// 兼容JAVA中的MD5
        /// </summary>
        /// <param name="offset">要加密的字符串</param>
        /// <returns>加密后符串</returns>
        public static string GetEncryption(String offset, String pass)
        {
            return Md5(String.Format("{0}{1}",Uri.UnescapeDataString(offset.ToUpper(), Encoding.UTF8),Md5(HttpUtility.UrlEncode(pass, Encoding.UTF8))));
        }
    }
}
