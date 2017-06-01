//======================================================================
//
//        Copyright : Zhengzhou Strawberry Computer Technology Co.,LTD.
//        All rights reserved
//        
//        Application:NFinal MVC framework
//        Filename : TryParseExtension.cs
//        Description :string转其它基本类型的扩展类
//
//        created by Lucas at  2015-5-31
//     
//        WebSite:http://www.nfinal.com
//
//======================================================================
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
        /// <param name="obj">SByte类型</param>
		/// <param name="str">string类型</param>
		/// <param name="value">转换后的类型</param>
        /// <returns>转换是否成功</returns>
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
        /// <param name="obj">Byte类型</param>
		/// <param name="str">string类型</param>
		/// <param name="value">转换后的类型</param>
        /// <returns>转换是否成功</returns>
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
        /// <param name="obj">Int16类型</param>
		/// <param name="str">string类型</param>
		/// <param name="value">转换后的类型</param>
        /// <returns>转换是否成功</returns>
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
        /// <param name="obj">UInt16类型</param>
		/// <param name="str">string类型</param>
		/// <param name="value">转换后的类型</param>
        /// <returns>转换是否成功</returns>
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
        /// <param name="obj">Int32类型</param>
		/// <param name="str">string类型</param>
		/// <param name="value">转换后的类型</param>
        /// <returns>转换是否成功</returns>
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
        /// <param name="obj">UInt32类型</param>
		/// <param name="str">string类型</param>
		/// <param name="value">转换后的类型</param>
        /// <returns>转换是否成功</returns>
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
        /// <param name="obj">Int64类型</param>
		/// <param name="str">string类型</param>
		/// <param name="value">转换后的类型</param>
        /// <returns>转换是否成功</returns>
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
        /// <param name="obj">UInt64类型</param>
		/// <param name="str">string类型</param>
		/// <param name="value">转换后的类型</param>
        /// <returns>转换是否成功</returns>
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
        /// <param name="obj">Boolean类型</param>
		/// <param name="str">string类型</param>
		/// <param name="value">转换后的类型</param>
        /// <returns>转换是否成功</returns>
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
        /// <param name="obj">Char类型</param>
		/// <param name="str">string类型</param>
		/// <param name="value">转换后的类型</param>
        /// <returns>转换是否成功</returns>
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
        /// <param name="obj">Decimal类型</param>
		/// <param name="str">string类型</param>
		/// <param name="value">转换后的类型</param>
        /// <returns>转换是否成功</returns>
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
        /// <param name="obj">Double类型</param>
		/// <param name="str">string类型</param>
		/// <param name="value">转换后的类型</param>
        /// <returns>转换是否成功</returns>
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
        /// <param name="obj">Single类型</param>
		/// <param name="str">string类型</param>
		/// <param name="value">转换后的类型</param>
        /// <returns>转换是否成功</returns>
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
        /// <param name="obj">DateTime类型</param>
		/// <param name="str">string类型</param>
		/// <param name="value">转换后的类型</param>
        /// <returns>转换是否成功</returns>
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
        /// <param name="obj">DateTimeOffset类型</param>
		/// <param name="str">string类型</param>
		/// <param name="value">转换后的类型</param>
        /// <returns>转换是否成功</returns>
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
