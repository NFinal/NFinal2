// 通过模板 ByteArrayUtil_Partial.template 自动生成的代码，请勿手工修改！
// 需要 ../build/ByteArrayUtil.targets 通过 MsBuild 来生成。依赖 MsBuildTasks 库。

using System;

// ReSharper disable CheckNamespace
namespace NFinal.Advanced
{
    partial class ByteArrayUtil
    {
        /// <summary>
        /// 将字节数据串中指定的2个字节转换为一个<c>short</c>值。
        /// </summary>
        /// <param name="bytes">字节数据串</param>
        /// <param name="start">指定从<c>start</c>开始的2个字节</param>
        /// <param name="isLittleEndian">true表示最大有效字节在右端，false表示最大有效字节在左端</param>
        /// <returns>由字节串转换得到的<c>short</c>值</returns>
        /// <exception cref="ArgumentNullException">字节数据为null</exception>
        /// <exception cref="ArgumentException"><c>bytes</c>从<c>start</c>开始不足2个字节</exception>
        public static short ToInt16(this byte[] bytes, int start, bool isLittleEndian)
        {
            return ToValue<short>(bytes, start, isLittleEndian, BitConverter.ToInt16, 2);
        }

        /// <summary>
        /// 按照 <see cref="P:Settings.Global.IsLittleEndian" /> 指定的默认 Endian 序，
        /// 将字节数据串中指定的2个字节转换为一个<c>short</c>值。
        /// </summary>
        /// <param name="bytes">字节数据串</param>
        /// <param name="start">指定从<c>start</c>开始的2个字节</param>
        /// <returns>由字节串转换得到的<c>short</c>值</returns>
        /// <exception cref="ArgumentNullException">字节数据为null</exception>
        /// <exception cref="ArgumentException"><c>bytes</c>从<c>start</c>开始不足2个字节</exception>
        public static short ToInt16(this byte[] bytes, int start = 0)
        {
            return ToInt16(bytes, start, Settings.Global.IsLittleEndian);
        }

        /// <summary>
        /// 将字节数据串以2个字节为一组，转换为<c>short[]</c>数据。
        /// </summary>
        /// <param name="bytes">字节数据串</param>
        /// <param name="start">指定从<c>start</c>开始的2的倍数个字节</param>
        /// <param name="length">从<c>start</c>开始的<c>length</c>个字节会参与转换。
        /// <c>length</c>必须是2的倍数。
        /// 如果<c>length</c>小于0，则使用从<c>start</c>开始的所有剩余字节。</param>
        /// <param name="isLittleEndian">true表示最大有效字节在右端，false表示最大有效字节在左端</param>
        /// <returns>转换生成的<c>short[]</c>数据</returns>
        /// <exception cref="ArgumentNullException">字节数据为null</exception>
        /// <exception cref="ArgumentException">start小于0，length小于0，
        /// 或参与转换的字节数不是0或2的倍数</exception>
        public static short[] ToInt16(this byte[] bytes, int start, int length, bool isLittleEndian)
        {
            return ToValueArray<short>(bytes, start, length, isLittleEndian, BitConverter.ToInt16, 2);
        }

        /// <summary>
        /// 按照 <see cref="P:Settings.Global.IsLittleEndian" /> 指定的默认 Endian 序，
        /// 将字节数据串以2个字节为一组，转换为<c>short[]</c>数据。
        /// </summary>
        /// <param name="bytes">字节数据串</param>
        /// <param name="start">指定从<c>start</c>开始的2的倍数个字节</param>
        /// <param name="length">从<c>start</c>开始的<c>length</c>个字节会参与转换。
        /// <c>length</c>必须是2的倍数。
        /// 如果<c>length</c>小于0，则使用从<c>start</c>开始的所有剩余字节。</param>
        /// <returns>转换生成的<c>short[]</c>数据</returns>
        /// <exception cref="ArgumentNullException">字节数据为null</exception>
        /// <exception cref="ArgumentException">start小于0，length小于0，
        /// 或参与转换的字节数不是0或2的倍数</exception>
        public static short[] ToInt16(this byte[] bytes, int start, int length)
        {
            return ToInt16(bytes, start, length, Settings.Global.IsLittleEndian);
        }

        /// <summary>
        /// 将short类型转换为长度为2的字节数组
        /// </summary>
        /// <param name="value">源数据</param>
        /// <param name="isLittleEndian">true表示最大有效字节在右端，false表示最大有效字节在左端</param>
        /// <returns>字节数组表示的数据</returns>
        public static byte[] ToByteArray(this short value, bool isLittleEndian)
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
        /// 将short类型转换为长度为2的字节数组
        /// </summary>
        /// <param name="value">源数据</param>
        /// <returns>字节数组表示的数据</returns>
        public static byte[] ToByteArray(this short value) {
            return ToByteArray(value, Settings.Global.IsLittleEndian);
        }

        /// <summary>
        /// 将short数组转换为长度为2*n的字节数组
        /// </summary>
        /// <param name="array">源数据</param>
        /// <param name="isLittleEndian">true表示最大有效字节在右端，false表示最大有效字节在左端</param>
        /// <returns>字节数组表示的数据</returns>
        public static byte[] ToByteArray(this short[] array, bool isLittleEndian)
        {
            const int STEP = 2;

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
        /// 将short数组转换为长度为2*n的字节数组
        /// </summary>
        /// <param name="array">源数据</param>
        /// <returns>字节数组表示的数据</returns>
        public static byte[] ToByteArray(this short[] array) {
            return ToByteArray(array, Settings.Global.IsLittleEndian);
        }
    }
}
