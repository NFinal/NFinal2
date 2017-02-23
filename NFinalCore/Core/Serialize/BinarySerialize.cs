//using System.IO;
//using System.Runtime.Serialization.Formatters.Binary;
//namespace NFinal
//{
//    /// <summary>
//    /// 序列化
//    /// </summary>
//    public class BinarySerialize : ISerializable
//    {
//        /// <summary>
//        /// 序列化
//        /// </summary>
//        /// <typeparam name="T"></typeparam>
//        /// <param name="t"></param>
//        /// <returns></returns>
//        public byte[] Serialize<T>(T t)
//        {
//            if (t == null)
//            {
//                return null;
//            }

//            BinaryFormatter binaryFormatter = new BinaryFormatter();
//            using (MemoryStream memoryStream = new MemoryStream())
//            {
//                binaryFormatter.Serialize(memoryStream, t);
//                byte[] objectDataAsStream = memoryStream.ToArray();
//                return objectDataAsStream;
//            }
//        }
//        /// <summary>
//        /// 反序列化
//        /// </summary>
//        /// <typeparam name="T"></typeparam>
//        /// <param name="content"></param>
//        /// <returns></returns>
//        public T Deserialize<T>(byte[] content)
//        {
//            if (content == null)
//            {
//                return default(T);
//            }

//            BinaryFormatter binaryFormatter = new BinaryFormatter();
//            using (MemoryStream memoryStream = new MemoryStream(content))
//            {
//                T result = (T)binaryFormatter.Deserialize(memoryStream);
//                return result;
//            }
//        }
//    }
//}