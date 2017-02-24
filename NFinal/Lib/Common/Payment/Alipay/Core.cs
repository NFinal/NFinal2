using System.Linq;
using System.Web;
using System.Text;
using System.IO;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace Alipay
{
    internal class Core
    {
        /// <summary>
        /// 除去数组中的空值和签名参数并以字母a到z的顺序排序
        /// </summary>
        internal static Dictionary<string, string> FilterPara(SortedDictionary<string, string> dicArrayPre)
        {
            return
                dicArrayPre.Where(
                    temp =>
                    temp.Key.ToLower() != "sign" && temp.Key.ToLower() != "sign_type" &&
                    !string.IsNullOrEmpty(temp.Value)).ToDictionary(temp => temp.Key, temp => temp.Value);
        }

        /// <summary>
        /// 把数组所有元素，按照“参数=参数值”的模式用“&amp;”字符拼接成字符串
        /// </summary>
        internal static string CreateLinkString(Dictionary<string, string> dicArray)
        {
            var prestr = new StringBuilder();
            foreach (var temp in dicArray)
            {
                prestr.Append(temp.Key + "=" + temp.Value + "&");
            }
            prestr.Remove(prestr.Length - 1, 1);
            return prestr.ToString();
        }

        /// <summary>
        /// 把数组所有元素，按照“参数=参数值”的模式用“&amp;”字符拼接成字符串，并对参数值做urlencode
        /// </summary>
        internal static string CreateLinkStringUrlencode(Dictionary<string, string> dicArray, Encoding code)
        {
            var prestr = new StringBuilder();
            foreach (var temp in dicArray)
            {
                prestr.AppendFormat("{0}={1}&", temp.Key, HttpUtility.UrlEncode(temp.Value, code));
            }
            prestr.Remove(prestr.Length - 1, 1);
            return prestr.ToString();
        }

        /// <summary>
        /// 写日志
        /// </summary>
        internal static void LogResult(string text)
        {
            var strPath = HttpContext.Current.Server.MapPath(Config._LogPath);
            var dateFloderName = DateTime.Now.ToString("yyyyMM");
            strPath = string.Format("{0}/{1}", strPath, dateFloderName);
            if (!Directory.Exists(strPath))
            {
                Directory.CreateDirectory(strPath);
            }
            strPath = strPath + "\\" + DateTime.Now.ToString("yyyyMMddHHmmssffff", DateTimeFormatInfo.InvariantInfo) + ".txt";
            var fs = new StreamWriter(strPath, true, Encoding.Default);
            fs.Write(text);
            fs.Close();
        }

        /// <summary>
        /// 获取支付宝POST过来通知消息，并以“参数名=参数值”的形式组成数组
        /// </summary>
        internal static SortedDictionary<string, string> GetRequestPost()
        {
            int i;
            var sArray = new SortedDictionary<string, string>();
            var coll = HttpContext.Current.Request.Form;
            var requestItem = coll.AllKeys;
            for (i = 0; i < requestItem.Length; i++)
                sArray.Add(requestItem[i], HttpContext.Current.Request.Form[requestItem[i]]);
            return sArray;
        }

        /*
        /// <summary>
        /// 获取文件的md5摘要
        /// </summary>
        /// <param name="sFile">文件流</param>
        /// <returns>MD5摘要结果</returns>
        internal static string GetAbstractToMD5(Stream sFile)
        {
            System.Security.Cryptography.MD5 md5 = new MD5CryptoServiceProvider();
            byte[] result = md5.ComputeHash(sFile);
            StringBuilder sb = new StringBuilder(32);
            for (int i = 0; i < result.Length; i++)
            {
                sb.Append(result[i].ToString("x").PadLeft(2, '0'));
            }
            return sb.ToString();
        }

        /// <summary>
        /// 获取文件的md5摘要
        /// </summary>
        /// <param name="dataFile">文件流</param>
        /// <returns>MD5摘要结果</returns>
        internal static string GetAbstractToMD5(byte[] dataFile)
        {
            System.Security.Cryptography.MD5 md5 = new MD5CryptoServiceProvider();
            byte[] result = md5.ComputeHash(dataFile);
            StringBuilder sb = new StringBuilder(32);
            for (int i = 0; i < result.Length; i++)
            {
                sb.Append(result[i].ToString("x").PadLeft(2, '0'));
            }
            return sb.ToString();
        }
        */
    }
}