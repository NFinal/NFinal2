using System.Text;
using System.IO;
using System.Net;
using System;
using System.Collections.Generic;
using System.Web;
using System.Xml;

namespace Alipay
{
    /// <summary>
    /// 支付宝各接口请求提交类
    /// 构造支付宝各接口表单HTML文本，获取远程HTTP数据
    /// </summary>
    public class Submit
    {
        public Submit()
        {

        }

        /// <summary>
        /// 实例化
        /// </summary>
        /// <param name="partner">合作者身份ID</param>
        /// <param name="key">交易安全校验码</param>
        /// <param name="sellerMail">卖方（收款方）支付宝账户</param>
        public Submit(string partner, string key, string sellerMail)
        {
            Config.Partner = partner;
            Config.Key = key;
            Config.SellerEmail = sellerMail;
        }

        /// <summary>
        /// 提交支付请求,get
        /// </summary>
        /// <param name="orderNo">商户订单号</param>
        /// <param name="fee">付款金额</param>
        /// <param name="title">订单名称</param>
        /// <param name="desc">订单描述</param>
        /// <param name="notifyUrl">服务器异步通知页面路径</param>
        /// <param name="returnUrl">页面跳转同步通知页面路径</param>
        /// <param name="showUrl">商品展示地址</param>
        public void Pay(string orderNo, string fee, string title, string desc, string notifyUrl,
                               string returnUrl, string showUrl)
        {
            var para = new SortedDictionary<string, string>
                {
                    {"partner", Config.Partner},
                    {"_input_charset", Config._Charset.ToLower()},
                    {"service", Config._PayMode},
                    {"payment_type", Config._PayType},
                    {"notify_url", notifyUrl},
                    {"return_url", returnUrl},
                    {"seller_email", Config.SellerEmail},
                    {"out_trade_no", orderNo},
                    {"subject", title},
                    {"total_fee", fee},
                    {"body", desc},
                    {"show_url", showUrl},
                    {"anti_phishing_key", _GetDateTimeString()}
                };
            var dicPara = BuildRequestPara(para);
            var sbHtml = new StringBuilder();
            sbHtml.AppendFormat(
                "<form id='alipaysubmit' name='alipaysubmit' action='{0}_input_charset={1}' method='get'>",
                Config._GateWay, Config._Charset);
            foreach (var temp in dicPara)
            {
                sbHtml.AppendFormat("<input type='hidden' name='{0}' value='{1}'/>", temp.Key, temp.Value);
            }
            //submit按钮控件请不要含有name属性
            sbHtml.Append("<input type='submit' value='支付' style='display:none;'></form>");
            sbHtml.Append("<script>document.forms['alipaysubmit'].submit();</script>");
            HttpContext.Current.Response.Write(sbHtml.ToString());
        }

        /// <summary>
        /// 提交支付请求,post
        /// </summary>
        /// <param name="notifyUrl">服务器异步通知页面路径</param>
        /// <param name="returnUrl">页面跳转同步通知页面路径</param>
        /// <param name="sellerEmail">卖家支付宝帐户</param>
        /// <param name="orderNo">商户订单号</param>
        /// <param name="subject">订单名称</param>
        /// <param name="fee">付款金额</param>
        /// <param name="title">订单描述</param>
        /// <param name="showUrl">商品展示地址</param>
        public void PostPay(string notifyUrl, string returnUrl, string sellerEmail, string orderNo, string subject
                               , string fee, string title, string showUrl)
        {
            var para = new SortedDictionary<string, string>
                {
                    {"partner", Config.Partner},
                    {"_input_charset", Config._Charset.ToLower()},
                    {"service", Config._PayMode},
                    {"payment_type", Config._PayType},
                    {"notify_url", notifyUrl},
                    {"return_url", returnUrl},
                    {"seller_email", sellerEmail},
                    {"out_trade_no", orderNo},
                    {"subject", subject},
                    {"total_fee", fee},
                    {"body", title},
                    {"show_url", showUrl},
                    {"anti_phishing_key", _GetDateTimeString()}
                };

            var encoding = Encoding.GetEncoding(Config._Charset);
            var strRequestData = BuildRequestParaToString(para, encoding);
            var bytesRequestData = encoding.GetBytes(strRequestData);
            var strUrl = string.Format("{0}_input_charset={1}", Config._GateWay, Config._Charset);

            string strResult;
            try
            {
                var myReq = (HttpWebRequest)WebRequest.Create(strUrl);
                myReq.Method = "post";
                myReq.ContentType = "application/x-www-form-urlencoded";
                myReq.ContentLength = bytesRequestData.Length;
                
                var requestStream = myReq.GetRequestStream();
                requestStream.Write(bytesRequestData, 0, bytesRequestData.Length);
                requestStream.Close();

                var HttpWResp = (HttpWebResponse)myReq.GetResponse();
                var myStream = HttpWResp.GetResponseStream();
                var reader = new StreamReader(myStream, encoding);
                var responseData = new StringBuilder();
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    responseData.Append(line);
                }
                myStream.Close();
                strResult = responseData.ToString();
            }
            catch (Exception ex)
            {
                strResult = "报错：" + ex.Message;
            }
            HttpContext.Current.Response.Write(strResult);
        }

        /// <summary>
        /// 生成请求时的签名
        /// </summary>
        internal string BuildRequestMysign(Dictionary<string, string> sPara)
        {
            return MD5.Sign(Core.CreateLinkString(sPara), Config.Key, Config._Charset);
        }

        /// <summary>
        /// 生成要请求给支付宝的参数数组
        /// </summary>
        internal Dictionary<string, string> BuildRequestPara(SortedDictionary<string, string> sParaTemp)
        {
            var sPara = Core.FilterPara(sParaTemp);
            var mysign = BuildRequestMysign(sPara);
            sPara.Add("sign", mysign);
            sPara.Add("sign_type", Config._SignType);
            return sPara;
        }

        /// <summary>
        /// 用于防钓鱼，调用接口query_timestamp来获取时间戳的处理函数
        /// 注意：远程解析XML出错，与IIS服务器配置有关
        /// </summary>
        /// <returns>时间戳字符串</returns>
        private string _GetDateTimeString()
        {
            var url = string.Format("{0}service=query_timestamp&partner={1}", Config._GateWay, Config.Partner);
            var Reader = new XmlTextReader(url);
            var xmlDoc = new XmlDocument();
            xmlDoc.Load(Reader);
            return xmlDoc.SelectSingleNode("/alipay/response/timestamp/encrypt_key").InnerText;
        }

        /// <summary>
        /// 生成要请求给支付宝的参数数组
        /// </summary>
        internal string BuildRequestParaToString(SortedDictionary<string, string> sParaTemp, Encoding code)
        {
            return Core.CreateLinkStringUrlencode(BuildRequestPara(sParaTemp), code);
        }

        /*
        /// <summary>
        /// 建立请求，以模拟远程HTTP的POST请求方式构造并获取支付宝的处理结果，带文件上传功能
        /// </summary>
        /// <param name="sParaTemp">请求参数数组</param>
        /// <param name="strMethod">提交方式。两个值可选：post、get</param>
        /// <param name="fileName">文件绝对路径</param>
        /// <param name="data">文件数据</param>
        /// <param name="contentType">文件内容类型</param>
        /// <param name="lengthFile">文件长度</param>
        /// <returns>支付宝处理结果</returns>
        internal static string BuildRequest(SortedDictionary<string, string> sParaTemp, string strMethod, string fileName, byte[] data, string contentType, int lengthFile)
        {

            //待请求参数数组
            Dictionary<string, string> dicPara = new Dictionary<string, string>();
            dicPara = BuildRequestPara(sParaTemp);

            //构造请求地址
            string strUrl = Config._GateWay + "_input_charset=" + Config._Charset;

            //设置HttpWebRequest基本信息
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(strUrl);
            //设置请求方式：get、post
            request.Method = strMethod;
            //设置boundaryValue
            string boundaryValue = DateTime.Now.Ticks.ToString("x");
            string boundary = "--" + boundaryValue;
            request.ContentType = "\r\nmultipart/form-data; boundary=" + boundaryValue;
            //设置KeepAlive
            request.KeepAlive = true;
            //设置请求数据，拼接成字符串
            StringBuilder sbHtml = new StringBuilder();
            foreach (KeyValuePair<string, string> key in dicPara)
            {
                sbHtml.Append(boundary + "\r\nContent-Disposition: form-data; name=\"" + key.Key + "\"\r\n\r\n" + key.Value + "\r\n");
            }
            sbHtml.Append(boundary + "\r\nContent-Disposition: form-data; name=\"withhold_file\"; filename=\"");
            sbHtml.Append(fileName);
            sbHtml.Append("\"\r\nContent-Type: " + contentType + "\r\n\r\n");
            string postHeader = sbHtml.ToString();
            //将请求数据字符串类型根据编码格式转换成字节流
            Encoding code = Encoding.GetEncoding(Config._Charset);
            byte[] postHeaderBytes = code.GetBytes(postHeader);
            byte[] boundayBytes = Encoding.ASCII.GetBytes("\r\n" + boundary + "--\r\n");
            //设置长度
            long length = postHeaderBytes.Length + lengthFile + boundayBytes.Length;
            request.ContentLength = length;

            //请求远程HTTP
            Stream requestStream = request.GetRequestStream();
            Stream myStream;
            try
            {
                //发送数据请求服务器
                requestStream.Write(postHeaderBytes, 0, postHeaderBytes.Length);
                requestStream.Write(data, 0, lengthFile);
                requestStream.Write(boundayBytes, 0, boundayBytes.Length);
                HttpWebResponse HttpWResp = (HttpWebResponse)request.GetResponse();
                myStream = HttpWResp.GetResponseStream();
            }
            catch (WebException e)
            {
                return e.ToString();
            }
            finally
            {
                if (requestStream != null)
                {
                    requestStream.Close();
                }
            }

            //读取支付宝返回处理结果
            StreamReader reader = new StreamReader(myStream, code);
            StringBuilder responseData = new StringBuilder();

            String line;
            while ((line = reader.ReadLine()) != null)
            {
                responseData.Append(line);
            }
            myStream.Close();
            return responseData.ToString();
        }
        */
    }
}