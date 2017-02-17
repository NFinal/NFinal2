//======================================================================
//
//        Copyright : Zhengzhou Strawberry Computer Technology Co.,LTD.
//        All rights reserved
//        
//        Application:NFinal MVC framework
//        Filename :Session.cs
//        Description :重写Session
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
using System.Data;

namespace NFinal.Session
{
    public class Session
    {
        private static string connectionString = @"Data Source=|DataDirectory|\Session.db";
#if AspNET
        private HttpContext context;
#endif
        private TimeSpan ts;
        private string session_name = "PHPSESSIONID";
        private bool hasSessionName=false;
#if AspNET
        public Session(HttpContext context,TimeSpan ts)
        {
            this.context = context;
            this.ts = ts;
        }
#endif
        //获取cookie值
        private string GetSessionCookie()
        {
#if AspNET
            if (this is System.Web.SessionState.IRequiresSessionState)
            {
                if (context.Request.Cookies[session_name] != null)
                {
                    return context.Request.Cookies[session_name].Value;
                }
                else

                {
                return null;
                }
            }
            else
#endif
            {
                return null;
            }
        }
        private void AddSessionCookie(string session_id)
        {
#if AspNET
            if (this is System.Web.SessionState.IRequiresSessionState)
            {
                HttpCookie cookie = new System.Web.HttpCookie(session_name, session_id);
                context.Response.Cookies.Add(cookie);
            }
#endif
        }
        public string GetSession(string key)
        {
            string id = GetSessionCookie();
            return GetSession(id, key);
        }
        //获取session
        public string GetSession(string id, string key)
        {
            SQLiteConnection con = new SQLiteConnection(connectionString);
            con.Open();
            SQLiteTransaction trans = con.BeginTransaction();
            SQLiteCommand cmd = null;
            SQLiteParameter idPar = null;
            //如果seesion_id不存在,则值一定不存在,不过要添加一个session_id
            if (hasSessionName ==false && id == null)
            { 
                id=Guid.NewGuid().ToString();
                AddSessionCookie(id);
                cmd = new SQLiteCommand("insert into session_id(id,time) values(@id,@time)",con,trans);
                idPar = new SQLiteParameter("@id",DbType.Guid, 16);
                idPar.Value =new Guid(id);
                SQLiteParameter timePar = new SQLiteParameter("@time",DbType.Int64);
                timePar.Value = DateTime.Now.Ticks;
                cmd.Parameters.Add(idPar);
                cmd.Parameters.Add(timePar);
                cmd.ExecuteNonQuery();
                hasSessionName = true;
                trans.Commit();
                con.Close();
                return null;
            }
            cmd = new SQLiteCommand("select count(0) from session_id where id=@id", con,trans);
            idPar = new SQLiteParameter("@id",DbType.Guid, 16);
            idPar.Value = new Guid(id);
            cmd.Parameters.Add(idPar);
            int count = Convert.ToInt32(cmd.ExecuteScalar());
            //如果有这个session,则查找相应的变量
            if (count > 0)
            {
                hasSessionName = true;
                cmd = new SQLiteCommand("select val from session_val where id=@id and key=@key", con,trans);
                SQLiteParameter keyPar = new SQLiteParameter("@key",DbType.String, 0);
                keyPar.Value = key;
                cmd.Parameters.Add(idPar);
                cmd.Parameters.Add(keyPar);
                SQLiteDataReader reader = cmd.ExecuteReader();
                string result = null;
                if(reader.Read() && !reader.IsDBNull(0))
                {
                    result = reader.GetString(0);
                    reader.Close();
                    trans.Commit();
                    con.Close();
                    return result;
                }
                else
                {
                    reader.Close();
                    trans.Commit();
                    con.Close();
                    return null;
                }
            }
            else
            //如果没有这个session
            {
                trans.Commit();
                con.Close();
                return null;
            }
        }
        //删除过期的session
        public void ClearSession(TimeSpan ts)
        {
            SQLiteConnection con = new SQLiteConnection(connectionString);
            con.Open();
            long expire = DateTime.Now.Ticks - ts.Ticks;
            SQLiteCommand cmd = new SQLiteCommand("delete from session_val where id in (select id from session_id where session_id.time<@expire)",con);
            SQLiteParameter expirePar = new SQLiteParameter("@expire", DbType.Int64);
            expirePar.Value = expire;
            cmd.Parameters.Add(expire);
            int rows = cmd.ExecuteNonQuery();
            cmd = new SQLiteCommand("delete from session_id where session_id.time<@expire",con);
            cmd.Parameters.Add(expire);
            cmd.ExecuteNonQuery();
            con.Close();
        }
        public void SetSession(string key, string val)
        {
            string id = GetSessionCookie();
            SetSession(id,key,val);
        }
        //设置session
        public string SetSession(string id,string key, string val)
        {
            int count=0;
            SQLiteConnection con = new SQLiteConnection(connectionString);
            con.Open();
            SQLiteTransaction trans = con.BeginTransaction();
            SQLiteCommand cmd = null;
            SQLiteParameter idPar = null;
            //如果session_id还不存在,并且id为null,添加session_id
            if (hasSessionName==false && id == null)
            {
                id = Guid.NewGuid().ToString();
                AddSessionCookie(id);
                cmd = new SQLiteCommand("insert into session_id(id,time) values(@id,@time)",con,trans);
                idPar = new SQLiteParameter("@id",DbType.Guid, 16);
                idPar.Value =new Guid(id);
                SQLiteParameter timePar = new SQLiteParameter("@time",DbType.Int64);
                timePar.Value = DateTime.Now.Ticks;
                cmd.Parameters.Add(timePar);
                cmd.Parameters.Add(idPar);
                int rows = cmd.ExecuteNonQuery();
                hasSessionName = true;
                count = 1;
            }
            //如果session_id存在,则查找数据库session
            else
            {
                cmd = new SQLiteCommand("select count(0) from session_id where id=@id", con,trans);
                idPar = new SQLiteParameter("@id",DbType.Guid, 16);
                idPar.Value =new Guid(id);
                cmd.Parameters.Add(idPar);
                SQLiteDataReader reader = cmd.ExecuteReader();
                reader.Read();
                count = reader.GetInt32(0);
                reader.Close();
            }
            //如果有这个session,则查找key是否存在
            if(count>0)
            {
                hasSessionName = true;
                //更改这条记录
                cmd = new SQLiteCommand("update session_val set val=@val where id=@id and key=@key", con,trans);
                SQLiteParameter keyPar = new SQLiteParameter("@key",DbType.String);
                keyPar.Value = key;
                SQLiteParameter valPar=new SQLiteParameter("@val",DbType.String);
                valPar.Value =val;
                cmd.Parameters.Add(idPar);
                cmd.Parameters.Add(keyPar);
                cmd.Parameters.Add(valPar);
                int rows = cmd.ExecuteNonQuery();
                //如果更改不成功,说明记录不存在,则执行添加操作
                if (rows < 1)
                {
                    cmd = new SQLiteCommand("insert into session_val(id,key,val) values(@id,@key,@val)",con,trans);
                    cmd.Parameters.Add(idPar);
                    cmd.Parameters.Add(keyPar);
                    cmd.Parameters.Add(valPar);
                    cmd.ExecuteNonQuery();
                }
                //更新session_id中的时间
                cmd = new SQLiteCommand("update session_id set time=@time where id=@id",con,trans);
                SQLiteParameter timePar = new SQLiteParameter("@time",DbType.Int64);
                timePar.Value = DateTime.Now.Ticks; 
                cmd.Parameters.Add(idPar);
                cmd.Parameters.Add(timePar);
                cmd.ExecuteNonQuery();
            }
            //如果没有这个session,则添加该Session
            else
            {
                //添加session_id
                cmd =new SQLiteCommand("insert into session_id(id,time) values(@id,@time)",con,trans);
                SQLiteParameter timePar=new SQLiteParameter("@time",DbType.Int64);
                timePar.Value = DateTime.Now.Ticks;
                cmd.Parameters.Add(timePar);
                cmd.Parameters.Add(idPar);
                int rows= cmd.ExecuteNonQuery();
                //添加session key,value
                if (rows > 0)
                {
                    hasSessionName = true;
                    cmd = new SQLiteCommand("insert into session_val(id,key,val) values(@id,@key,@val)", con,trans);
                    SQLiteParameter keyPar = new SQLiteParameter("@key",DbType.String);
                    keyPar.Value = key;
                    SQLiteParameter valPar = new SQLiteParameter("@val",DbType.String);
                    valPar.Value = val;
                    cmd.Parameters.Add(idPar);
                    cmd.Parameters.Add(keyPar);
                    cmd.Parameters.Add(valPar);
                    cmd.ExecuteNonQuery();
                }
            }
            trans.Commit();
            con.Close();
            return id;
        }
    }
}