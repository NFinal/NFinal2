using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Web;

namespace NFinal.Common.SMS.Open189
{
    /// <summary>
    /// 公共基础类
    /// </summary>
    public abstract class API
    {
        public static readonly string DefaultUserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; SV1; .NET CLR 1.1.4322; .NET CLR 2.0.50727; .NET CLR 4.0.30319)";
        public static readonly string NotSupportMessage = "This request method is not supported by API!";
        protected IDictionary<string, string> _requestParams;
        protected string Access_Token;
        private RequestType method;
        private Stopwatch sw;
        private string app_id;
        private string app_secret;
        public string APP_ID
        {
            get { return app_id; }
            set { app_id = value; }
        }
        public string APP_SECRET
        {
            get { return app_secret; }
            set { app_secret = value; }
        }
        public abstract IDictionary<string, string> RequestParams { get; }
        public abstract string URL { get; }
        public RequestType Method
        {
            get { return method; }
            set { method = value; }
        }
         protected string HttpGetRequest(IDictionary<string, string> requestParams)
        {
            string uri = URL + "?" + generateParameterString(requestParams);
            Debug.WriteLine("send get request");
            Debug.WriteLine("uri:" + uri);
            WebClient client = new WebClient();
            client.Headers.Add("user-agent", DefaultUserAgent);
            sw = new Stopwatch();
            sw.Start();
            Stream data = client.OpenRead(uri);
            sw.Stop();
            StreamReader reader = new StreamReader(data);
            string s = reader.ReadToEnd();          
            Debug.Write("Time Eclpsed:" + sw.ElapsedMilliseconds);
            Console.WriteLine("Time Eclpsed:" + sw.ElapsedMilliseconds);
            return s;
        }

        protected string HttpGetRequest(IDictionary<string, string> bodyParams, IDictionary<string, string> headerParams)
        {
            string uri = URL + "?" + generateParameterString(bodyParams);
            Debug.WriteLine("send get request");
            Debug.WriteLine("uri:" + uri);
            WebClient client = new WebClient();
            client.Headers.Add("user-agent", DefaultUserAgent);
            if (headerParams != null)
            { 
                foreach(KeyValuePair<string,string> item in headerParams)
                {
                    client.Headers.Add(item.Key,item.Value);
                }
            }
            sw = new Stopwatch();
            sw.Start();
            Stream data = client.OpenRead(uri);
            sw.Stop();
            StreamReader reader = new StreamReader(data);
            string s = reader.ReadToEnd();

            Debug.WriteLine("Time Eclpsed:" + sw.ElapsedMilliseconds);
            Console.WriteLine("Time Eclpsed:" + sw.ElapsedMilliseconds);
            return s;
        }

        protected string HttpPostRequest(IDictionary<string, string> requestParams)
        {
            string postString = generateParameterString(requestParams);
            byte[] postData = Encoding.UTF8.GetBytes(postString);
            WebClient client = new WebClient();
            client.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
            byte[] responseData = client.UploadData(URL, RequestType.post.ToString(), postData);
            string srcString = Encoding.UTF8.GetString(responseData);
            return srcString;//获取最后的验证码
        }

        protected string HttpPostRequest(IDictionary<string, string> bodyParams, IDictionary<string, string> headerParams)
        {
            string postString = generateParameterString(bodyParams);
            byte[] postData = Encoding.UTF8.GetBytes(postString);
            WebClient client = new WebClient();
            client.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
            if (headerParams != null)
            {
                foreach (KeyValuePair<string, string> item in headerParams)
                {
                    client.Headers.Add(item.Key, item.Value);
                }
            }
            byte[] responseData = client.UploadData(URL, RequestType.post.ToString(), postData);
            string srcString = Encoding.UTF8.GetString(responseData);
            return srcString;
        }

        protected byte[] HttpPostRequest(IDictionary<string, string> headerParams, byte[] data)
        {
           // string postString = generateParameterString(headerParams);
            byte[] postData = data;
            WebClient client = new WebClient();
            client.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
            if (headerParams != null)
            {
                foreach (KeyValuePair<string, string> item in headerParams)
                {
                    client.Headers.Add(item.Key, item.Value);
                }
            }
            byte[] responseData = client.UploadData(URL, RequestType.post.ToString(), postData);
            
            string srcString = Encoding.UTF8.GetString(responseData);
            return responseData;
        }

        protected string HttpPutRequest(byte[] data, string url, IDictionary<string,string> headerParams)
        {
            using (WebClient client = new WebClient())
            {
                if (headerParams != null)
                {
                    foreach (KeyValuePair<string, string> pair in headerParams)
                    {
                        client.Headers.Add(pair.Key, pair.Value);
                    }
                }
                byte[] responseData = client.UploadData(url, "PUT", data);
                string srcString = Encoding.UTF8.GetString(responseData);
                return srcString;
            }
         
        }

        private string generateParameterString(IDictionary<string, string> requestParams)
        {
                string paramstring = "";
                if (requestParams != null)
                {
                    foreach (KeyValuePair<string, string> pair in requestParams)
                    {
                        paramstring += pair.Key + "=" + pair.Value + "&";
                    }
                   // paramstring.Remove(paramstring.LastIndexOf('&'));
                    paramstring = paramstring.Substring(0, paramstring.Length - 1);
                }
                return paramstring;
        }        
        #region SendHttpsRequest Functions

        protected HttpWebResponse CreateGetHttpResponse(string url, int? timeout, string userAgent, CookieCollection cookies)
        {
            if (string.IsNullOrEmpty(url))
            {
                throw new ArgumentNullException("url");
            }
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            request.Method = "GET";
            request.UserAgent = API.DefaultUserAgent;
            if (!string.IsNullOrEmpty(userAgent))
            {
                request.UserAgent = userAgent;
            }
            if (timeout.HasValue)
            {
                request.Timeout = timeout.Value;
            }
            if (cookies != null)
            {
                request.CookieContainer = new CookieContainer();
                request.CookieContainer.Add(cookies);
            }
            return request.GetResponse() as HttpWebResponse;
        }

        protected HttpWebResponse CreatePostHttpResponse(string url, IDictionary<string, string> parameters, int? timeout, string userAgent, Encoding requestEncoding, CookieCollection cookies)
        {
            if (string.IsNullOrEmpty(url))
            {
                throw new ArgumentNullException("url");
            }
            if (requestEncoding == null)
            {
                throw new ArgumentNullException("requestEncoding");
            }
            HttpWebRequest request = null;
            //如果是发送HTTPS请求
            if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
            {
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
                request = WebRequest.Create(url) as HttpWebRequest;
                request.ProtocolVersion = HttpVersion.Version10;
            }
            else
            {
                request = WebRequest.Create(url) as HttpWebRequest;
            }
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";

            if (!string.IsNullOrEmpty(userAgent))
            {
                request.UserAgent = userAgent;
            }
            else
            {
                request.UserAgent = DefaultUserAgent;
            }

            if (timeout.HasValue)
            {
                request.Timeout = timeout.Value;
            }
            if (cookies != null)
            {
                request.CookieContainer = new CookieContainer();
                request.CookieContainer.Add(cookies);
            }
            //如果需要POST数据
            if (!(parameters == null || parameters.Count == 0))
            {
                StringBuilder buffer = new StringBuilder();
                int i = 0;
                foreach (string key in parameters.Keys)
                {
                    if (i > 0)
                    {
                        buffer.AppendFormat("&{0}={1}", key, parameters[key]);
                    }
                    else
                    {
                        buffer.AppendFormat("{0}={1}", key, parameters[key]);
                    }
                    i++;
                }
                byte[] data = requestEncoding.GetBytes(buffer.ToString());
                using (Stream stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }
            }
            return request.GetResponse() as HttpWebResponse;
        }

        private static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            return true; //总是接受
        }

        #endregion


        public abstract string GetResult{get;}
        public abstract string PostResult{get;}
        public string Result
        {
            get
            {
                if (Method == RequestType.get)
                    return GetResult;
                else if (Method == RequestType.post)
                    return PostResult;
                else
                    return null;
            }
        }
        protected abstract void PrepareParam();       
    }
}