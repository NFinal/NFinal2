using System;
using System.Globalization;
using System.Text;

namespace NFinal.Extension
{
    public static class JsonConvert
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
            DateTime dt = DateTime.ParseExact("Wed Apr 7 14:40:41 UTC+0800 2010", fmtDate, ciDate);
            return dt;
        }
    }
}
