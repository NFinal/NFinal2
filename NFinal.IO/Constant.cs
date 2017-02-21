using System;
using System.Text;
using System.Collections.Generic;

namespace NFinal
{
    /// <summary>
    /// 常量字符串，可以减少内存占用
    /// </summary>
    public struct Constant
    {
        /// <summary>
        /// 默认编码
        /// </summary>
        public static readonly Encoding encoding = new System.Text.UTF8Encoding(false);
        /// <summary>
        /// Headers头 Set-Cookie
        /// </summary>
        public const string HeaderCookie = "Cookie";
        /// <summary>
        /// & Char And
        /// </summary>
        public const char CharAnd = '&';
        public const char CharSemicolon = ';';
        /// <summary>
        /// = Char Equal sign
        /// </summary>
        public const char CharEqual = '=';
        /// <summary>
        /// "POST"
        /// </summary>
        public const string MethodTypePOST = "POST";
        /// <summary>
        /// "GET"
        /// </summary>
        public const string MethodTypeGET = "GET";
        /// <summary>
        /// "DELETE"
        /// </summary>
        public const string MethodTypeDELETE = "DELETE";
        /// <summary>
        /// "PUT"
        /// </summary>
        public const string MethodTypePUT = "PUT";
        /// <summary>
        /// "AJAX"
        /// </summary>
        public const string MethodTypeAJAX = "AJAX";
        /// <summary>
        /// "multipart/form-data"
        /// </summary>
        public const string ContentType_Multipart_form_data = "multipart/form-data";
        /// <summary>
        /// "text/json"
        /// </summary>
        public const string ContentType_Text_json = "text/json";
        /// <summary>
        /// "application/json"
        /// </summary>
        public const string ContentType_Application_json = "application/json";
        /// <summary>
        /// "application/x-www-form-urlencoded"
        /// </summary>
        public const string ContentType_Application_x_www_form_urlencoded = "application/x-www-form-urlencoded";
        /// <summary>
        /// "application/xml"
        /// </summary>
        public const string ContentType_Application_xml = "application/xml";
        /// <summary>
        /// "text/xml"
        /// </summary>
        public const string ContentType_Text_xml = "text/xml";
        /// <summary>
        /// "Host"
        /// </summary>
        public const string HeaderHost = "Host";
        /// <summary>
        /// "localhost"
        /// </summary>
        public const string Localhost = "localhost";
        /// <summary>
        /// "application/json ;charset=utf-8"
        /// </summary>
        public const string ResponseContentType_Application_json = "application/json ;charset=utf-8";
        /// <summary>
        /// "text/html ;charset=utf-8"
        /// </summary>
        public const string ResponseContentType_Text_html = "text/html ;charset=utf-8";
        /// <summary>
        /// Headers头 Set-Cookie
        /// </summary>
        public const string HeaderSetCookie="Set-Cookie";
        public const string HeaderSetCookieEqual = "=";
        public const string HeaderSetCookiePath = "; path=/";
        public const string HeaderSetCookieExpire = "=; expires=Thu, 01-Jan-1970 00:00:00 GMT";
        /// <summary>
        /// Headers头 Content-Type
        /// </summary>
        public const string HeaderContentType = "Content-Type";
        /// <summary>
        /// sessionId的名称
        /// </summary>
        public const string sessionKey = "session_id";
        /// <summary>
        /// session通道的名称
        /// </summary>
        public const string SessionChannel = "Session:";
        /// <summary>
        /// http协议中的分隔符
        /// </summary>
        public const string SchemeDelimiter = "://";
        /// <summary>
        /// 左中括号
        /// </summary>
        public const string openingBracket = "[";
        /// <summary>
        /// 右中括号
        /// </summary>
        public const string closingBracket = "]";
        /// <summary>
        /// null字符串
        /// </summary>
        public const string nullString="null";
        /// <summary>
        /// "true"
        /// </summary>
        public const string trueString = "true";
        /// <summary>
        /// "false"
        /// </summary>
        public const string falseString = "false";
        /// <summary>
        /// "<br/>"
        /// </summary>
        public const string Html_Br = "<br/>";
        /// <summary>
        /// "{\"code\":"
        /// </summary>
        public const string AjaxReturnJson_Code = "{\"code\":";
        /// <summary>
        /// ",\"msg\":\""
        /// </summary>
        public const string AjaxReturnJson_Msg = ",\"msg\":\"";
        /// <summary>
        /// "\",\"result\":"
        /// </summary>
        public const string AjaxReturnJson_Json = "\",\"result\":";
        /// <summary>
        /// "}"
        /// </summary>
        public const string AjaxReturnJson_End = "}";
        /// <summary>
        /// "Location"
        /// </summary>
        public const string HeaderLocation = "Location";
        /// <summary>
        /// "Content-Encoding"
        /// </summary>
        public const string HeaderContentEncoding = "Content-Encoding";
        /// <summary>
        /// new string[]{ "gzip" }
        /// </summary>
        public static readonly string[] HeaderContentEncodingGzip =new string[]{ "gzip" };
        /// <summary>
        /// new string[] { "deflate" }
        /// </summary>
        public static readonly string[] HeaderContentEncodingDeflate =new string[] { "deflate" };
        /// <summary>
        /// new string[] { "Android", "iPhone", "iPod", "iPad", "Windows Phone", "MQQBrowser" }
        /// </summary>
        public static readonly string[] UserAgent_MobileKeyWords =new string[] { "Android", "iPhone", "iPod", "iPad", "Windows Phone", "MQQBrowser" };
        /// <summary>
        /// "Windows NT"
        /// </summary>
        public const string UserAgnet_Windows_NT = "Windows NT";
        /// <summary>
        /// "Macintosh"
        /// </summary>
        public const string UserAgent_Macintosh = "Macintosh";
        /// <summary>
        /// 逗号
        /// </summary>
        public const string comma = ",";
        /// <summary>
        /// 单引号
        /// </summary>
        public const string CsharpSingleQuote = "\\'";
        /// <summary>
        /// 双引号
        /// </summary>
        public const string CsharpDoubleQuote = "\\\"";
        /// <summary>
        /// 斜杠
        /// </summary>
        public const string CsharpSlash = "\\\\";
        /// <summary>
        /// \0
        /// </summary>
        public const string CsharpSlash0 = "\\0";
        /// <summary>
        /// \a
        /// </summary>
        public const string CsharpSlasha = "\\a";
        /// <summary>
        /// \b
        /// </summary>
        public const string CsharpSlashb = "\\b";
        /// <summary>
        /// \f
        /// </summary>
        public const string CsharpSlashf = "\\f";
        /// <summary>
        /// \n
        /// </summary>
        public const string CsharpSlashn = "\\n";
        /// <summary>
        /// \r
        /// </summary>
        public const string CsharpSlashr = "\\r";
        /// <summary>
        /// \t
        /// </summary>
        public const string CsharpSlasht = "\\t";
        /// <summary>
        /// \v
        /// </summary>
        public const string CsharpSlashv = "\\v";
    }
}