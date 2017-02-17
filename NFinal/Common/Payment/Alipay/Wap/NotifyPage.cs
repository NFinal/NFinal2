using System;
using System.Linq;
using System.Xml;

namespace Alipay.Wap
{
    /// <summary>
    /// 手机网页支付接口接入页
    /// </summary>
    public abstract class NotifyPage
    {
        /// <summary>
        /// 商户订单号
        /// </summary>
        protected string OrderNo { get; private set; }

        /// <summary>
        /// 支付宝交易号
        /// </summary>
        protected string TradeNo { get; private set; }

        /// <summary>
        /// 交易状态
        /// </summary>
        protected string TradeStatus { get; private set; }

        /// <summary>
        /// 业务逻辑处理
        /// </summary>
        protected abstract void OnNotifyConfirm();


        protected bool PaySucceed = false;
        protected string PayMsg = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            var sPara = Core.GetRequestPost();
            Core.LogResult(Request.Url.ToString());
            
            if (sPara.Count > 0) //判断是否有带返回参数
            {
                var formString = Request.Form.Keys.Cast<string>()
                                        .Aggregate("", (current, key) =>
                                                   current + string.Format("{0} = {1}\r\n", key.PadLeft(30, ' '),
                                                                 Request.Form[key]));
                Core.LogResult(formString);

                var aliNotify = new Alipay.Notify();
                var verifyResult = aliNotify.Verify(sPara, Request.Form["notify_id"], Request.Form["sign"]);

                if (verifyResult) //验证成功
                {
                    try
                    {
                        var xmlDoc = new XmlDocument();
                        xmlDoc.LoadXml(sPara["notify_data"]);

                        OrderNo = xmlDoc.SelectSingleNode("/notify/out_trade_no").InnerText; //商户订单号
                        TradeNo = xmlDoc.SelectSingleNode("/notify/trade_no").InnerText; //支付宝交易号
                        TradeStatus = xmlDoc.SelectSingleNode("/notify/trade_status").InnerText; //交易状态

                        //该种交易状态只在两种情况下出现
                        //1、开通了普通即时到账，买家付款成功后。
                        //2、开通了高级即时到账，从该笔交易成功时间算起，过了签约时的可退款时限（如：三个月以内可退款、一年以内可退款等）后
                        if (TradeStatus == "TRADE_FINISHED" || TradeStatus == "TRADE_SUCCESS")
                        {
                            try
                            {
                                OnNotifyConfirm();
                                Core.LogResult(string.Format("业务逻辑处理,OrderNo:{0},TradeNo:{1},TradeStatus:{2}", OrderNo, TradeNo, TradeStatus));
                                Response.Write("success");
                            }
                            catch
                            {
                            }
                        }
                        else
                        {
                            Response.Write(TradeStatus);
                        }
                    }
                    catch (Exception exc)
                    {
                        Response.Write(exc.ToString());
                    }
                }
                else //验证失败
                {
                    Response.Write("fail");
                }
            }
            else
            {
                Response.Write("无返回参数");
            }
        }
    }
}
