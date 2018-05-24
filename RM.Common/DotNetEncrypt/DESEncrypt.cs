using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Web.Security;
using System.Linq;

namespace RM.Common.DotNetEncrypt
{
    public class DESEncrypt
    {
        public static string Encrypt(string Text)
        {
            return DESEncrypt.Encrypt(Text, "RM_Base");
        }

        public static string Encrypt(string Text, string sKey)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] inputByteArray = Encoding.Default.GetBytes(Text);
            des.Key = Encoding.ASCII.GetBytes(FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5").Substring(0, 8));
            des.IV = Encoding.ASCII.GetBytes(FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5").Substring(0, 8));
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            StringBuilder ret = new StringBuilder();
            byte[] array = ms.ToArray();
            for (int i = 0; i < array.Length; i++)
            {
                byte b = array[i];
                ret.AppendFormat("{0:X2}", b);
            }
            return ret.ToString();
        }

        public static string Decrypt(string Text)
        {
            return DESEncrypt.Decrypt(Text, "RM_Base");
        }

        public static string Decrypt(string Text, string sKey)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            int len = Text.Length / 2;
            byte[] inputByteArray = new byte[len];
            for (int x = 0; x < len; x++)
            {
                int i = Convert.ToInt32(Text.Substring(x * 2, 2), 16);
                inputByteArray[x] = (byte)i;
            }
            des.Key = Encoding.ASCII.GetBytes(FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5").Substring(0, 8));
            des.IV = Encoding.ASCII.GetBytes(FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5").Substring(0, 8));
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            return Encoding.Default.GetString(ms.ToArray());
        }
        /// <summary>
        /// 绿云接口加密
        /// </summary>
        /// <param name="signParams"></param>
        /// <param name="appSecret"></param>
        /// <returns></returns>
        public static string  getSign(Dictionary<string, string> signParams, string appSecret)
        {
            //Dictionary<string, string> signParams = new Dictionary<string, string>();
            //signParams.Add("", "");
            StringBuilder sb = new StringBuilder();
            ArrayList keyList = new ArrayList(signParams.Count);
            foreach (var item in signParams)
            {
                keyList.Add(item.Key);
            }
            keyList.Sort();
            sb.Append(appSecret);
            foreach (var item in keyList)
            {
                var a = item.ToString();
                var b = signParams.Where(x => x.Key == item.ToString()).FirstOrDefault();
                sb.Append(item.ToString()).Append(signParams.Where(x => x.Key == item.ToString()).FirstOrDefault().Value);
            }
            sb.Append(appSecret);
           // var c = sb.ToString();
            try
            {
               
            }
            catch (Exception)
            {
                
            }
            return SHA1(sb.ToString()).ToUpper();
        }

        /// <summary>
        /// SHA1加密返回大写字符串（默认UTF-8）
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string SHA1(string content)
        {
            return SHA1(content, Encoding.UTF8);
        }
        /// <summary>
        /// SHA1加密返回大写字符串（可传编码格式）
        /// </summary>
        /// <param name="content"></param>
        /// <param name="encode"></param>
        /// <returns></returns>
        public static string SHA1(string content, Encoding encode)
        {
            try
            {
                SHA1 sha1 = new SHA1CryptoServiceProvider();
                byte[] bytes_in = encode.GetBytes(content);
                byte[] bytes_out = sha1.ComputeHash(bytes_in);
                sha1.Dispose();
                string result = BitConverter.ToString(bytes_out);
                result = result.Replace("-", "");
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("SHA1加密出错：" + ex.Message);
            }
        }
    }
}