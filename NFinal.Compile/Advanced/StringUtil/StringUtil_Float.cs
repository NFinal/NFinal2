// 通过模板 StringUtil_Partial.template 自动生成的代码，请勿手工修改！
// 需要 ../build/StringUtil.targets 通过 MsBuild 来生成。依赖 MsBuildTasks 库。

using System.Globalization;

// ReSharper disable CheckNamespace
namespace NFinal.Advanced
{
    partial class StringUtil
    {
        /// <summary>
        /// 将字符串转换为<c>Nullable&lt;float&gt;</c>类型的值
        /// </summary>
        /// <param name="s">源字符串</param>
        /// <param name="style">源字符串描述数字的样式</param>
        /// <returns><c>float?</c>值。若转换失败返回null。</returns>
        public static float? ToFloat(this string s, NumberStyles style)
        {
            float value;
            return float.TryParse(s, style, NumberFormatInfo.CurrentInfo, out value)
                ? (float?) value
                : null;
        }

        /// <summary>
        /// 将字符串转换为<c>float</c>类型的值
        /// </summary>
        /// <param name="s">源字符串</param>
        /// <param name="style">源字符串描述数字的样式</param>
        /// <param name="defaultValue">用转换失败时返回的默认值</param>
        /// <returns>转换得来的<c>float</c>值。若转换失败返回<c>defaultValue</c></returns>
        public static float ToFloat(this string s, NumberStyles style, float defaultValue)
        {
            float value;
            return float.TryParse(s, style, NumberFormatInfo.CurrentInfo, out value)
                ? value
                : defaultValue;
        }

        /// <summary>
        /// 将字符串转换为<c>Nullable&lt;float&gt;</c>类型的值。
        /// 源字符串会被当作采用十进制描述数值。
        /// </summary>
        /// <param name="s">源字符串</param>
        /// <returns><c>float?</c>值。若转换失败返回null。</returns>
        public static float? ToFloat(this string s)
        {
            float value;
            return float.TryParse(s, out value) ? (float?) value : null;
        }

        /// <summary>
        /// 将字符串转换为<c>float</c>类型的值
        /// 源字符串会被当作采用十进制描述数值。
        /// </summary>
        /// <param name="s">源字符串</param>
        /// <param name="defaultValue">用转换失败时返回的默认值</param>
        /// <returns>转换得来的<c>float</c>值。若转换失败返回<c>defaultValue</c></returns>
        public static float ToFloat(this string s, float defaultValue)
        {
            float value;
            return float.TryParse(s, out value) ? value : defaultValue;
        }
    }
}
