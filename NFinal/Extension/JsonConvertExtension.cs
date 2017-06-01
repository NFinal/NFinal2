//======================================================================
//
//        Copyright : Zhengzhou Strawberry Computer Technology Co.,LTD.
//        All rights reserved
//        
//        Application:NFinal MVC framework
//        Filename : Int32Extension.cs
//        Description :Int32扩展函数
//
//        created by Lucas at  2015-5-31
//     
//        WebSite:http://www.nfinal.com
//
//======================================================================
using System;
using System.Globalization;
using System.Text;

namespace NFinal
{
    /// <summary>
    /// JSON转换类，时间和布尔类型
    /// </summary>
    public static class JsonConvertExtension
    {
        //设置转换格式  需要引入命名空间：using System.Globalization;  
        static string  fmtDate = "ddd MMM d HH:mm:ss 'UTC'zz'00' yyyy";
        static CultureInfo ciDate = CultureInfo.CurrentCulture;
        /// <summary>
        /// 把时间转为long类型
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string AsJsonDataTime(this DateTime dt)
        {
            //将C#时间转换成JS时间字符串
            string JSstring = dt.ToString(fmtDate, ciDate);
            return JSstring;
        }
        /// <summary>
        /// 把时间类型转为datetime
        /// </summary>
        /// <param name="datetime"></param>
        /// <returns></returns>
        public static DateTime AsJsonDateTime(this string datetime)
        {
            //将JS时间字符串转换成C#时间  
            DateTime dt = DateTime.ParseExact(datetime, fmtDate, ciDate);
            return dt;
        }
        /// <summary>
        /// 把布尔类型转Json
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToJson(this bool obj)
        {
            return obj ? "true" : "false";
        }
    }
}
