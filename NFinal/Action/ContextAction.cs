//======================================================================
//
//        Copyright : Zhengzhou Strawberry Computer Technology Co.,LTD.
//        All rights reserved
//        
//        Application:NFinal MVC framework
//        Filename :ContextAction.cs
//        Description :IOwinContext对应的控制器基类，未实现
//
//        created by Lucas at  2015-6-30
//     
//        WebSite:http://www.nfinal.com
//
//======================================================================
#if (NET40 || NET451 || NET461)
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Owin;
using NFinal.Owin;
using NFinal.Http;
using System.Data;

namespace NFinal
{
    /// <summary>
    /// IOwinContext
    /// </summary>
    public class ContextAction : NFinal.Action.AbstractAction<IOwinContext, IOwinRequest>
    {
        /// <summary>
        /// 控制器行为执行后函数
        /// </summary>
        public override void After()
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="context"></param>
        /// <param name="methodName"></param>
        /// <param name="plugConfig"></param>
        public override void BaseInitialization(IOwinContext context,string methodName, NFinal.Config.Plug.PlugConfig plugConfig)
        {
        }
        /// <summary>
        /// 控制器行为执行前函数
        /// </summary>
        /// <returns></returns>
        public override bool Before()
        {
            return true;
        }

        public override void Close()
        {
            
        }

        public override void Dispose()
        {
            throw new NotImplementedException();
        }

        public override string GetRemoteIpAddress()
        {
            throw new NotImplementedException();
        }

        public override Stream GetRequestBody()
        {
            throw new NotImplementedException();
        }

        public override string GetRequestHeader(string key)
        {
            throw new NotImplementedException();
        }

        public override string GetRequestPath()
        {
            throw new NotImplementedException();
        }

        public override string GetSubDomain(IOwinContext context)
        {
            throw new NotImplementedException();
        }

        public override void Initialization(IOwinContext context,string methodName, Stream outputStream, IOwinRequest request, CompressMode compressMode, NFinal.Config.Plug.PlugConfig plugConfig)
        {
            throw new NotImplementedException();
        }

        public override void SetResponseHeader(string key, string[] value)
        {
            throw new NotImplementedException();
        }

        public override void SetResponseHeader(string key, string value)
        {
            throw new NotImplementedException();
        }

        public override void SetResponseStatusCode(int statusCode)
        {
            throw new NotImplementedException();
        }

        public override void Write(string value)
        {
            throw new NotImplementedException();
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            throw new NotImplementedException();
        }
    }
}
#endif