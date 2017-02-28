using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ProtoBuf;

namespace NFinal.Owin
{
    //int,statusCode
    //int,headers的数量，即headers.Count
    //int,header的名称的长度
    //  string,HeaderName,即header的名称
    //  int,headerValues的数量
    //      int,headerValue的长度
    //      string,headerValue的值
    //int,stream的长度
    //byte[] stream的值
    [ProtoContract]
    public class Response
    {
        public Response()
        {
            this.headers = new Dictionary<string, string[]>();
            this.statusCode = 200;
            this.stream = new MemoryStream();
        }
        private static readonly Encoding encoding=new System.Text.UnicodeEncoding(false,false);
        [ProtoMember(1)]
        public IDictionary<string, string[]> headers;
        [ProtoMember(2)]
        public Stream stream;
        [ProtoMember(3)]
        public int statusCode;
        private static void WriteInt32(MemoryStream stream, int count)
        {
            byte[] buffer = BitConverter.GetBytes(count);
            stream.Write(buffer, 0, buffer.Length);
            //stream.WriteByte((byte)count);
            //stream.WriteByte((byte)(count >> 8));
            //stream.WriteByte((byte)(count >> 16));
            //stream.WriteByte((byte)(count >> 24));
        }
        /// <summary>
        /// 序列化
        /// </summary>
        /// <returns></returns>
        public byte[] GetStream()
        {
            byte[] buffer = null;
            using (MemoryStream memoryStream = new MemoryStream())
            {
                WriteInt32(memoryStream, statusCode);
                WriteInt32(memoryStream, headers.Count);
                foreach (var header in headers)
                {
                    buffer = encoding.GetBytes(header.Key);
                    WriteInt32(memoryStream, buffer.Length);
                    memoryStream.Write(buffer, 0, buffer.Length);
                    WriteInt32(memoryStream, header.Value.Length);
                    foreach (var value in header.Value)
                    {
                        buffer = encoding.GetBytes(value);
                        WriteInt32(memoryStream, buffer.Length);
                        memoryStream.Write(buffer, 0, buffer.Length);
                    }
                }
                WriteInt32(memoryStream, (int)stream.Length);
                buffer = new byte[stream.Length];
                stream.Seek(0, SeekOrigin.Begin);
                stream.Read(buffer, 0, buffer.Length);
                stream.Seek(0, SeekOrigin.Begin);
                memoryStream.Write(buffer, 0, buffer.Length);
                buffer = memoryStream.ToBytes();
            }
            return buffer;
        }
        /// <summary>
        /// 反序列化
        /// </summary>
        /// <param name="buffer"></param>
        public void GetResponse(byte[] buffer)
        {
            int startIndex = 0;
            int statusCode= BitConverter.ToInt32(buffer, startIndex);
            startIndex += 4;
            this.statusCode = statusCode;
            int headerCount = BitConverter.ToInt32(buffer, startIndex);
            this.headers = new Dictionary<string, string[]>(headerCount);
            string key;
            string[] values;
            for (int i = 0; i < headerCount; i++)
            {
                startIndex += 4;
                int keyLength = BitConverter.ToInt32(buffer, startIndex);
                startIndex += 4;
                key = encoding.GetString(buffer, startIndex, keyLength);
                startIndex += keyLength;
                int valuesCount = BitConverter.ToInt32(buffer, startIndex);
                startIndex += 4;
                values = new string[valuesCount];
                for (int j = 0; j < valuesCount; j++)
                {
                    int valueLength = BitConverter.ToInt32(buffer, startIndex);
                    startIndex += 4;
                    values[j] = encoding.GetString(buffer, startIndex, valueLength);
                    startIndex += valueLength;
                }
                headers.Add(key, values);
            }
            int streamLength = BitConverter.ToInt32(buffer,startIndex);
            startIndex += 4;
            this.stream.Write(buffer, startIndex, streamLength);
            this.stream.Flush();
        }
    }
}
