//======================================================================
//
//        Copyright : Zhengzhou Strawberry Computer Technology Co.,LTD.
//        All rights reserved
//
//        Application:NFinal MVC framework
//        Filename :Template.cs
//        Description :短信模板类
//
//        created by Lucas at  2015-6-30`
//     
//        WebSite:http://www.nfinal.com
//
//======================================================================
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Web;
using System.Data.SQLite;

namespace NFinal.Common.SMS.Open189
{
    public class Template:SMS
    {
        public class TemplateResult
        {
            public int res_code;
            public string res_message;
            public string idertifier;
        }
        public string tplId;
        public Template(string tplId)
        {
            this.tplId = tplId;
        }
        protected bool Send(string phone, string tplId, NameValueCollection nvc,string code)
        {
            SMSDB db = new SMSDB();
            SMSDB.TemplateData templateData= db.GetTemplate(tplId);
            string access_token = null;
            //查看access_token是否过期
            if (templateData.expire < DateTime.Now.Ticks)
            {
                CCTokenResult tokenResult = GetCCToken(templateData.app_id, templateData.app_secret);
                if (tokenResult.res_code == "0")
                {
                    db.UpdateToken(tplId, tokenResult.access_token, DateTime.Now.Ticks + tokenResult.expires_in * 1000);
                    access_token = tokenResult.access_token;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                access_token = templateData.access_token;
            }
            return Send(templateData.app_id,templateData.app_secret,access_token,phone,templateData.tpl_id,BuildParameter(nvc),code);
        }
        protected bool Send(string appId, string appScrect,string access_token, string acceptor_tel, string templateId, string template_param,string code)
        {
            string timestamp = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string data = string.Format("app_id={0}&access_token={1}&acceptor_tel={2}&template_id={3}&template_param={4}&timestamp={5}",
                appId, access_token, acceptor_tel, templateId, template_param, timestamp);
            string sendResult = PostData("http://api.189.cn/v2/emp/templateSms/sendSms", data);
            if (sendResult != null)
            {
                LitJson.JsonData jsonData = LitJson.JsonMapper.ToObject(sendResult);
                TemplateResult result = new TemplateResult();
                result.res_code = (int)jsonData["res_code"];
                result.res_message = (string)jsonData["res_message"];
                result.idertifier = (string)jsonData["idertifier"];
                SMSDB db = new SMSDB();
                return db.InsertSMS(acceptor_tel, template_param, GetTimeStamp(), result.res_code == 0, code);
            }
            return false;
        }        
    }
}