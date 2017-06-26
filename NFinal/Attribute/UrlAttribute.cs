//======================================================================
//
//        Copyright : Zhengzhou Strawberry Computer Technology Co.,LTD.
//        All rights reserved
//        
//        Application:NFinal MVC framework
//        Filename : UrlAttribute.cs
//        Description :对控制器行为设置请求方法，响应类型，压缩方式以及请求URL的特性
//
//        created by Lucas at  2015-5-31
//     
//        WebSite:http://www.nfinal.com
//
//======================================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NFinal.Http;

namespace NFinal
{
    /// <summary>
    /// 对控制器行为设置请求方法，响应类型，压缩方式以及请求URL的特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Method,AllowMultiple=false,Inherited=false)]
    public class UrlAttribute: System.Attribute
    {
        /// <summary>
        /// 控制器行为的URl设置
        /// </summary>
        /// <param name="urlString">请求URL</param>
        public UrlAttribute(string urlString)
        {
            this.urlString = urlString;
            this.methodType = MethodType.NONE;
            this.contentType = "text/html; charset=utf-8";
            this.compressMode = CompressMode.None;
        }
        /// <summary>
        /// 请求URL
        /// </summary>
        public string urlString { get; set; }
        /// <summary>
        /// 请求的类型
        /// </summary>
        public MethodType methodType { get; set; }
        /// <summary>
        /// 响应类型
        /// </summary>
        public string contentType { get; set; }
        /// <summary>
        /// 压缩模式
        /// </summary>
        public CompressMode compressMode { get; set; }
    }
    /// <summary>
    /// form表单提交并返回html
    /// </summary>
    public class FormHtmlAttribute : UrlAttribute
    {
        /// <summary>
        /// form表单提交并返回html
        /// </summary>
        /// <param name="urlString">URL请求路径</param>
        public FormHtmlAttribute(string urlString):base(urlString)
        {
            this.urlString = urlString;
            this.compressMode = CompressMode.None;
        }
    }
    ///// <summary>
    ///// 返回html
    ///// </summary>
    //public class HtmlAttribute : UrlAttribute
    //{
    //    /// <summary>
    //    /// 定义URL请求路径
    //    /// </summary>
    //    /// <param name="path">URL请求路径</param>
    //    public HtmlAttribute(string path)
    //    { }
    //}
    /// <summary>
    /// get提交并返回html
    /// </summary>
    public class GetHtmlAttribute : UrlAttribute
    {
        /// <summary>
        /// get提交并返回html
        /// </summary>
        /// <param name="urlString">URL请求路径</param>
        public GetHtmlAttribute(string urlString) : base(urlString)
        {
            this.methodType = MethodType.GET;
            this.contentType = "text/html; charset=utf-8";
        }
    }
    /// <summary>
    /// post提交并返回html
    /// </summary>
    public class PostHtmlAttribute : UrlAttribute
    {
        /// <summary>
        /// post提交并返回html
        /// </summary>
        /// <param name="urlString">URL请求路径</param>
        public PostHtmlAttribute(string urlString) : base(urlString)
        {
            this.methodType= this.methodType = MethodType.POST;
            this.contentType = "text/html; charset=utf-8";
        }
    }
    ///// <summary>
    ///// 请求并返回zip压缩后的html
    ///// </summary>
    //public class HtmlZipAttribute : UrlAttribute
    //{
    //    /// <summary>
    //    /// 定义URL请求路径
    //    /// </summary>
    //    /// <param name="path">URL请求路径</param>
    //    public HtmlZipAttribute(string path)
    //    { }
    //}
    ///// <summary>
    ///// get请求并返回zip压缩后的html
    ///// </summary>
    //public class GetHtmlZipAttribute : UrlAttribute
    //{
    //    /// <summary>
    //    /// 定义URL请求路径
    //    /// </summary>
    //    /// <param name="path">URL请求路径</param>
    //    public GetHtmlZipAttribute(string path)
    //    { }
    //}
    ///// <summary>
    ///// post请求并返回zip压缩后的html
    ///// </summary>
    //public class PostHtmlZipAttribute : UrlAttribute
    //{
    //    /// <summary>
    //    /// 定义URL请求路径
    //    /// </summary>
    //    /// <param name="path">URL请求路径</param>
    //    public PostHtmlZipAttribute(string path)
    //    { }
    //}
    ///// <summary>
    ///// 请求并返回deflate压缩后的html
    ///// </summary>
    //public class HtmlDeflateAttribute : UrlAttribute
    //{
    //    /// <summary>
    //    /// 定义URL请求路径
    //    /// </summary>
    //    /// <param name="path">URL请求路径</param>
    //    public HtmlDeflateAttribute(string path)
    //    { }
    //}
    ///// <summary>
    ///// get请求并返回deflate压缩后的html
    ///// </summary>
    //public class GetHtmlDeflateAttribute : UrlAttribute
    //{
    //    /// <summary>
    //    /// 定义URL请求路径
    //    /// </summary>
    //    /// <param name="path">URL请求路径</param>
    //    public GetHtmlDeflateAttribute(string path)
    //    { }
    //}
    ///// <summary>
    ///// post请求并返回deflate压缩后的html
    ///// </summary>
    //public class PostHtmlDeflateAttribute : UrlAttribute
    //{
    //    /// <summary>
    //    /// 定义URL请求路径
    //    /// </summary>
    //    /// <param name="path">URL请求路径</param>
    //    public PostHtmlDeflateAttribute(string path)
    //    { }
    //}
    ///// <summary>
    ///// 请求并返回空页面
    ///// </summary>
    //public class EmptyAttribute : UrlAttribute
    //{
    //    /// <summary>
    //    /// 定义URL请求路径
    //    /// </summary>
    //    /// <param name="path">URL请求路径</param>
    //    public EmptyAttribute(string path)
    //    { }
    //}
    ///// <summary>
    ///// get请求并返回空页面
    ///// </summary>
    //public class GetEmptyAttribute : UrlAttribute
    //{
    //    /// <summary>
    //    /// 定义URL请求路径
    //    /// </summary>
    //    /// <param name="path">URL请求路径</param>
    //    public GetEmptyAttribute(string path)
    //    { }
    //}
    ///// <summary>
    ///// post请求并返回空页面
    ///// </summary>
    //public class PostEmptyAttribute : UrlAttribute
    //{
    //    /// <summary>
    //    /// 定义URL请求路径
    //    /// </summary>
    //    /// <param name="path">URL请求路径</param>
    //    public PostEmptyAttribute(string path)
    //    { }
    //}
    ///// <summary>
    ///// 请求后页面重定向
    ///// </summary>
    //public class RedirectAttribute : UrlAttribute
    //{
    //    /// <summary>
    //    /// 定义URL请求路径
    //    /// </summary>
    //    /// <param name="path">URL请求路径</param>
    //    public RedirectAttribute(string path)
    //    { }
    //}
    /// <summary>
    /// get请求后页面重定向
    /// </summary>
    public class GetRedirectAttribute : UrlAttribute
    {
        /// <summary>
        /// get请求后页面重定向
        /// </summary>
        /// <param name="urlString">URL请求路径</param>
        public GetRedirectAttribute(string urlString) : base(urlString)
        {
            this.methodType = MethodType.GET;
            this.contentType = "text/html; charset=utf-8";
        }
    }
    ///// <summary>
    ///// post请求后页面重定向
    ///// </summary>
    //public class PostRedirectAttribute : UrlAttribute
    //{
    //    /// <summary>
    //    /// 定义URL请求路径
    //    /// </summary>
    //    /// <param name="path">URL请求路径</param>
    //    public PostRedirectAttribute(string path)
    //    { }
    //}
    ///// <summary>
    ///// 请求后返回json
    ///// </summary>
    //public class JsonAttribute : UrlAttribute
    //{
    //    /// <summary>
    //    /// 定义URL请求路径
    //    /// </summary>
    //    /// <param name="path">URL请求路径</param>
    //    public JsonAttribute(string path)
    //    { }
    //}
    /// <summary>
    /// get请求后返回json
    /// </summary>
    public class GetJsonAttribute : UrlAttribute
    {
        /// <summary>
        /// get请求后返回json
        /// </summary>
        /// <param name="urlString">URL请求路径</param>
        public GetJsonAttribute(string urlString) : base(urlString)
        {
            this.methodType = MethodType.GET;
            this.contentType = "application/json; charset=utf-8";
        }
    }
    /// <summary>
    /// post请求后返回json
    /// </summary>
    public class PostJsonAttribute : UrlAttribute
    {
        /// <summary>
        /// post请求后返回json
        /// </summary>
        /// <param name="urlString">URL请求路径</param>
        public PostJsonAttribute(string urlString) : base(urlString)
        {
            this.methodType = MethodType.POST;
            this.contentType = "application/json; charset=utf-8";
        }
    }
    ///// <summary>
    ///// 请求后返回zip压缩的json
    ///// </summary>
    //public class JsonZipAttribute : UrlAttribute
    //{
    //    /// <summary>
    //    /// 定义URL请求路径
    //    /// </summary>
    //    /// <param name="path">URL请求路径</param>
    //    public JsonZipAttribute(string path)
    //    { }
    //}
    ///// <summary>
    ///// get请求后返回zip压缩的json
    ///// </summary>
    //public class GetJsonZipAttribute : UrlAttribute
    //{
    //    /// <summary>
    //    /// 定义URL请求路径
    //    /// </summary>
    //    /// <param name="path">URL请求路径</param>
    //    public GetJsonZipAttribute(string path)
    //    { }
    //}
    ///// <summary>
    ///// post请求后返回zip压缩的json
    ///// </summary>
    //public class PostJsonZipAttribute : UrlAttribute
    //{
    //    /// <summary>
    //    /// 定义URL请求路径
    //    /// </summary>
    //    /// <param name="path">URL请求路径</param>
    //    public PostJsonZipAttribute(string path)
    //    { }
    //}
    ///// <summary>
    ///// 请求后返回deflate压缩的json
    ///// </summary>
    //public class JsonDeflateAttribute : UrlAttribute
    //{
    //    /// <summary>
    //    /// 定义URL请求路径
    //    /// </summary>
    //    /// <param name="path">URL请求路径</param>
    //    public JsonDeflateAttribute(string path)
    //    { }
    //}
    ///// <summary>
    ///// get请求后返回deflate压缩的json
    ///// </summary>
    //public class GetJsonDeflateAttribute : UrlAttribute
    //{
    //    /// <summary>
    //    /// 定义URL请求路径
    //    /// </summary>
    //    /// <param name="path">URL请求路径</param>
    //    public GetJsonDeflateAttribute(string path)
    //    { }
    //}
    ///// <summary>
    ///// post请求后返回deflate压缩的json
    ///// </summary>
    //public class PostJsonDeflateAttribute : UrlAttribute
    //{
    //    /// <summary>
    //    /// 定义URL请求路径
    //    /// </summary>
    //    /// <param name="path">URL请求路径</param>
    //    public PostJsonDeflateAttribute(string path)
    //    { }
    //}
    //   /// <summary>
    //   /// 请求后返回javascript
    //   /// </summary>
    //   public class JavaScriptAttribute : UrlAttribute
    //   {
    //       /// <summary>
    //       /// 定义URL请求路径
    //       /// </summary>
    //       /// <param name="path">URL请求路径</param>
    //       public JavaScriptAttribute(string path)
    //       { }
    //   }
    //   /// <summary>
    //   /// get请求后返回javascript
    //   /// </summary>
    //   public class GetJavaScriptAttribute : UrlAttribute
    //   {
    //       /// <summary>
    //       /// 定义URL请求路径
    //       /// </summary>
    //       /// <param name="path">URL请求路径</param>
    //       public GetJavaScriptAttribute(string path)
    //       { }
    //   }
    //   /// <summary>
    //   /// post请求后返回javascript
    //   /// </summary>
    //   public class PostJavaScriptAttribute : UrlAttribute
    //   {
    //       /// <summary>
    //       /// 定义URL请求路径
    //       /// </summary>
    //       /// <param name="path">URL请求路径</param>
    //       public PostJavaScriptAttribute(string path)
    //       { }
    //   }
    //   /// <summary>
    //   /// 请求后返回zip压缩的javascript
    //   /// </summary>
    //   public class JavaScriptZipAttribute : UrlAttribute
    //   {
    //       /// <summary>
    //       /// 定义URL请求路径
    //       /// </summary>
    //       /// <param name="path">URL请求路径</param>
    //       public JavaScriptZipAttribute(string path)
    //       { }
    //   }
    //   /// <summary>
    //   /// get请求后返回zip压缩的javascript
    //   /// </summary>
    //   public class GetJavaScriptZipAttribute : UrlAttribute
    //   {
    //       /// <summary>
    //       /// 定义URL请求路径
    //       /// </summary>
    //       /// <param name="path">URL请求路径</param>
    //       public GetJavaScriptZipAttribute(string path)
    //       { }
    //   }
    //   /// <summary>
    //   /// post请求后返回zip压缩的javascript
    //   /// </summary>
    //   public class PostJavaScriptZipAttribute : UrlAttribute
    //   {
    //       /// <summary>
    //       /// 定义URL请求路径
    //       /// </summary>
    //       /// <param name="path">URL请求路径</param>
    //       public PostJavaScriptZipAttribute(string path)
    //       { }
    //   }
    //   /// <summary>
    //   /// 请求后返回deflate压缩的javascript
    //   /// </summary>
    //   public class JavaScriptDeflateAttribute : UrlAttribute
    //   {
    //       /// <summary>
    //       /// 定义URL请求路径
    //       /// </summary>
    //       /// <param name="path">URL请求路径</param>
    //       public JavaScriptDeflateAttribute(string path)
    //       { }
    //   }
    //   /// <summary>
    //   /// get请求后返回deflate压缩的javascript
    //   /// </summary>
    //   public class GetJavaScriptDeflateAttribute : UrlAttribute
    //   {
    //       /// <summary>
    //       /// 定义URL请求路径
    //       /// </summary>
    //       /// <param name="path">URL请求路径</param>
    //       public GetJavaScriptDeflateAttribute(string path)
    //       { }
    //   }
    //   /// <summary>
    //   /// get请求后返回deflate压缩的javascript
    //   /// </summary>
    //   public class PostJavaScriptDeflateAttribute : UrlAttribute
    //   {
    //       /// <summary>
    //       /// 定义URL请求路径
    //       /// </summary>
    //       /// <param name="path">URL请求路径</param>
    //       public PostJavaScriptDeflateAttribute(string path)
    //       { }
    //   }
    //   /// <summary>
    //   /// 请求后返回css样式
    //   /// </summary>
    //   public class CssAttribute : UrlAttribute
    //   {
    //       /// <summary>
    //       /// 定义URL请求路径
    //       /// </summary>
    //       /// <param name="path">URL请求路径</param>
    //       public CssAttribute(string path)
    //       { }
    //   }
    //   /// <summary>
    //   /// get请求后返回css样式
    //   /// </summary>
    //   public class GetCssAttribute : UrlAttribute
    //   {
    //       /// <summary>
    //       /// 定义URL请求路径
    //       /// </summary>
    //       /// <param name="path">URL请求路径</param>
    //       public GetCssAttribute(string path)
    //       { }
    //   }
    //   /// <summary>
    //   /// post请求后返回css样式
    //   /// </summary>
    //   public class PostCssAttribute : UrlAttribute
    //   {
    //       /// <summary>
    //       /// 定义URL请求路径
    //       /// </summary>
    //       /// <param name="path">URL请求路径</param>
    //       public PostCssAttribute(string path)
    //       { }
    //   }
    //   /// <summary>
    //   /// 请求后返回zip压缩的css样式
    //   /// </summary>
    //   public class CssZipAttribute : UrlAttribute
    //   {
    //       /// <summary>
    //       /// 定义URL请求路径
    //       /// </summary>
    //       /// <param name="path">URL请求路径</param>
    //       public CssZipAttribute(string path)
    //       { }
    //   }
    //   /// <summary>
    //   /// get请求后返回zip压缩的css样式
    //   /// </summary>
    //   public class GetCssZipAttribute : UrlAttribute
    //   {
    //       /// <summary>
    //       /// 定义URL请求路径
    //       /// </summary>
    //       /// <param name="path">URL请求路径</param>
    //       public GetCssZipAttribute(string path)
    //       { }
    //   }
    //   /// <summary>
    //   /// post请求后返回zip压缩的css样式
    //   /// </summary>
    //   public class PostCssZipAttribute : UrlAttribute
    //   {
    //       /// <summary>
    //       /// 定义URL请求路径
    //       /// </summary>
    //       /// <param name="path">URL请求路径</param>
    //       public PostCssZipAttribute(string path)
    //       { }
    //   }
    //   /// <summary>
    //   /// 请求后返回deflate压缩的css样式
    //   /// </summary>
    //   public class CssDeflateAttribute : UrlAttribute
    //   {
    //       /// <summary>
    //       /// 定义URL请求路径
    //       /// </summary>
    //       /// <param name="path">URL请求路径</param>
    //       public CssDeflateAttribute(string path)
    //       { }
    //   }
    //   /// <summary>
    //   /// get请求后返回deflate压缩的css样式
    //   /// </summary>
    //   public class GetCssDeflateAttribute : UrlAttribute
    //   {
    //       /// <summary>
    //       /// 定义URL请求路径
    //       /// </summary>
    //       /// <param name="path">URL请求路径</param>
    //       public GetCssDeflateAttribute(string path)
    //       { }
    //   }
    //   /// <summary>
    //   /// post请求后返回deflate压缩的css样式
    //   /// </summary>
    //   public class PostCssDeflateAttribute : UrlAttribute
    //   {
    //       /// <summary>
    //       /// 定义URL请求路径
    //       /// </summary>
    //       /// <param name="path">URL请求路径</param>
    //       public PostCssDeflateAttribute(string path)
    //       { }
    //   }
    //   /// <summary>
    //   /// 请求后返回二进制文件
    //   /// </summary>
    //   public class FileAttribute : UrlAttribute
    //   {
    //       /// <summary>
    //       /// 定义URL请求路径
    //       /// </summary>
    //       /// <param name="path">URL请求路径</param>
    //       public FileAttribute(string path)
    //       { }
    //   }
    //   /// <summary>
    //   /// get请求后返回二进制文件
    //   /// </summary>
    //   public class GetFileAttribute : UrlAttribute
    //   {
    //       /// <summary>
    //       /// 定义URL请求路径
    //       /// </summary>
    //       /// <param name="path">URL请求路径</param>
    //       public GetFileAttribute(string path)
    //       { }
    //   }
    //   /// <summary>
    //   /// post请求后返回二进制文件
    //   /// </summary>
    //   public class PostFileAttribute : UrlAttribute
    //   {
    //       /// <summary>
    //       /// 定义URL请求路径
    //       /// </summary>
    //       /// <param name="path">URL请求路径</param>
    //       public PostFileAttribute(string path)
    //       { }
    //   }
    //   /// <summary>
    //   /// 请求后返回jpeg图片
    //   /// </summary>
    //   public class JpegAttribute : UrlAttribute
    //   {
    //       /// <summary>
    //       /// 定义URL请求路径
    //       /// </summary>
    //       /// <param name="path">URL请求路径</param>
    //       public JpegAttribute(string path)
    //       { }
    //   }
    //   /// <summary>
    //   /// get请求后返回jpeg图片
    //   /// </summary>
    //public class GetJpegAttribute : UrlAttribute
    //   {
    //       /// <summary>
    //       /// 定义URL请求路径
    //       /// </summary>
    //       /// <param name="path">URL请求路径</param>
    //	public GetJpegAttribute(string path)
    //       { }
    //   }
    //   /// <summary>
    //   /// post请求后返回jpeg图片
    //   /// </summary>
    //public class PostJpegAttribute : UrlAttribute
    //   {
    //       /// <summary>
    //       /// 定义URL请求路径
    //       /// </summary>
    //       /// <param name="path">URL请求路径</param>
    //	public PostJpegAttribute(string path)
    //       { }
    //   }
    //   /// <summary>
    //   /// 请求后返回gif图片
    //   /// </summary>
    //public class GifAttribute : UrlAttribute
    //{
    //       /// <summary>
    //       /// 定义URL请求路径
    //       /// </summary>
    //       /// <param name="path">URL请求路径</param>
    //	public GifAttribute(string path)
    //	{ }
    //}
    //   /// <summary>
    //   /// get请求后返回gif图片
    //   /// </summary>
    //public class GetGifAttribute : UrlAttribute
    //{
    //       /// <summary>
    //       /// 定义URL请求路径
    //       /// </summary>
    //       /// <param name="path">URL请求路径</param>
    //	public GetGifAttribute(string path)
    //	{ }
    //}
    //   /// <summary>
    //   /// post请求后返回gif图片
    //   /// </summary>
    //public class PostGifAttribute : UrlAttribute
    //{
    //       /// <summary>
    //       /// 定义URL请求路径
    //       /// </summary>
    //       /// <param name="path">URL请求路径</param>
    //	public PostGifAttribute(string path)
    //	{ }
    //}
    //   /// <summary>
    //   /// 请求后返回png图片
    //   /// </summary>
    //public class PngAttribute : UrlAttribute
    //{
    //       /// <summary>
    //       /// 定义URL请求路径
    //       /// </summary>
    //       /// <param name="path">URL请求路径</param>
    //	public PngAttribute(string path)
    //	{ }
    //}
    //   /// <summary>
    //   /// get请求后返回png图片
    //   /// </summary>
    //public class GetPngAttribute : UrlAttribute
    //{
    //       /// <summary>
    //       /// 定义URL请求路径
    //       /// </summary>
    //       /// <param name="path">URL请求路径</param>
    //	public GetPngAttribute(string path)
    //	{ }
    //}
    //   /// <summary>
    //   /// post请求后返回png图片
    //   /// </summary>
    //public class PostPngAttribute : UrlAttribute
    //{
    //       /// <summary>
    //       /// 定义URL请求路径
    //       /// </summary>
    //       /// <param name="path">URL请求路径</param>
    //	public PostPngAttribute(string path)
    //	{ }
    //}
    //   /// <summary>
    //   /// 请求后返回xml文本
    //   /// </summary>
    //   public class XmlAttribute : UrlAttribute
    //   {
    //       /// <summary>
    //       /// 定义URL请求路径
    //       /// </summary>
    //       /// <param name="path">URL请求路径</param>
    //       public XmlAttribute(string path)
    //       { }
    //   }
    /// <summary>
    /// get请求后返回xml文本
    /// </summary>
    public class GetXmlAttribute : UrlAttribute
    {
        /// <summary>
        /// get请求后返回xml文本
        /// </summary>
        /// <param name="urlString">URL请求路径</param>
        public GetXmlAttribute(string urlString):base(urlString)
        { }
    }
    /// <summary>
    /// post请求后返回xml文本
    /// </summary>
    public class PostXmlAttribute : UrlAttribute
    {
        /// <summary>
        /// post请求后返回xml文本
        /// </summary>
        /// <param name="urlString">URL请求路径</param>
        /// <param name="queryString">请求的QueryString的key数组</param>
        public PostXmlAttribute(string urlString):base(urlString)
        { }
    }
    //   /// <summary>
    //   /// 请求后返回zip压缩的xml文本
    //   /// </summary>
    //   public class XmlZipAttribute : UrlAttribute
    //   {
    //       /// <summary>
    //       /// 定义URL请求路径
    //       /// </summary>
    //       /// <param name="path">URL请求路径</param>
    //       public XmlZipAttribute(string path)
    //       { }
    //   }
    //   /// <summary>
    //   /// get请求后返回zip压缩的xml文本
    //   /// </summary>
    //   public class GetXmlZipAttribute : UrlAttribute
    //   {
    //       /// <summary>
    //       /// 定义URL请求路径
    //       /// </summary>
    //       /// <param name="path">URL请求路径</param>
    //       public GetXmlZipAttribute(string path)
    //       { }
    //   }
    //   /// <summary>
    //   /// post请求后返回zip压缩的xml文本
    //   /// </summary>
    //   public class PostXmlZipAttribute : UrlAttribute
    //   {
    //       /// <summary>
    //       /// 定义URL请求路径
    //       /// </summary>
    //       /// <param name="path">URL请求路径</param>
    //       public PostXmlZipAttribute(string path)
    //       { }
    //   }
    //   /// <summary>
    //   /// 请求后返回deflate压缩的xml文本
    //   /// </summary>
    //   public class XmlDeflateAttribute : UrlAttribute
    //   {
    //       /// <summary>
    //       /// 定义URL请求路径
    //       /// </summary>
    //       /// <param name="path">URL请求路径</param>
    //       public XmlDeflateAttribute(string path)
    //       { }
    //   }
    //   /// <summary>
    //   /// get请求后返回deflate压缩的xml文本
    //   /// </summary>
    //   public class GetXmlDeflateAttribute : UrlAttribute
    //   {
    //       /// <summary>
    //       /// 定义URL请求路径
    //       /// </summary>
    //       /// <param name="path">URL请求路径</param>
    //       public GetXmlDeflateAttribute(string path)
    //       { }
    //   }
    //   /// <summary>
    //   /// post请求后返回deflate压缩的xml文本
    //   /// </summary>
    //   public class PostXmlDeflateAttribute : UrlAttribute
    //   {
    //       /// <summary>
    //       /// 定义URL请求路径
    //       /// </summary>
    //       /// <param name="path">URL请求路径</param>
    //       public PostXmlDeflateAttribute(string path)
    //       { }
    //   }
    //   /// <summary>
    //   /// 请求后返回文本
    //   /// </summary>
    //   public class TextAttribute : UrlAttribute
    //   {
    //       /// <summary>
    //       /// 定义URL请求路径
    //       /// </summary>
    //       /// <param name="path">URL请求路径</param>
    //       public TextAttribute(string path)
    //       { }
    //   }
    //   /// <summary>
    //   /// get请求后返回文本
    //   /// </summary>
    //   public class GetTextAttribute : UrlAttribute
    //   {
    //       /// <summary>
    //       /// 定义URL请求路径
    //       /// </summary>
    //       /// <param name="path">URL请求路径</param>
    //       public GetTextAttribute(string path)
    //       { }
    //   }
    //   /// <summary>
    //   /// post请求后返回文本
    //   /// </summary>
    //   public class PostTextAttribute : UrlAttribute
    //   {
    //       /// <summary>
    //       /// 定义URL请求路径
    //       /// </summary>
    //       /// <param name="path">URL请求路径</param>
    //       public PostTextAttribute(string path)
    //       { }
    //   }
    //   /// <summary>
    //   /// 请求后返回zip压缩的文本
    //   /// </summary>
    //   public class TextZipAttribute : UrlAttribute
    //   {
    //       /// <summary>
    //       /// 定义URL请求路径
    //       /// </summary>
    //       /// <param name="path">URL请求路径</param>
    //       public TextZipAttribute(string path)
    //       { }
    //   }
    //   /// <summary>
    //   /// get请求后返回zip压缩的文本
    //   /// </summary>
    //   public class GetTextZipAttribute : UrlAttribute
    //   {
    //       /// <summary>
    //       /// 定义URL请求路径
    //       /// </summary>
    //       /// <param name="path">URL请求路径</param>
    //       public GetTextZipAttribute(string path)
    //       { }
    //   }
    //   /// <summary>
    //   /// post请求后返回zip压缩的文本
    //   /// </summary>
    //   public class PostTextZipAttribute : UrlAttribute
    //   {
    //       /// <summary>
    //       /// 定义URL请求路径
    //       /// </summary>
    //       /// <param name="path">URL请求路径</param>
    //       public PostTextZipAttribute(string path)
    //       { }
    //   }
    //   /// <summary>
    //   /// 请求后返回deflate压缩的文本
    //   /// </summary>
    //   public class TextDeflateAttribute : UrlAttribute
    //   {
    //       /// <summary>
    //       /// 定义URL请求路径
    //       /// </summary>
    //       /// <param name="path">URL请求路径</param>
    //       public TextDeflateAttribute(string path)
    //       { }
    //   }
    //   /// <summary>
    //   /// get请求后返回deflate压缩的文本
    //   /// </summary>
    //   public class GetTextDeflateAttribute : UrlAttribute
    //   {
    //       /// <summary>
    //       /// 定义URL请求路径
    //       /// </summary>
    //       /// <param name="path">URL请求路径</param>
    //       public GetTextDeflateAttribute(string path)
    //       { }
    //   }
    //   /// <summary>
    //   /// post请求后返回deflate压缩的文本
    //   /// </summary>
    //   public class PostTextDeflateAttribute : UrlAttribute
    //   {
    //       /// <summary>
    //       /// 定义URL请求路径
    //       /// </summary>
    //       /// <param name="path">URL请求路径</param>
    //       public PostTextDeflateAttribute(string path)
    //       { }
    //   }
    //   /// <summary>
    //   /// get请求后返回svg图像
    //   /// </summary>
    //   public class GetSvgAttribute : UrlAttribute
    //   {
    //       /// <summary>
    //       /// 定义URL请求路径
    //       /// </summary>
    //       /// <param name="path">URL请求路径</param>
    //       public GetSvgAttribute(string path)
    //       {
    //       }
    //   }
    //   /// <summary>
    //   /// get请求后返回压缩后的svg图像
    //   /// </summary>
    //   public class GetSvgZipAttribute : UrlAttribute
    //   {
    //       /// <summary>
    //       /// 定义URL请求路径
    //       /// </summary>
    //       /// <param name="path">URL请求路径</param>
    //       public GetSvgZipAttribute(string path)
    //       { }
    //   }
}
