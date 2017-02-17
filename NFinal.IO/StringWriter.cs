using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NFinal.IO
{
    public class StringWriter : NFinal.IO.IWriter
    {
        private StringBuilder sb = null;
        public StringWriter()
        {
            sb = new StringBuilder();
        }
        public void Write(byte[] buffer, int offset, int count)
        {
            string value = System.Text.Encoding.UTF8.GetString(buffer,offset,count);
            sb.Append(value);
        }
        public override string ToString()
        {
            return sb.ToString();
        }

        public void Write(string value)
        {
            sb.Append(value);
        }
    }
}
