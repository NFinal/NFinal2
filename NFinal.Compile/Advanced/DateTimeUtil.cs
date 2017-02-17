using System;

namespace NFinal.Advanced
{
    /// <summary>
    /// 日期和时间扩展方法工具类
    /// </summary>
    /// <example>
    /// <code>
    /// string s1 = DateTime.Now.Format();
    /// string s2 = DateTime.Now.FormatTime();
    /// string s3 = DateTime.Today.FormatDate();
    /// string s4 = DateTime.Now.FormatRfc3339();
    /// string s5 = DateTime.Now.FormatRfc3339(DateTimeKind.Utc);
    /// </code>
    /// </example>
    public static class DateTimeUtil
    {
        public static readonly IFormats DefaultFormats = InternalDefaultFormats.Instance;

        /// <summary>
        /// 全局格式
        /// </summary>
        public static IFormats Formats
        {
            get
            {
                return formats ?? (formats = new DefinedFormat
                {
                    DateTime = DefaultFormats.DateTime,
                    Date = DefaultFormats.Date,
                    Time = DefaultFormats.Time
                });
            }
            set { formats = value; }
        }

        private static IFormats formats;

        /// <summary>
        /// <p>根据 RFC 3339 规定，得到表示日期的字符串。</p>
        /// <p>结果示例</p>
        /// <ul>
        ///     <li>2014-10-20T01:23:45Z</li>
        ///     <li>2014-10-20T09:23:45+08:00</li>
        /// </ul>
        /// </summary>
        /// <param name="value">日期时间值</param>
        /// <param name="defaultKind">当 <paramref name="value"/>
        /// 的 <c>Kind</c> 属性值是 <see cref="DateTimeKind.Unspecified" /> 时，
        /// 用 <paramref name="defaultKind" /> 代替。
        /// </param>
        /// <returns></returns>
        /// <remark>
        /// 如果 <c>value.Kind == DateTimeKind.Unspecified</c>，默认格式化结果不符合 RFC3339 标准，
        /// 没有后缀的 <c>Z</c> 或时区偏移量。而通常从数据库获取的日期都是没有指定 <c>Kind</c> 的。
        /// 这种情况下，一般应该使用 <c>DateTimeKind.Local</c> 作为 <c>DateTimeKind</c>。
        /// </remark>
        public static string FormatRfc3339(
            DateTime value,
            DateTimeKind defaultKind = DateTimeKind.Local
        )
        {
            if (value.Kind == DateTimeKind.Unspecified && value.Kind != defaultKind)
            {
                value = new DateTime(value.Ticks, defaultKind);
            }

            return value.ToString("yyyy-MM-dd'T'HH:mm:ssK");
        }

        /// <summary>
        /// 根据全局日期时间格式，格式指定日期对象得到格式化后的字符串
        /// </summary>
        /// <param name="datetime"></param>
        /// <returns></returns>
        public static string Format(this DateTime datetime)
        {
            return datetime.ToString(Formats.DateTime ?? DefaultFormats.DateTime);
        }

        /// <summary>
        /// 根据全局日期格式，格式化指定日期对象得到格式化后的字符串
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string FormatDate(this DateTime date)
        {
            return date.ToString(Formats.Date ?? DefaultFormats.Date);
        }

        /// <summary>
        /// 根据全局时间格式，格式化指定时间对象得到格式化后的字符串
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static string FormatTime(this DateTime time)
        {
            return time.ToString(Formats.Time ?? DefaultFormats.Time);
        }

        /// <summary>
        /// 计算从1970年1月1日到指定日期的毫秒数数，与 Java 中 <c>java.util.Date.getTime()</c> 返回的值相同。
        /// </summary>
        /// <param name="value"></param>
        /// <param name="timezone"></param>
        /// <returns></returns>
        public static long ToJavaMilliseconds(this DateTime value, TimeZoneInfo timezone = null)
        {
            DateTime date1970 = new DateTime(1970, 1, 1, 0, 0, 0);
            date1970 = TimeZoneInfo.ConvertTimeFromUtc(date1970, timezone ?? TimeZoneInfo.Local);
            return (value.Ticks - date1970.Ticks) / 10000;
        }

        /// <summary>
        /// 根据 Java 中表示时间和 <c>long</c> 型数据，恢复成 <see cref="DateTime" /> 类型数据。
        /// </summary>
        /// <param name="value"></param>
        /// <param name="timezone"></param>
        /// <returns></returns>
        public static DateTime FromJavaMilliseconds(long value, TimeZoneInfo timezone = null)
        {
            DateTime date1970 = TimeZoneInfo.ConvertTimeFromUtc(
                new DateTime(1970, 1, 1), timezone ?? TimeZoneInfo.Local);
            return new DateTime(date1970.Ticks + value * 10000);
        }

        /// <summary>
        /// 格式设置接口
        /// </summary>
        public interface IFormats
        {
            string DateTime { get; }
            string Date { get; }
            string Time { get; }
        }

        /// <summary>
        /// 日期时间格式参数类
        /// </summary>
        public class DefinedFormat : IFormats
        {
            /// <summary>
            /// 默认的，含时间的日期格式，精确到秒
            /// </summary>
            public string DateTime { get; set; }

            /// <summary>
            /// 日期格式，不含时间
            /// </summary>
            public string Date { get; set; }

            /// <summary>
            /// 时间格式，不含日期部分，精确到秒
            /// </summary>
            public string Time { get; set; }
        }

        private sealed class InternalDefaultFormats : IFormats
        {
            public static readonly InternalDefaultFormats Instance;
            static InternalDefaultFormats() { Instance = new InternalDefaultFormats(); }
            public string DateTime { get { return "yyyy-MM-dd HH:mm:ss"; } }
            public string Date { get { return "yyyy-MM-dd"; } }
            public string Time { get { return "HH:mm:ss"; } }
        }
    }
}
