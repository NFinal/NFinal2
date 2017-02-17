using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFinal.Lib.Oauth
{
    public class JsonHelper
    {
        /// <summary> 
        /// 将JSON文本转换成数据行 
        /// </summary> 
        /// <param name="jsonText">JSON文本</param> 
        /// <returns>数据行的字典</returns>
        public static IDictionary<string, object> DataRowFromJSON(string jsonText)
        {
            Json.IJsonSerialize serialize = new Json.NewtonsoftJsonSerialize();
            return serialize.DeserializeObject<IDictionary<string, object>>(jsonText);
        }
    }
}