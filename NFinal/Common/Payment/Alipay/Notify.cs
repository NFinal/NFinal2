using System.Text;
using System.IO;
using System.Net;
using System;
using System.Collections.Generic;

namespace Alipay
{
    /// <summary>
    /// 处理支付宝各接口通知返回
    /// </summary>
    internal class Notify
    {
        /// <summary>
        /// 验证消息是否是支付宝发出的合法消息
        /// </summary>
        /// <param name="inputPara">通知返回参数数组</param>
        /// <param name="notifyId">通知验证ID</param>
        /// <param name="sign">支付宝生成的签名结果</param>
        /// <returns>验证结果</returns>
        internal bool Verify(SortedDictionary<string, string> inputPara, string notifyId, string sign)
        {
            var isSign = GetSignVeryfy(inputPara, sign);
            var responseTxt = "true";
            if (!string.IsNullOrEmpty(notifyId)) { responseTxt = GetResponseTxt(notifyId); }
            
            //判断responsetTxt是否为true，isSign是否为true
            //responsetTxt的结果不是true，与服务器设置问题、合作身份者ID、notify_id一分钟失效有关
            //isSign不是true，与安全校验码、请求时的参数格式（如：带自定义参数等）、编码格式有关
            return responseTxt == "true" && isSign;
        }

        /// <summary>
        /// 获取返回时的签名验证结果
        /// </summary>
        /// <param name="inputPara">通知返回参数数组</param>
        /// <param name="sign">对比的签名结果</param>
        /// <returns>签名验证结果</returns>
        private bool GetSignVeryfy(SortedDictionary<string, string> inputPara, string sign)
        {
            var sPara = Core.FilterPara(inputPara); //过滤空值、sign与sign_type参数
            var preSignStr = Core.CreateLinkString(sPara); //获取待签名字符串
            return MD5.Verify(preSignStr, sign, Config.Key, Config._Charset);
        }

        /// <summary>
        /// 获取是否是支付宝服务器发来的请求的验证结果
        /// </summary>
        /// <param name="notifyId">通知验证ID</param>
        /// <returns>验证结果</returns>
        private string GetResponseTxt(string notifyId)
        {
            var veryfyUrl = string.Format("{0}partner={1}&notify_id={2}", Config._VeryfyUrl, Config.Partner, notifyId);

            //获取远程服务器ATN结果，验证是否是支付宝服务器发来的请求
            try
            {
                var myReq = (HttpWebRequest)WebRequest.Create(veryfyUrl);
                myReq.Timeout = 120000;
                var HttpWResp = (HttpWebResponse)myReq.GetResponse();
                var myStream = HttpWResp.GetResponseStream();
                var sr = new StreamReader(myStream, Encoding.Default);
                var strBuilder = new StringBuilder();
                while (-1 != sr.Peek())
                {
                    strBuilder.Append(sr.ReadLine());
                }
                return strBuilder.ToString();
            }
            catch (Exception exp)
            {
                return string.Format("错误:{0}", exp.Message);
            }
        }
    }
}