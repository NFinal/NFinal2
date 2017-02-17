using System;

// ReSharper disable CheckNamespace
namespace NFinal.Advanced
{
    /// <summary>
    /// 对若干基本数据类型及其对应的数组类型进行扩展，提供将这些类型的数据转换
    /// 为<c>byte[]</c>的扩展方法。
    /// </summary>
    public static partial class ByteArrayUtil
    {
        private static readonly byte[] EmptyByteArray = new byte[0];

        // 转换单值
        private static T ToValue<T>(this byte[] bytes, int start, bool isLittleEndian,
            Func<byte[], int, T> convert, int dataSize)
        {
            if (isLittleEndian == BitConverter.IsLittleEndian)
            {
                return convert(bytes, start);
            }
            else
            {
                byte[] data = new byte[dataSize];
                Array.Copy(bytes, start, data, 0, dataSize);
                Array.Reverse(data);
                return convert(data, 0);
            }
        }

        // 转换值数组
        private static T[] ToValueArray<T>(byte[] bytes, int start, int length,
            bool isLittleEndian, Func<byte[], int, T> convert, int dataSize)
        {
            if (bytes == null) { return null; }
            if (bytes.Length == 0 && length == 0) { return new T[0]; }

            if (start < 0 || start + length > bytes.Length)
            {
                throw new ArgumentException("start不是有效的索引位", "start");
            }

            if (length < 0 || length % dataSize != 0)
            {
                throw new ArgumentException(string.Format("length必须是能被{0}整除的非负整数", dataSize), "length");
            }

            // 如果isLittleEndian与BitConverter.IsLittleEndian不一致，
            // 需要将数组反转，然后从后往前按dataSize分组进行转换，
            // 转换结果顺序保存，以下3个变量在这种情况下需要进相关计算
            byte[] buffer = bytes;      // 缓冲，如果需要反转，一定得从bytes拷贝过去，否则会修改原数据
            int offset = start;         // 字符数组中的偏移位置，每次计算偏移位置往后的2个字节
            int step = dataSize;        // 反转的情况下需要置为负数

            if (isLittleEndian != BitConverter.IsLittleEndian)
            {
                // 反转源数据
                buffer = new byte[length];
                Array.Copy(bytes, start, buffer, 0, length);
                Array.Reverse(buffer);

                // 重新计算起始偏移位置和步长
                offset = length - dataSize;
                step = -dataSize;
            }

            // count是返回的数据元素个数
            // 前面通过检查length必须被dataSize整除，已经保证了count的正确性
            int count = length / dataSize;
            T[] result = new T[count];

            for (int i = 0; i < count; i++)
            {
                result[i] = convert(buffer, offset);
                offset += step;
            }

            return result;
        }
    }
}
