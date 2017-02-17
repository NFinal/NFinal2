using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NFinal.IO
{
    public class StreamWriter:IWriter,IDisposable
    {
        public System.IO.Stream stream = null;
        public StreamWriter(System.IO.Stream stream)
        {
            this.stream = stream;
        }
        public void Dispose()
        {
            if (this.stream != null)
            {
                this.stream.Dispose();
            }
        }
        public void Write(byte[] buffer, int offset, int count)
        {
            this.stream.Write(buffer, offset, count);
        }
        public void Write(string value)
        {
            byte[] buffer = NFinal.Constant.encoding.GetBytes(value);
            this.stream.Write(buffer, 0, buffer.Length);
        }
    }
}
