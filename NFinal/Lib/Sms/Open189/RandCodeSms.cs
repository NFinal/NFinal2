//======================================================================
//
//        Copyright : Zhengzhou Strawberry Computer Technology Co.,LTD.
//        All rights reserved
//
//        Application:NFinal MVC framework
//        Filename :VerifyCodeTemplate.cs
//        Description :验证码模板类
//
//        created by Lucas at  2015-6-30`
//     
//        WebSite:http://www.nfinal.com
//
//======================================================================
using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace NFinal.Common.SMS.Open189
{
    /// <summary>
    /// 验证码短信
    /// </summary>
    public class RandCodeSms : BaseSms
    {
        /// <summary>
        /// 发送验证码时间间隔
        /// </summary>
        public int exp_time = 2;
        /// <summary>
        /// 发送验证码返回的JSON数据
        /// </summary>
        public class SendRandCodeResult
        {
            public int res_code;
            public string identifier;
            public string create_at;
        }
        public RandCodeSms()
        {

        }
        /// <summary>
        /// 获取信任码返回Json数据
        /// </summary>
        public class RandCodeToken
        {
            public int res_code;
            public string token;
        }
        /// <summary>
        /// MHAC_SHA1 加密
        /// </summary>
        /// <param name="plaintext">明文</param>
        /// <param name="key">加密key</param>
        /// <returns></returns>
        protected static byte[] HMAC_SHA1(string plaintext, string key)
        {
            UTF8Encoding utf8encoding = new UTF8Encoding();
            byte[] keybyte = utf8encoding.GetBytes(key);
            byte[] contentbyte = utf8encoding.GetBytes(plaintext);
            byte[] cipherbyte;
            using (System.Security.Cryptography.HMACSHA1 hmacsha1 = new System.Security.Cryptography.HMACSHA1(keybyte))
            {
                cipherbyte = hmacsha1.ComputeHash(contentbyte);
            }
            return cipherbyte;
        }
        /// <summary>
        /// 发送验证码，并把验证码提交到URL上
        /// </summary>
        /// <param name="tplId">模板id</param>
        /// <param name="phone">电话</param>
        /// <param name="url">提交地址</param>
        /// <returns></returns>
        public bool SendRandCode(string app_id, string phone, string url)
        {
            DataBase db = new DataBase();
            DataBase.SmsAppEntity appData = db.GetApp(app_id);
            string access_token = null;
            //查看access_token是否过期
            if (appData.expire < DateTime.Now.Ticks)
            {
                CCTokenResult tokenResult = GetCCToken(appData.app_id, appData.app_secret);
                if (tokenResult.res_code == "0")
                {
                    db.UpdateToken(app_id, tokenResult.access_token, DateTime.Now.Ticks + tokenResult.expires_in * 1000);
                    access_token = tokenResult.access_token;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                access_token = appData.access_token;
            }
            NameValueCollection nvc = new NameValueCollection();
            nvc.Add("app_id", app_id);
            nvc.Add("access_token", access_token);
            nvc.Add("timestamp", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            RandCodeToken randCodeToken = GetToken(app_id, appData.app_secret, access_token, nvc);
            if (randCodeToken != null && randCodeToken.res_code==0)
            {
                SendRandCodeResult randCodeResult = SendRandCode(app_id, appData.app_secret, access_token, randCodeToken.token, phone, url);
                if (randCodeResult != null)
                {
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// 发送自定义验证码
        /// </summary>
        /// <param name="tplId">模板id</param>
        /// <param name="phone">电话</param>
        /// <returns></returns>
        public bool SendUserRandCode(string app_id, string phone)
        {
            DataBase db = new DataBase();
            DataBase.SmsAppEntity appData = db.GetApp(app_id);
            string access_token = null;
            //查看access_token是否过期
            if (appData.expire < DateTime.Now.Ticks)
            {
                CCTokenResult tokenResult = GetCCToken(appData.app_id, appData.app_secret);
                if (tokenResult.res_code == "0")
                {
                    db.UpdateToken(app_id, tokenResult.access_token, DateTime.Now.Ticks + tokenResult.expires_in * 1000);
                    access_token = tokenResult.access_token;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                access_token = appData.access_token;
            }
            NameValueCollection nvc = new NameValueCollection();
            nvc.Add("app_id", app_id);
            nvc.Add("access_token", access_token);
            nvc.Add("timestamp", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            RandCodeToken randCodeToken = GetToken(app_id, appData.app_secret, access_token, nvc);
            if (randCodeToken != null && randCodeToken.res_code==0)
            {
                SendRandCodeResult randCodeResult = SendUserRandCode(app_id, appData.app_secret, access_token, randCodeToken.token, phone);
                if (randCodeResult != null && randCodeResult.res_code==0)
                {
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// 获取信息码
        /// </summary>
        /// <param name="app_id">app_id</param>
        /// <param name="app_screct">app_screct</param>
        /// <param name="access_token">access_token</param>
        /// <param name="nvc">参数</param>
        /// <returns></returns>
        public RandCodeToken GetToken(string app_id, string app_screct, string access_token, NameValueCollection nvc)
        {
            string plaintext = GetPlainText(nvc);
            string sign = Convert.ToBase64String(HMAC_SHA1(plaintext, app_screct));
            string url = string.Format("http://api.189.cn/v2/dm/randcode/token?{0}&sign={1}",
                plaintext, sign);
            string data = GetData(url);
            RandCodeToken result = null;
            if (data != null)
            {
                LitJson.JsonData json = LitJson.JsonMapper.ToObject(data);
                result = new RandCodeToken();
                result.res_code = (int)json["res_code"];
                result.token = (string)json["token"];
            }
            return result;
        }
        /// <summary>
        /// 向验证码接口发送数据
        /// </summary>
        /// <param name="app_id"></param>
        /// <param name="app_screct"></param>
        /// <param name="access_token"></param>
        /// <param name="token"></param>
        /// <param name="phone"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        private SendRandCodeResult SendRandCode(string app_id,string app_screct, string access_token,string token,string phone,string url)
        {
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            NameValueCollection nvc = new NameValueCollection();
            nvc.Add("app_id", app_id);
            nvc.Add("access_token",access_token);
            nvc.Add("token",token);
            nvc.Add("phone",phone);
            nvc.Add("url",url);
            nvc.Add("exp_time",exp_time.ToString());
            nvc.Add("timestamp",timestamp);
            string plainText = GetPlainText(nvc);
            string sign = Convert.ToBase64String(HMAC_SHA1(plainText, app_screct));
            string data = string.Format("{0}&sign={1}",
                plainText,sign);
            string json= PostData("http://api.189.cn/v2/dm/randcode/send", data);
            SendRandCodeResult result = null;
            if (json != null)
            {
                LitJson.JsonData jsonData = LitJson.JsonMapper.ToObject(json);
                result = new SendRandCodeResult();
                result.res_code = (int)jsonData["res_code"];
                result.identifier = (string)jsonData["identifier"];
                result.create_at = (string)jsonData["create_at"];
            }
            return result;
        }
        /// <summary>
        /// 向用户自定义验证码接口发送数据
        /// </summary>
        /// <param name="app_id"></param>
        /// <param name="app_screct"></param>
        /// <param name="access_token"></param>
        /// <param name="token"></param>
        /// <param name="phone"></param>
        /// <returns></returns>
        private SendRandCodeResult SendUserRandCode(string app_id,string app_screct, string access_token, string token, string phone)
        {
            string randcode = GetRandCode(6);
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            NameValueCollection nvc = new NameValueCollection();
            nvc.Add("app_id",app_id);
            nvc.Add("access_token", access_token);
            nvc.Add("token",token);
            nvc.Add("phone",phone);
            nvc.Add("randcode", randcode);
            nvc.Add("exp_time", exp_time.ToString());
            nvc.Add("timestamp",timestamp);
            string plainText = GetPlainText(nvc);
            string sign= Convert.ToBase64String(HMAC_SHA1(plainText, app_screct));
            string data = string.Format("{0}&sign={1}",
                plainText, sign);
            string json = PostData("http://api.189.cn/v2/dm/randcode/sendSms", data);
            SendRandCodeResult result = null;
            if (json != null)
            {
                LitJson.JsonData jsonData = LitJson.JsonMapper.ToObject(json);
                result = new SendRandCodeResult();
                result.res_code = (int)jsonData["res_code"];
                result.identifier = (string)jsonData["identifier"];
                result.create_at = (string)jsonData["create_at"];
            }
            return result;
        }
        /// <summary>
        /// 生成验证码所用的字符
        /// </summary>
        private static char[] constant =
        {
            '0','1','2','3','4','5','6','7','8','9'
          };
        /// <summary>
        /// 生成验证码
        /// </summary>
        /// <param name="len">验证码长度</param>
        /// <returns></returns>
        public string GetRandCode(int len)
        {
            System.Text.StringBuilder newRandom = new System.Text.StringBuilder(62);
            Random rd = new Random();
            for (int i = 0; i < len; i++)
            {
                newRandom.Append(constant[rd.Next(constant.Length)]);
            }
            return newRandom.ToString();
        }
        /// <summary>
        /// 验证
        /// </summary>
        /// <param name="phone">电话</param>
        /// <param name="randcode">验证码</param>
        /// <returns></returns>
        public bool VerifyCode(string phone, string randcode)
        {
            DataBase db = new DataBase();
            DataBase.SmsRecordEntity content = db.GetLastSMS(phone);
            if (content != null)
            {
                if (content.randcode == randcode)
                {
                    return true;
                }
            }
            return false;
        }
    }
}