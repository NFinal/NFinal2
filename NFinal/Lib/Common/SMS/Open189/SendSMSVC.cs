using System;
using System.Collections.Generic;
using System.Web;
using LitJson;
namespace NFinal.Common.SMS.Open189
{
    public class SendSMSVC_API:API 
    {
        private string _token;
        private string _phoneNumTo;
        private string _timestamp;
        private int? _exptime = null;
        private string _url = "http://open.189.cn";//这里请替换为您的状态监控页
        public SendSMSVC_API(string app_id, string app_secret, string access_token, string numTo, int? exptime)
        {
            APP_ID = app_id;
            APP_SECRET = app_secret;
            Access_Token = access_token;
            Method = RequestType.post;
            SMSVCToken_API smsvctoken_api = new SMSVCToken_API(app_id, app_secret, access_token);
            string result = smsvctoken_api.Result;
            try
            {
              
                JsonData json = JsonMapper.ToObject(result);
                _token = json["token"].ToString();
                _phoneNumTo = numTo;
                if (exptime != null)
                    _exptime = exptime;
                _timestamp = Utility.GetCurrentDate();
                PrepareParam();
            }
            catch (Exception e)
            {                
                throw e;
            }
        }
        public override IDictionary<string, string> RequestParams
        {
            get { return _requestParams; }
        }
        public override string URL
        {
            get { return "http://api.189.cn/v2/dm/randcode/send"; }
        }
        public override string GetResult
        {
            get { return NotSupportMessage; }
        }
        public override string PostResult
        {
            get { return this.HttpPostRequest(_requestParams); }
        }
        private string sign
        {
            get
            {
                string plaintext = "access_token=" + Access_Token;
                plaintext += "&app_id=" + APP_ID;
                if (_exptime != null)
                    plaintext += "&exp_time=" + _exptime;
                plaintext += "&phone=" + _phoneNumTo;
                plaintext += "&timestamp=" + _timestamp;
                plaintext += "&token=" + _token;
                plaintext += "&url=" + _url;
                System.Diagnostics.Debug.WriteLine(plaintext);
                return Utility.DoSignature(plaintext, APP_SECRET);
            }
        }
        protected override void PrepareParam()
        {
            _requestParams = new Dictionary<string, string>();
            _requestParams.Add("access_token", Access_Token);
            _requestParams.Add("app_id", APP_ID);
            _requestParams.Add("token", _token);
            _requestParams.Add("phone", _phoneNumTo);
            _requestParams.Add("url", _url);
            if (_exptime != null)
                _requestParams.Add("exp_time", _exptime.ToString());
            _requestParams.Add("timestamp", _timestamp);
            _requestParams.Add("sign", sign);
        }
    }
}