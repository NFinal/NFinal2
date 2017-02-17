//======================================================================
//
//        Copyright : Zhengzhou Strawberry Computer Technology Co.,LTD.
//        All rights reserved
//
//        Application:NFinal MVC framework
//        Filename :SMSDB.cs
//        Description :短信数据操作类
//
//        created by Lucas at  2015-6-30`
//     
//        WebSite:http://www.nfinal.com
//
//======================================================================
using System;
using System.Collections.Generic;
using System.Web;
using System.Data.SQLite;

namespace NFinal.Common.SMS.Open189
{
    public class DataBase
    {
        private static string connectionString = Config.ConfigurationManager.ConnectionStrings["Common"].ConnectionString;
        /// <summary>
        /// 模板短信实体
        /// </summary>
        public class SmsTemplateEntity
        {
            public string id;
            public string name;
            public string template_id;
            public string app_id;
            public SmsAppEntity appData;
            public string content;
        }
        /// <summary>
        /// app应用实体类
        /// </summary>
        public class SmsAppEntity
        {
            public string id;
            public string name;
            public string app_id;
            public string app_secret;
            public string access_token;
            public long expire;
        }
        /// <summary>
        /// 已经发送的短信
        /// </summary>
        public class SmsRecordEntity
        {
            public int id;
            public string phone;
            public string app_id;
            public string template_id;
            public string parameters;
            public string time;
            public bool success;
            public string randcode;
        }
        /// <summary>
        /// 更新accessToken
        /// </summary>
        /// <param name="tpl_id">模板id</param>
        /// <param name="accessToken">访问Token</param>
        /// <param name="expire">过期时间</param>
        /// <returns></returns>
        public bool UpdateToken(string app_id, string accessToken, long expire)
        {
            SQLiteConnection connection = new SQLiteConnection(connectionString);
            connection.Open();
            int count = 0;
            try
            {
                string sql = string.Format("update sms_app set access_token='{0}',expire={1} where app_id='{2}'", accessToken, expire, app_id);
                SQLiteCommand cmd = new SQLiteCommand(sql, connection);
                count = cmd.ExecuteNonQuery();
            }
            catch
            { }
            finally
            {
                connection.Close();
            }
            if (count > 0)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// 获取模板内容
        /// </summary>
        /// <param name="tpl_id">模板id</param>
        /// <returns></returns>
        public SmsTemplateEntity GetTemplate(string template_id)
        {
            SQLiteConnection connection = new SQLiteConnection(connectionString);
            connection.Open();
            SQLiteCommand cmd = new SQLiteCommand(string.Format("select * from sms_template where template_id='{0}'", template_id), connection);
            SQLiteDataReader reader = null;
            SQLiteDataReader appReader = null;
            SmsTemplateEntity templateData = null;
            try
            {
                reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    templateData = new SmsTemplateEntity();
                    templateData.id = reader["id"].ToString();
                    templateData.name = reader["name"].ToString();
                    templateData.template_id =reader["template_id"].ToString();
                    templateData.app_id = reader["app_id"].ToString();
                    templateData.content = reader["content"].ToString();
                    cmd.Dispose();
                }
                if (string.IsNullOrEmpty(templateData.app_id))
                {
                    cmd = new SQLiteCommand(string.Format("select * from sms_app where app_id='{0}'", templateData.app_id),connection);
                    try
                    {
                        appReader = cmd.ExecuteReader();
                        if (appReader.HasRows)
                        {
                            appReader.Read();
                            SmsAppEntity appData = new SmsAppEntity();
                            appData.id = appReader["id"].ToString();
                            appData.name = appReader["name"].ToString();
                            appData.app_id = appReader["app_id"].ToString();
                            appData.app_secret = appReader["app_secret"].ToString();
                            appData.access_token = appReader["access_token"].ToString();
                            appData.expire = Convert.ToInt64(appReader["expire"]);
                            templateData.appData = appData;
                        }
                    }
                    finally
                    {
                        cmd.Dispose();
                        appReader.Close();
                    }
                }
            }
            catch (SQLiteException)
            { }
            finally {
                reader.Close();
                connection.Close();
            }
            return templateData;
        }
        /// <summary>
        /// 获取应用实体
        /// </summary>
        /// <param name="app_id">应用id</param>
        /// <returns></returns>
        public SmsAppEntity GetApp(string app_id)
        {
            SQLiteConnection connection = new SQLiteConnection(connectionString);
            connection.Open();
            SQLiteCommand cmd = new SQLiteCommand(string.Format("select * from sms_app where app_id='{0}'", app_id), connection);
            SQLiteDataReader reader = null;
            SmsAppEntity appData = null;
            try
            {
                reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    appData = new SmsAppEntity();
                    appData.id = reader["id"].ToString();
                    appData.name = reader["name"].ToString();
                    appData.app_id = reader["app_id"].ToString();
                    appData.app_secret = reader["app_secret"].ToString();
                    appData.access_token = reader["access_token"].ToString();
                    appData.expire = Convert.ToInt64(reader["expire"]);
                    
                }
            }
            finally
            {
                cmd.Dispose();
                reader.Close();
            }
            return appData;
        }
        /// <summary>
        /// 插入短信记录
        /// </summary>
        /// <param name="phone">电话</param>
        /// <param name="app_id">应用id</param>
        /// <param param name="template_id">模板id</param>
        /// <param name="parameters">模板短信的参数</param>
        /// <param name="success">发送是否成功</param>
        /// <param name="randcode">验证码，没有为空</param>
        /// <returns></returns>
        public bool InsertSMSRecord(string phone, string app_id, string template_id,string parameters,bool success,string randcode)
        {
            SQLiteConnection connetion = new SQLiteConnection(connectionString);
            connetion.Open();
            string sql = string.Format("insert into sms_send(phone,app_id,template_id,parameters,time,success,randcode) values('{0}','{1}','{2}','{3}','{4}',{5},'{6}')",
            phone, app_id, template_id, parameters,DateTime.Now.ToString(),success?1:0,randcode);
            SQLiteCommand cmd = new SQLiteCommand(sql,connetion);
            try
            {
                int count = cmd.ExecuteNonQuery();
                if (count > 0)
                {
                    return true;
                }
            }
            catch (SQLiteException)
            {
            }
            finally
            {
                connetion.Close();
            }
            return false;
        }
        /// <summary>
        /// 获取最后一次用于发送的短信
        /// </summary>
        /// <param name="phone">电话</param>
        /// <returns></returns>
        public SmsRecordEntity GetLastSMS(string phone)
        {
            SQLiteConnection connection=new SQLiteConnection(connectionString);
            connection.Open();
            string sql=string.Format("select * from sms_record where phone='{0}' order by id desc limit 1",phone);
            SQLiteCommand cmd=new SQLiteCommand(sql,connection);
            SQLiteDataReader reader=null;
            SmsRecordEntity content =null;
            try
            {
                reader = cmd.ExecuteReader();
                if(reader.HasRows)
                {
                    reader.Read();
                    content =new SmsRecordEntity();
                    content.id=Convert.ToInt32(reader["id"]);
                    content.phone = reader["phone"].ToString();
                    content.app_id =reader["app_id"].ToString();
                    content.template_id=reader["template_id"].ToString();
                    content.parameters =reader["parameters"].ToString();
                    content.time = reader["time"].ToString();
                    content.success =reader["success"].ToString()=="1";
                }
            }
            catch(SQLiteException)
            {}
            finally{
                reader.Close();
                connection.Close();
            }
            return content;
        }
    }
}