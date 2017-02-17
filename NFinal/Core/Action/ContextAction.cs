using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Owin;
using NFinal.Owin;

namespace NFinal
{
    public class ContextAction<TViewBag,TUser>:IAction<IOwinContext,IOwinRequest>
    {
        public TViewBag viewBat;
        public TUser user;

        public IOwinContext context
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public Request request
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public Response response
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        IOwinRequest IAction<IOwinContext, IOwinRequest>.request
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public Stream outputStream
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public CompressMode compressMode
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public void Execute()
        {
            throw new NotImplementedException();
        }

        public string GetRemoteIpAddress()
        {
            throw new NotImplementedException();
        }

        public string GetRequestHeader(string key)
        {
            throw new NotImplementedException();
        }

        public void SetResponseHeader(string key, string value)
        {
            throw new NotImplementedException();
        }

        public void SetResponseHeader(string key, string[] value)
        {
            throw new NotImplementedException();
        }

        public void SetResponseStatusCode(int statusCode)
        {
            throw new NotImplementedException();
        }

        public bool Before()
        {
            throw new NotImplementedException();
        }

        public void After()
        {
            throw new NotImplementedException();
        }

        public void Close()
        {
            throw new NotImplementedException();
        }

        public void Write(byte[] buffer, int offset, int count)
        {
            throw new NotImplementedException();
        }

        public void Write(string value)
        {
            throw new NotImplementedException();
        }

        public void BaseInitialization(IOwinContext context)
        {
            throw new NotImplementedException();
        }

        public void Initialization(IOwinContext context, Stream outputStream, Request request, CompressMode compressMode)
        {
            throw new NotImplementedException();
        }

        public string GetRequestPath()
        {
            throw new NotImplementedException();
        }

        string IAction<IOwinContext,IOwinRequest>.GetRequestPath()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void Initialization(IOwinContext context, Stream outputStream, IOwinRequest request, CompressMode compressMode)
        {
            throw new NotImplementedException();
        }

        public void Initialization(IAction<IOwinContext, IOwinRequest> action, IOwinContext context, Stream outputStream, IOwinRequest request, CompressMode compressMode)
        {
            throw new NotImplementedException();
        }

        public void Redirect(string url)
        {
            throw new NotImplementedException();
        }

        public string GetSubDomain(IOwinContext context)
        {
            throw new NotImplementedException();
        }
    }
}
