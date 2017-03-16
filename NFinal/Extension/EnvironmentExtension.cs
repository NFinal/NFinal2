using System;
using System.Collections.Generic;
using System.IO;
using NFinal.Owin;
using NFinal.Http;

namespace NFinal
{
    public static class EnvironmentExtension
    {
        public static string GetSubDomain(this IDictionary<string, object> environment)
        {
            string host = ((IDictionary<string, string[]>)(environment[NFinal.Owin.OwinKeys.RequestHeaders]))[NFinal.Constant.HeaderHost][0];
            int dotQty = 0;
            int firstDotIndex = 0;
            for (int i = 0; i < host.Length; i++)
            {
                if (host[i] == '.')
                {
                    if (firstDotIndex == 0)
                    {
                        firstDotIndex = i;
                    }
                    dotQty++;
                }
            }
            if (dotQty == 3)
            {
                return host.Substring(0, firstDotIndex);
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// <!--An IDictionary<string, string[]> of request headers.-->
        /// </summary>
        public static IDictionary<string, string[]> GetRequestHeaders(this IDictionary<string, object> environment)
        {
            return (IDictionary<string, string[]>)environment[OwinKeys.RequestHeaders];
        }
        /// <summary>
        /// A string containing the HTTP request method of the request (e.g., "GET", "POST").
        /// </summary>
        public static string GetRequestMethod(this IDictionary<string, object> environment)
        {
            return (string)environment[OwinKeys.RequestMethod];
        }
        /// <summary>
        /// A string containing the request path. The path MUST be relative to the "root" of the application delegate.
        /// </summary>
        public static string GetRequestPath(this IDictionary<string, object> environment)
        {

           return (string)environment[OwinKeys.RequestPath];
        }
        /// <summary>
        /// A string containing the portion of the request path corresponding to the "root" of the application delegate;
        /// </summary>
        public static string GetRequestPathBase(this IDictionary<string, object> environment)
        {
            return (string)environment[OwinKeys.RequestPathBase];
        }
        /// <summary>
        /// A string containing the protocol name and version (e.g. "HTTP/1.0" or "HTTP/1.1").
        /// </summary>
        public static string GetRequestProtocol(this IDictionary<string, object> environment)
        {
            return (string)environment[OwinKeys.RequestProtocol];
        }
        /// <summary>
        /// A string containing the query string component of the HTTP request URI, without the leading "?" (e.g., "foo=bar&amp;baz=quux"). The value may be an empty string.
        /// </summary>
        public static string GetRequestQueryString(this IDictionary<string, object> environment)
        {
                return (string)environment[OwinKeys.RequestQueryString];
        }
        /// <summary>
        /// A string containing the URI scheme used for the request (e.g., "http", "https"); 
        /// </summary>
        public static string GetRequestScheme(this IDictionary<string, object> environment)
        {
                return (string)environment[OwinKeys.RequestScheme];
        }
        /// <summary>
        /// 获取Cookie
        /// </summary>
        /// <param name="headers"></param>
        /// <returns></returns>
        public static IDictionary<string, string> GetCookies(this IDictionary<string,object> environment, IDictionary<string, string[]> headers)
        {
            IDictionary<string, string> cookie = null;
            if (headers.ContainsKey(NFinal.Constant.HeaderCookie))
            {
                cookie = new Dictionary<string, string>(StringComparer.Ordinal);
                string[] tempArray = headers[NFinal.Constant.HeaderCookie][0].Split(NFinal.Constant.CharAnd, NFinal.Constant.CharEqual);
                if ((tempArray.Length & 1) == 0)
                {
                    int len = tempArray.Length >> 1;
                    int i = 0;

                    while (i < len)
                    {
                        cookie.Add(tempArray[i << 1], Uri.UnescapeDataString(tempArray[(i << 1) + 1]));
                        i++;
                    }
                }
            }
            return cookie;
        }
        /// <summary>
        /// 获取Cookie
        /// </summary>
        /// <param name="headers"></param>
        /// <returns></returns>
        public static IDictionary<string,string> GetCookies(this IDictionary<string, object> environment)
        {
            IDictionary<string, string[]> headers = environment.GetRequestHeaders();
            return environment.GetCookies(headers);
        }
        /// <summary>
        /// A Stream with the request body, if any. Stream.Null MAY be used as a placeholder if there is no request body.
        /// </summary>
        public static Stream GetRequestBody(this IDictionary<string, object> environment)
        {
                return (Stream)environment[OwinKeys.RequestBody];
        }
        public static Stream GetResponseBody(this IDictionary<string, object> environment)
        {
            return (Stream)environment[OwinKeys.ResponseBody];
        }
        /// <summary>
        /// <!--An IDictionary<string, string[]> of response headers.-->
        /// </summary>
        public static IDictionary<string,string[]> GetResponseHeaders(this IDictionary<string, object> environment)
        {
            return (IDictionary<string, string[]>)environment[OwinKeys.ResponseHeaders];
        }
        /// <summary>
        /// <!--An IDictionary<string, string[]> of response headers.-->
        /// </summary>
        public static void SetResponseHeaders(this IDictionary<string, object> environment,IDictionary<string, string[]> headers)
        {
            environment[OwinKeys.ResponseHeaders] = headers;
        }
        /// <summary>
        /// An optional int containing the HTTP response status code as defined in RFC 2616 section 6.1.1. The default is 200.
        /// </summary>
        public static void SetResponseStatusCode(this IDictionary<string, object> environment,int statusCode)
        {
                environment[OwinKeys.ResponseStatusCode] = statusCode;
        }
        /// <summary>
        /// An optional string containing the reason phrase associated the given status code. If none is provided then the server SHOULD provide a default as described in RFC 2616 section 6.1.1
        /// </summary>
        public static void SetResponseReasonPhrase(this IDictionary<string, object> environment, string reasonPhrase)
        {
            environment[OwinKeys.ResponseReasonPhrase] = reasonPhrase;
        }
        /// <summary>
        /// An optional string containing the protocol name and version (e.g. "HTTP/1.0" or "HTTP/1.1"). If none is provided then the "owin.RequestProtocol" key's value is the default.
        /// </summary>
        public static void SetResponseProtocol(this IDictionary<string, object> environment, string protocol)
        {
                environment[OwinKeys.ResponseProtocol] = protocol;
        }
        /// <summary>
        /// A CancellationToken indicating if the request has been canceled/aborted. See [Request Lifetime][sec-req-lifetime].
        /// </summary>
        /// <param name="environment"></param>
        /// <returns></returns>
        public static System.Threading.CancellationToken GetCallCancelled(this IDictionary<string, object> environment)
        {
                return (System.Threading.CancellationToken)environment[OwinKeys.CallCancelled];
        }
        /// <summary>
        /// A string indicating the OWIN version. See Versioning.
        /// </summary>
        /// <param name="environment"></param>
        /// <returns></returns>
        public static string GetVersion(this IDictionary<string, object> environment)
        {
                return (string)environment[OwinKeys.Version];
        }
        /// <summary>
        /// The IP Address of the remote client. E.g. 192.168.1.1 or ::1
        /// </summary>
        /// <param name="environment"></param>
        /// <returns></returns>
        public static string GetRemoteIpAddress(this IDictionary<string, object> environment)
        {
            return (string)environment[OwinKeys.RemoteIpAddress];
        }
        /// <summary>
        /// The port of the remote client. E.g. 1234
        /// </summary>
        /// <param name="environment"></param>
        /// <returns></returns>
        public static string GetRemotePort(this IDictionary<string, object> environment)
        {
            return (string)environment[OwinKeys.RemotePort];
        }
        /// <summary>
        /// The local IP Address the request was received on. E.g. 127.0.0.1 or ::1
        /// </summary>
        /// <param name="environment"></param>
        /// <returns></returns>
        public static string GetLocalIpAddress(this IDictionary<string, object> environment)
        {
            return (string)environment[OwinKeys.LocalIpAddress];
        }
        /// <summary>
        /// The port the request was received on. E.g. 80
        /// </summary>
        /// <param name="environment"></param>
        /// <returns></returns>
        public static string GetLocalPort(this IDictionary<string, object> environment)
        {
            return (string)environment[OwinKeys.LocalPort];
        }
        /// <summary>
        /// Was the request sent from the same machine? E.g. true or false.
        /// </summary>
        /// <param name="environment"></param>
        /// <returns></returns>
        public static bool GetIsLocal(this IDictionary<string, object> environment)
        {
            return (bool)environment[OwinKeys.IsLocal];
        }
        public static string GetRequestRoot(this IDictionary<string, object> environment,IDictionary<string,string[]> headers)
        {
            string host;

            if (headers.ContainsKey(NFinal.Constant.HeaderHost))
            {
                host = headers[NFinal.Constant.HeaderHost][0];
            }
            else
            {
                host = environment[OwinKeys.LocalIpAddress] == null ? NFinal.Constant.Localhost : (string)environment[OwinKeys.LocalIpAddress];
                host += (string)environment[OwinKeys.LocalPort];
            }
            //return new Uri(environment["owin.RequestScheme"] +"//:" + host+":"+ environment["owin.RequestPathBase"] + environment["owin.RequestPath"] + environment["owin.RequestQueryString"]);
            return environment[OwinKeys.RequestScheme] + NFinal.Constant.SchemeDelimiter + host;
        }
        public static string GetRequestRoot(this IDictionary<string, object> environment)
        {
            return environment.GetRequestRoot((IDictionary<string,string[]>)environment[OwinKeys.RequestHeaders]);
        }
        public static MethodType GetMethodType(this IDictionary<string, object> environment)
        {
            string methodTypeTemp = (string)environment[OwinKeys.RequestMethod];
            MethodType methodType;
            switch (methodTypeTemp)
            {
                case NFinal.Constant.MethodTypePOST: methodType = MethodType.POST; break;
                case NFinal.Constant.MethodTypeGET: methodType = MethodType.GET; break;
                case NFinal.Constant.MethodTypeDELETE: methodType = MethodType.DELETE; break;
                case NFinal.Constant.MethodTypePUT: methodType = MethodType.PUT; break;
                case NFinal.Constant.MethodTypeAJAX: methodType = MethodType.AJAX; break;
                default: methodType = MethodType.NONE; break;
            }
            return methodType;
        }
        /// <summary>
        /// 从Owin中提取所需的请求信息
        /// </summary>
        /// <param name="environment">Owin参数</param>
        public static NFinal.Owin.Request GetRequest(this IDictionary<string, object> environment)
        {
            NFinal.Owin.Request request = new Request();
            request.environment = environment;
            request.headers = (IDictionary<string,string[]>)environment[OwinKeys.RequestHeaders];
            request.cookies = environment.GetCookies();
            request.stream = (Stream)environment[OwinKeys.RequestBody];
            //请求参数
            request.requestPath = (string)environment[OwinKeys.RequestPath];
            request.parameters = new NFinal.NameValueCollection();
            request.files = null;
            //获取POST方法
            string methodTypeTemp = (string)environment[OwinKeys.RequestMethod];
            switch (methodTypeTemp)
            {
                case NFinal.Constant.MethodTypePOST: request.methodType = MethodType.POST; break;
                case NFinal.Constant.MethodTypeGET: request.methodType = MethodType.GET; break;
                case NFinal.Constant.MethodTypeDELETE: request.methodType = MethodType.DELETE; break;
                case NFinal.Constant.MethodTypePUT: request.methodType = MethodType.PUT; break;
                case NFinal.Constant.MethodTypeAJAX: request.methodType = MethodType.AJAX; break;
                default: request.methodType = MethodType.NONE; break;
            }
            //提取内容类型
            if (request.methodType == MethodType.POST)
            {
                ContentType contentType = ContentType.NONE;
                string contentTypeString = null;
                if (request.headers.ContainsKey(NFinal.Constant.HeaderContentType))
                {
                    contentTypeString = request.headers[NFinal.Constant.HeaderContentType][0];
                    switch (contentTypeString.Split(NFinal.Constant.CharSemicolon)[0])
                    {
                        case NFinal.Constant.ContentType_Multipart_form_data: contentType = ContentType.Multipart_form_data; break;
                        case NFinal.Constant.ContentType_Text_json:
                        case NFinal.Constant.ContentType_Application_json: contentType = ContentType.Application_json; break;
                        case NFinal.Constant.ContentType_Application_x_www_form_urlencoded: contentType = ContentType.Application_x_www_form_urlencoded; break;
                        case NFinal.Constant.ContentType_Application_xml:
                        case NFinal.Constant.ContentType_Text_xml: contentType = ContentType.Text_xml; break;
                        default: contentType = ContentType.NONE; break;
                    }
                }
                //提取form参数
                if (request.stream != System.IO.Stream.Null)
                {
                    if (contentType == ContentType.Application_x_www_form_urlencoded)
                    {
                        System.IO.StreamReader sr = new System.IO.StreamReader(request.stream);
                        string body = sr.ReadToEnd();
                        sr.Dispose();
                        if (!string.IsNullOrEmpty(body))
                        {
                            string[] formArray = body.Split(NFinal.Constant.CharAnd, NFinal.Constant.CharEqual);
                            if (formArray.Length > 1 && (formArray.Length & 1) == 0)
                            {
                                for (int i = 0; i < formArray.Length; i += 2)
                                {
                                    request.parameters.Add(formArray[i], NFinal.Utility.UrlDecode(formArray[i + 1]));
                                }
                            }
                        }
                    }
                    //else if (contentType == NFinal.ContentType.Text_xml)
                    //{
                    //    System.IO.StreamReader sr = new System.IO.StreamReader(stream);
                    //    string body = sr.ReadToEnd();
                    //    sr.Dispose();
                    //    if (!string.IsNullOrEmpty(body))
                    //    {
                    //        System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
                    //        doc.LoadXml(body);
                    //        if (doc.DocumentElement != null)
                    //        {
                    //            foreach (System.Xml.XmlElement xmlNode in doc.DocumentElement.ChildNodes)
                    //            {
                    //                get.Add(xmlNode.Name, xmlNode.Value);
                    //            }
                    //        }
                    //    }
                    //}
                    else if (contentType == ContentType.Application_json)
                    {
                        System.IO.StreamReader sr = new System.IO.StreamReader(request.stream);
                        string body = sr.ReadToEnd();
                        sr.Dispose();
                        NFinal.Json.IJsonSerialize serializable = new NFinal.Json.NewtonsoftJsonSerialize();
                        IDictionary<string, object> data = serializable.DeserializeObject<IDictionary<string, object>>(body);
                        foreach (var ele in data)
                        {
                            request.parameters.Add(ele.Key, ele.Value.ToString());
                        }
                    }
                    else if (contentType == ContentType.Multipart_form_data)
                    {
                        //multipart/form-data
                        string boundary = NFinal.Http.HttpMultipart.HttpMultipart.boundaryReg.Match(contentTypeString).Value;
                        var multipart = new NFinal.Http.HttpMultipart.HttpMultipart(request.stream, boundary);
                        request.files = new Dictionary<string, NFinal.Http.HttpMultipart.HttpFile>(StringComparer.Ordinal);
                        foreach (var httpMultipartBoundary in multipart.GetBoundaries())
                        {
                            if (string.IsNullOrEmpty(httpMultipartBoundary.Filename))
                            {
                                string name = httpMultipartBoundary.Name;
                                if (!string.IsNullOrEmpty(name))
                                {
                                    string value = new System.IO.StreamReader(httpMultipartBoundary.Value).ReadToEnd();
                                    request.parameters.Add(name, value);
                                }
                            }
                            else
                            {
                                request.files.Add(httpMultipartBoundary.Name, new NFinal.Http.HttpMultipart.HttpFile(httpMultipartBoundary));
                            }
                        }
                    }
                }
            }
            request.queryString = (string)environment[OwinKeys.RequestQueryString];
            //提取URL?后的参数
            if (request.queryString.Length > 0)
            {
                string[] queryArray = request.queryString.Split(NFinal.Constant.CharAnd,NFinal.Constant.CharEqual);
                if (queryArray.Length > 1 && (queryArray.Length & 1) == 0)
                {
                    for (int i = 0; i < queryArray.Length; i += 2)
                    {
                        request.parameters.Add(queryArray[i], NFinal.Utility.UrlDecode(queryArray[i + 1]));
                    }
                }
            }
            return request;
        }
        public static NFinal.Owin.HtmlWriter GetHtmlWriter(this IDictionary<string, object> environment)
        {
            NFinal.Owin.HtmlWriter htmlWriter = new NFinal.Owin.HtmlWriter(environment.GetResponseBody(),CompressMode.GZip);
            return htmlWriter;
        }

        public static void WriteHtml(this IDictionary<string, object> environment,string html)
        {
            Stream stream= environment.GetResponseBody();
            byte[] buffer = NFinal.Constant.encoding.GetBytes(html);
            stream.Write(buffer, 0, buffer.Length);

            IDictionary<string,string[]> headers= environment.GetResponseHeaders();
            if (headers.ContainsKey(NFinal.Constant.HeaderContentType))
            {
                headers[NFinal.Constant.HeaderContentType] = new string[] { NFinal.Constant.ResponseContentType_Text_html };
            }
            else
            {
                headers.Add(NFinal.Constant.HeaderContentType, new string[] { NFinal.Constant.ResponseContentType_Text_html });
            }
        }
        public static void WriteJson(this IDictionary<string, object> environment,string json)
        {
            Stream stream = environment.GetResponseBody();
            byte[] buffer = NFinal.Constant.encoding.GetBytes(json);
            stream.Write(buffer, 0, buffer.Length);
            
            IDictionary<string, string[]> headers = environment.GetResponseHeaders();
            if (headers.ContainsKey(NFinal.Constant.HeaderContentType))
            {
                headers[NFinal.Constant.HeaderContentType] = new string[] { NFinal.Constant.ResponseContentType_Application_json };
            }
            else
            {
                headers.Add(NFinal.Constant.HeaderContentType, new string[] { NFinal.Constant.ResponseContentType_Application_json });
            }
        }
        public static void Forbiden(this IDictionary<string, object> environment, string url)
        {
            environment.SetResponseStatusCode(403);
        }
        public static void Redirect(this IDictionary<string, object> environment,string url)
        {
            environment.SetResponseStatusCode(302);
            IDictionary<string, string[]> headers = environment.GetResponseHeaders();
            if (headers.ContainsKey(NFinal.Constant.HeaderContentType))
            {
                headers[NFinal.Constant.HeaderContentType] = new string[] { NFinal.Constant.ResponseContentType_Text_html };
            }
            else
            {
                headers.Add(NFinal.Constant.HeaderContentType, new string[] { NFinal.Constant.ResponseContentType_Text_html });
            }
            if (headers.ContainsKey(NFinal.Constant.HeaderLocation))
            {
                headers[NFinal.Constant.HeaderLocation] = new string[] { url };
            }
            else
            {
                headers.Add(NFinal.Constant.HeaderLocation, new string[] { url });
            }
        }
        /// <summary>
        /// 把Response输出到前端
        /// </summary>
        /// <param name="environment"></param>
        /// <param name="response"></param>
        public static void WriteResponse(this IDictionary<string, object> environment, NFinal.Owin.Response response)
        {
            environment.SetResponseStatusCode(response.statusCode);
            IDictionary<string, string[]> headers = (IDictionary<string,string[]>)environment[OwinKeys.ResponseHeaders];
            if (response.headers.Count > 0)
            {
                foreach (var header in response.headers)
                {
                    headers.Add(header.Key, header.Value);
                }
            }
            response.stream.CopyTo((Stream)environment[OwinKeys.ResponseBody]);
            response.stream.Flush();
            response.stream.Dispose();
        }
    }
}
