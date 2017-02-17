// 通过模板 ByteArrayUtil_Partial.template 自动生成的代码，请勿手工修改！
// 需要 ../build/ByteArrayUtil.targets 通过 MsBuild 来生成。依赖 MsBuildTasks 库。

using System;

// ReSharper disable CheckNamespace
namespace NFinal.Advanced
{
    partial class ByteArrayUtil
    {
        /// <summary>
        /// 将字节数据串中指定的4个字节转换为一个<c>int</c>值。
        /// </summary>
        /// <param name="bytes">字节数据串</param>
        /// <param name="start">指定从<c>start</c>开始的4个字节</param>
        /// <param name="isLittleEndian">true表示最大有效字节在右端，false表示最大有效字节在左端</param>
        /// <returns>由字节串转换得到的<c>int</c>值</returns>
        /// <exception cref="ArgumentNullException">字节数据为null</exception>
        /// <exception cref="ArgumentException"><c>bytes</c>从<c>start</c>开始不足4个字节</exception>
        public static int ToInt32(this byte[] bytes, int start, bool isLittleEndian)
        {
            return ToValue<int>(bytes, start, isLittleEndian, BitConverter.ToInt32, 4);
        }

        /// <summary>
        /// 按照 <see cref="P:Settings.Global.IsLittleEndian" /> 指定的默认 Endian 序，
        /// 将字节数据串中指定的4个字节转换为一个<c>int</c>值。
        /// </summary>
        /// <param name="bytes">字节数据串</param>
        /// <param name="start">指定从<c>start</c>开始的4个字节</param>
        /// <returns>由字节串转换得到的<c>int</c>值</returns>
        /// <exception cref="ArgumentNullException">字节数据为null</exception>
        /// <exception cref="ArgumentException"><c>bytes</c>从<c>start</c>开始不足4个字节</exception>
        public static int ToInt32(this byte[] bytes, int start = 0)
        {
            return ToInt32(bytes, start, Settings.Global.IsLittleEndian);
        }

        /// <summary>
        /// 将字节数据串以4个字节为一组，转换为<c>int[]</c>数据。
        /// </summary>
        /// <param name="bytes">字节数据串</param>
        /// <param name="start">指定从<c>start</c>开始的4的倍数个字节</param>
        /// <param name="length">从<c>start</c>开始的<c>length</c>个字节会参与转换。
        /// <c>length</c>必须是4的倍数。
        /// 如果<c>length</c>小于0，则使用从<c>start</c>开始的所有剩余字节。</param>
        /// <param name="isLittleEndian">true表示最大有效字节在右端，false表示最大有效字节在左端</param>
        /// <returns>转换生成的<c>int[]</c>数据</returns>
        /// <exception cref="ArgumentNullException">字节数据为null</exception>
        /// <exception cref="ArgumentException">start小于0，length小于0，
        /// 或参与转换的字节数不是0或4的倍数</exception>
        public static int[] ToInt32(this byte[] bytes, int start, int length, bool isLittleEndian)
        {
            return ToValueArray<int>(bytes, start, length, isLittleEndian, BitConverter.ToInt32, 4);
        }

        /// <summary>
        /// 按照 <see cref="P:Settings.Global.IsLittleEndian" /> 指定的默认 Endian 序，
        /// 将字节数据串以4个字节为一组，转换为<c>int[]</c>数据。
        /// </summary>
        /// <param name="bytes">字节数据串</param>
        /// <param name="start">指定从<c>start</c>开始的4的倍数个字节</param>
        /// <param name="length">从<c>start</c>开始的<c>length</c>个字节会参与转换。
        /// <c>length</c>必须是4的倍数。
        /// 如果<c>length</c>小于0，则使用从<c>start</c>开始的所有剩余字节。</param>
        /// <returns>转换生成的<c>int[]</c>数据</returns>
        /// <exception cref="ArgumentNullException">字节数据为null</exception>
        /// <exception cref="ArgumentException">start小于0，length小于0，
        /// 或参与转换的字节数不是0或4的倍数</exception>
        public static int[] ToInt32(this byte[] bytes, int start, int length)
        {
            return ToInt32(bytes, start, length, Settings.Global.IsLittleEndian);
        }

        /// <summary>
        /// 将int类型转换为长度为4的字节数组
        /// </summary>
        /// <param name="value">源数据</param>
        /// <param name="isLittleEndian">true表示最大有效字节在右端，false表示最大有效字节在左端</param>
        /// <returns>字节数组表示的数据</returns>
        public static byte[] ToByteArray(this int value, bool isLittleEndian)
        {
            if (BitConverter.IsLittleEndian == isLittleEndian)
            {
                return BitConverter.GetBytes(value);
            }
            else
            {
                byte[] data = BitConverter.GetBytes(value);
                Array.Reverse(data);
                return data;
            }
        }

        /// <summary>
        /// 按照 <see cref="P:Settings.Global.IsLittleEndian" /> 指定的默认 Endian 序，
        /// 将int类型转换为长度为4的字节数组
        /// </summary>
        /// <param name="value">源数据</param>
        /// <returns>字节数组表示的数据</returns>
        public static byte[] ToByteArray(this int value) {
            return ToByteArray(value, Settings.Global.IsLittleEndian);
        }

        /// <summary>
        /// 将int数组转换为长度为4*n的字节数组
        /// </summary>
        /// <param name="array">源数据</param>
        /// <param name="isLittleEndian">true表示最大有效字节在右端，false表示最大有效字节在左端</param>
        /// <returns>字节数组表示的数据</returns>
        public static byte[] ToByteArray(this int[] array, bool isLittleEndian)
        {
            const int STEP = 4;

            if (array == null) { return null; }
            if (array.Length == 0) { return EmptyByteArray; }

            byte[] bytes = new byte[array.Length * STEP];
            int offset = 0;
            int step = STEP;

            if (BitConverter.IsLittleEndian != isLittleEndian)
            {
                // 需要Reverse数组
                offset = bytes.Length - STEP;
                step = -STEP;
            }

            for (int i = 0; i < array.Length; i++)
            {
                Array.Copy(BitConverter.GetBytes(array[i]), 0, bytes, offset, STEP);
                offset += step;
            }

            if (step < 0) { Array.Reverse(bytes); }

            return bytes;
        }

        /// <summary>
        /// 按照 <see cref="P:Settings.Global.IsLittleEndian" /> 指定的默认 Endian 序，
        /// 将int数组转换为长度为4*n的字节数组
        /// </summary>
        /// <param name="array">源数据</param>
        /// <returns>字节数组表示的数据</returns>
        public static byte[] ToByteArray(this int[] array) {
            return ToByteArray(array, Settings.Global.IsLittleEndian);
        }
    }
}
