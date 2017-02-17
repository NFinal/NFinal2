//======================================================================
//
//        Copyright : Zhengzhou Strawberry Computer Technology Co.,LTD.
//        All rights reserved
//        
//        Application:NFinal MVC framework
//        Filename :SqlObject.cs
//        Description :Object类型转其它类型
//
//        created by Lucas at  2015-6-30`
//     
//        WebSite:http://www.nfinal.com
//
//======================================================================
using System;
using System.Collections.Generic;
using System.Web;

namespace NFinal.Compile.DB
{
    public class SqlObject
    {
        public object obj = null;
        public SqlObject(object obj)
        {
            this.obj = obj;
        }
        public static bool operator true(SqlObject obj)
        {
            if (obj == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public static bool operator false(SqlObject obj)
        {
            if (obj == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public override string ToString()
        {
            if (obj != null)
            {
                return obj.ToString();
            }
            else
            {
                return null;
            }
        }
        public byte ToByte()
        {
            if (obj != null)
            {
                return Convert.ToByte(obj);
            }
            else
            {
                return 0;
            }
        }
        public int ToInt()
        {
            if (obj != null)
            {
                return Convert.ToInt32(obj);
            }
            else
            {
                return 0;
            }
        }
        public long ToLong()
        {
            if (obj != null)
            {
                return Convert.ToInt64(obj);
            }
            else
            {
                return 0;
            }
        }
        public float ToFloat()
        {
            if (obj != null)
            {
                return Convert.ToSingle(obj);
            }
            else
            {
                return 0;
            }
        }
        public double ToDouble()
        {
            if (obj != null)
            {
                return Convert.ToDouble(obj);
            }
            else
            {
                return 0;
            }
        }
        public decimal ToDecimal()
        {
            if (obj != null)
            {
                return Convert.ToDecimal(obj);
            }
            else
            {
                return 0;
            }
        }
        public DateTime ToDateTime()
        {
            if (obj != null)
            {
                return Convert.ToDateTime(obj);
            }
            else
            {
                return DateTime.Now;
            }
        }
    }
}