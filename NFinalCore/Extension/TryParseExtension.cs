using System;
namespace NFinal
{
	/// <summary>
    /// string转其它基本类型的扩展类
    /// </summary>
    public static class TryParseExtension
    {
		/// <summary>
        /// 转换为SByte类型
        /// </summary>
        /// <param name="obj">字符串</param>
        /// <returns>SByte类型</returns>
		public static bool TryParse(this SByte obj,string str,out SByte? value)
        {
			bool result;
			if(string.IsNullOrEmpty(str))
			{
				value=null;
				result=false;
			}
			else
			{
				SByte temp;
				result=SByte.TryParse(str,out temp);
				value=temp;
			}
            return result;
        }
		/// <summary>
        /// 转换为Byte类型
        /// </summary>
        /// <param name="obj">字符串</param>
        /// <returns>Byte类型</returns>
		public static bool TryParse(this Byte obj,string str,out Byte? value)
        {
			bool result;
			if(string.IsNullOrEmpty(str))
			{
				value=null;
				result=false;
			}
			else
			{
				Byte temp;
				result=Byte.TryParse(str,out temp);
				value=temp;
			}
            return result;
        }
		/// <summary>
        /// 转换为Int16类型
        /// </summary>
        /// <param name="obj">字符串</param>
        /// <returns>Int16类型</returns>
		public static bool TryParse(this Int16 obj,string str,out Int16? value)
        {
			bool result;
			if(string.IsNullOrEmpty(str))
			{
				value=null;
				result=false;
			}
			else
			{
				Int16 temp;
				result=Int16.TryParse(str,out temp);
				value=temp;
			}
            return result;
        }
		/// <summary>
        /// 转换为UInt16类型
        /// </summary>
        /// <param name="obj">字符串</param>
        /// <returns>UInt16类型</returns>
		public static bool TryParse(this UInt16 obj,string str,out UInt16? value)
        {
			bool result;
			if(string.IsNullOrEmpty(str))
			{
				value=null;
				result=false;
			}
			else
			{
				UInt16 temp;
				result=UInt16.TryParse(str,out temp);
				value=temp;
			}
            return result;
        }
		/// <summary>
        /// 转换为Int32类型
        /// </summary>
        /// <param name="obj">字符串</param>
        /// <returns>Int32类型</returns>
		public static bool TryParse(this Int32 obj,string str,out Int32? value)
        {
			bool result;
			if(string.IsNullOrEmpty(str))
			{
				value=null;
				result=false;
			}
			else
			{
				Int32 temp;
				result=Int32.TryParse(str,out temp);
				value=temp;
			}
            return result;
        }
		/// <summary>
        /// 转换为UInt32类型
        /// </summary>
        /// <param name="obj">字符串</param>
        /// <returns>UInt32类型</returns>
		public static bool TryParse(this UInt32 obj,string str,out UInt32? value)
        {
			bool result;
			if(string.IsNullOrEmpty(str))
			{
				value=null;
				result=false;
			}
			else
			{
				UInt32 temp;
				result=UInt32.TryParse(str,out temp);
				value=temp;
			}
            return result;
        }
		/// <summary>
        /// 转换为Int64类型
        /// </summary>
        /// <param name="obj">字符串</param>
        /// <returns>Int64类型</returns>
		public static bool TryParse(this Int64 obj,string str,out Int64? value)
        {
			bool result;
			if(string.IsNullOrEmpty(str))
			{
				value=null;
				result=false;
			}
			else
			{
				Int64 temp;
				result=Int64.TryParse(str,out temp);
				value=temp;
			}
            return result;
        }
		/// <summary>
        /// 转换为UInt64类型
        /// </summary>
        /// <param name="obj">字符串</param>
        /// <returns>UInt64类型</returns>
		public static bool TryParse(this UInt64 obj,string str,out UInt64? value)
        {
			bool result;
			if(string.IsNullOrEmpty(str))
			{
				value=null;
				result=false;
			}
			else
			{
				UInt64 temp;
				result=UInt64.TryParse(str,out temp);
				value=temp;
			}
            return result;
        }
		/// <summary>
        /// 转换为Boolean类型
        /// </summary>
        /// <param name="obj">字符串</param>
        /// <returns>Boolean类型</returns>
		public static bool TryParse(this Boolean obj,string str,out Boolean? value)
        {
			bool result;
			if(string.IsNullOrEmpty(str))
			{
				value=null;
				result=false;
			}
			else
			{
				Boolean temp;
				result=Boolean.TryParse(str,out temp);
				value=temp;
			}
            return result;
        }
		/// <summary>
        /// 转换为Char类型
        /// </summary>
        /// <param name="obj">字符串</param>
        /// <returns>Char类型</returns>
		public static bool TryParse(this Char obj,string str,out Char? value)
        {
			bool result;
			if(string.IsNullOrEmpty(str))
			{
				value=null;
				result=false;
			}
			else
			{
				Char temp;
				result=Char.TryParse(str,out temp);
				value=temp;
			}
            return result;
        }
		/// <summary>
        /// 转换为Decimal类型
        /// </summary>
        /// <param name="obj">字符串</param>
        /// <returns>Decimal类型</returns>
		public static bool TryParse(this Decimal obj,string str,out Decimal? value)
        {
			bool result;
			if(string.IsNullOrEmpty(str))
			{
				value=null;
				result=false;
			}
			else
			{
				Decimal temp;
				result=Decimal.TryParse(str,out temp);
				value=temp;
			}
            return result;
        }
		/// <summary>
        /// 转换为Double类型
        /// </summary>
        /// <param name="obj">字符串</param>
        /// <returns>Double类型</returns>
		public static bool TryParse(this Double obj,string str,out Double? value)
        {
			bool result;
			if(string.IsNullOrEmpty(str))
			{
				value=null;
				result=false;
			}
			else
			{
				Double temp;
				result=Double.TryParse(str,out temp);
				value=temp;
			}
            return result;
        }
		/// <summary>
        /// 转换为Single类型
        /// </summary>
        /// <param name="obj">字符串</param>
        /// <returns>Single类型</returns>
		public static bool TryParse(this Single obj,string str,out Single? value)
        {
			bool result;
			if(string.IsNullOrEmpty(str))
			{
				value=null;
				result=false;
			}
			else
			{
				Single temp;
				result=Single.TryParse(str,out temp);
				value=temp;
			}
            return result;
        }
		/// <summary>
        /// 转换为DateTime类型
        /// </summary>
        /// <param name="obj">字符串</param>
        /// <returns>DateTime类型</returns>
		public static bool TryParse(this DateTime obj,string str,out DateTime? value)
        {
			bool result;
			if(string.IsNullOrEmpty(str))
			{
				value=null;
				result=false;
			}
			else
			{
				DateTime temp;
				result=DateTime.TryParse(str,out temp);
				value=temp;
			}
            return result;
        }
		/// <summary>
        /// 转换为DateTimeOffset类型
        /// </summary>
        /// <param name="obj">字符串</param>
        /// <returns>DateTimeOffset类型</returns>
		public static bool TryParse(this DateTimeOffset obj,string str,out DateTimeOffset? value)
        {
			bool result;
			if(string.IsNullOrEmpty(str))
			{
				value=null;
				result=false;
			}
			else
			{
				DateTimeOffset temp;
				result=DateTimeOffset.TryParse(str,out temp);
				value=temp;
			}
            return result;
        }
    }
}
