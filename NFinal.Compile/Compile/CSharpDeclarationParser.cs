//======================================================================
//
//        Copyright : Zhengzhou Strawberry Computer Technology Co.,LTD.
//        All rights reserved
//
//        Application:NFinal MVC framework
//        Filename :CSharpDeclaration.cs
//        Description :csharp中参数解析类
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
    /// <summary>
    /// csharp中参数解析类
    /// </summary>
    public class CSharpDeclarationParser
    {
        public System.Collections.Generic.List<CSharpDeclaration> Parse(string csharpCode)
        {
            string partern = @"(?:\s*//([^\r\n]*?)\s*)?((?:[_0-9a-zA-Z\.]+)\s*(?:\[\s*\]|\<\s*[_0-9a-zA-Z\.]+\s*\>|\<\s*[_0-9a-zA-Z\.]+\s*,\s*[_0-9a-zA-Z\.]+\s*\>)?)\s+([_0-9a-zA-Z]+)\s*(?:;|=([^;]+);)";
            Regex reg = new Regex(partern);
            MatchCollection mac = reg.Matches(csharpCode);
            System.Collections.Generic.List<CSharpDeclaration> declarations = new System.Collections.Generic.List<CSharpDeclaration>();
            CSharpDeclaration declaration = null; 
            string varName=string.Empty;
            bool hasValue=false;
            if (mac.Count > 0)
            {
                for(int i=0;i<mac.Count;i++)
                {
                    varName=mac[i].Groups[3].Value;
                    hasValue=false;
                    for(int j=0;j<declarations.Count;j++)
                    {
                        if(declarations[j].varName==varName)
                        {
                            hasValue=true;
                        }
                    }
                    if(!hasValue)
                    {
                        //排除命名空间引入
                        if (mac[i].Groups[2].Value != "using")
                        {
                            declaration = new CSharpDeclaration();
                            declaration.comment = mac[i].Groups[1].Value;
                            declaration.typeName = mac[i].Groups[2].Value;
                            declaration.varName = mac[i].Groups[3].Value;
                            if (mac[i].Groups[4].Success)
                            {
                                declaration.expression = mac[i].Groups[4].Value;
                            }
                            else
                            {
                                declaration.expression = null;
                            }
                            if (declaration.typeName != "var")
                            {
                                declarations.Add(declaration);
                            }
                        }
                    }
                }
            }
            return declarations;
        }
    }
}