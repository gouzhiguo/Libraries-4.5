using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace EC.Libraries.Util
{
    /// <summary>
    /// 加密解密工具
    /// </summary>
    public class EncryptionUtil
    {
        /// <summary>
        /// 对字符串附加随机字符进行MD5加密
        /// </summary>
        /// <param name="str">要加密的字符串</param>
        /// <param name="offset">加密所需的偏移量</param>
        /// <returns>加密后的小写字符串</returns>
        public static string EncryptWithMd5AndSalt(string str, string offset)
        {
            return EncryptWithMd5(offset + str) + ":" + offset;
        }

        /// <summary>
        /// 对字符串进行MD5加密
        /// </summary>
        /// <param name="str">要加密的字符串</param>
        /// <returns>加密后的小写字符串</returns>
        public static string EncryptWithMd5(string str)
        {
            return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(str, "md5").ToLower();
        }

        /// <summary>
        /// 兼容PHP中的MD5
        /// </summary>
        /// <param name="str">要加密的字符串</param>
        /// <returns>加密后的小写字符串</returns>
        public static string EncryptMD5(string str)
        {
            byte[] textBytes = System.Text.Encoding.Default.GetBytes(str);
            try
            {
                System.Security.Cryptography.MD5CryptoServiceProvider cryptHandler;
                cryptHandler = new System.Security.Cryptography.MD5CryptoServiceProvider();
                byte[] hash = cryptHandler.ComputeHash(textBytes);
                string ret = "";
                foreach (byte a in hash){
                    if (a < 16)
                        ret += "0" + a.ToString("x");
                    else
                        ret += a.ToString("x");
                }
                return ret;
            }
            catch
            {
                throw new Exception(" 兼容PHP中的MD5失败");
            }
        } 

        /// <summary>
        /// 对字符串附加随机字符进行MD5加密
        /// </summary>
        /// <param name="str">要加密的字符串</param>
        /// <returns>加密后的小写字符串</returns>
        public static string EncryptWithMd5AndSalt(string str)
        {
            string password = "";
            password = new Random().Next(10000000, 99999999).ToString();
            string salt = EncryptWithMd5(password).Substring(0, 2);
            password = EncryptWithMd5(salt + str) + ":" + salt;
            return password;
        }

        /// <summary>
        /// 对加密字符串（字符串附加随机字符进行MD5加密后的字符串）进行验证
        /// </summary>
        /// <param name="unencrypted">明文</param>
        /// <param name="encrypted">密文</param>
        /// <returns>验证结果</returns>
        public static bool VerifyCiphetrextWithMd5AndSalt(string unencrypted, string encrypted)
        {
            if (!string.IsNullOrEmpty(unencrypted) && !string.IsNullOrEmpty(encrypted))
            {
                string[] stack = encrypted.Split(':');
                if (stack.Length != 2) return false;
                if (EncryptWithMd5(stack[1] + unencrypted) == stack[0])
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// RSA加密 将公钥导入到RSA对象中，准备加密
        /// </summary>
        /// <param name="publicKey">公钥</param>
        /// <param name="encryptstring">待加密的字符串</param>
        public static string RSAEncrypt(string publicKey, string encryptstring)
        {
            byte[] PlainTextBArray;
            byte[] CypherTextBArray;
            string Result;
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(publicKey);
            PlainTextBArray = (new UnicodeEncoding()).GetBytes(encryptstring);
            CypherTextBArray = rsa.Encrypt(PlainTextBArray, false);
            Result = Convert.ToBase64String(CypherTextBArray);
            return Result;
        }

        /// <summary>
        /// RSA解密 将私钥导入RSA中，准备解密
        /// </summary>
        /// <param name="privateKey">私钥</param>
        /// <param name="decryptstring">待解密的字符串</param>
        public static string RSADecrypt(string privateKey, string decryptstring)
        {
            byte[] PlainTextBArray;
            byte[] DypherTextBArray;
            string Result;
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(privateKey);
            PlainTextBArray = Convert.FromBase64String(decryptstring);
            DypherTextBArray = rsa.Decrypt(PlainTextBArray, false);
            Result = (new UnicodeEncoding()).GetString(DypherTextBArray);
            return Result;
        }


        /// <summary>  
        /// 加密  
        /// </summary>  
        /// <param name="resData">需要加密的字符串</param>  
        /// <param name="publicKey">公钥</param>  
        /// <param name="input_charset">编码格式</param>  
        /// <returns>明文</returns>  
        public static string encryptData(string resData, string publicKey, string input_charset)
        {
            byte[] DataToEncrypt = Encoding.ASCII.GetBytes(resData);
            string result = encrypt(DataToEncrypt, publicKey, input_charset);
            return result;
        }

        private static string encrypt(byte[] data, string publicKey, string input_charset)
        {
            RSACryptoServiceProvider rsa = DecodePemPublicKey(publicKey);
            SHA1 sh = new SHA1CryptoServiceProvider();
            byte[] result = rsa.Encrypt(data, false);

            return Convert.ToBase64String(result);
        }

        private static bool CompareBytearrays(byte[] a, byte[] b)
        {
            if (a.Length != b.Length)
                return false;
            int i = 0;
            foreach (byte c in a)
            {
                if (c != b[i])
                    return false;
                i++;
            }
            return true;
        }  
  

        private static RSACryptoServiceProvider DecodeRSAPublicKey(byte[] publickey)
        {
            // encoded OID sequence for  PKCS #1 rsaEncryption szOID_RSA_RSA = "1.2.840.113549.1.1.1"  
            byte[] SeqOID = { 0x30, 0x0D, 0x06, 0x09, 0x2A, 0x86, 0x48, 0x86, 0xF7, 0x0D, 0x01, 0x01, 0x01, 0x05, 0x00 };
            byte[] seq = new byte[15];
            // ---------  Set up stream to read the asn.1 encoded SubjectPublicKeyInfo blob  ------  
            MemoryStream mem = new MemoryStream(publickey);
            BinaryReader binr = new BinaryReader(mem);    //wrap Memory Stream with BinaryReader for easy reading  
            byte bt = 0;
            ushort twobytes = 0;

            try
            {

                twobytes = binr.ReadUInt16();
                if (twobytes == 0x8130) //data read as little endian order (actual data order for Sequence is 30 81)  
                    binr.ReadByte();    //advance 1 byte  
                else if (twobytes == 0x8230)
                    binr.ReadInt16();   //advance 2 bytes  
                else
                    return null;

                seq = binr.ReadBytes(15);       //read the Sequence OID  
                if (!CompareBytearrays(seq, SeqOID))    //make sure Sequence for OID is correct  
                    return null;

                twobytes = binr.ReadUInt16();
                if (twobytes == 0x8103) //data read as little endian order (actual data order for Bit String is 03 81)  
                    binr.ReadByte();    //advance 1 byte  
                else if (twobytes == 0x8203)
                    binr.ReadInt16();   //advance 2 bytes  
                else
                    return null;

                bt = binr.ReadByte();
                if (bt != 0x00)     //expect null byte next  
                    return null;

                twobytes = binr.ReadUInt16();
                if (twobytes == 0x8130) //data read as little endian order (actual data order for Sequence is 30 81)  
                    binr.ReadByte();    //advance 1 byte  
                else if (twobytes == 0x8230)
                    binr.ReadInt16();   //advance 2 bytes  
                else
                    return null;

                twobytes = binr.ReadUInt16();
                byte lowbyte = 0x00;
                byte highbyte = 0x00;

                if (twobytes == 0x8102) //data read as little endian order (actual data order for Integer is 02 81)  
                    lowbyte = binr.ReadByte();  // read next bytes which is bytes in modulus  
                else if (twobytes == 0x8202)
                {
                    highbyte = binr.ReadByte(); //advance 2 bytes  
                    lowbyte = binr.ReadByte();
                }
                else
                    return null;
                byte[] modint = { lowbyte, highbyte, 0x00, 0x00 };   //reverse byte order since asn.1 key uses big endian order  
                int modsize = BitConverter.ToInt32(modint, 0);

                byte firstbyte = binr.ReadByte();
                binr.BaseStream.Seek(-1, SeekOrigin.Current);

                if (firstbyte == 0x00)
                {   //if first byte (highest order) of modulus is zero, don't include it  
                    binr.ReadByte();    //skip this null byte  
                    modsize -= 1;   //reduce modulus buffer size by 1  
                }

                byte[] modulus = binr.ReadBytes(modsize);   //read the modulus bytes  

                if (binr.ReadByte() != 0x02)            //expect an Integer for the exponent data  
                    return null;
                int expbytes = (int)binr.ReadByte();        // should only need one byte for actual exponent data (for all useful values)  
                byte[] exponent = binr.ReadBytes(expbytes);

                // ------- create RSACryptoServiceProvider instance and initialize with public key -----  
                RSACryptoServiceProvider RSA = new RSACryptoServiceProvider();
                RSAParameters RSAKeyInfo = new RSAParameters();
                RSAKeyInfo.Modulus = modulus;
                RSAKeyInfo.Exponent = exponent;
                RSA.ImportParameters(RSAKeyInfo);
                return RSA;
            }
            catch (Exception)
            {
                return null;
            }

            finally { binr.Close(); }

        }  

        private static RSACryptoServiceProvider DecodePemPublicKey(String pemstr)
        {
            byte[] pkcs8publickkey;
            pkcs8publickkey = Convert.FromBase64String(pemstr);
            if (pkcs8publickkey != null)
            {
                RSACryptoServiceProvider rsa = DecodeRSAPublicKey(pkcs8publickkey);
                return rsa;
            }
            else
                return null;
        }  
    }
}
