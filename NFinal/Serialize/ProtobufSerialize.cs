//======================================================================
//
//        Copyright : Zhengzhou Strawberry Computer Technology Co.,LTD.
//        All rights reserved
//        
//        Application:NFinal MVC framework
//        Filename : ProtobufSerialize.cs
//        Description :基于Protobuf的序列化
//
//        created by Lucas at  2015-5-31
//     
//        WebSite:http://www.nfinal.com
//
//======================================================================
using System.IO;
using ProtoBuf;

namespace NFinal
{
    /// <summary>
    /// 序列化
    /// </summary>
    public class ProtobufSerialize : ISerializable
    {
        /// <summary>
        /// 序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public byte[] Serialize<T>(T t)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                Serializer.Serialize<T>(ms, t);
                return ms.ToArray();
            }
        }
        /// <summary>
        /// 反序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="content"></param>
        /// <returns></returns>
        public T Deserialize<T>(byte[] content)
        {
            using (MemoryStream ms = new MemoryStream(content))
            {
                T t = Serializer.Deserialize<T>(ms);
                return t;
            }
        }
    }
}
