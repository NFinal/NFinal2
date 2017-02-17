using System.Configuration;

namespace Alipay
{
    /// <summary>
    /// 账户配置
    /// 1.登录支付宝网站(www.alipay.com)
    /// 2.点击“商家服务”(https://b.alipay.com/order/myOrder.htm)
    /// 3.点击“查询合作者身份(PID)”、“查询安全校验码(Key)”
    /// </summary>
    public class Config
    {
        /// <summary>
        /// 合作者身份ID
        /// </summary>
        public static string Partner{ internal get; set; }

        /// <summary>
        /// 交易安全校验码
        /// </summary>
        public static string Key { internal get; set; }

        /// <summary>
        /// 卖方（收款方）支付宝账户
        /// </summary>
        public static string SellerEmail { internal get; set; }

        /// <summary>
        /// 字符编码格式:utf-8
        /// </summary>
        internal static string _Charset { get { return "utf-8"; } }

        /// <summary>
        /// 签名方式:MD5
        /// </summary>
        internal static string _SignType { get { return "MD5"; } }

        /// <summary>
        /// 支付宝网关地址pc
        /// </summary>
        internal static string _GateWay { get { return "https://mapi.alipay.com/gateway.do?"; } }

        /// <summary>
        /// 支付宝网关地址wap
        /// </summary>
        internal static string _GateWayWap { get { return "http://wappaygw.alipay.com/service/rest.htm?"; } }

        /// <summary>
        /// 支付宝消息验证地址
        /// </summary>
        internal static string _VeryfyUrl { get { return "https://mapi.alipay.com/gateway.do?service=notify_verify&"; } }

        /// <summary>
        /// 交易方式:即时到账交易pc
        /// </summary>
        internal static string _PayMode { get { return "create_direct_pay_by_user"; } }

        /// <summary>
        /// 支付类型:1
        /// </summary>
        internal static string _PayType { get { return "1"; } }

        /// <summary>
        /// 日志路径:/log/Alipay
        /// </summary>
        internal static string _LogPath { get { return "/log/Alipay"; } }

        /// <summary>
        /// 商户的私钥,如果签名方式设置为“0001”时，请设置该参数
        /// </summary>
        internal static string _PrivateKey { get { return ""; } }

        /// <summary>
        /// 商户的公钥,如果签名方式设置为“0001”时，请设置该参数
        /// </summary>
        internal static string _PublicKey { get { return ""; } }

        static Config()
        {
            if (string.IsNullOrEmpty(Partner))
                Partner = ConfigurationManager.AppSettings["Alipay.Partner"];
            if (string.IsNullOrEmpty(Key))
                Key = ConfigurationManager.AppSettings["Alipay.Key"];
            if (string.IsNullOrEmpty(SellerEmail))
                SellerEmail = ConfigurationManager.AppSettings["Alipay.SellerEmail"];
        }
    }
}