using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NFinal.Core.Serialize
{
    public class HproseSerialize : ISerializable
    {
        public T Deserialize<T>(byte[] content)
        {
            return Hprose.IO.HproseFormatter.Unserialize<T>(content);
        }

        public byte[] Serialize<T>(T t)
        {
            return Hprose.IO.HproseFormatter.Serialize(t, Hprose.IO.HproseMode.FieldMode).ToArray();
        }
    }
}
