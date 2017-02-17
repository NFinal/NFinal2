using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace NFinal.Common.SMS.Open189
{
    public class Utility
    {
        /// <summary>
        /// return a yyyy-MM-dd HH:mm:ss formate time 获取当前时间
        /// </summary>
        /// <returns></returns>
        public static string GetCurrentDate()
        {
            DateTime today = DateTime.Now;
            return today.ToString("yyyy-MM-dd HH:mm:ss");
        }
        /// <summary>
        /// return a yyyyMMddhhmmss formate time
        /// </summary>
        /// <returns></returns>
        public static string GetCurrentDate2()
        {
            DateTime today = DateTime.Now;
            string strDate = today.ToString("yyyyMMddHHmmss");
            return strDate;
        }

        /// <summary>
        /// Build a plaintext for encryption,
        /// format is like key1=value1&key2=value2&key3=value3
        /// </summary>
        /// <param name="paramlist">A dictionary contains all parameters</param>
        /// <returns>plaintext</returns>
        private static string buildPlainText(IDictionary<string, string> paramlist)
        {
            string plaintext = string.Empty;
            int count = 0;
            foreach (KeyValuePair<string, string> pair in paramlist)
            {
                plaintext += pair.Key + "=" + pair.Value;
                if (count < paramlist.Count - 1)
                    plaintext += "&";
                count++;
            }
            return plaintext;
        }

        /// <summary>
        /// MD5 encryption by plaintext
        /// </summary>
        /// <param name="plaintext">plaintext</param>
        /// <returns>ciphertext</returns>
        public static string MD5(string plaintext)
        {
            byte[] result = Encoding.Default.GetBytes(plaintext);
            return MD5(result);
        }

        /// <summary>
        /// MD5 encryption by parameters
        /// </summary>
        /// <param name="signParams">encrpt parameters</param>
        /// <param name="appSecret">app_secret</param>
        /// <returns></returns>
        public static string MD5(IDictionary<string, string> signParams, String appSecret)
        {
            string plaintext = appSecret + "&" + buildPlainText(signParams);
            return MD5(plaintext);
        }

        /// <summary>
        /// MD5 encryption by binary
        /// </summary>
        /// <param name="binaryData">a byte array for plaintext</param>
        /// <returns></returns>
        public static string MD5(byte[] binaryData)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] output = md5.ComputeHash(binaryData);
            return BitConverter.ToString(output).Replace("-", "");
        }


        /// <summary>
        /// MHAC_SHA1 Encryption
        /// </summary>
        /// <param name="plaintext"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        protected static byte[] HMAC_SHA1(string plaintext, string key)
        {
            UTF8Encoding utf8encoding = new UTF8Encoding();
            byte[] keybyte = utf8encoding.GetBytes(key);
            byte[] contentbyte = utf8encoding.GetBytes(plaintext);
            byte[] cipherbyte;
            using (HMACSHA1 hmacsha1 = new HMACSHA1(keybyte))
            {
                cipherbyte = hmacsha1.ComputeHash(contentbyte);
            }

            // return utf8encoding.GetString(cipherbyte);
            return cipherbyte;
        }

        /// <summary>
        /// Base64 Encryption
        /// </summary>
        /// <param name="cipherbyte"></param>
        /// <returns></returns>
        protected static string Base64Encrypt(byte[] cipherbyte)
        {
            string base64str = Convert.ToBase64String(cipherbyte);
            return base64str;
        }

        /// <summary>
        /// HMAC_SHA1 Encryption
        /// </summary>
        /// <param name="plaintext">plaintext</param>
        /// <param name="key">key</param>
        /// <returns>urlencoded ciphertext</returns>
        public static string DoSignature(string plaintext, string key)
        {
            return HttpUtility.UrlEncode(Base64Encrypt(HMAC_SHA1(plaintext, key)));
        }

        /// <summary>
        /// HMAC_SHA1 Encryption
        /// </summary>
        /// <param name="parameters">parameters for generate plaintext</param>
        /// <param name="key">key</param>
        /// <returns>urlencoded ciphertext</returns>
        public static string DoSignature(SortedDictionary<string, string> parameters, string key)
        {
            string plaintext = buildPlainText(parameters);
            return HttpUtility.UrlEncode(Base64Encrypt(HMAC_SHA1(plaintext, key)));
        }


        /// <summary>
        /// open a FileStream to read file content into a byte array.
        /// </summary>
        /// <returns>byte array contains binary data of the input file</returns>
        public static byte[] ReadFile(string filepath)
        {
            using (FileStream fs = new FileStream(filepath, FileMode.Open))
            {
                byte[] byteData = new byte[fs.Length];
                fs.Read(byteData, 0, byteData.Length);
                fs.Close();
                return byteData;
            }
        }
    }
}
