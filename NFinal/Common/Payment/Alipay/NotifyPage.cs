using System;
using System.Collections.Generic;
using System.Linq;

namespace Alipay
{
    /// <summary>
    /// pc网页支付接口接入页
    /// </summary>
    public abstract class NotifyPage : System.Web.UI.Page
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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            var sPara = GetRequestPost();
            Core.LogResult(Request.Url.ToString());

            if (sPara.Count > 0) //判断是否有带返回参数
            {
                var formString = Request.Form.Keys.Cast<string>()
                                        .Aggregate("", (current, key) =>
                                                   current + string.Format("{0} = {1}\r\n", key.PadLeft(30, ' '),
                                                                 Request.Form[key]));
                Core.LogResult(formString);

                var aliNotify = new Notify();
                var verifyResult = aliNotify.Verify(sPara, Request.Form["notify_id"], Request.Form["sign"]);
                if (verifyResult)//验证成功
                {
                    OrderNo = Request.Form["out_trade_no"];
                    TradeNo = Request.Form["trade_no"];
                    TradeStatus = Request.Form["trade_status"];

                    //该种交易状态只在一种情况下出现——开通了高级即时到账，买家付款成功后。
                    if (TradeStatus == "TRADE_FINISHED" || TradeStatus == "TRADE_SUCCESS")
                    {
                        OnNotifyConfirm(); //业务逻辑处理
                        Core.LogResult(string.Format("业务逻辑处理,OrderNo:{0},TradeNo:{1},TradeStatus:{2}", OrderNo, TradeNo, TradeStatus));
                    }
                    Response.Write("success");
                }
                else
                {
                    Response.Write("fail");
                }
            }
            else
            {
                Response.Write("无返回参数");
            }
        }

        /// <summary>
        /// 获取支付宝POST过来通知消息，并以“参数名=参数值”的形式组成数组
        /// </summary>
        private SortedDictionary<string, string> GetRequestPost()
        {
            int i;
            var sArray = new SortedDictionary<string, string>();
            var coll = Request.Form;
            var requestItem = coll.AllKeys;
            for (i = 0; i < requestItem.Length; i++)
                sArray.Add(requestItem[i], Request.Form[requestItem[i]]);
            return sArray;
        }
    }
}
