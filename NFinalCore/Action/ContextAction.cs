﻿#if (NET40 || NET451 || NET461)
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Owin;
using NFinal.Owin;

namespace NFinal
{
    public class ContextAction<TMasterPage, TUser> : AbstractAction<IOwinContext, IOwinRequest, TUser, TMasterPage> where TMasterPage : NFinal.MasterPageModel
    {
        public override void After()
        {
            throw new NotImplementedException();
        }

        public override void BaseInitialization(IOwinContext context,string methodName)
        {
            throw new NotImplementedException();
        }

        public override bool Before()
        {
            throw new NotImplementedException();
        }

        public override void Close()
        {
            throw new NotImplementedException();
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

        public override void Initialization(IOwinContext context,string methodName, Stream outputStream, IOwinRequest request, CompressMode compressMode)
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