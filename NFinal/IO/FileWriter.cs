using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace NFinal.IO
{
    public class FileWriter : Writer,IDisposable
    {
        public System.IO.Stream stream = null;
        public FileWriter(string path)
        {
            this.stream = new FileStream(path,FileMode.Create);
        }
        public void Dispose()
        {
            if (this.stream != null)
            {
                this.stream.Dispose();
            }
        }
        public override void Write(string value)
        {
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(value);
            this.stream.Write(buffer, 0, buffer.Length);
        }
        public override void Write(byte[] buffer, int offset, int count)
        {
            this.stream.Write(buffer, offset, count);
        }
    }
}
