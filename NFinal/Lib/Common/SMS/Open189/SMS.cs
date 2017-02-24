//======================================================================
//
//        Copyright : Zhengzhou Strawberry Computer Technology Co.,LTD.
//        All rights reserved
//
//        Application:NFinal MVC framework
//        Filename :SMS.cs
//        Description :短信接口类
//
//        created by Lucas at  2015-6-30`
//     
//        WebSite:http://www.nfinal.com
//
//======================================================================
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.IO;
using System.Text;
using System.Web;

namespace NFinal.Common.SMS.Open189
{
    public class SMS
    {
        private System.Text.Encoding encoding = System.Text.Encoding.UTF8;
        //获取时间
        public long GetTimeStamp()
        {
            return DateTime.Now.Ticks;
        }
        //获取随机码
        public string GetRandomCode(int len)
        {
            return Guid.NewGuid().ToString("N").Substring(0, len);
        }
        public class CCTokenResult
        { 
            public string access_token;
            public int expires_in;
            public string res_code;
            public string res_message;
        }
        public static bool ValidateServerCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }
        public string PostData(string url, string data)
        {
            HttpWebRequest request = null;
            if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(ValidateServerCertificate);
                request = (HttpWebRequest)WebRequest.Create(url);
                request.ProtocolVersion = HttpVersion.Version10;
            }
            else
            {
                request = (HttpWebRequest)WebRequest.Create(url);
            }
            request.KeepAlive = true;
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.Proxy = null;
            byte[] buffer = encoding.GetBytes(data);
            request.ContentLength = buffer.Length;
            Stream stream = request.GetRequestStream();
            stream.Write(buffer, 0, buffer.Length);
            stream.Close();
            HttpWebResponse response = null;
            StreamReader sr = null;
            string tokenJson = null;
            try
            {
                response = (HttpWebResponse)request.GetResponse();
                sr = new StreamReader(response.GetResponseStream(), encoding);
                tokenJson = sr.ReadToEnd();
                sr.Close();
                response.Close();
            }
            catch(System.Net.WebException)
            {
            }
            return tokenJson;
        }

        public CCTokenResult GetCCToken(string appid, string appScrect)
        {
            string data = string.Format("grant_type=client_credentials&app_id={0}&app_secret={1}",appid,appScrect);
            string tokenJson = PostData("https://oauth.api.189.cn/emp/oauth2/v3/access_token", data);
            CCTokenResult result = null;
            if (tokenJson != null)
            {
                LitJson.JsonData json = LitJson.JsonMapper.ToObject(tokenJson);
                result = new CCTokenResult();
                result.access_token = (string)json["access_token"];
                result.expires_in = (int)json["expires_in"];
                result.res_code = (string)json["res_code"];
                result.res_message = (string)json["res_message"];
            }
            return result;
        }
        public string BuildParameter(NameValueCollection nvc)
        {
            StringBuilder sb=new StringBuilder();
            sb.Append("{");
            for (int i = 0; i < nvc.Count; i++)
            {
                if (i != 0)
                {
                    sb.Append(",");
                }
                sb.Append("\"");
                sb.Append(nvc.Keys[i]);
                sb.Append("\":");
                sb.Append("\"");
                sb.Append(nvc[i]);
                sb.Append("\"");
            }
            sb.Append("}");
            return sb.ToString();
        }
    }
}