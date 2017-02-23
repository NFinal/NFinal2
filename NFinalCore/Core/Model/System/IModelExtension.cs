using System;
using System.Collections.Generic;
using System.IO;
using NFinal;

namespace System
{
    public static class IModelExtension
    {
        public static void AddNewField(this System.Collections.Generic.List<IModel> modelList, string fieldName, Type t)
        {

        }
        public static string ToJson(this System.Collections.Generic.List<IModel> modelList)
        {
            return modelList.ToJson();
        }
        
        public static void AddNewField<T>(this System.Collections.Generic.List<T> model, string fieldName,Type t) where T:IModel
        {

        }
        public static string ToJson(this IModel model)
        {
            StringWriter sw = new StringWriter();
            model.WriteJson(sw);
            return sw.ToString();
        }
        public static void WriteJson<T>(this System.Collections.Generic.List<T> modelList, System.IO.TextWriter tw, bool addBracket = true) where T:IModel
        {
            if (modelList == null)
            {
                tw.Write(Constant.nullString);
            }
            else
            {
                if (addBracket)
                {
                    tw.Write(Constant.openingBracket);
                }
                bool isFirst = true;
                foreach (NFinal.IModel model in modelList)
                {
                    if (!isFirst)
                    {
                        tw.Write(Constant.comma);
                    }
                    if (isFirst)
                    {
                        isFirst = false;
                    }
                    if (model.__Is_Null__)
                    {
                        tw.Write(Constant.nullString);
                    }
                    else
                    {
                        model.WriteJson(tw);
                    }
                }
                if (addBracket)
                {
                    tw.Write(Constant.closingBracket);
                }
            }
        }
        public static string ToJson<T>(this System.Collections.Generic.List<T> modelList) where T : IModel
        {
            StringWriter sw = new StringWriter();
            modelList.WriteJson(sw, true);
            return sw.ToString();
        }
    }
}
