using System;

namespace NFinal.Advanced
{
    /// <summary>
    /// 包含一系列对<c>object</c>进行扩展的方法的类。
    /// </summary>
    public static class ObjectUtil
    {
        /// <summary>
        /// 将object对象当作指定数据类型进行处理。
        /// </summary>
        /// <typeparam name="T">目标类型</typeparam>
        /// <param name="object">源数据</param>
        /// <param name="defaultValue">缺省值</param>
        /// <returns>返回转换引用类型之后的值，如果不能转换为指定引用类型，
        /// 返回<c>defaultValue</c></returns>
        /// <remarks>与as关键字的区别在于，<c>As</c>方法可以处理结构。</remarks>
        public static T As<T>(this object @object, T defaultValue)
        {
            try { return (T) @object; }
            catch { return defaultValue; }
        }

        /// <summary>
        /// 将object对象当作指定数据类型进行处理。
        /// </summary>
        /// <typeparam name="T">目标类型</typeparam>
        /// <param name="object">源数据</param>
        /// <returns>返回转换引用类型之后的值，如果不能转换为指定引用类型，
        /// 返回<c>default(T)</c></returns>
        /// <remarks>与as关键字的区别在于，<c>As</c>方法可以处理结构。</remarks>
        public static T As<T>(this object @object)
        {
            return @object.As<T>(default(T));
        }

        /// <summary>
        /// 将object对象转换为指定数据类型。
        /// </summary>
        /// <typeparam name="T">目标类型</typeparam>
        /// <param name="object">源数据</param>
        /// <param name="defaultValue">缺省值</param>
        /// <returns>通过<c>System.Convert</c>将源数据转换为指定类型的数据，
        /// 如果转换不成功，返回<c>defaultValue</c></returns>
        public static T ConvertTo<T>(object @object, T defaultValue)
        {
            try { return (T) Convert.ChangeType(@object, typeof(T)); }
            catch { return defaultValue; }
        }

        /// <summary>
        /// 将object对象转换为指定数据类型。
        /// </summary>
        /// <typeparam name="T">目标类型</typeparam>
        /// <param name="object">源数据</param>
        /// <returns>通过<c>System.Convert</c>将源数据转换为指定类型的数据，
        /// 如果转换不成功，返回<c>default(T)</c></returns>
        public static T ConvertTo<T>(object @object)
        {
            return ConvertTo<T>(@object, default(T));
        }

        /// <summary>
        /// 将object对象转换为Int32类型的数据。
        /// </summary>
        /// <param name="object">源数据</param>
        /// <param name="defaultValue">缺省值</param>
        /// <returns>转换结果数据，如果转换不成功，返回<c>defaultValue</c></returns>
        /// <remarks>与<c>ConverTo</c>相比，明确指定类型的转换效率更高。</remarks>
        public static int ToInt32(object @object, int defaultValue = default(int))
        {
            try { return Convert.ToInt32(@object); }
            catch { return defaultValue; }
        }

        /// <summary>
        /// 将object对象转换为Int64类型的数据。
        /// </summary>
        /// <param name="object">源数据</param>
        /// <param name="defaultValue">缺省值</param>
        /// <returns>转换结果数据，如果转换不成功，返回<c>defaultValue</c></returns>
        /// <remarks>与<c>ConverTo</c>相比，明确指定类型的转换效率更高。</remarks>
        public static long ToInt64(object @object, long defaultValue = default(long))
        {
            try { return Convert.ToInt64(@object); }
            catch { return defaultValue; }
        }

        /// <summary>
        /// 将object对象转换为Int16类型的数据。
        /// </summary>
        /// <param name="object">源数据</param>
        /// <param name="defaultValue">缺省值</param>
        /// <returns>转换结果数据，如果转换不成功，返回<c>defaultValue</c></returns>
        /// <remarks>与<c>ConverTo</c>相比，明确指定类型的转换效率更高。</remarks>
        public static short ToInt16(object @object, short defaultValue = default(short))
        {
            try { return Convert.ToInt16(@object); }
            catch { return defaultValue; }
        }

        /// <summary>
        /// 将object对象转换为UInt32类型的数据。
        /// </summary>
        /// <param name="object">源数据</param>
        /// <param name="defaultValue">缺省值</param>
        /// <returns>转换结果数据，如果转换不成功，返回<c>defaultValue</c></returns>
        /// <remarks>与<c>ConverTo</c>相比，明确指定类型的转换效率更高。</remarks>
        public static uint ToUInt32(object @object, uint defaultValue = default(uint))
        {
            try { return Convert.ToUInt32(@object); }
            catch { return defaultValue; }
        }

        /// <summary>
        /// 将object对象转换为UInt64类型的数据。
        /// </summary>
        /// <param name="object">源数据</param>
        /// <param name="defaultValue">缺省值</param>
        /// <returns>转换结果数据，如果转换不成功，返回<c>defaultValue</c></returns>
        /// <remarks>与<c>ConverTo</c>相比，明确指定类型的转换效率更高。</remarks>
        public static ulong ToUInt64(object @object, ulong defaultValue = default(ulong))
        {
            try { return Convert.ToUInt64(@object); }
            catch { return defaultValue; }
        }

        /// <summary>
        /// 将object对象转换为UInt16类型的数据。
        /// </summary>
        /// <param name="object">源数据</param>
        /// <param name="defaultValue">缺省值</param>
        /// <returns>转换结果数据，如果转换不成功，返回<c>defaultValue</c></returns>
        /// <remarks>与<c>ConverTo</c>相比，明确指定类型的转换效率更高。</remarks>
        public static ushort ToUInt16(object @object, ushort defaultValue = default(ushort))
        {
            try { return Convert.ToUInt16(@object); }
            catch { return defaultValue; }
        }

        /// <summary>
        /// 将object对象转换为Byte类型的数据。
        /// </summary>
        /// <param name="object">源数据</param>
        /// <param name="defaultValue">缺省值</param>
        /// <returns>转换结果数据，如果转换不成功，返回<c>defaultValue</c></returns>
        /// <remarks>与<c>ConverTo</c>相比，明确指定类型的转换效率更高。</remarks>
        public static byte ToByte(object @object, byte defaultValue = default(byte))
        {
            try { return Convert.ToByte(@object); }
            catch { return defaultValue; }
        }

        /// <summary>
        /// 将object对象转换为SByte类型的数据。
        /// </summary>
        /// <param name="object">源数据</param>
        /// <param name="defaultValue">缺省值</param>
        /// <returns>转换结果数据，如果转换不成功，返回<c>defaultValue</c></returns>
        /// <remarks>与<c>ConverTo</c>相比，明确指定类型的转换效率更高。</remarks>
        public static sbyte ToSByte(object @object, sbyte defaultValue = default(sbyte))
        {
            try { return Convert.ToSByte(@object); }
            catch { return defaultValue; }
        }

        /// <summary>
        /// 将object对象转换为Decimal类型的数据。
        /// </summary>
        /// <param name="object">源数据</param>
        /// <param name="defaultValue">缺省值</param>
        /// <returns>转换结果数据，如果转换不成功，返回<c>defaultValue</c></returns>
        /// <remarks>与<c>ConverTo</c>相比，明确指定类型的转换效率更高。</remarks>
        public static decimal ToDecimal(object @object, decimal defaultValue = default(decimal))
        {
            try { return Convert.ToDecimal(@object); }
            catch { return defaultValue; }
        }

        /// <summary>
        /// 将object对象转换为Float类型的数据。
        /// </summary>
        /// <param name="object">源数据</param>
        /// <param name="defaultValue">缺省值</param>
        /// <returns>转换结果数据，如果转换不成功，返回<c>defaultValue</c></returns>
        /// <remarks>与<c>ConverTo</c>相比，明确指定类型的转换效率更高。</remarks>
        public static float ToFloat(object @object, float defaultValue = default(float))
        {
            try { return Convert.ToSingle(@object); }
            catch { return defaultValue; }
        }

        /// <summary>
        /// 将object对象转换为Double类型的数据。
        /// </summary>
        /// <param name="object">源数据</param>
        /// <param name="defaultValue">缺省值</param>
        /// <returns>转换结果数据，如果转换不成功，返回<c>defaultValue</c></returns>
        /// <remarks>与<c>ConverTo</c>相比，明确指定类型的转换效率更高。</remarks>
        public static double ToDouble(object @object, double defaultValue = default(double))
        {
            try { return Convert.ToDouble(@object); }
            catch { return defaultValue; }
        }

        /// <summary>
        /// 将object对象转换为Boolean类型的数据。
        /// </summary>
        /// <param name="object">源数据</param>
        /// <param name="defaultValue">缺省值</param>
        /// <returns>转换结果数据，如果转换不成功，返回<c>defaultValue</c></returns>
        /// <remarks>与<c>ConverTo</c>相比，明确指定类型的转换效率更高。</remarks>
        public static bool ToBoolean(object @object, bool defaultValue = default(bool))
        {
            try { return Convert.ToBoolean(@object); }
            catch { return defaultValue; }
        }
    }
}
