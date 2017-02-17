using System;
using System.Globalization;

// ReSharper disable CheckNamespace
namespace NFinal.Advanced
{
    /// <summary>
    /// 该类提供将<c>string</c>转换为其它数据类型的若干扩展方法。
    /// </summary>
    public static partial class StringUtil
    {
        #region Any Type Value
        /// <summary>
        /// 使用<see cref="System.Convert.ChangeType(object ,Type)"/>
        /// 方法将当前字符串值转换为指定的数据类型。
        /// </summary>
        /// <typeparam name="T">目标类型</typeparam>
        /// <param name="s">源字符串</param>
        /// <returns>转换失败返回指定类型的默认值，否则返回转换后的值。</returns>
        /// <remarks>相对于指定目的类型的转换来说，该转换效率较低，应尽量避免使用。</remarks>
        public static T ToValue<T>(this string s)
        {
            object v = ToValue(s, typeof(T), default(T));
            return v == null ? default(T) : (T) v;
        }

        /// <summary>
        /// 使用<see cref="System.Convert.ChangeType(object ,Type)"/>
        /// 方法将当前字符串值转换为指定的数据类型。
        /// </summary>
        /// <typeparam name="T">目标类型</typeparam>
        /// <param name="s">源字符串</param>
        /// <param name="defaultValue">转换失败时使用的默认值</param>
        /// <returns>转换失败返回传入的默认值，否则返回转换后的值。</returns>
        /// <remarks>相对于指定目的类型的转换来说，该转换效率较低，应尽量避免使用。</remarks>
        public static T ToValue<T>(this string s, T defaultValue)
        {
            object v = ToValue(s, typeof(T), defaultValue);
            return v == null ? defaultValue : (T) v;
        }

        /// <summary>
        /// 使用<see cref="System.Convert.ChangeType(object ,Type)"/>
        /// 方法将当前字符串值转换为指定的数据类型。
        /// </summary>
        /// <param name="s">源字符串</param>
        /// <param name="type">目标类型</param>
        /// <returns>转换失败返回null，否则返回转换后的值。</returns>
        /// <remarks>相对于指定目的类型的转换来说，该转换效率较低，应尽量避免使用。</remarks>
        public static object ToValue(this string s, Type type)
        {
            return ToValue(s, type, null);
        }

        /// <summary>
        /// 使用<see cref="System.Convert.ChangeType(object ,Type)"/>
        /// 方法将当前字符串值转换为指定的数据类型。
        /// </summary>
        /// <param name="s">源字符串</param>
        /// <param name="type">目标类型</param>
        /// <param name="defaultValue">转换失败时使用的默认值</param>
        /// <returns>转换失败返回传入的默认值（object类型引用），否则返回转换后的值。</returns>
        /// <remarks>相对于指定目的类型的转换来说，该转换效率较低，应尽量避免使用。</remarks>
        public static object ToValue(this string s, Type type, object defaultValue)
        {
            if (s == null) { return defaultValue; }
            try { return Convert.ChangeType(s, type); }
            catch { return defaultValue; }
        }
        #endregion

        #region ToBoolean
        /// <summary>
        /// 将字符串转换为<c>bool</c>类型。
        /// </summary>
        /// <param name="s">源字符串</param>
        /// <param name="defaultValue">转换失败时返回的默认值</param>
        /// <returns>转换得来的<c>bool</c>值。若转换失败返回<c>defaultValue</c>。</returns>
        /// <remark>如果输入的字符串是<c>"true"</c>或<c>"false"</c>
        /// （不区分大小写，可以在前端或后端包含空白字符），
        /// 则分别得到<c>true</c>或<c>false</c>。其它情况均会造成转换失败。</remark>
        public static bool ToBoolean(this string s, bool defaultValue)
        {
            bool value;
            return bool.TryParse(s, out value) ? value : defaultValue;
        }

        /// <summary>
        /// 将字符串转换为<c>bool</c>类型。
        /// </summary>
        /// <param name="s">源字符串</param>
        /// <returns>转换得来的<c>bool</c>值。若转换失败返回<c>false</c>。</returns>
        /// <remark>如果输入的字符串是<c>"true"</c>或<c>"false"</c>
        /// （不区分大小写，可以在前端或后端包含空白字符），
        /// 则分别得到<c>true</c>或<c>false</c>。其它情况均会造成转换失败。</remark>
        public static bool ToBoolean(this string s)
        {
            bool value;
            return bool.TryParse(s, out value) && value;
        }

        /// <summary>
        /// 将字符串转换为<c>Nullable&lt;bool&gt;</c>类型。
        /// </summary>
        /// <param name="s">源字符串</param>
        /// <returns>转换得来的<c>bool?</c>值。若转换失败返回<c>null</c>。</returns>
        /// <remark>如果输入的字符串是<c>"true"</c>或<c>"false"</c>
        /// （不区分大小写，可以在前端或后端包含空白字符），
        /// 则分别得到<c>true</c>或<c>false</c>。其它情况均会造成转换失败。</remark>
        public static bool? ToNullableBoolean(this string s)
        {
            bool value;
            return bool.TryParse(s, out value) ? (bool?) value : null;
        }
        #endregion

        #region ToDateTime
        /// <summary>
        /// 将字符串转换为<c>Nullable&lt;DateTime&gt;</c>类型。
        /// </summary>
        /// <param name="s">源字符串</param>
        /// <param name="style">源字符串描述日期/时间的样式</param>
        /// <returns>转换得到的<c>DateTime?</c>对象，若转换失败返回<c>null</c>。</returns>
        public static DateTime? ToDateTime(this string s, DateTimeStyles style)
        {
            DateTime value;
            return DateTime.TryParse(s, DateTimeFormatInfo.CurrentInfo, style, out value)
                ? (DateTime?) value
                : null;
        }

        /// <summary>
        /// 将字符串转换为<c>DateTime</c>类型。
        /// </summary>
        /// <param name="s">源字符串</param>
        /// <param name="style">源字符串描述日期/时间的样式</param>
        /// <param name="defaultValue">转换失败时返回的默认值</param>
        /// <returns>转换得到的<c>DateTime</c>值，若转换失败返回<c>defaultValue</c>。</returns>
        public static DateTime ToDateTime(this string s, DateTimeStyles style, DateTime defaultValue)
        {
            DateTime value;
            return DateTime.TryParse(s, DateTimeFormatInfo.CurrentInfo, style, out value)
                ? value
                : defaultValue;
        }

        /// <summary>
        /// 按默认的样式将字符串转换为<c>Nullable&lt;DateTime&gt;</c>类型。
        /// </summary>
        /// <param name="s">源字符串</param>
        /// <returns>转换得到的<c>DateTime?</c>对象，若转换失败返回<c>null</c>。</returns>
        public static DateTime? ToDateTime(this string s)
        {
            DateTime value;
            return DateTime.TryParse(s, out value) ? (DateTime?) value : null;
        }

        /// <summary>
        /// 将字符串转换为<c>DateTime</c>类型。
        /// </summary>
        /// <param name="s">源字符串</param>
        /// <param name="defaultValue">转换失败时返回的默认值</param>
        /// <returns>转换得到的<c>DateTime</c>值，若转换失败返回<c>defaultValue</c>。</returns>
        public static DateTime ToDateTime(this string s, DateTime defaultValue)
        {
            DateTime value;
            return DateTime.TryParse(s, out value) ? value : defaultValue;
        }

        /// <summary>
        /// 按指定的格式将字符串转换为<c>Nullable&lt;DateTime&gt;</c>类型。
        /// </summary>
        /// <param name="s">源字符串</param>
        /// <param name="isEscapeSlash">是否先对斜线(/)字符进行转义</param>
        /// <param name="formats">格式。<see cref="DateTime.TryParseExact(String, String[], IFormatProvider, DateTimeStyles, out DateTime)"/></param>
        /// <returns>转换得到的<c>DateTime?</c>对象，若转换失败返回<c>null</c>。</returns>
        public static DateTime? ToDateTime(this string s, bool isEscapeSlash,
            params string[] formats)
        {
            if (isEscapeSlash)
            {
                var newFormats = new string[formats.Length];
                for (int i = 0; i < newFormats.Length; i++)
                {
                    newFormats[i] = formats[i].Replace("/", @"\/");
                }
                return s.ToDateTime(newFormats);
            }
            else
            {
                return s.ToDateTime(formats);
            }
        }

        /// <summary>
        /// 按指定的格式将字符串转换为<c>Nullable&lt;DateTime&gt;</c>类型。
        /// </summary>
        /// <param name="s">源字符串</param>
        /// <param name="formats">格式。<see cref="DateTime.TryParseExact(String, String[], IFormatProvider, DateTimeStyles, out DateTime)"/></param>
        /// <returns>转换得到的<c>DateTime?</c>对象，若转换失败返回<c>null</c>。</returns>
        public static DateTime? ToDateTime(this string s, params string[] formats)
        {
            DateTime value;
            bool r = formats != null && formats.Length == 1
                ? DateTime.TryParseExact(s, formats[0],
                    DateTimeFormatInfo.CurrentInfo, DateTimeStyles.None, out value)
                : DateTime.TryParseExact(s, formats,
                    DateTimeFormatInfo.CurrentInfo, DateTimeStyles.None, out value);
            return r ? (DateTime?) value : null;
        }

        /// <summary>
        /// 将字符串转换为<c>DateTime</c>类型。
        /// </summary>
        /// <param name="s">源字符串</param>
        /// <param name="defaultValue"></param>
        /// <param name="formats">格式。<see cref="DateTime.TryParseExact(String, String[], IFormatProvider, DateTimeStyles, out DateTime)"/></param>
        /// <returns>转换得到的<c>DateTime</c>值，若转换失败返回<c>defaultValue</c>。</returns>
        public static DateTime ToDateTime(this string s, DateTime defaultValue, params string[] formats)
        {
            DateTime value;
            bool r = formats != null && formats.Length == 1
                ? DateTime.TryParseExact(s, formats[0],
                DateTimeFormatInfo.CurrentInfo, DateTimeStyles.None, out value)
                : DateTime.TryParseExact(s, formats,
                DateTimeFormatInfo.CurrentInfo, DateTimeStyles.None, out value);
            return r ? value : defaultValue;
        }
        #endregion
    }
}
