using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NFinal.Middleware
{
    public enum UrlRouteRule
    {
        /// <summary>
        /// /Area/Controller/CustomActionUrl;
        /// </summary>
        AreaControllerCustomActionUrl,
        /// <summary>
        /// /Area/Controller/Action/parameter1/parameter2/.....(.html|.php|.asp|.aspx|.cshtml)
        /// </summary>
        AreaControllerActionParameters
    }
    public enum Mode
    {
        On,
        Off
    }
    public class CustomErrors
    {
        public Mode mode = Mode.Off;
        public string defaultRedirect = string.Empty;
    }
    public class Plug
    {
        /// <summary>
        /// dll文件路径
        /// </summary>
        public string filePath;
        /// <summary>
        /// 二级域名
        /// </summary>
        public string subDomain;
    }
    public class MiddlewareConfigOptions
    {
        public string debugUrl;
        /// <summary>
        /// 插件集合
        /// </summary>
        public Plug[] plugs;
        /// <summary>
        /// 是否是调试状态
        /// </summary>
        public bool debug = true;
        public string debugDirectory = "Debug";
        /// <summary>
        /// 默认文档
        /// </summary>
        public string defaultDocument = "Index.html";
        /// <summary>
        /// 默认后缀
        /// </summary>
        public string defaultSuffix = ".html";
        /// <summary>
        /// 自定义错误页
        /// </summary>
        public CustomErrors customErrors;
        /// <summary>
        /// Url路由规则
        /// </summary>
        public UrlRouteRule urlRouteRule = UrlRouteRule.AreaControllerActionParameters;
        /// <summary>
        /// 默认二级域名(当主机为localhost,需要一个默认的二级域名)。
        /// </summary>
        public string defaultSubDomain = "www";
        /// <summary>
        /// 服务器可接受的请求类型
        /// </summary>
        public string[] verbs = new string[] { "GET", "POST", "HEAD","DEBUG" };

        public void LoadFromWebConfig()
        {

        }
        public void LoadFromJsonConfig()
        {

        }
    }
}