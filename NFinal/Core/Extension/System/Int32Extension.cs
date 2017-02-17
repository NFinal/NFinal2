using System;
using System.Collections.Generic;
using System.Text;

namespace System
{
    public static class Int32Extension
    {
        private static readonly int[] sigment = new int[32] {
            1,2,4,8,16,32,64,128,256,512,
            1024,2048,4096,8192,16384,32768,65536,131072,262144,524288,
            1048576,2097152,4194304,8388608,16777216,33554432,67108864,134217728,268435456,536870912,
            1073741824,-2147483648
        };
        public static bool HasValue(this int value, int bitPosition)
        {
            return (value & sigment[bitPosition]) != 0;
        }
        public static bool HasValue(this int value, params int[] bitPositions)
        {
            int bitNumber = 0;
            foreach (int bp in bitPositions)
            {
                bitNumber |= sigment[bp];
            }
            return (value & bitNumber) != 0;
        }
        public static bool HasAllValue(this int value, params int[] bitPositions)
        {
            int bitNumber = 0;
            foreach (int bp in bitPositions)
            {
                bitNumber |= sigment[bp];
            }
            return (value & bitNumber) == bitNumber;
        }
    }
}
