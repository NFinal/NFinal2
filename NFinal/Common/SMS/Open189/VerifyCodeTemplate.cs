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
using System.Collections.Specialized;
using System.Text;
using System.Web;

namespace NFinal.Common.SMS.Open189
{
    public class VerifyCodeTemplate:Template
    {
        public int timeInterval = 60 * 1000;
        public int codeLen = 4;
        public VerifyCodeTemplate(string tplId)
            : base(tplId)
        { }
        public bool EnableReSend(string acceptor_tel)
        {
            SMSDB db = new SMSDB();
            SMSDB.SMSContent content = db.GetLastSMS(acceptor_tel);
            if (content == null)
            {
                return true;
            }
            else
            {
                if (DateTime.Now.Ticks - content.time > timeInterval)
                {
                    return true;
                }
            }
            return false;
        }
        public bool SendSMS(string acceptor_tel)
        {
            if (EnableReSend(acceptor_tel))
            {
                NameValueCollection nvc = new NameValueCollection();
                string verify_code = GetRandomCode();
                nvc.Add("verify_code", verify_code);
                return Send(acceptor_tel, tplId, nvc, verify_code);
            }
            else
            {
                return false;
            }
        }
        public string GetRandomCode()
        {
            return GetRandomCode(codeLen);
        }
        public bool VerifyCode(string phone, string code)
        {
            SMSDB db = new SMSDB();
            SMSDB.SMSContent content = db.GetLastSMS(phone);
            if (content != null)
            {
                if (content.code == code)
                {
                    return true;
                }
            }
            return false;
        }
    }
}