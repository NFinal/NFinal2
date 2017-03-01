#if NETCORE
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace NFinal
{
    public class CoreAction<TMasterPage, TUser> : AbstractAction<HttpContext, HttpRequest, TUser, TMasterPage> where TMasterPage : MasterPageModel
    {
        public override void After()
        {
            
        }

        public override bool Before()
        {
            return true;
        }

        public override void Close()
        {
            if (_serverType != ServerType.IsStatic)
            {
                if (context.Response.Headers.ContainsKey(NFinal.Constant.HeaderContentType))
                {
                    context.Response.ContentType = contentType;
                }
                else
                {
                    context.Response.Headers.Add(NFinal.Constant.HeaderContentType, new string[] { this.contentType });
                }
                foreach (var header in this.response.headers)
                {
                    context.Response.Headers.AddValue(header.Key, header.Value);
                }
            }
            this.writeStream.Flush();
            this.writeStream.Dispose();
            this.response.stream.Seek(0,SeekOrigin.Begin);
            this.response.stream.CopyTo(this.outputStream);
            this.Dispose();
        }

        public override void Dispose()
        {
            this.writeStream?.Dispose();
            this.response.stream?.Dispose();
            if (this.request.Body != null)
            {
                this.request.Body.Dispose();
            }
        }

        public override string GetRemoteIpAddress()
        {
            return request.Host.Host;
        }

        public override string GetRequestHeader(string key)
        {
            return request.Headers[key];
        }

        public override string GetRequestPath()
        {
            return request.Path;
        }

        public override string GetSubDomain(HttpContext context)
        {
            return context.Request.Host.Host.Split('.')[0];
        }

        public override void SetResponseHeader(string key, string[] value)
        {
            response.headers[key] = value;
        }

        public override void SetResponseHeader(string key, string value)
        {
            response.headers[key] =new string[] { value };
        }

        public override void SetResponseStatusCode(int statusCode)
        {
            response.statusCode = statusCode;
        }
    }
}
#endif