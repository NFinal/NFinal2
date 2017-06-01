//======================================================================
//
//        Copyright : Zhengzhou Strawberry Computer Technology Co.,LTD.
//        All rights reserved
//        
//        Application:NFinal MVC framework
//        Filename : StringContainer.cs
//        Description :字符串容器类，用于基本类型的自动转换。
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
    /// 字符串容器类，用于基本类型的自动转换。
    /// </summary>
    public struct StringContainer
    {
		/// <summary>
		/// 空容器类，用于判断初始化等。
		/// </summary>
        public static readonly StringContainer Empty=new StringContainer();
		/// <summary>
		/// 字符串
		/// </summary>
        public string value;
        internal StringContainer(string value)
        {
            this.value = value;
        }
		/// <summary>
        /// 比较判断
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
		public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }
        /// <summary>
        /// GetHashCode
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        /// <summary>
        /// 相等判断
        /// </summary>
        /// <param name="container"></param>
        /// <param name="str"></param>
        /// <returns></returns>
		public static bool operator ==(StringContainer container, string str)
        {
            return container.value == str;
        }
        /// <summary>
        /// 不等判断
        /// </summary>
        /// <param name="container"></param>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool operator !=(StringContainer container, string str)
        {
            return container.value != str;
        }
		/// <summary>
        /// 把SByte类型转为字符串容器类，即字符串类
        /// </summary>
        /// <param name="obj">SByte类型</param>
        public static implicit operator StringContainer(SByte obj)
		{
            return new StringContainer(obj.ToString());
		}
		/// <summary>
        /// 把Byte类型转为字符串容器类，即字符串类
        /// </summary>
        /// <param name="obj">Byte类型</param>
        public static implicit operator StringContainer(Byte obj)
		{
            return new StringContainer(obj.ToString());
		}
		/// <summary>
        /// 把Int16类型转为字符串容器类，即字符串类
        /// </summary>
        /// <param name="obj">Int16类型</param>
        public static implicit operator StringContainer(Int16 obj)
		{
            return new StringContainer(obj.ToString());
		}
		/// <summary>
        /// 把UInt16类型转为字符串容器类，即字符串类
        /// </summary>
        /// <param name="obj">UInt16类型</param>
        public static implicit operator StringContainer(UInt16 obj)
		{
            return new StringContainer(obj.ToString());
		}
		/// <summary>
        /// 把Int32类型转为字符串容器类，即字符串类
        /// </summary>
        /// <param name="obj">Int32类型</param>
        public static implicit operator StringContainer(Int32 obj)
		{
            return new StringContainer(obj.ToString());
		}
		/// <summary>
        /// 把UInt32类型转为字符串容器类，即字符串类
        /// </summary>
        /// <param name="obj">UInt32类型</param>
        public static implicit operator StringContainer(UInt32 obj)
		{
            return new StringContainer(obj.ToString());
		}
		/// <summary>
        /// 把Int64类型转为字符串容器类，即字符串类
        /// </summary>
        /// <param name="obj">Int64类型</param>
        public static implicit operator StringContainer(Int64 obj)
		{
            return new StringContainer(obj.ToString());
		}
		/// <summary>
        /// 把UInt64类型转为字符串容器类，即字符串类
        /// </summary>
        /// <param name="obj">UInt64类型</param>
        public static implicit operator StringContainer(UInt64 obj)
		{
            return new StringContainer(obj.ToString());
		}
		/// <summary>
        /// 把Boolean类型转为字符串容器类，即字符串类
        /// </summary>
        /// <param name="obj">Boolean类型</param>
        public static implicit operator StringContainer(Boolean obj)
		{
            return new StringContainer(obj.ToString());
		}
		/// <summary>
        /// 把Char类型转为字符串容器类，即字符串类
        /// </summary>
        /// <param name="obj">Char类型</param>
        public static implicit operator StringContainer(Char obj)
		{
            return new StringContainer(obj.ToString());
		}
		/// <summary>
        /// 把Decimal类型转为字符串容器类，即字符串类
        /// </summary>
        /// <param name="obj">Decimal类型</param>
        public static implicit operator StringContainer(Decimal obj)
		{
            return new StringContainer(obj.ToString());
		}
		/// <summary>
        /// 把Double类型转为字符串容器类，即字符串类
        /// </summary>
        /// <param name="obj">Double类型</param>
        public static implicit operator StringContainer(Double obj)
		{
            return new StringContainer(obj.ToString());
		}
		/// <summary>
        /// 把Single类型转为字符串容器类，即字符串类
        /// </summary>
        /// <param name="obj">Single类型</param>
        public static implicit operator StringContainer(Single obj)
		{
            return new StringContainer(obj.ToString());
		}
		/// <summary>
        /// 把DateTime类型转为字符串容器类，即字符串类
        /// </summary>
        /// <param name="obj">DateTime类型</param>
        public static implicit operator StringContainer(DateTime obj)
		{
            return new StringContainer(obj.ToString());
		}
		/// <summary>
        /// 把DateTimeOffset类型转为字符串容器类，即字符串类
        /// </summary>
        /// <param name="obj">DateTimeOffset类型</param>
        public static implicit operator StringContainer(DateTimeOffset obj)
		{
            return new StringContainer(obj.ToString());
		}
		/// <summary>
        /// 把SByte类型转为字符串容器类，即字符串类
        /// </summary>
        /// <param name="obj">可以为null的SByte类型</param>
        public static implicit operator StringContainer(SByte? obj)
		{
            if(obj!=null)
            {
                return new StringContainer(obj.ToString());
            }
			return StringContainer.Empty;
		}
		/// <summary>
        /// 把Byte类型转为字符串容器类，即字符串类
        /// </summary>
        /// <param name="obj">可以为null的Byte类型</param>
        public static implicit operator StringContainer(Byte? obj)
		{
            if(obj!=null)
            {
                return new StringContainer(obj.ToString());
            }
			return StringContainer.Empty;
		}
		/// <summary>
        /// 把Int16类型转为字符串容器类，即字符串类
        /// </summary>
        /// <param name="obj">可以为null的Int16类型</param>
        public static implicit operator StringContainer(Int16? obj)
		{
            if(obj!=null)
            {
                return new StringContainer(obj.ToString());
            }
			return StringContainer.Empty;
		}
		/// <summary>
        /// 把UInt16类型转为字符串容器类，即字符串类
        /// </summary>
        /// <param name="obj">可以为null的UInt16类型</param>
        public static implicit operator StringContainer(UInt16? obj)
		{
            if(obj!=null)
            {
                return new StringContainer(obj.ToString());
            }
			return StringContainer.Empty;
		}
		/// <summary>
        /// 把Int32类型转为字符串容器类，即字符串类
        /// </summary>
        /// <param name="obj">可以为null的Int32类型</param>
        public static implicit operator StringContainer(Int32? obj)
		{
            if(obj!=null)
            {
                return new StringContainer(obj.ToString());
            }
			return StringContainer.Empty;
		}
		/// <summary>
        /// 把UInt32类型转为字符串容器类，即字符串类
        /// </summary>
        /// <param name="obj">可以为null的UInt32类型</param>
        public static implicit operator StringContainer(UInt32? obj)
		{
            if(obj!=null)
            {
                return new StringContainer(obj.ToString());
            }
			return StringContainer.Empty;
		}
		/// <summary>
        /// 把Int64类型转为字符串容器类，即字符串类
        /// </summary>
        /// <param name="obj">可以为null的Int64类型</param>
        public static implicit operator StringContainer(Int64? obj)
		{
            if(obj!=null)
            {
                return new StringContainer(obj.ToString());
            }
			return StringContainer.Empty;
		}
		/// <summary>
        /// 把UInt64类型转为字符串容器类，即字符串类
        /// </summary>
        /// <param name="obj">可以为null的UInt64类型</param>
        public static implicit operator StringContainer(UInt64? obj)
		{
            if(obj!=null)
            {
                return new StringContainer(obj.ToString());
            }
			return StringContainer.Empty;
		}
		/// <summary>
        /// 把Boolean类型转为字符串容器类，即字符串类
        /// </summary>
        /// <param name="obj">可以为null的Boolean类型</param>
        public static implicit operator StringContainer(Boolean? obj)
		{
            if(obj!=null)
            {
                return new StringContainer(obj.ToString());
            }
			return StringContainer.Empty;
		}
		/// <summary>
        /// 把Char类型转为字符串容器类，即字符串类
        /// </summary>
        /// <param name="obj">可以为null的Char类型</param>
        public static implicit operator StringContainer(Char? obj)
		{
            if(obj!=null)
            {
                return new StringContainer(obj.ToString());
            }
			return StringContainer.Empty;
		}
		/// <summary>
        /// 把Decimal类型转为字符串容器类，即字符串类
        /// </summary>
        /// <param name="obj">可以为null的Decimal类型</param>
        public static implicit operator StringContainer(Decimal? obj)
		{
            if(obj!=null)
            {
                return new StringContainer(obj.ToString());
            }
			return StringContainer.Empty;
		}
		/// <summary>
        /// 把Double类型转为字符串容器类，即字符串类
        /// </summary>
        /// <param name="obj">可以为null的Double类型</param>
        public static implicit operator StringContainer(Double? obj)
		{
            if(obj!=null)
            {
                return new StringContainer(obj.ToString());
            }
			return StringContainer.Empty;
		}
		/// <summary>
        /// 把Single类型转为字符串容器类，即字符串类
        /// </summary>
        /// <param name="obj">可以为null的Single类型</param>
        public static implicit operator StringContainer(Single? obj)
		{
            if(obj!=null)
            {
                return new StringContainer(obj.ToString());
            }
			return StringContainer.Empty;
		}
		/// <summary>
        /// 把DateTime类型转为字符串容器类，即字符串类
        /// </summary>
        /// <param name="obj">可以为null的DateTime类型</param>
        public static implicit operator StringContainer(DateTime? obj)
		{
            if(obj!=null)
            {
                return new StringContainer(obj.ToString());
            }
			return StringContainer.Empty;
		}
		/// <summary>
        /// 把DateTimeOffset类型转为字符串容器类，即字符串类
        /// </summary>
        /// <param name="obj">可以为null的DateTimeOffset类型</param>
        public static implicit operator StringContainer(DateTimeOffset? obj)
		{
            if(obj!=null)
            {
                return new StringContainer(obj.ToString());
            }
			return StringContainer.Empty;
		}
		/// <summary>
        /// 初始化函数
        /// </summary>
        /// <param name="value">字符串</param>
		public static implicit operator StringContainer(string value)
        {
            return new StringContainer(value);
        }
		/// <summary>
        /// 初始化函数
        /// </summary>
        /// <param name="stringContainar">字符串容器类</param>
		public static implicit operator string(StringContainer stringContainar)
		{
			return stringContainar.value;
		}
		/// <summary>
        /// 把字符串容器类转换为SByte类型
        /// </summary>
        /// <param name="stringContainar">字符串容器</param>
        public static implicit operator SByte(StringContainer stringContainar)
        {
            SByte result;
            SByte.TryParse(stringContainar.value, out result);
            return result;
        }
		/// <summary>
        /// 把字符串容器类转换为Byte类型
        /// </summary>
        /// <param name="stringContainar">字符串容器</param>
        public static implicit operator Byte(StringContainer stringContainar)
        {
            Byte result;
            Byte.TryParse(stringContainar.value, out result);
            return result;
        }
		/// <summary>
        /// 把字符串容器类转换为Int16类型
        /// </summary>
        /// <param name="stringContainar">字符串容器</param>
        public static implicit operator Int16(StringContainer stringContainar)
        {
            Int16 result;
            Int16.TryParse(stringContainar.value, out result);
            return result;
        }
		/// <summary>
        /// 把字符串容器类转换为UInt16类型
        /// </summary>
        /// <param name="stringContainar">字符串容器</param>
        public static implicit operator UInt16(StringContainer stringContainar)
        {
            UInt16 result;
            UInt16.TryParse(stringContainar.value, out result);
            return result;
        }
		/// <summary>
        /// 把字符串容器类转换为Int32类型
        /// </summary>
        /// <param name="stringContainar">字符串容器</param>
        public static implicit operator Int32(StringContainer stringContainar)
        {
            Int32 result;
            Int32.TryParse(stringContainar.value, out result);
            return result;
        }
		/// <summary>
        /// 把字符串容器类转换为UInt32类型
        /// </summary>
        /// <param name="stringContainar">字符串容器</param>
        public static implicit operator UInt32(StringContainer stringContainar)
        {
            UInt32 result;
            UInt32.TryParse(stringContainar.value, out result);
            return result;
        }
		/// <summary>
        /// 把字符串容器类转换为Int64类型
        /// </summary>
        /// <param name="stringContainar">字符串容器</param>
        public static implicit operator Int64(StringContainer stringContainar)
        {
            Int64 result;
            Int64.TryParse(stringContainar.value, out result);
            return result;
        }
		/// <summary>
        /// 把字符串容器类转换为UInt64类型
        /// </summary>
        /// <param name="stringContainar">字符串容器</param>
        public static implicit operator UInt64(StringContainer stringContainar)
        {
            UInt64 result;
            UInt64.TryParse(stringContainar.value, out result);
            return result;
        }
		/// <summary>
        /// 把字符串容器类转换为Boolean类型
        /// </summary>
        /// <param name="stringContainar">字符串容器</param>
        public static implicit operator Boolean(StringContainer stringContainar)
        {
            Boolean result;
            Boolean.TryParse(stringContainar.value, out result);
            return result;
        }
		/// <summary>
        /// 把字符串容器类转换为Char类型
        /// </summary>
        /// <param name="stringContainar">字符串容器</param>
        public static implicit operator Char(StringContainer stringContainar)
        {
            Char result;
            Char.TryParse(stringContainar.value, out result);
            return result;
        }
		/// <summary>
        /// 把字符串容器类转换为Decimal类型
        /// </summary>
        /// <param name="stringContainar">字符串容器</param>
        public static implicit operator Decimal(StringContainer stringContainar)
        {
            Decimal result;
            Decimal.TryParse(stringContainar.value, out result);
            return result;
        }
		/// <summary>
        /// 把字符串容器类转换为Double类型
        /// </summary>
        /// <param name="stringContainar">字符串容器</param>
        public static implicit operator Double(StringContainer stringContainar)
        {
            Double result;
            Double.TryParse(stringContainar.value, out result);
            return result;
        }
		/// <summary>
        /// 把字符串容器类转换为Single类型
        /// </summary>
        /// <param name="stringContainar">字符串容器</param>
        public static implicit operator Single(StringContainer stringContainar)
        {
            Single result;
            Single.TryParse(stringContainar.value, out result);
            return result;
        }
		/// <summary>
        /// 把字符串容器类转换为DateTime类型
        /// </summary>
        /// <param name="stringContainar">字符串容器</param>
        public static implicit operator DateTime(StringContainer stringContainar)
        {
            DateTime result;
            DateTime.TryParse(stringContainar.value, out result);
            return result;
        }
		/// <summary>
        /// 把字符串容器类转换为DateTimeOffset类型
        /// </summary>
        /// <param name="stringContainar">字符串容器</param>
        public static implicit operator DateTimeOffset(StringContainer stringContainar)
        {
            DateTimeOffset result;
            DateTimeOffset.TryParse(stringContainar.value, out result);
            return result;
        }
		/// <summary>
        /// 把字符串容器类转换为可为null的SByte类型
        /// </summary>
        /// <param name="stringContainar">字符串容器</param>
        public static implicit operator SByte?(StringContainer stringContainar)
        {
            SByte result;
            if(SByte.TryParse(stringContainar.value, out result))
			{
				return result;
			}
			else
			{
				return null;
			}
        }
		/// <summary>
        /// 把字符串容器类转换为可为null的Byte类型
        /// </summary>
        /// <param name="stringContainar">字符串容器</param>
        public static implicit operator Byte?(StringContainer stringContainar)
        {
            Byte result;
            if(Byte.TryParse(stringContainar.value, out result))
			{
				return result;
			}
			else
			{
				return null;
			}
        }
		/// <summary>
        /// 把字符串容器类转换为可为null的Int16类型
        /// </summary>
        /// <param name="stringContainar">字符串容器</param>
        public static implicit operator Int16?(StringContainer stringContainar)
        {
            Int16 result;
            if(Int16.TryParse(stringContainar.value, out result))
			{
				return result;
			}
			else
			{
				return null;
			}
        }
		/// <summary>
        /// 把字符串容器类转换为可为null的UInt16类型
        /// </summary>
        /// <param name="stringContainar">字符串容器</param>
        public static implicit operator UInt16?(StringContainer stringContainar)
        {
            UInt16 result;
            if(UInt16.TryParse(stringContainar.value, out result))
			{
				return result;
			}
			else
			{
				return null;
			}
        }
		/// <summary>
        /// 把字符串容器类转换为可为null的Int32类型
        /// </summary>
        /// <param name="stringContainar">字符串容器</param>
        public static implicit operator Int32?(StringContainer stringContainar)
        {
            Int32 result;
            if(Int32.TryParse(stringContainar.value, out result))
			{
				return result;
			}
			else
			{
				return null;
			}
        }
		/// <summary>
        /// 把字符串容器类转换为可为null的UInt32类型
        /// </summary>
        /// <param name="stringContainar">字符串容器</param>
        public static implicit operator UInt32?(StringContainer stringContainar)
        {
            UInt32 result;
            if(UInt32.TryParse(stringContainar.value, out result))
			{
				return result;
			}
			else
			{
				return null;
			}
        }
		/// <summary>
        /// 把字符串容器类转换为可为null的Int64类型
        /// </summary>
        /// <param name="stringContainar">字符串容器</param>
        public static implicit operator Int64?(StringContainer stringContainar)
        {
            Int64 result;
            if(Int64.TryParse(stringContainar.value, out result))
			{
				return result;
			}
			else
			{
				return null;
			}
        }
		/// <summary>
        /// 把字符串容器类转换为可为null的UInt64类型
        /// </summary>
        /// <param name="stringContainar">字符串容器</param>
        public static implicit operator UInt64?(StringContainer stringContainar)
        {
            UInt64 result;
            if(UInt64.TryParse(stringContainar.value, out result))
			{
				return result;
			}
			else
			{
				return null;
			}
        }
		/// <summary>
        /// 把字符串容器类转换为可为null的Boolean类型
        /// </summary>
        /// <param name="stringContainar">字符串容器</param>
        public static implicit operator Boolean?(StringContainer stringContainar)
        {
            Boolean result;
            if(Boolean.TryParse(stringContainar.value, out result))
			{
				return result;
			}
			else
			{
				return null;
			}
        }
		/// <summary>
        /// 把字符串容器类转换为可为null的Char类型
        /// </summary>
        /// <param name="stringContainar">字符串容器</param>
        public static implicit operator Char?(StringContainer stringContainar)
        {
            Char result;
            if(Char.TryParse(stringContainar.value, out result))
			{
				return result;
			}
			else
			{
				return null;
			}
        }
		/// <summary>
        /// 把字符串容器类转换为可为null的Decimal类型
        /// </summary>
        /// <param name="stringContainar">字符串容器</param>
        public static implicit operator Decimal?(StringContainer stringContainar)
        {
            Decimal result;
            if(Decimal.TryParse(stringContainar.value, out result))
			{
				return result;
			}
			else
			{
				return null;
			}
        }
		/// <summary>
        /// 把字符串容器类转换为可为null的Double类型
        /// </summary>
        /// <param name="stringContainar">字符串容器</param>
        public static implicit operator Double?(StringContainer stringContainar)
        {
            Double result;
            if(Double.TryParse(stringContainar.value, out result))
			{
				return result;
			}
			else
			{
				return null;
			}
        }
		/// <summary>
        /// 把字符串容器类转换为可为null的Single类型
        /// </summary>
        /// <param name="stringContainar">字符串容器</param>
        public static implicit operator Single?(StringContainer stringContainar)
        {
            Single result;
            if(Single.TryParse(stringContainar.value, out result))
			{
				return result;
			}
			else
			{
				return null;
			}
        }
		/// <summary>
        /// 把字符串容器类转换为可为null的DateTime类型
        /// </summary>
        /// <param name="stringContainar">字符串容器</param>
        public static implicit operator DateTime?(StringContainer stringContainar)
        {
            DateTime result;
            if(DateTime.TryParse(stringContainar.value, out result))
			{
				return result;
			}
			else
			{
				return null;
			}
        }
		/// <summary>
        /// 把字符串容器类转换为可为null的DateTimeOffset类型
        /// </summary>
        /// <param name="stringContainar">字符串容器</param>
        public static implicit operator DateTimeOffset?(StringContainer stringContainar)
        {
            DateTimeOffset result;
            if(DateTimeOffset.TryParse(stringContainar.value, out result))
			{
				return result;
			}
			else
			{
				return null;
			}
        }
    }
}
