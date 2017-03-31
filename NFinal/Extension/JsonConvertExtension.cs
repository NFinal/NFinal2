using System;
using System.Globalization;
using System.Text;

namespace NFinal
{
    public static class JsonConvertExtension
    {
        //设置转换格式  需要引入命名空间：using System.Globalization;  
        static string  fmtDate = "ddd MMM d HH:mm:ss 'UTC'zz'00' yyyy";
        static CultureInfo ciDate = CultureInfo.CurrentCulture;
        public static string AsJsonDataTime(this DateTime dt)
        {
            //将C#时间转换成JS时间字符串
            string JSstring = dt.ToString(fmtDate, ciDate);
            return JSstring;
        }
        public static DateTime AsJsonDateTime(this string datetime)
        {
            //将JS时间字符串转换成C#时间  
            DateTime dt = DateTime.ParseExact(datetime, fmtDate, ciDate);
            return dt;
        }
        public static string ToJson(this bool obj)
        {
            return obj ? "true" : "false";
        }
    }
}
