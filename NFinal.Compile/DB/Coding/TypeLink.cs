//======================================================================
//
//        Copyright : Zhengzhou Strawberry Computer Technology Co.,LTD.
//        All rights reserved
//
//        Application:NFinal MVC framework
//        Filename :TypeLink.cs
//        Description :数据库字段对应类型
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
    /// 字段类型
    /// </summary>
    public struct TypeLink
    {
        //数据库中的类型
        public string sqlType;
        //csharp 基本类型
        public bool isValueType;
        public string csharpType;
        //reader.GetMethod();
        public string GetMethodConvert;
        public string GetMethodName;
        public string GetMethodValue;
        //csharp自定义的数据库枚举类型
        public int dbTypeInt;
        public string dbType;
        //数据转换时所需的类型
        public string jsonType;
    }
}
