using System;
using System.Collections.Generic;
using System.Text;

namespace NFinal.IO
{
    public class StreamWriter:Writer,IDisposable
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
        public override void Write(byte[] buffer, int offset, int count)
        {
            this.stream.Write(buffer, offset, count);
        }
        public override void Write(string value)
        {
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(value);
            this.stream.Write(buffer, 0, buffer.Length);
        }
    }
}
