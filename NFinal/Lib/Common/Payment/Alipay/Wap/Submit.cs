using System.Text;
using System.IO;
using System.Net;
using System;
using System.Collections.Generic;
using System.Web;
using System.Xml;

namespace Alipay.Wap
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
            var req_id = DateTime.Now.ToString("yyyyMMddHHmmssfff"); //请求号,须保证每次请求都是唯一
            const string format = "xml";
            const string v = "2.0";

            if (string.IsNullOrEmpty(fee)) fee = "0";
            if (string.IsNullOrEmpty(title)) title = "在线捐赠(WAP)";
            if (string.IsNullOrEmpty(orderNo)) orderNo = req_id;

            var req_dataToken =
                string.Format(
                    "<direct_trade_create_req><notify_url>{0}</notify_url><call_back_url>{1}</call_back_url><seller_account_name>{2}</seller_account_name><out_trade_no>{3}</out_trade_no><subject>{4}</subject><total_fee>{5}</total_fee><merchant_url>{6}</merchant_url></direct_trade_create_req>",
                    notifyUrl, returnUrl, Config.SellerEmail, orderNo, title, fee, showUrl);

            //创建支付宝交易订单,并获取授权码token,alipay.wap.trade.create.direct
            var sParaTempToken = new SortedDictionary<string, string>();
            sParaTempToken.Add("partner", Config.Partner);
            sParaTempToken.Add("_input_charset", Config._Charset.ToLower());
            sParaTempToken.Add("sec_id", Config._SignType.ToUpper());
            sParaTempToken.Add("service", "alipay.wap.trade.create.direct");
            sParaTempToken.Add("format", format);
            sParaTempToken.Add("v", v);
            sParaTempToken.Add("req_id", req_id);
            sParaTempToken.Add("req_data", req_dataToken);

            var sHtmlTextToken = BuildRequest(Config._GateWayWap, sParaTempToken);
            var code = Encoding.GetEncoding(Config._Charset);
            sHtmlTextToken = HttpUtility.UrlDecode(sHtmlTextToken, code);
            var dicHtmlTextToken = ParseResponse(sHtmlTextToken);
            var request_token = dicHtmlTextToken["request_token"];

            //根据授权码token调用交易接口alipay.wap.auth.authAndExecute
            var req_data = string.Format("<auth_and_execute_req><request_token>{0}</request_token></auth_and_execute_req>", request_token);
            var sParaTemp = new SortedDictionary<string, string>
                {
                    {"partner", Config.Partner},
                    {"_input_charset", Config._Charset.ToLower()},
                    {"sec_id", Config._SignType.ToUpper()},
                    {"service", "alipay.wap.auth.authAndExecute"},
                    {"format", format},
                    {"v", v},
                    {"req_data", req_data}
                };
            var sHtmlText = BuildRequest(Config._GateWayWap, sParaTemp, "get", "确认");
            HttpContext.Current.Response.Write(sHtmlText);
        }

        /// <summary>
        /// 建立请求，以表单HTML形式构造（默认）
        /// </summary>
        /// <param name="GATEWAY_NEW">支付宝网关地址</param>
        /// <param name="sParaTemp">请求参数数组</param>
        /// <param name="strMethod">提交方式。两个值可选：post、get</param>
        /// <param name="strButtonValue">确认按钮显示文字</param>
        /// <returns>提交表单HTML文本</returns>
        private string BuildRequest(string GATEWAY_NEW, SortedDictionary<string, string> sParaTemp, string strMethod, string strButtonValue)
        {
            var dicPara = BuildRequestPara(sParaTemp);

            var sbHtml = new StringBuilder();
            sbHtml.Append("<form id='alipaysubmit' name='alipaysubmit' action='" + GATEWAY_NEW + "_input_charset=" + Config._Charset + "' method='" + strMethod.ToLower().Trim() + "'>");

            foreach (var temp in dicPara)
            {
                sbHtml.Append("<input type='hidden' name='" + temp.Key + "' value='" + temp.Value + "'/>");
            }

            //submit按钮控件请不要含有name属性
            sbHtml.Append("<input type='submit' value='" + strButtonValue + "' style='display:none;'></form>");

            sbHtml.Append("<script>document.forms['alipaysubmit'].submit();</script>");

            return sbHtml.ToString();
        }


        /// <summary>
        /// 建立请求，以模拟远程HTTP的POST请求方式构造并获取支付宝的处理结果
        /// </summary>
        /// <param name="gateWayWap">支付宝网关地址</param>
        /// <param name="sParaTemp">请求参数数组</param>
        /// <returns>支付宝处理结果</returns>
        private string BuildRequest(string gateWayWap, SortedDictionary<string, string> sParaTemp)
        {
            var code = Encoding.GetEncoding(Config._Charset);
            var strRequestData = BuildRequestParaToString(sParaTemp,code);
            var bytesRequestData = code.GetBytes(strRequestData);
            var strUrl = string.Format("{0}_input_charset={1}", gateWayWap, Config._Charset);

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

                var reader = new StreamReader(myStream, code);
                var responseData = new StringBuilder();
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    responseData.Append(line);
                }
                myStream.Close();
                strResult = responseData.ToString();
            }
            catch (Exception exp)
            {
                strResult = "报错："+exp.Message;
            }
            return strResult;
        }

        /// <summary>
        /// 生成要请求给支付宝的参数数组
        /// </summary>
        private string BuildRequestParaToString(SortedDictionary<string, string> sParaTemp, Encoding code)
        {
            return Core.CreateLinkStringUrlencode(BuildRequestPara(sParaTemp), code);
        }

        /// <summary>
        /// 生成要请求给支付宝的参数数组
        /// </summary>
        private Dictionary<string, string> BuildRequestPara(SortedDictionary<string, string> sParaTemp)
        {
            var sPara = Core.FilterPara(sParaTemp);
            var mysign = MD5.Sign(Core.CreateLinkString(sPara), Config.Key, Config._Charset);
            sPara.Add("sign", mysign);
            if (sPara["service"] != "alipay.wap.trade.create.direct" && sPara["service"] != "alipay.wap.auth.authAndExecute")
            {
                sPara.Add("sign_type", Config._SignType);
            }
            return sPara;
        }

        /// <summary>
        /// 解析远程模拟提交后返回的信息
        /// </summary>
        /// <param name="strText">要解析的字符串</param>
        /// <returns>解析结果</returns>
        private Dictionary<string, string> ParseResponse(string strText)
        {
            var strSplitText = strText.Split('&');
            var dicText = new Dictionary<string, string>();
            foreach (string text in strSplitText)
            {
                var nPos = text.IndexOf('=');
                dicText.Add(text.Substring(0, nPos), text.Substring(nPos + 1, text.Length - nPos - 1));
            }

            if (dicText["res_data"] != null)
            {
                //token从res_data中解析出来（也就是说res_data中已经包含token的内容）
                var xmlDoc = new XmlDocument();
                try
                {
                    xmlDoc.LoadXml(dicText["res_data"]);
                    var strRequest_token = xmlDoc.SelectSingleNode("/direct_trade_create_res/request_token").InnerText;
                    dicText.Add("request_token", strRequest_token);
                }
                catch (Exception exp)
                {
                    dicText.Add("request_token", exp.ToString());
                }
            }
            return dicText;
        }
    }
}