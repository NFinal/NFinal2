//======================================================================
//
//        Copyright : Zhengzhou Strawberry Computer Technology Co.,LTD.
//        All rights reserved
//
//        Application:NFinal MVC framework
//        Filename :Field.cs
//        Description :数据库字段信息类
//
//        created by Lucas at  2015-6-30`
//
//======================================================================
using System;
using System.Collections.Generic;
using System.Text;

namespace NFinal.Compile.DB.Coding
{
    
    /// <summary>
    /// 数据库字段信息类
    /// </summary>
    public class Field
    {
        public string name;
        public string nameCs;
        public string nameJs;
        public string structFieldName;

        public int length;
        public int octLength;
        public int position;
        public bool allowNull;
        public bool isId;
        public bool hasDefault;
        public string defautlValue;
        public string sqlType;
        public bool isValueType;
        public string csharpType;
        public string jsonType;
        public string GetMethodConvert;
        public string GetMethodName;
        public string GetMethodValue;
        public int dbTypeInt;
        public string dbType;
        public string description;
        //public CsTypeLink csTypeLink;
        public Field()
        {
            
        }
    }
}
