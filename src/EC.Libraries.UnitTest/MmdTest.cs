using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EC.Libraries.UnitTest
{
    [TestClass]
    public class MmdTest
    {
        /// <summary>
        /// Redis分布式锁
        /// </summary>
        [TestMethod]
        public void TestMethod1()
        {
            string expressPass = "gzg4484578";

            string output = md5(expressPass);

            Assert.IsNotNull(output);
        }

        public static string md5ss(string password)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] res = md5.ComputeHash(Encoding.Default.GetBytes(password), 0, password.Length);
            StringBuilder builder = new StringBuilder();
            foreach (byte b in res)
            {
                builder.Append(Convert.ToString(b, 16));
            }
            return builder.ToString();
        }

        public static string md5(string str)
        {
            try
            {
                MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
                byte[] bytValue, bytHash;
                bytValue = System.Text.Encoding.UTF8.GetBytes(str);
                bytHash = md5.ComputeHash(bytValue);
                md5.Clear();
                string sTemp = "";
                for (int i = 0; i < bytHash.Length; i++)
                {
                    sTemp += bytHash[i].ToString("X").PadLeft(2, '0');
                }
                str = sTemp.ToLower();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return str;
        }

    }
}
