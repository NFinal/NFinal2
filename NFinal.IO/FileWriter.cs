using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace NFinal.IO
{
    public class FileWriter : IWriter,IDisposable
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
        public void Write(string value)
        {
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(value);
            this.stream.Write(buffer, 0, buffer.Length);
        }
        public void Write(byte[] buffer, int offset, int count)
        {
            this.stream.Write(buffer, offset, count);
        }
    }
}
