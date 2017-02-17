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

namespace NFinal.Common.SMS.Open189
{
    /// <summary>
    /// 模板短信
    /// </summary>
    public class TemplateSms : BaseSms
    {
        /// <summary>
        /// 发送模板短信返回的结果
        /// </summary>
        public class SendTemplateSmsResult
        {
            public int res_code;
            public string res_message;
            public string idertifier;
        }
        public TemplateSms()
        {
        }
        /// <summary>
        /// 发送模板短信
        /// </summary>
        /// <param name="phone">电话</param>
        /// <param name="template_id">模板ID</param>
        /// <param name="nvc">模板参数</param>
        /// <returns></returns>
        public bool Send(string phone, string template_id, NameValueCollection nvc)
        {
            DataBase db = new DataBase();
            DataBase.SmsTemplateEntity templateData= db.GetTemplate(template_id);
            string access_token = null;
            //查看access_token是否过期
            if (templateData.appData.expire < DateTime.Now.Ticks)
            {
                CCTokenResult tokenResult = GetCCToken(templateData.appData.app_id, templateData.appData.app_secret);
                if (tokenResult.res_code == "0")
                {
                    db.UpdateToken(template_id, tokenResult.access_token, DateTime.Now.Ticks + tokenResult.expires_in * 1000);
                    access_token = tokenResult.access_token;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                access_token = templateData.appData.access_token;
            }
            return Send(templateData.appData.app_id,templateData.appData.app_secret,access_token,phone,template_id,BuildJsonFromParameters(nvc));
        }
        /// <summary>
        /// 向发送模板短信接口提交数据
        /// </summary>
        /// <param name="app_id"></param>
        /// <param name="appScrect"></param>
        /// <param name="access_token"></param>
        /// <param name="acceptor_tel"></param>
        /// <param name="template_id"></param>
        /// <param name="template_param"></param>
        /// <returns></returns>
        private bool Send(string app_id, string appScrect,string access_token, string acceptor_tel, string template_id, string template_param)
        {
            string timestamp = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string data = string.Format("app_id={0}&access_token={1}&acceptor_tel={2}&template_id={3}&template_param={4}&timestamp={5}",
                app_id, access_token, acceptor_tel, template_id, template_param, timestamp);
            string sendResult = PostData("http://api.189.cn/v2/emp/templateSms/sendSms", data);
            if (sendResult != null)
            {
                LitJson.JsonData jsonData = LitJson.JsonMapper.ToObject(sendResult);
                SendTemplateSmsResult result = new SendTemplateSmsResult();
                result.res_code = (int)jsonData["res_code"];
                result.res_message = (string)jsonData["res_message"];
                result.idertifier = (string)jsonData["idertifier"];
                DataBase db = new DataBase();
                return db.InsertSMSRecord(acceptor_tel, app_id, template_id, template_param, result.res_code == 0, "");
            }
            return false;
        }        
    }
}