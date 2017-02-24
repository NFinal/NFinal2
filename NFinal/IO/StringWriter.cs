using System;
using System.Collections.Generic;
using System.Text;

namespace NFinal.IO
{
    public class StringWriter : Writer
    {
        private StringBuilder sb = null;
        public StringWriter()
        {
            sb = new StringBuilder();
        }
        public override void Write(byte[] buffer, int offset, int count)
        {
            string value = System.Text.Encoding.UTF8.GetString(buffer,offset,count);
            sb.Append(value);
        }
        public override string ToString()
        {
            return sb.ToString();
        }

        public override void Write(string value)
        {
            sb.Append(value);
        }
    }
}
