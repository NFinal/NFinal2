using System;

namespace System
{
	/// <summary>
    /// 基本类型转SqlParameter类型的类
    /// </summary>
    public static class WithDBNullExtension
    {
		/// <summary>
        /// 转换为SqlParameter的值类型
        /// </summary>
        /// <param name="obj">object类型变量</param>
        /// <returns>object</returns>
		public static object WithDBNull(this object obj)
		{
			if (obj != null)
			{
				return obj.ToString();
			}
			return DBNull.Value;
		}
		/// <summary>
        /// 转换为SqlParameter的值类型
        /// </summary>
        /// <param name="obj">String类型变量</param>
        /// <returns>object</returns>
		public static object WithDBNull(this String obj)
		{
			if (obj != null)
			{
				return obj.ToString();
			}
			return DBNull.Value;
		}
		/// <summary>
        /// 转换为SqlParameter的值类型
        /// </summary>
        /// <param name="obj">SByte类型变量</param>
        /// <returns>object</returns>
		public static object WithDBNull(this SByte obj)
        {
            return obj;
        }
		/// <summary>
        /// 转换为SqlParameter的值类型
        /// </summary>
        /// <param name="obj">SByte?类型变量</param>
        /// <returns>object</returns>
        public static object WithDBNull(this SByte? obj)
        {
            if (obj != null)
            {
                return obj;
            }
            return DBNull.Value;
        }
		/// <summary>
        /// 转换为SqlParameter的值类型
        /// </summary>
        /// <param name="obj">Byte类型变量</param>
        /// <returns>object</returns>
		public static object WithDBNull(this Byte obj)
        {
            return obj;
        }
		/// <summary>
        /// 转换为SqlParameter的值类型
        /// </summary>
        /// <param name="obj">Byte?类型变量</param>
        /// <returns>object</returns>
        public static object WithDBNull(this Byte? obj)
        {
            if (obj != null)
            {
                return obj;
            }
            return DBNull.Value;
        }
		/// <summary>
        /// 转换为SqlParameter的值类型
        /// </summary>
        /// <param name="obj">Int16类型变量</param>
        /// <returns>object</returns>
		public static object WithDBNull(this Int16 obj)
        {
            return obj;
        }
		/// <summary>
        /// 转换为SqlParameter的值类型
        /// </summary>
        /// <param name="obj">Int16?类型变量</param>
        /// <returns>object</returns>
        public static object WithDBNull(this Int16? obj)
        {
            if (obj != null)
            {
                return obj;
            }
            return DBNull.Value;
        }
		/// <summary>
        /// 转换为SqlParameter的值类型
        /// </summary>
        /// <param name="obj">UInt16类型变量</param>
        /// <returns>object</returns>
		public static object WithDBNull(this UInt16 obj)
        {
            return obj;
        }
		/// <summary>
        /// 转换为SqlParameter的值类型
        /// </summary>
        /// <param name="obj">UInt16?类型变量</param>
        /// <returns>object</returns>
        public static object WithDBNull(this UInt16? obj)
        {
            if (obj != null)
            {
                return obj;
            }
            return DBNull.Value;
        }
		/// <summary>
        /// 转换为SqlParameter的值类型
        /// </summary>
        /// <param name="obj">Int32类型变量</param>
        /// <returns>object</returns>
		public static object WithDBNull(this Int32 obj)
        {
            return obj;
        }
		/// <summary>
        /// 转换为SqlParameter的值类型
        /// </summary>
        /// <param name="obj">Int32?类型变量</param>
        /// <returns>object</returns>
        public static object WithDBNull(this Int32? obj)
        {
            if (obj != null)
            {
                return obj;
            }
            return DBNull.Value;
        }
		/// <summary>
        /// 转换为SqlParameter的值类型
        /// </summary>
        /// <param name="obj">UInt32类型变量</param>
        /// <returns>object</returns>
		public static object WithDBNull(this UInt32 obj)
        {
            return obj;
        }
		/// <summary>
        /// 转换为SqlParameter的值类型
        /// </summary>
        /// <param name="obj">UInt32?类型变量</param>
        /// <returns>object</returns>
        public static object WithDBNull(this UInt32? obj)
        {
            if (obj != null)
            {
                return obj;
            }
            return DBNull.Value;
        }
		/// <summary>
        /// 转换为SqlParameter的值类型
        /// </summary>
        /// <param name="obj">Int64类型变量</param>
        /// <returns>object</returns>
		public static object WithDBNull(this Int64 obj)
        {
            return obj;
        }
		/// <summary>
        /// 转换为SqlParameter的值类型
        /// </summary>
        /// <param name="obj">Int64?类型变量</param>
        /// <returns>object</returns>
        public static object WithDBNull(this Int64? obj)
        {
            if (obj != null)
            {
                return obj;
            }
            return DBNull.Value;
        }
		/// <summary>
        /// 转换为SqlParameter的值类型
        /// </summary>
        /// <param name="obj">UInt64类型变量</param>
        /// <returns>object</returns>
		public static object WithDBNull(this UInt64 obj)
        {
            return obj;
        }
		/// <summary>
        /// 转换为SqlParameter的值类型
        /// </summary>
        /// <param name="obj">UInt64?类型变量</param>
        /// <returns>object</returns>
        public static object WithDBNull(this UInt64? obj)
        {
            if (obj != null)
            {
                return obj;
            }
            return DBNull.Value;
        }
		/// <summary>
        /// 转换为SqlParameter的值类型
        /// </summary>
        /// <param name="obj">Boolean类型变量</param>
        /// <returns>object</returns>
		public static object WithDBNull(this Boolean obj)
        {
            return obj;
        }
		/// <summary>
        /// 转换为SqlParameter的值类型
        /// </summary>
        /// <param name="obj">Boolean?类型变量</param>
        /// <returns>object</returns>
        public static object WithDBNull(this Boolean? obj)
        {
            if (obj != null)
            {
                return obj;
            }
            return DBNull.Value;
        }
		/// <summary>
        /// 转换为SqlParameter的值类型
        /// </summary>
        /// <param name="obj">Char类型变量</param>
        /// <returns>object</returns>
		public static object WithDBNull(this Char obj)
        {
            return obj;
        }
		/// <summary>
        /// 转换为SqlParameter的值类型
        /// </summary>
        /// <param name="obj">Char?类型变量</param>
        /// <returns>object</returns>
        public static object WithDBNull(this Char? obj)
        {
            if (obj != null)
            {
                return obj;
            }
            return DBNull.Value;
        }
		/// <summary>
        /// 转换为SqlParameter的值类型
        /// </summary>
        /// <param name="obj">Decimal类型变量</param>
        /// <returns>object</returns>
		public static object WithDBNull(this Decimal obj)
        {
            return obj;
        }
		/// <summary>
        /// 转换为SqlParameter的值类型
        /// </summary>
        /// <param name="obj">Decimal?类型变量</param>
        /// <returns>object</returns>
        public static object WithDBNull(this Decimal? obj)
        {
            if (obj != null)
            {
                return obj;
            }
            return DBNull.Value;
        }
		/// <summary>
        /// 转换为SqlParameter的值类型
        /// </summary>
        /// <param name="obj">Double类型变量</param>
        /// <returns>object</returns>
		public static object WithDBNull(this Double obj)
        {
            return obj;
        }
		/// <summary>
        /// 转换为SqlParameter的值类型
        /// </summary>
        /// <param name="obj">Double?类型变量</param>
        /// <returns>object</returns>
        public static object WithDBNull(this Double? obj)
        {
            if (obj != null)
            {
                return obj;
            }
            return DBNull.Value;
        }
		/// <summary>
        /// 转换为SqlParameter的值类型
        /// </summary>
        /// <param name="obj">Single类型变量</param>
        /// <returns>object</returns>
		public static object WithDBNull(this Single obj)
        {
            return obj;
        }
		/// <summary>
        /// 转换为SqlParameter的值类型
        /// </summary>
        /// <param name="obj">Single?类型变量</param>
        /// <returns>object</returns>
        public static object WithDBNull(this Single? obj)
        {
            if (obj != null)
            {
                return obj;
            }
            return DBNull.Value;
        }
		/// <summary>
        /// 转换为SqlParameter的值类型
        /// </summary>
        /// <param name="obj">DateTime类型变量</param>
        /// <returns>object</returns>
		public static object WithDBNull(this DateTime obj)
        {
            return obj;
        }
		/// <summary>
        /// 转换为SqlParameter的值类型
        /// </summary>
        /// <param name="obj">DateTime?类型变量</param>
        /// <returns>object</returns>
        public static object WithDBNull(this DateTime? obj)
        {
            if (obj != null)
            {
                return obj;
            }
            return DBNull.Value;
        }
		/// <summary>
        /// 转换为SqlParameter的值类型
        /// </summary>
        /// <param name="obj">DateTimeOffset类型变量</param>
        /// <returns>object</returns>
		public static object WithDBNull(this DateTimeOffset obj)
        {
            return obj;
        }
		/// <summary>
        /// 转换为SqlParameter的值类型
        /// </summary>
        /// <param name="obj">DateTimeOffset?类型变量</param>
        /// <returns>object</returns>
        public static object WithDBNull(this DateTimeOffset? obj)
        {
            if (obj != null)
            {
                return obj;
            }
            return DBNull.Value;
        }
	}
}