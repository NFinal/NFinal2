//======================================================================
//
//        Copyright : Zhengzhou Strawberry Computer Technology Co.,LTD.
//        All rights reserved
//        
//        Application:NFinal MVC framework
//        Filename :MagicViewBag.cs
//        Description :控制器父类
//
//        created by Lucas at  2015-6-30`
//     
//        WebSite:http://www.nfinal.com
//
//======================================================================
using System;
using System.Collections.Generic;
using System.IO;

namespace NFinal
{
    /// <summary>
    /// NFinal的控制器基类，核心类，类似于asp.net中的page类
    /// </summary>
    public class NFinalOwinBaseAction : Action,IDisposable
    {
        #region 初始化函数
        /// <summary>
        /// http请求
        /// </summary>
        //public NFinal.Owin.Request request;
        /// <summary>
        /// Response
        /// </summary>
        //public NFinal.Owin.Response response;
        //public Stream contentStream;
        //public CompressMode compressMode;

        public IDictionary<string, object> enviroment;
        //public IDictionary<string, string> cookies;
        public ICookie Cookie;
        public ISession Session;

        private bool beforeWrite = true;
        public NFinalOwinBaseAction()
        { }
        /// <summary>
        /// 页面初始化
        /// </summary>
        /// <param name="action"></param>
        public NFinalOwinBaseAction(NFinalOwinBaseAction action) : base(action)
        {
            this.enviroment = action.enviroment;
            //this.contentStream = action.contentStream;
        }
        /// <summary>
        /// Http输出初始化
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="request"></param>
        public NFinalOwinBaseAction(IDictionary<string, object> enviroment, Owin.Request request)
        {
            this.enviroment = enviroment;
            this.request.cookies=new Dictionary<string,string>();
            this.request = request;
            this.response.stream = (System.IO.Stream)enviroment["owin.ResponseBody"];
            this.response.headers = new Dictionary<string,string[]>();
            this.response.statusCode = 200;
            this._serverType = ServerType.NFinalOwin;
            //this.compressMode = compressMode;
            //if (compressMode == CompressMode.None)
            //{
            //    this.contentStream = this.response.stream;
            //}
            //else
            //{
            //    if (compressMode == CompressMode.GZip)
            //    {
            //        this.contentStream = new System.IO.Compression.GZipStream(this.response.stream, System.IO.Compression.CompressionMode.Compress,false);
            //        this.response.headers.Add("Content-Encoding",new string[]{ "gzip"});
            //    }
            //    else if (compressMode == CompressMode.Deflate)
            //    {
            //        this.contentStream = new System.IO.Compression.DeflateStream(this.response.stream, System.IO.Compression.CompressionMode.Compress, true);
            //        this.response.headers.Add("Content-Encoding", new string[] { "deflate" });
            //    }
            //}
        }
        public void SetCompressMode()
        {
            this.compressMode = compressMode;
            if (compressMode == CompressMode.None)
            {
                this.contentStream = this.response.stream;
            }
            else
            {
                if (compressMode == CompressMode.GZip)
                {
                    this.contentStream = new System.IO.Compression.GZipStream(this.response.stream, System.IO.Compression.CompressionMode.Compress, true);
                    this.response.headers.Add("Content-Encoding", new string[] { "gzip" });
                }
                else if (compressMode == CompressMode.Deflate)
                {
                    this.contentStream = new System.IO.Compression.DeflateStream(this.response.stream, System.IO.Compression.CompressionMode.Compress, true);
                    this.response.headers.Add("Content-Encoding", new string[] { "deflate" });
                }
            }
        }
        /// <summary>
        /// 流输出初始化函数
        /// </summary>
        /// <param name="request"></param>
        /// <param name="compressMode"></param>
        public NFinalOwinBaseAction(string fileName,Owin.Request request)
        {
            this.request = request;
            this.response.stream = new FileStream(fileName,FileMode.Create,FileAccess.ReadWrite);
            this.response.headers = new Dictionary<string,string[]>();
            this.response.statusCode = 200;
            this._serverType = ServerType.IsStatic;
            //this.compressMode = compressMode;
            //if (compressMode == CompressMode.None)
            //{
            //    this.contentStream = this.response.stream;
            //}
            //else
            //{
            //    if (compressMode == CompressMode.GZip)
            //    {
            //        this.contentStream = new System.IO.Compression.GZipStream(this.response.stream, System.IO.Compression.CompressionMode.Compress, true);
            //        this.response.headers.Add("Content-Encoding", new string[] { "gzip" });
            //    }
            //    else if (compressMode == CompressMode.Deflate)
            //    {
            //        this.contentStream = new System.IO.Compression.DeflateStream(this.response.stream, System.IO.Compression.CompressionMode.Compress, true);
            //        this.response.headers.Add("Content-Encoding", new string[] { "deflate" });
            //    }
            //}
        }
        /// <summary>
        /// 流输出初始化函数
        /// </summary>
        /// <param name="request"></param>
        /// <param name="compressMode"></param>
        public NFinalOwinBaseAction(System.IO.Stream stream,Owin.Request request)
        {
            this.request = request;
            this.response.stream = stream;
            this.response.headers = new Dictionary<string, string[]>();
            this.response.statusCode = 200;
            this._serverType = ServerType.IsStatic;
            //this.compressMode = compressMode;
            //if (compressMode == CompressMode.None)
            //{
            //    this.contentStream = this.response.stream;
            //}
            //else
            //{
            //    if (compressMode == CompressMode.GZip)
            //    {
            //        this.contentStream = new System.IO.Compression.GZipStream(this.response.stream, System.IO.Compression.CompressionMode.Compress, true);
            //        this.response.headers.Add("Content-Encoding", new string[] { "gzip" });
            //    }
            //    else if (compressMode == CompressMode.Deflate)
            //    {
            //        this.contentStream = new System.IO.Compression.DeflateStream(this.response.stream, System.IO.Compression.CompressionMode.Compress, true);
            //        this.response.headers.Add("Content-Encoding", new string[] { "deflate" });
            //    }
            //}
        }
        #endregion
        #region 输出函数
        ///// <summary>
        ///// 请求的根路径，例：http://www.x.com
        ///// </summary>
        //public override string RequestRoot
        //{
        //    get
        //    {
        //        return request.requestRoot;
        //    }
        //}
        /// <summary>
        /// 获取请求头
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public override string GetRequestHeader(string key)
        {
            if (request.headers.ContainsKey(key))
            {
                return request.headers[key][0];
            }
            return null;
        }
        /// <summary>
        /// 请求时的流
        /// </summary>
        //public override System.IO.Stream InputStream
        //{
        //    get
        //    {
        //        return this.request.stream;
        //    }
        //}
        /// <summary>
        /// 设置输出头
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public override void SetResponseHeader(string key, string value)
        {
            this.response.headers.AddValue(key, new string[] { value });
        }
        public override void SetResponseHeader(string key, string[] value)
        {
            this.response.headers.AddValue(key, value);
        }
        /// <summary>
        /// 设置状态码
        /// </summary>
        /// <param name="statusCode"></param>
        public override void SetResponseStatusCode(int statusCode)
        {
            this.response.statusCode = statusCode;
            if (this._serverType != ServerType.IsStatic)
            {
                this.enviroment["owin.ResponseStatusCode"] = this.response.statusCode;
            }
        }
        /// <summary>
        /// 模板渲染前函数，用于子类重写
        /// </summary>
        public override void Before()
        {

        }
        /// <summary>
        /// 输出字节流，用于输出二进制流，如图象，文件等。
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="offset"></param>
        /// <param name="count"></param>
        public override void Write(byte[] buffer,int offset,int count)
        {
            if (beforeWrite)
            {
                beforeWrite = false;
                if (_serverType != ServerType.IsStatic)
                {
                    IDictionary<string, string[]> headers = (IDictionary<string, string[]>)enviroment["owin.ResponseHeaders"];
                    foreach (var header in this.response.headers)
                    {
                        headers.AddValue(header.Key, header.Value);
                    }
                    //if (cookies.Count > 0)
                    //{
                    //    string[] value = new string[cookies.Count];
                    //    cookies.Values.CopyTo(value, 0);
                    //    headers.AddValue(Constant.HeaderSetCookie, value);
                    //}
                }
            }
            contentStream.Write(buffer, 0, count);
        }
        /// <summary>
        /// 模板渲染后函数，用于子类重写
        /// </summary>
        public override void After()
        {

        }
        /// <summary>
        /// 获取输出结果
        /// </summary>
        /// <returns></returns>
        public Owin.Response GetResponse()
        {
            if (compressMode == CompressMode.GZip || compressMode == CompressMode.Deflate)
            {
                this.contentStream.Dispose();
            }
            return response;
        }
        /// <summary>
        /// 把Response输出到前端
        /// </summary>
        /// <param name="enviroment"></param>
        /// <param name="response"></param>
        public static void WriteResponse(IDictionary<string,object> enviroment, Owin.Response response)
        {
            enviroment["owin.ResponseStatusCode"] = response.statusCode;
            IDictionary<string, string[]> headers = (IDictionary<string, string[]>)enviroment["owin.ResponseHeaders"];
            if (response.headers.Count > 0)
            {
                foreach (var header in response.headers)
                {
                    headers.Add(header.Key, header.Value);
                }
            }
            response.stream.CopyTo((Stream)enviroment["owin.ResponseBody"]);
            response.stream.Close();
        }
        /// <summary>
        /// 释放掉压缩流对象
        /// </summary>
        public override void Close()
        {
            if (compressMode == CompressMode.GZip || compressMode == CompressMode.Deflate)
            {
                this.contentStream.Dispose();
            }
            if (request.stream != null)
            {
                request.stream.Dispose();
            }
        }
        /// <summary>
        /// 释放掉压缩流对象
        /// </summary>
        public void Dispose()
        {
            if (compressMode == CompressMode.GZip || compressMode == CompressMode.Deflate)
            {
                ((IDisposable)this.contentStream).Dispose();
            }
            if (request.stream != null)
            {
                request.stream.Dispose();
            }
        }
        #endregion
        public void Execute(IDictionary<string, object> env)
        {

        }
    }
}