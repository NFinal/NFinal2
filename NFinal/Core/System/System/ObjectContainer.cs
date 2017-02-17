using System;
namespace System
{
    public struct ObjectContainer
    {
        public object value;
        public ObjectContainer(object obj)
        {
            this.value = obj;
        }
		public override string ToString()
        {
            return this.value.ToString();
        }
		public static implicit operator string(ObjectContainer obj)
		{
			return obj.value.ToString();
		}
		/// <summary>
        /// ExecuteScalar()返回值转换为SByte
        /// </summary>
        /// <param name="obj">ExecuteScalar()返回值</param>
        /// <returns>SByte类型</returns>
        public static implicit operator SByte(ObjectContainer obj)
        {
            SByte result;
            if (obj.value != null && obj.value != DBNull.Value)
            {
                SByte.TryParse(obj.value.ToString(), out result);
            }
            else
            {
                SByte.TryParse(null, out result);
            }
            return result;
        }
		/// <summary>
        /// ExecuteScalar()返回值转换为Byte
        /// </summary>
        /// <param name="obj">ExecuteScalar()返回值</param>
        /// <returns>Byte类型</returns>
        public static implicit operator Byte(ObjectContainer obj)
        {
            Byte result;
            if (obj.value != null && obj.value != DBNull.Value)
            {
                Byte.TryParse(obj.value.ToString(), out result);
            }
            else
            {
                Byte.TryParse(null, out result);
            }
            return result;
        }
		/// <summary>
        /// ExecuteScalar()返回值转换为Int16
        /// </summary>
        /// <param name="obj">ExecuteScalar()返回值</param>
        /// <returns>Int16类型</returns>
        public static implicit operator Int16(ObjectContainer obj)
        {
            Int16 result;
            if (obj.value != null && obj.value != DBNull.Value)
            {
                Int16.TryParse(obj.value.ToString(), out result);
            }
            else
            {
                Int16.TryParse(null, out result);
            }
            return result;
        }
		/// <summary>
        /// ExecuteScalar()返回值转换为UInt16
        /// </summary>
        /// <param name="obj">ExecuteScalar()返回值</param>
        /// <returns>UInt16类型</returns>
        public static implicit operator UInt16(ObjectContainer obj)
        {
            UInt16 result;
            if (obj.value != null && obj.value != DBNull.Value)
            {
                UInt16.TryParse(obj.value.ToString(), out result);
            }
            else
            {
                UInt16.TryParse(null, out result);
            }
            return result;
        }
		/// <summary>
        /// ExecuteScalar()返回值转换为Int32
        /// </summary>
        /// <param name="obj">ExecuteScalar()返回值</param>
        /// <returns>Int32类型</returns>
        public static implicit operator Int32(ObjectContainer obj)
        {
            Int32 result;
            if (obj.value != null && obj.value != DBNull.Value)
            {
                Int32.TryParse(obj.value.ToString(), out result);
            }
            else
            {
                Int32.TryParse(null, out result);
            }
            return result;
        }
		/// <summary>
        /// ExecuteScalar()返回值转换为UInt32
        /// </summary>
        /// <param name="obj">ExecuteScalar()返回值</param>
        /// <returns>UInt32类型</returns>
        public static implicit operator UInt32(ObjectContainer obj)
        {
            UInt32 result;
            if (obj.value != null && obj.value != DBNull.Value)
            {
                UInt32.TryParse(obj.value.ToString(), out result);
            }
            else
            {
                UInt32.TryParse(null, out result);
            }
            return result;
        }
		/// <summary>
        /// ExecuteScalar()返回值转换为Int64
        /// </summary>
        /// <param name="obj">ExecuteScalar()返回值</param>
        /// <returns>Int64类型</returns>
        public static implicit operator Int64(ObjectContainer obj)
        {
            Int64 result;
            if (obj.value != null && obj.value != DBNull.Value)
            {
                Int64.TryParse(obj.value.ToString(), out result);
            }
            else
            {
                Int64.TryParse(null, out result);
            }
            return result;
        }
		/// <summary>
        /// ExecuteScalar()返回值转换为UInt64
        /// </summary>
        /// <param name="obj">ExecuteScalar()返回值</param>
        /// <returns>UInt64类型</returns>
        public static implicit operator UInt64(ObjectContainer obj)
        {
            UInt64 result;
            if (obj.value != null && obj.value != DBNull.Value)
            {
                UInt64.TryParse(obj.value.ToString(), out result);
            }
            else
            {
                UInt64.TryParse(null, out result);
            }
            return result;
        }
		/// <summary>
        /// ExecuteScalar()返回值转换为Boolean
        /// </summary>
        /// <param name="obj">ExecuteScalar()返回值</param>
        /// <returns>Boolean类型</returns>
        public static implicit operator Boolean(ObjectContainer obj)
        {
            Boolean result;
            if (obj.value != null && obj.value != DBNull.Value)
            {
                Boolean.TryParse(obj.value.ToString(), out result);
            }
            else
            {
                Boolean.TryParse(null, out result);
            }
            return result;
        }
		/// <summary>
        /// ExecuteScalar()返回值转换为Char
        /// </summary>
        /// <param name="obj">ExecuteScalar()返回值</param>
        /// <returns>Char类型</returns>
        public static implicit operator Char(ObjectContainer obj)
        {
            Char result;
            if (obj.value != null && obj.value != DBNull.Value)
            {
                Char.TryParse(obj.value.ToString(), out result);
            }
            else
            {
                Char.TryParse(null, out result);
            }
            return result;
        }
		/// <summary>
        /// ExecuteScalar()返回值转换为Decimal
        /// </summary>
        /// <param name="obj">ExecuteScalar()返回值</param>
        /// <returns>Decimal类型</returns>
        public static implicit operator Decimal(ObjectContainer obj)
        {
            Decimal result;
            if (obj.value != null && obj.value != DBNull.Value)
            {
                Decimal.TryParse(obj.value.ToString(), out result);
            }
            else
            {
                Decimal.TryParse(null, out result);
            }
            return result;
        }
		/// <summary>
        /// ExecuteScalar()返回值转换为Double
        /// </summary>
        /// <param name="obj">ExecuteScalar()返回值</param>
        /// <returns>Double类型</returns>
        public static implicit operator Double(ObjectContainer obj)
        {
            Double result;
            if (obj.value != null && obj.value != DBNull.Value)
            {
                Double.TryParse(obj.value.ToString(), out result);
            }
            else
            {
                Double.TryParse(null, out result);
            }
            return result;
        }
		/// <summary>
        /// ExecuteScalar()返回值转换为Single
        /// </summary>
        /// <param name="obj">ExecuteScalar()返回值</param>
        /// <returns>Single类型</returns>
        public static implicit operator Single(ObjectContainer obj)
        {
            Single result;
            if (obj.value != null && obj.value != DBNull.Value)
            {
                Single.TryParse(obj.value.ToString(), out result);
            }
            else
            {
                Single.TryParse(null, out result);
            }
            return result;
        }
		/// <summary>
        /// ExecuteScalar()返回值转换为DateTime
        /// </summary>
        /// <param name="obj">ExecuteScalar()返回值</param>
        /// <returns>DateTime类型</returns>
        public static implicit operator DateTime(ObjectContainer obj)
        {
            DateTime result;
            if (obj.value != null && obj.value != DBNull.Value)
            {
                DateTime.TryParse(obj.value.ToString(), out result);
            }
            else
            {
                DateTime.TryParse(null, out result);
            }
            return result;
        }
		/// <summary>
        /// ExecuteScalar()返回值转换为DateTimeOffset
        /// </summary>
        /// <param name="obj">ExecuteScalar()返回值</param>
        /// <returns>DateTimeOffset类型</returns>
        public static implicit operator DateTimeOffset(ObjectContainer obj)
        {
            DateTimeOffset result;
            if (obj.value != null && obj.value != DBNull.Value)
            {
                DateTimeOffset.TryParse(obj.value.ToString(), out result);
            }
            else
            {
                DateTimeOffset.TryParse(null, out result);
            }
            return result;
        }
		/// <summary>
        /// ExecuteScalar()返回值转换为SByte
        /// </summary>
        /// <param name="obj">ExecuteScalar()返回值</param>
        /// <returns>SByte类型</returns>
        public static implicit operator SByte?(ObjectContainer obj)
        {
            SByte result;
            if (obj.value != null && obj.value != DBNull.Value)
            {
                SByte.TryParse(obj.value.ToString(), out result);
				return result;
            }
            else
            {
                return null;
            }
        }
		/// <summary>
        /// ExecuteScalar()返回值转换为Byte
        /// </summary>
        /// <param name="obj">ExecuteScalar()返回值</param>
        /// <returns>Byte类型</returns>
        public static implicit operator Byte?(ObjectContainer obj)
        {
            Byte result;
            if (obj.value != null && obj.value != DBNull.Value)
            {
                Byte.TryParse(obj.value.ToString(), out result);
				return result;
            }
            else
            {
                return null;
            }
        }
		/// <summary>
        /// ExecuteScalar()返回值转换为Int16
        /// </summary>
        /// <param name="obj">ExecuteScalar()返回值</param>
        /// <returns>Int16类型</returns>
        public static implicit operator Int16?(ObjectContainer obj)
        {
            Int16 result;
            if (obj.value != null && obj.value != DBNull.Value)
            {
                Int16.TryParse(obj.value.ToString(), out result);
				return result;
            }
            else
            {
                return null;
            }
        }
		/// <summary>
        /// ExecuteScalar()返回值转换为UInt16
        /// </summary>
        /// <param name="obj">ExecuteScalar()返回值</param>
        /// <returns>UInt16类型</returns>
        public static implicit operator UInt16?(ObjectContainer obj)
        {
            UInt16 result;
            if (obj.value != null && obj.value != DBNull.Value)
            {
                UInt16.TryParse(obj.value.ToString(), out result);
				return result;
            }
            else
            {
                return null;
            }
        }
		/// <summary>
        /// ExecuteScalar()返回值转换为Int32
        /// </summary>
        /// <param name="obj">ExecuteScalar()返回值</param>
        /// <returns>Int32类型</returns>
        public static implicit operator Int32?(ObjectContainer obj)
        {
            Int32 result;
            if (obj.value != null && obj.value != DBNull.Value)
            {
                Int32.TryParse(obj.value.ToString(), out result);
				return result;
            }
            else
            {
                return null;
            }
        }
		/// <summary>
        /// ExecuteScalar()返回值转换为UInt32
        /// </summary>
        /// <param name="obj">ExecuteScalar()返回值</param>
        /// <returns>UInt32类型</returns>
        public static implicit operator UInt32?(ObjectContainer obj)
        {
            UInt32 result;
            if (obj.value != null && obj.value != DBNull.Value)
            {
                UInt32.TryParse(obj.value.ToString(), out result);
				return result;
            }
            else
            {
                return null;
            }
        }
		/// <summary>
        /// ExecuteScalar()返回值转换为Int64
        /// </summary>
        /// <param name="obj">ExecuteScalar()返回值</param>
        /// <returns>Int64类型</returns>
        public static implicit operator Int64?(ObjectContainer obj)
        {
            Int64 result;
            if (obj.value != null && obj.value != DBNull.Value)
            {
                Int64.TryParse(obj.value.ToString(), out result);
				return result;
            }
            else
            {
                return null;
            }
        }
		/// <summary>
        /// ExecuteScalar()返回值转换为UInt64
        /// </summary>
        /// <param name="obj">ExecuteScalar()返回值</param>
        /// <returns>UInt64类型</returns>
        public static implicit operator UInt64?(ObjectContainer obj)
        {
            UInt64 result;
            if (obj.value != null && obj.value != DBNull.Value)
            {
                UInt64.TryParse(obj.value.ToString(), out result);
				return result;
            }
            else
            {
                return null;
            }
        }
		/// <summary>
        /// ExecuteScalar()返回值转换为Boolean
        /// </summary>
        /// <param name="obj">ExecuteScalar()返回值</param>
        /// <returns>Boolean类型</returns>
        public static implicit operator Boolean?(ObjectContainer obj)
        {
            Boolean result;
            if (obj.value != null && obj.value != DBNull.Value)
            {
                Boolean.TryParse(obj.value.ToString(), out result);
				return result;
            }
            else
            {
                return null;
            }
        }
		/// <summary>
        /// ExecuteScalar()返回值转换为Char
        /// </summary>
        /// <param name="obj">ExecuteScalar()返回值</param>
        /// <returns>Char类型</returns>
        public static implicit operator Char?(ObjectContainer obj)
        {
            Char result;
            if (obj.value != null && obj.value != DBNull.Value)
            {
                Char.TryParse(obj.value.ToString(), out result);
				return result;
            }
            else
            {
                return null;
            }
        }
		/// <summary>
        /// ExecuteScalar()返回值转换为Decimal
        /// </summary>
        /// <param name="obj">ExecuteScalar()返回值</param>
        /// <returns>Decimal类型</returns>
        public static implicit operator Decimal?(ObjectContainer obj)
        {
            Decimal result;
            if (obj.value != null && obj.value != DBNull.Value)
            {
                Decimal.TryParse(obj.value.ToString(), out result);
				return result;
            }
            else
            {
                return null;
            }
        }
		/// <summary>
        /// ExecuteScalar()返回值转换为Double
        /// </summary>
        /// <param name="obj">ExecuteScalar()返回值</param>
        /// <returns>Double类型</returns>
        public static implicit operator Double?(ObjectContainer obj)
        {
            Double result;
            if (obj.value != null && obj.value != DBNull.Value)
            {
                Double.TryParse(obj.value.ToString(), out result);
				return result;
            }
            else
            {
                return null;
            }
        }
		/// <summary>
        /// ExecuteScalar()返回值转换为Single
        /// </summary>
        /// <param name="obj">ExecuteScalar()返回值</param>
        /// <returns>Single类型</returns>
        public static implicit operator Single?(ObjectContainer obj)
        {
            Single result;
            if (obj.value != null && obj.value != DBNull.Value)
            {
                Single.TryParse(obj.value.ToString(), out result);
				return result;
            }
            else
            {
                return null;
            }
        }
		/// <summary>
        /// ExecuteScalar()返回值转换为DateTime
        /// </summary>
        /// <param name="obj">ExecuteScalar()返回值</param>
        /// <returns>DateTime类型</returns>
        public static implicit operator DateTime?(ObjectContainer obj)
        {
            DateTime result;
            if (obj.value != null && obj.value != DBNull.Value)
            {
                DateTime.TryParse(obj.value.ToString(), out result);
				return result;
            }
            else
            {
                return null;
            }
        }
		/// <summary>
        /// ExecuteScalar()返回值转换为DateTimeOffset
        /// </summary>
        /// <param name="obj">ExecuteScalar()返回值</param>
        /// <returns>DateTimeOffset类型</returns>
        public static implicit operator DateTimeOffset?(ObjectContainer obj)
        {
            DateTimeOffset result;
            if (obj.value != null && obj.value != DBNull.Value)
            {
                DateTimeOffset.TryParse(obj.value.ToString(), out result);
				return result;
            }
            else
            {
                return null;
            }
        }
	}
}
