using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace NFinal.Advanced
{
    /// <summary>
    /// 通过对string, byte[], ushort[], uint[], ulong[]等数据类型的扩展方法，
    /// 实现数据和十六进制字符串之间的转换。
    /// </summary>
    public static class Hex
    {
        static readonly char[] HexChars =
        {
            '0', '1', '2', '3', '4', '5', '6', '7',
            '8', '9', 'A', 'B', 'C', 'D', 'E', 'F'
        };

        static readonly char[] LowerHexChars =
        {
            '0', '1', '2', '3', '4', '5', '6', '7',
            '8', '9', 'a', 'b', 'c', 'd', 'e', 'f'
        };

        static readonly int[] ReverseHexCode =
        {
            0, 1, 2, 3, 4, 5, 6, 7, 8, 9,   // ASCII 48 ~ 57 (count 10)
            0, 0, 0, 0, 0, 0, 0,            // ASCII 58 ~ 64 (count 7)
            10, 11, 12, 13, 14, 15          // ASCII 65 ~ 70 (count 6)
        };

        static readonly byte[] EmptyByteArray = new byte[0];

        /// <summary>
        /// 把使用16进制表示数据的字符串转换成对应的2进制数据（字节组表示）
        /// </summary>
        /// <param name="me">16进制</param>
        /// <returns>2进制数据</returns>
        public static byte[] HexDecode(this string me)
        {
            if (me == null) { return null; }

            // 去掉非16进制字符
            // 
            // FIXME 去掉非16进制字符的目的是除去十六进制字符串中间的分隔符
            // 但这里假设16进制字符串中均是使用2位HEX表示一个字节。
            // 如果有用单个HEX表示一个字节的情况，如01-2-34-5-6，
            // 转换结果就是出错（结果会是00-12-34-56的转换结果）
            string hex = Regex.Replace(me, "[^a-fA-F0-9]", string.Empty);

            if (hex.Length == 0) { return EmptyByteArray; }

            if (hex.Length % 2 == 1) { hex = '0' + hex; }
            hex = hex.ToUpper();

            int num = hex.Length / 2;
            byte[] data = new byte[num];
            int b;
            char c;
            int j = 0;
            for (int i = 0; i < num; i++)
            {
                c = hex[j++];
                b = ReverseHexCode[c - 48];
                c = hex[j++];
                b = (b << 4) | (ReverseHexCode[c - 48] & 0x0f);
                data[i] = (byte) b;
            }

            return data;
        }

        /// <summary>
        /// 把字符数组表示的2进制数据转换成16进制编码的字符串。
        /// 转换结果中每个字节用2位<c>HEX</c>表示。
        /// </summary>
        /// <param name="me">源2进制数据</param>
        /// <returns>编码结果（16进制字符串）</returns>
        public static string HexEncode(this byte[] me)
        {
            return me == null ? null : me.HexEncode(0, me.Length);
        }

        /// <summary>
        /// 把字符数组表示的2进制数据中的一部分转换成16进制编码的字符串。
        /// 转换结果中每个字节用2位<c>HEX</c>表示。
        /// </summary>
        /// <param name="me">源2进制数据</param>
        /// <param name="start">要转换的数据起始索引</param>
        /// <param name="length">要转换的数据长度</param>
        /// <returns>编码结果（16进制字符串）</returns>
        /// <exception cref="ArgumentOutOfRangeException"><c>length</c>大于<c>Int32.MaxValue / 2</c>时</exception>
        /// <remarks>如果转换出错（比如源数据为<c>null</c>索引位置不正确，长度不正确等），返回null；
        /// 如果<c>length</c>为<c>0</c>或源数据长度为<c>0</c>，返回<c>string.Empty</c>。</remarks>
        /// <remarks>在<c>start</c>和<c>length</c>均有效的情况下，如果<c>start + length</c>大于数据长度，
        /// 会转换从<c>start</c>开始到数据数组结束的数据，不视为出错（不返回null）</remarks>
        public static string HexEncode(this byte[] me, int start, int length)
        {
            if (!IsValidArguments(me, start, ref length)) { return null; }
            if (length == 0) { return string.Empty; }

            // 如果长度大于int.MaxValue的一半，抛出异常
            // 因为需要产生两倍大的字符数组，而数组的最大索引在int.MaxValue之内
            if (length > 1073741823)
            {
                throw new ArgumentOutOfRangeException("length", "length to large, please split it.");
            }

            char[] hexChars = Settings.Global.IsUpperCaseInHexadecimal
                ? HexChars
                : LowerHexChars;

            int num = length * 2;
            char[] array = new char[num];

            int j = start;
            for (int i = 0; i < num; i += 2)
            {
                int b = me[j++];
                array[i] = hexChars[b >> 4];
                array[i + 1] = hexChars[b & 0x0f];
            }

            return new string(array);
        }

        /// <summary>
        /// 将用<c>ushort[]</c>表示的2进制数据转换为16进制字符串。
        /// </summary>
        /// <param name="me">源2进制数据</param>
        /// <param name="isAsByteArray">源数据中每个<c>ushort</c>是否仅包含8位数据</param>
        /// <returns>编码结果（16进制字符串）</returns>
        /// <remarks>如果<c>isAsByteArray</c>为<c>true</c>，
        /// 每个<c>ushort</c>仅8位数据有效，转换为2位<c>HEX</c>。
        /// 如果<c>isAsByteArray</c>为<c>false</c>，
        /// 每个<c>ushort</c>含16位有效数据，转换为4位<c>HEX</c>。</remarks>
        public static string HexEncode(this ushort[] me, bool isAsByteArray = false)
        {
            if (me == null) { return null; }
            if (me.Length == 0) { return string.Empty; }

            byte[] data = isAsByteArray
                ? (from item in me select (byte) item).ToArray()
                : me.ToByteArray();

            return data.HexEncode();
        }

        /// <summary>
        /// 将用<c>ushort[]</c>表示的2进制数据中的一部分转换为16进制字符串。
        /// </summary>
        /// <param name="me">源2进制数据</param>
        /// <param name="start">要转换的数据起始索引</param>
        /// <param name="length">要转换的数据长度</param>
        /// <param name="isAsByteArray">源数据中每个<c>ushort</c>是否仅包含8位数据</param>
        /// <returns>编码结果（16进制字符串）</returns>
        /// <remarks>如果<c>isAsByteArray</c>为<c>true</c>，
        /// 每个<c>ushort</c>仅8位数据有效，转换为2位<c>HEX</c>。
        /// 如果<c>isAsByteArray</c>为<c>false</c>，
        /// 每个<c>ushort</c>含16位有效数据，转换为4位<c>HEX</c>。</remarks>
        public static string HexEncode(this ushort[] me, int start, int length, bool isAsByteArray = false)
        {
            if (!IsValidArguments(me, start, ref length)) { return null; }
            if (length == 0) { return string.Empty; }

            ushort[] source = new ushort[length];
            Array.Copy(me, start, source, 0, length);
            return source.HexEncode(isAsByteArray);
        }

        /// <summary>
        /// 将用<c>uint[]</c>表示的2进制数据转换为16进制字符串。
        /// </summary>
        /// <param name="me">源2进制数据</param>
        /// <param name="isAsByteArray">源数据中每个<c>uint</c>是否仅包含8位数据</param>
        /// <returns>编码结果（16进制字符串）</returns>
        /// <remarks>如果<c>isAsByteArray</c>为<c>true</c>，
        /// 每个<c>uint</c>仅8位数据有效，转换为2位<c>HEX</c>。
        /// 如果<c>isAsByteArray</c>为<c>false</c>，
        /// 每个<c>uint</c>含16位有效数据，转换为4位<c>HEX</c>。</remarks>
        public static string HexEncode(this uint[] me, bool isAsByteArray = false)
        {
            if (me == null) { return null; }
            if (me.Length == 0) { return string.Empty; }

            byte[] data = isAsByteArray
                ? (from item in me select (byte) item).ToArray()
                : me.ToByteArray();

            return data.HexEncode();
        }

        /// <summary>
        /// 将用<c>uint[]</c>表示的2进制数据中的一部分转换为16进制字符串。
        /// </summary>
        /// <param name="me">源2进制数据</param>
        /// <param name="start">要转换的数据起始索引</param>
        /// <param name="length">要转换的数据长度</param>
        /// <param name="isAsByteArray">源数据中每个<c>uint</c>是否仅包含8位数据</param>
        /// <returns>编码结果（16进制字符串）</returns>
        /// <remarks>如果<c>isAsByteArray</c>为<c>true</c>，
        /// 每个<c>uint</c>仅8位数据有效，转换为2位<c>HEX</c>。
        /// 如果<c>isAsByteArray</c>为<c>false</c>，
        /// 每个<c>uint</c>含16位有效数据，转换为4位<c>HEX</c>。</remarks>
        public static string HexEncode(this uint[] me, int start, int length, bool isAsByteArray = false)
        {
            if (!IsValidArguments(me, start, ref length)) { return null; }
            if (length == 0) { return string.Empty; }

            uint[] source = new uint[length];
            Array.Copy(me, start, source, 0, length);
            return source.HexEncode(isAsByteArray);
        }

        /// <summary>
        /// 将用<c>ulong[]</c>表示的2进制数据转换为16进制字符串。
        /// </summary>
        /// <param name="me">源2进制数据</param>
        /// <param name="isAsByteArray">源数据中每个<c>ulong</c>是否仅包含8位数据</param>
        /// <returns>编码结果（16进制字符串）</returns>
        /// <remarks>如果<c>isAsByteArray</c>为<c>true</c>，
        /// 每个<c>ulong</c>仅8位数据有效，转换为2位<c>HEX</c>。
        /// 如果<c>isAsByteArray</c>为<c>false</c>，
        /// 每个<c>ulong</c>含16位有效数据，转换为4位<c>HEX</c>。</remarks>
        public static string HexEncode(this ulong[] me, bool isAsByteArray = false)
        {
            if (me == null) { return null; }
            if (me.Length == 0) { return string.Empty; }

            byte[] data = isAsByteArray
                ? (from item in me select (byte) item).ToArray()
                : me.ToByteArray();

            return data.HexEncode();
        }

        /// <summary>
        /// 将用<c>ulong[]</c>表示的2进制数据中的一部分转换为16进制字符串。
        /// </summary>
        /// <param name="me">源2进制数据</param>
        /// <param name="start">要转换的数据起始索引</param>
        /// <param name="length">要转换的数据长度</param>
        /// <param name="isAsByteArray">源数据中每个<c>ulong</c>是否仅包含8位数据</param>
        /// <returns>编码结果（16进制字符串）</returns>
        /// <remarks>如果<c>isAsByteArray</c>为<c>true</c>，
        /// 每个<c>ulong</c>仅8位数据有效，转换为2位<c>HEX</c>。
        /// 如果<c>isAsByteArray</c>为<c>false</c>，
        /// 每个<c>ulong</c>含16位有效数据，转换为4位<c>HEX</c>。</remarks>
        public static string HexEncode(this ulong[] me, int start, int length, bool isAsByteArray = false)
        {
            if (!IsValidArguments(me, start, ref length)) { return null; }
            if (length == 0) { return string.Empty; }

            ulong[] source = new ulong[length];
            Array.Copy(me, start, source, 0, length);
            return source.HexEncode(isAsByteArray);
        }

        /// <summary>
        /// 将字符串按指定的编码转换成字节数据之后再进行HEX编码。
        /// </summary>
        /// <param name="me">源字符串</param>
        /// <param name="encoding">编码对象</param>
        /// <returns>十六进制表示的编码值。
        /// 如果<c>me</c>为<c>null</c>，返回<c>null</c>；
        /// 如果<c>me</c>为空字符串，返回<c>string.Empty</c>。</returns>
        /// <seealso cref="System.Text.Encoding"/>
        public static string HexEncode(this string me, Encoding encoding = null)
        {
            if (string.IsNullOrEmpty(me)) { return me; }
            return (encoding ?? Settings.Global.DefaultEncoding).GetBytes(me).HexEncode();
        }

        /// <summary>
        /// 将字符串按指定的编码转换成字节数据之后再进行HEX编码。
        /// </summary>
        /// <param name="me">源字符串</param>
        /// <param name="encoding">编码名称（代码页名称）</param>
        /// <returns>十六进制表示的编码值。
        /// 如果<c>me</c>为<c>null</c>，返回<c>null</c>；
        /// 如果<c>me</c>为空字符串，返回<c>string.Empty</c>。</returns>
        /// <exception cref="ArgumentException">name不是有效的代码页名称，或平台不支持name所指的代码页。
        /// <see cref="System.Text.Encoding.GetEncoding(string)"/></exception>
        public static string HexEncode(this string me, string encoding)
        {
            if (string.IsNullOrEmpty(me)) { return me; }
            if (string.IsNullOrEmpty(encoding)) { return me.HexEncode(); }
            return Encoding.GetEncoding(encoding).GetBytes(me).HexEncode();
        }

        private static bool IsValidArguments(Array array, int start, ref int length)
        {
            if (array == null) { return false; }
            if (start < 0 || length < 0) { return false; }
            if (start > array.Length) { return false; }

            if (start + length > array.Length) { length = array.Length - start; }
            return true;
        }
    }
}
