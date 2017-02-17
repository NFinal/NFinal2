using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace NFinal.Owin
{
    public class OwinContext
    {
        public IDictionary<string, object> env;
        //Request Data
        public Stream RequestBody;
        public IDictionary<string, string[]> RequestHeaders;
        public string RequestMethod;
        public string RequestPath;
        public string RequestPathBase;
        public string RequestProtocol;
        public string RequestQueryString;
        public string RequestScheme;

        //Response Data
        public Stream ResponseBody;
        public IDictionary<string, string[]> ResponseHeaders;
        public int ResponseStatusCode;
        public string ResponseReasonPhrase;
        public string ResponseProtocol;

        //Other Data
        public CancellationToken CallCancelled;
        public string Version;

        public OwinContext(IDictionary<string,object> env)
        {
            this.env = env;
            this.RequestBody = env.GetRequestBody();
            this.RequestHeaders = env.GetRequestHeaders();
            this.RequestMethod = env.GetRequestMethod();
            this.RequestPath = env.GetRequestPath();
            this.RequestPathBase = env.GetRequestPathBase();
            this.RequestProtocol = env.GetRequestProtocol();
            this.RequestQueryString = env.GetRequestQueryString();
            this.RequestScheme = env.GetRequestScheme();

            this.ResponseBody = env.GetResponseBody();
            this.ResponseHeaders = env.GetResponseHeaders();
        }
        public void Write(Response response)
        {
            response.stream.Seek(0, SeekOrigin.Begin);
            response.stream.CopyTo(env.GetResponseBody());
            env.SetResponseHeaders(response.headers);
            env.SetResponseStatusCode(response.statusCode);
        }
    }
}
