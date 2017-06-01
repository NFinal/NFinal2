//======================================================================
//
//        Copyright : Zhengzhou Strawberry Computer Technology Co.,LTD.
//        All rights reserved
//        
//        Application:NFinal MVC framework
//        Filename : ContentType.cs
//        Description :Http请求文档类型
//
//        created by Lucas at  2015-5-31
//     
//        WebSite:http://www.nfinal.com
//
//======================================================================
namespace NFinal.Http
{
    /// <summary>
    /// Http请求文档类型
    /// </summary>
    public enum ContentType
    {
        /// <summary>
        /// 未知
        /// </summary>
        NONE = 0,
        /// <summary>
        /// form提交，不带文件
        /// </summary>
        Application_x_www_form_urlencoded = 1,
        /// <summary>
        /// form提交，带文件
        /// </summary>
        Multipart_form_data = 2,
        /// <summary>
        /// Json提交
        /// </summary>
        Application_json = 3,
        /// <summary>
        /// xml提交
        /// </summary>
        Text_xml = 4,
        /// <summary>
        /// 二进制流提交，即文件提交
        /// </summary>
        Application_octet_stream = 5
    }
}