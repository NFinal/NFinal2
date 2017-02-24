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
    public class SMSDB
    {
        private static string connectionString = @"Data Source=|DataDirectory|\Common.db";
        public class TemplateData
        {
            public string tpl_id;
            public string app_id;
            public string app_secret;
            public string access_token;
            public long expire;
            public string name;
            public string content;
        }
        public class SMSContent
        {
            public int id;
            public long time;
            public string content;
            public string phone;
            public string code;
        }
        public bool UpdateToken(string tpl_id,string token,long expire)
        {
            SQLiteConnection connection = new SQLiteConnection(connectionString);
            connection.Open();
            string sql=string.Format("update sms_template set access_token='{0}',expire={1} where tpl_id={2}",token,expire,tpl_id);
            SQLiteCommand cmd = new SQLiteCommand(sql,connection);
            int count = 0;
            try
            {
                count = cmd.ExecuteNonQuery();
            }
            catch
            { }
            finally {
                connection.Close();
            }
            if (count > 0)
            {
                return true;
            }
            return false;
        }
        public TemplateData GetTemplate(string tpl_id)
        {
            SQLiteConnection connection = new SQLiteConnection(connectionString);
            connection.Open();
            SQLiteCommand cmd = new SQLiteCommand("select * from sms_template where tpl_id="+tpl_id,connection);
            SQLiteDataReader reader = null;
            TemplateData templateData = null;
            try
            {
                reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    templateData = new TemplateData();
                    templateData.tpl_id = reader["tpl_id"].ToString();
                    templateData.app_id = reader["app_id"].ToString();
                    templateData.app_secret = reader["app_secret"].ToString();
                    templateData.access_token = reader["access_token"].ToString();
                    templateData.expire =Convert.ToInt64(reader["expire"]);
                    templateData.name = reader["name"].ToString();
                    templateData.content = reader["content"].ToString();
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
        public bool InsertSMS(string phone,string content,long timestamp,bool success,string code)
        {
            SQLiteConnection connetion = new SQLiteConnection(connectionString);
            connetion.Open();
            string sql = string.Format("insert into sms_send(phone,content,time,success,code) values('{0}','{1}',{2},{3},'{4}')",
            phone,content,timestamp,success?1:0,code);
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
        public SMSContent GetLastSMS(string phone)
        {
            SQLiteConnection connection=new SQLiteConnection(connectionString);
            connection.Open();
            string sql=string.Format("select * from sms_send where phone='{0}' order by time desc limit 1",phone);
            SQLiteCommand cmd=new SQLiteCommand(sql,connection);
            SQLiteDataReader reader=null;
            SMSContent content=null;
            try
            {
                reader = cmd.ExecuteReader();
                if(reader.HasRows)
                {
                    reader.Read();
                    content =new SMSContent();
                    content.id=Convert.ToInt32(reader["id"]);
                    content.time =Convert.ToInt64(reader["time"]);
                    content.content=reader["content"].ToString();
                    content.phone =reader["phone"].ToString();
                    content.code =reader["code"].ToString();
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