using System;

namespace NFinal
{
    public struct StringContainer
    {
        public static readonly StringContainer Empty=new StringContainer();
        public string value;
        internal StringContainer(string value)
        {
            this.value = value;
        }
		public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
		public static bool operator ==(StringContainer container, string str)
        {
            return container.value == str;
        }
        public static bool operator !=(StringContainer container, string str)
        {
            return container.value != str;
        }
        public static implicit operator StringContainer(SByte obj)
		{
            return new StringContainer(obj.ToString());
		}
        public static implicit operator StringContainer(Byte obj)
		{
            return new StringContainer(obj.ToString());
		}
        public static implicit operator StringContainer(Int16 obj)
		{
            return new StringContainer(obj.ToString());
		}
        public static implicit operator StringContainer(UInt16 obj)
		{
            return new StringContainer(obj.ToString());
		}
        public static implicit operator StringContainer(Int32 obj)
		{
            return new StringContainer(obj.ToString());
		}
        public static implicit operator StringContainer(UInt32 obj)
		{
            return new StringContainer(obj.ToString());
		}
        public static implicit operator StringContainer(Int64 obj)
		{
            return new StringContainer(obj.ToString());
		}
        public static implicit operator StringContainer(UInt64 obj)
		{
            return new StringContainer(obj.ToString());
		}
        public static implicit operator StringContainer(Boolean obj)
		{
            return new StringContainer(obj.ToString());
		}
        public static implicit operator StringContainer(Char obj)
		{
            return new StringContainer(obj.ToString());
		}
        public static implicit operator StringContainer(Decimal obj)
		{
            return new StringContainer(obj.ToString());
		}
        public static implicit operator StringContainer(Double obj)
		{
            return new StringContainer(obj.ToString());
		}
        public static implicit operator StringContainer(Single obj)
		{
            return new StringContainer(obj.ToString());
		}
        public static implicit operator StringContainer(DateTime obj)
		{
            return new StringContainer(obj.ToString());
		}
        public static implicit operator StringContainer(DateTimeOffset obj)
		{
            return new StringContainer(obj.ToString());
		}
        public static implicit operator StringContainer(SByte? obj)
		{
            if(obj!=null)
            {
                return new StringContainer(obj.ToString());
            }
			return StringContainer.Empty;
		}
        public static implicit operator StringContainer(Byte? obj)
		{
            if(obj!=null)
            {
                return new StringContainer(obj.ToString());
            }
			return StringContainer.Empty;
		}
        public static implicit operator StringContainer(Int16? obj)
		{
            if(obj!=null)
            {
                return new StringContainer(obj.ToString());
            }
			return StringContainer.Empty;
		}
        public static implicit operator StringContainer(UInt16? obj)
		{
            if(obj!=null)
            {
                return new StringContainer(obj.ToString());
            }
			return StringContainer.Empty;
		}
        public static implicit operator StringContainer(Int32? obj)
		{
            if(obj!=null)
            {
                return new StringContainer(obj.ToString());
            }
			return StringContainer.Empty;
		}
        public static implicit operator StringContainer(UInt32? obj)
		{
            if(obj!=null)
            {
                return new StringContainer(obj.ToString());
            }
			return StringContainer.Empty;
		}
        public static implicit operator StringContainer(Int64? obj)
		{
            if(obj!=null)
            {
                return new StringContainer(obj.ToString());
            }
			return StringContainer.Empty;
		}
        public static implicit operator StringContainer(UInt64? obj)
		{
            if(obj!=null)
            {
                return new StringContainer(obj.ToString());
            }
			return StringContainer.Empty;
		}
        public static implicit operator StringContainer(Boolean? obj)
		{
            if(obj!=null)
            {
                return new StringContainer(obj.ToString());
            }
			return StringContainer.Empty;
		}
        public static implicit operator StringContainer(Char? obj)
		{
            if(obj!=null)
            {
                return new StringContainer(obj.ToString());
            }
			return StringContainer.Empty;
		}
        public static implicit operator StringContainer(Decimal? obj)
		{
            if(obj!=null)
            {
                return new StringContainer(obj.ToString());
            }
			return StringContainer.Empty;
		}
        public static implicit operator StringContainer(Double? obj)
		{
            if(obj!=null)
            {
                return new StringContainer(obj.ToString());
            }
			return StringContainer.Empty;
		}
        public static implicit operator StringContainer(Single? obj)
		{
            if(obj!=null)
            {
                return new StringContainer(obj.ToString());
            }
			return StringContainer.Empty;
		}
        public static implicit operator StringContainer(DateTime? obj)
		{
            if(obj!=null)
            {
                return new StringContainer(obj.ToString());
            }
			return StringContainer.Empty;
		}
        public static implicit operator StringContainer(DateTimeOffset? obj)
		{
            if(obj!=null)
            {
                return new StringContainer(obj.ToString());
            }
			return StringContainer.Empty;
		}
		public static implicit operator StringContainer(string value)
        {
            return new StringContainer(value);
        }
		public static implicit operator string(StringContainer stringContainar)
		{
			return stringContainar.value;
		}
        public static implicit operator SByte(StringContainer stringContainar)
        {
            SByte result;
            SByte.TryParse(stringContainar.value, out result);
            return result;
        }
        public static implicit operator Byte(StringContainer stringContainar)
        {
            Byte result;
            Byte.TryParse(stringContainar.value, out result);
            return result;
        }
        public static implicit operator Int16(StringContainer stringContainar)
        {
            Int16 result;
            Int16.TryParse(stringContainar.value, out result);
            return result;
        }
        public static implicit operator UInt16(StringContainer stringContainar)
        {
            UInt16 result;
            UInt16.TryParse(stringContainar.value, out result);
            return result;
        }
        public static implicit operator Int32(StringContainer stringContainar)
        {
            Int32 result;
            Int32.TryParse(stringContainar.value, out result);
            return result;
        }
        public static implicit operator UInt32(StringContainer stringContainar)
        {
            UInt32 result;
            UInt32.TryParse(stringContainar.value, out result);
            return result;
        }
        public static implicit operator Int64(StringContainer stringContainar)
        {
            Int64 result;
            Int64.TryParse(stringContainar.value, out result);
            return result;
        }
        public static implicit operator UInt64(StringContainer stringContainar)
        {
            UInt64 result;
            UInt64.TryParse(stringContainar.value, out result);
            return result;
        }
        public static implicit operator Boolean(StringContainer stringContainar)
        {
            Boolean result;
            Boolean.TryParse(stringContainar.value, out result);
            return result;
        }
        public static implicit operator Char(StringContainer stringContainar)
        {
            Char result;
            Char.TryParse(stringContainar.value, out result);
            return result;
        }
        public static implicit operator Decimal(StringContainer stringContainar)
        {
            Decimal result;
            Decimal.TryParse(stringContainar.value, out result);
            return result;
        }
        public static implicit operator Double(StringContainer stringContainar)
        {
            Double result;
            Double.TryParse(stringContainar.value, out result);
            return result;
        }
        public static implicit operator Single(StringContainer stringContainar)
        {
            Single result;
            Single.TryParse(stringContainar.value, out result);
            return result;
        }
        public static implicit operator DateTime(StringContainer stringContainar)
        {
            DateTime result;
            DateTime.TryParse(stringContainar.value, out result);
            return result;
        }
        public static implicit operator DateTimeOffset(StringContainer stringContainar)
        {
            DateTimeOffset result;
            DateTimeOffset.TryParse(stringContainar.value, out result);
            return result;
        }
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
