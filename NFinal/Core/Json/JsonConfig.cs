using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace NFinal
{
    public class JsonConfig
    {
        public static bool hasRead = false;
        public static json _data;
        public static JsonObject Get(string json)
        {
            if (!hasRead)
            {
                hasRead = true;
                //json = DeleteComment(json);
                Json.IJsonSerialize serialize = new Json.NewtonsoftJsonSerialize();
                serialize.DeserializeObject<Config.config>(json);
                JsonObject _data= SimpleJson.DeserializeObject(json) as JsonObject;
            }
            return _data;
        }
        public static string DeleteComment(string json)
        {
            Regex multiCommentPattern = new Regex("/\\*.*?\\*/", RegexOptions.IgnoreCase | RegexOptions.Singleline);
            Regex signleCommentPattern = new Regex("//.*");
            MatchCollection multiCommentCollection = multiCommentPattern.Matches(json);
            if (multiCommentCollection.Count > 0)
            {
                for (int i = multiCommentCollection.Count - 1; i >= 0; i--)
                {
                    json = json.Remove(multiCommentCollection[i].Index, multiCommentCollection[i].Length);
                }
            }
            MatchCollection signleCommentCollection = signleCommentPattern.Matches(json);
            if (signleCommentCollection.Count > 0)
            {
                for (int i = signleCommentCollection.Count - 1; i >= 0; i--)
                {
                    json = json.Remove(signleCommentCollection[i].Index,signleCommentCollection[i].Length);
                }
            }
            return json;
        }
    }
}
