// 通过模板 StringUtil_Partial.template 自动生成的代码，请勿手工修改！
// 需要 ../build/StringUtil.targets 通过 MsBuild 来生成。依赖 MsBuildTasks 库。

using System.Globalization;

// ReSharper disable CheckNamespace
namespace NFinal.Advanced
{
    partial class StringUtil
    {
        /// <summary>
        /// 将字符串转换为<c>Nullable&lt;short&gt;</c>类型的值
        /// </summary>
        /// <param name="s">源字符串</param>
        /// <param name="style">源字符串描述数字的样式</param>
        /// <returns><c>short?</c>值。若转换失败返回null。</returns>
        public static short? ToInt16(this string s, NumberStyles style)
        {
            short value;
            return short.TryParse(s, style, NumberFormatInfo.CurrentInfo, out value)
                ? (short?) value
                : null;
        }

        /// <summary>
        /// 将字符串转换为<c>short</c>类型的值
        /// </summary>
        /// <param name="s">源字符串</param>
        /// <param name="style">源字符串描述数字的样式</param>
        /// <param name="defaultValue">用转换失败时返回的默认值</param>
        /// <returns>转换得来的<c>short</c>值。若转换失败返回<c>defaultValue</c></returns>
        public static short ToInt16(this string s, NumberStyles style, short defaultValue)
        {
            short value;
            return short.TryParse(s, style, NumberFormatInfo.CurrentInfo, out value)
                ? value
                : defaultValue;
        }

        /// <summary>
        /// 将字符串转换为<c>Nullable&lt;short&gt;</c>类型的值。
        /// 源字符串会被当作采用十进制描述数值。
        /// </summary>
        /// <param name="s">源字符串</param>
        /// <returns><c>short?</c>值。若转换失败返回null。</returns>
        public static short? ToInt16(this string s)
        {
            short value;
            return short.TryParse(s, out value) ? (short?) value : null;
        }

        /// <summary>
        /// 将字符串转换为<c>short</c>类型的值
        /// 源字符串会被当作采用十进制描述数值。
        /// </summary>
        /// <param name="s">源字符串</param>
        /// <param name="defaultValue">用转换失败时返回的默认值</param>
        /// <returns>转换得来的<c>short</c>值。若转换失败返回<c>defaultValue</c></returns>
        public static short ToInt16(this string s, short defaultValue)
        {
            short value;
            return short.TryParse(s, out value) ? value : defaultValue;
        }
    }
}
