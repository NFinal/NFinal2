using System;
namespace System
{
	/// <summary>
    /// string转其它基本类型的扩展类
    /// </summary>
    public static class ConvertExtension
    {
		/// <summary>
        /// 转换为SByte类型
        /// </summary>
        /// <param name="obj">字符串</param>
        /// <returns>SByte类型</returns>
		public static SByte AsSByte(this string obj)
        {
            SByte result;
            SByte.TryParse(obj, out result);
            return result;
        }
		/// <summary>
        /// 转换为Byte类型
        /// </summary>
        /// <param name="obj">字符串</param>
        /// <returns>Byte类型</returns>
		public static Byte AsByte(this string obj)
        {
            Byte result;
            Byte.TryParse(obj, out result);
            return result;
        }
		/// <summary>
        /// 转换为Int16类型
        /// </summary>
        /// <param name="obj">字符串</param>
        /// <returns>Int16类型</returns>
		public static Int16 AsInt16(this string obj)
        {
            Int16 result;
            Int16.TryParse(obj, out result);
            return result;
        }
		/// <summary>
        /// 转换为UInt16类型
        /// </summary>
        /// <param name="obj">字符串</param>
        /// <returns>UInt16类型</returns>
		public static UInt16 AsUInt16(this string obj)
        {
            UInt16 result;
            UInt16.TryParse(obj, out result);
            return result;
        }
		/// <summary>
        /// 转换为Int32类型
        /// </summary>
        /// <param name="obj">字符串</param>
        /// <returns>Int32类型</returns>
		public static Int32 AsInt32(this string obj)
        {
            Int32 result;
            Int32.TryParse(obj, out result);
            return result;
        }
		/// <summary>
        /// 转换为UInt32类型
        /// </summary>
        /// <param name="obj">字符串</param>
        /// <returns>UInt32类型</returns>
		public static UInt32 AsUInt32(this string obj)
        {
            UInt32 result;
            UInt32.TryParse(obj, out result);
            return result;
        }
		/// <summary>
        /// 转换为Int64类型
        /// </summary>
        /// <param name="obj">字符串</param>
        /// <returns>Int64类型</returns>
		public static Int64 AsInt64(this string obj)
        {
            Int64 result;
            Int64.TryParse(obj, out result);
            return result;
        }
		/// <summary>
        /// 转换为UInt64类型
        /// </summary>
        /// <param name="obj">字符串</param>
        /// <returns>UInt64类型</returns>
		public static UInt64 AsUInt64(this string obj)
        {
            UInt64 result;
            UInt64.TryParse(obj, out result);
            return result;
        }
		/// <summary>
        /// 转换为Boolean类型
        /// </summary>
        /// <param name="obj">字符串</param>
        /// <returns>Boolean类型</returns>
		public static Boolean AsBoolean(this string obj)
        {
            Boolean result;
            Boolean.TryParse(obj, out result);
            return result;
        }
		/// <summary>
        /// 转换为Char类型
        /// </summary>
        /// <param name="obj">字符串</param>
        /// <returns>Char类型</returns>
		public static Char AsChar(this string obj)
        {
            Char result;
            Char.TryParse(obj, out result);
            return result;
        }
		/// <summary>
        /// 转换为Decimal类型
        /// </summary>
        /// <param name="obj">字符串</param>
        /// <returns>Decimal类型</returns>
		public static Decimal AsDecimal(this string obj)
        {
            Decimal result;
            Decimal.TryParse(obj, out result);
            return result;
        }
		/// <summary>
        /// 转换为Double类型
        /// </summary>
        /// <param name="obj">字符串</param>
        /// <returns>Double类型</returns>
		public static Double AsDouble(this string obj)
        {
            Double result;
            Double.TryParse(obj, out result);
            return result;
        }
		/// <summary>
        /// 转换为Single类型
        /// </summary>
        /// <param name="obj">字符串</param>
        /// <returns>Single类型</returns>
		public static Single AsSingle(this string obj)
        {
            Single result;
            Single.TryParse(obj, out result);
            return result;
        }
		/// <summary>
        /// 转换为DateTime类型
        /// </summary>
        /// <param name="obj">字符串</param>
        /// <returns>DateTime类型</returns>
		public static DateTime AsDateTime(this string obj)
        {
            DateTime result;
            DateTime.TryParse(obj, out result);
            return result;
        }
		/// <summary>
        /// 转换为DateTimeOffset类型
        /// </summary>
        /// <param name="obj">字符串</param>
        /// <returns>DateTimeOffset类型</returns>
		public static DateTimeOffset AsDateTimeOffset(this string obj)
        {
            DateTimeOffset result;
            DateTimeOffset.TryParse(obj, out result);
            return result;
        }
    }
}
