//======================================================================
//
//        Copyright : Zhengzhou Strawberry Computer Technology Co.,LTD.
//        All rights reserved
//
//        Application:NFinal MVC framework
//        Filename :CSharpDeclaration.cs
//        Description :Csharp代码分析类
//
//        created by Lucas at  2015-6-30`
//     
//        WebSite:http://www.nfinal.com
//
//======================================================================
using System;
using System.Collections.Generic;
using System.Web;
using System.Text.RegularExpressions;

namespace NFinal.Compile
{
    public class CSharpDeclaration
    {
        /// <summary>
        /// 注释
        /// </summary>
        public string comment;
        /// <summary>
        /// 类型
        /// </summary>
        public string typeName;
        /// <summary>
        /// 是否是字符串类型
        /// </summary>
        public bool isString;
        /// <summary>
        /// 编译器内部类型
        /// </summary>
        public string compileTypeName;
        /// <summary>
        /// 变量名
        /// </summary>
        public string varName;
        /// <summary>
        /// 表达式
        /// </summary>
        public string expression;
        /// <summary>
        /// 转换函数
        /// </summary>
        public string covertMethod;
    }
}