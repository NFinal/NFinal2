using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.MSBuild;

namespace NFinalControllerGenerator.Execute
{
    public class ExecuteMethod
    {
        public static string[] baseType = new string[]{ "SByte", "Byte", "Int16", "UInt16", "Int32"
                , "UInt32", "Int64", "UInt64", "Boolean", "Char", "Decimal"
                , "Double", "Single", "DateTime", "DateTimeOffset"  };
        public bool IsBaseType(ITypeSymbol type)
        {
            int i= type.MatchFullName(baseType
                , new string[] { "System"});
            return i > 0;
        }
        public void WriteMethod(StringWriter sw, SemanticModel model,ClassDeclarationSyntax controllerSyntax, MethodDeclarationSyntax methodSyntax)
        {
            string baseUrl = "";
            List<string> IEnvironmentFilterList = new List<string>();
            List<string> IRequestFilterList = new List<string>();
            List<string> IResponseFilterList = new List<string>();
            foreach (var attrList in controllerSyntax.AttributeLists)
            {
                foreach (var attr in attrList.Attributes)
                {
                    TypeInfo typeInfo= model.GetTypeInfo(attr);
                    foreach (var inter in typeInfo.Type.Interfaces)
                    {
                        if (inter.ToString() == "NFinal.Filter.IEnvironmentFilter")
                        {
                            string code = typeInfo.Type.Name +" _filter"+ attr.Name.ToString() +"=new "+ attr.ToFullString();
                        }
                        else if (inter.ToString() == "NFinal.Filter.IRequestFilter")
                        {

                        }
                        else if (inter.ToString() == "NFinal.Filter.IResponseFilter")
                        {

                        }
                    }

                    //var attrSymbol =model.GetDeclaredSymbol(attr);
                    if (attr.Name.ToString() == "BaseUrl")
                    {
                        var StringLiteral =((LiteralExpressionSyntax)attr.ArgumentList.Arguments[0].Expression);
                        baseUrl = StringLiteral.GetText().ToString().Trim('$','@','\"');
                        continue;
                    }
                }
            }
            var methodSymbol = model.GetDeclaredSymbol(methodSyntax);
            sw.WriteLine("\t\tpublic void Execute(System.Collections.Generic.IDictionary<string,object> env)");
            sw.WriteLine("\t\t{");
            string responseContentType = "text/html ;charset=utf-8";
            string requestMethod = null;
            foreach (var attrList in methodSyntax.AttributeLists)
            {
                foreach (var attr in attrList.Attributes)
                {
                    Regex verbRegex = new Regex("^(Get|Post)?(Html|Empty|Redirect|Json|Css|JavaScript|File|Jpeg|Gif|Png|Xml|Text|Svg)(Zip|Deflate)?$");
                    Match verbMat = verbRegex.Match(attr.Name.ToString());
                    #region GetMethod
                    if (verbMat.Success)
                    {
                        if (verbMat.Groups[2].Success)
                        {
                            if (verbMat.Groups[2].Value == "Html")
                            {
                                responseContentType = "text/html; charset=utf-8";
                            }
                            else if (verbMat.Groups[2].Value == "Empty")
                            {
                                responseContentType = "text/html; charset=utf-8";
                            }
                            else if (verbMat.Groups[2].Value == "Redirect")
                            {
                                responseContentType = "text/html; charset=utf-8";
                            }
                            else if (verbMat.Groups[2].Value == "Json")
                            {
                                responseContentType = "application/json; charset=utf-8";
                            }
                            else if (verbMat.Groups[2].Value == "Css")
                            {
                                responseContentType = "text/css; charset=utf-8";
                            }
                            else if (verbMat.Groups[2].Value == "JavaScript")
                            {
                                responseContentType = "text/javascript; charset=utf-8";
                            }
                            else if (verbMat.Groups[2].Value == "File")
                            {
                                responseContentType = "application/octet-stream";
                            }
                            else if (verbMat.Groups[2].Value == "Jpeg")
                            {
                                responseContentType = "image/jpeg";
                            }
                            else if (verbMat.Groups[2].Value == "Gif")
                            {
                                responseContentType = "image/gif";
                            }
                            else if (verbMat.Groups[2].Value == "Png")
                            {
                                responseContentType = "image/png";
                            }
                            else if (verbMat.Groups[2].Value == "Xml")
                            {
                                responseContentType = "text/xml; charset=utf-8";
                            }
                            else if (verbMat.Groups[2].Value == "Text")
                            {
                                responseContentType = "text/plain; charset=utf-8";
                            }
                            else if (verbMat.Groups[2].Value == "Svg")
                            {
                                responseContentType = "image/svg+xml";
                            }
                            else
                            {
                                responseContentType = "text/html; charset=utf-8";
                            }
                        }
                        if(verbMat.Groups[1].Success)
                        {
                            requestMethod = verbMat.Groups[1].Value;
                        }
                    }
                    #endregion
                }
            }
            sw.WriteLine("\t\t\tthis.EnvironmentFilter(env);");
            sw.WriteLine("\t\t\tIDictionary<string, string[]> headers = env.GetResponseHeaders();");
            sw.Write("\t\t\theaders.AddValue(\"Content-Type\", new string[] { \"");
            sw.Write(responseContentType);
            sw.WriteLine("\" });");
            if (!string.IsNullOrEmpty(requestMethod))
            {
                if (requestMethod == "Post")
                {
                    sw.Write("\t\t\t");
                    sw.WriteLine("if (env.GetMethodType() != NFinal.MethodType.POST)");
                }
                else if (requestMethod == "Get")
                {
                    sw.Write("\t\t\t");
                    sw.WriteLine("if (env.GetMethodType() != NFinal.MethodType.GET)");
                }
                sw.Write("\t\t\t");
                sw.WriteLine("{");
                sw.Write("\t\t\t");
                sw.WriteLine("\tenv.SetResponseStatusCode(405);");
                sw.Write("\t\t\t");
                sw.WriteLine("\treturn;");
                sw.Write("\t\t\t");
                sw.WriteLine("}");
            }
            sw.Write("\t\t\t");
            sw.WriteLine("NFinal.Owin.Request request = env.GetRequest();");
            sw.Write("\t\t\t");
            sw.WriteLine("this.RequestFilter(request)");
            IParameterSymbol parameterSymbol = null;
            INamedTypeSymbol parameterType = null;
            bool isNullable = false;
            foreach (var parameter in methodSyntax.ParameterList.Parameters)
            {
                parameterSymbol =model.GetDeclaredSymbol(parameter);
                parameterType = (INamedTypeSymbol)parameterSymbol.Type;
                string tempType = parameterType.MetadataName;
                if (tempType == "Nullable`1")
                {
                    isNullable = true;
                    parameterType =  (INamedTypeSymbol)parameterType.TypeArguments[0];
                }
                if (parameterType.MatchFullName(baseType,new string[] { "System"})>0)
                {
                    sw.Write("\t\t\t");
                    sw.Write(parameterSymbol.Type);
                    sw.Write(" ");
                    sw.Write(parameterSymbol.Name);
                    sw.Write("=");
                    sw.Write("request.get[\"");
                    sw.Write(parameterSymbol.Name);
                    sw.WriteLine("\"].AsVar();");
                }
                else
                {
                    sw.Write("\t\t\t");
                    sw.Write(parameterSymbol.Type);
                    sw.Write(" ");
                    sw.Write(parameterSymbol.Name);
                    sw.Write("=new ");
                    sw.Write(parameterSymbol.Type);
                    sw.WriteLine("();");
                    sw.Write("\t\t\t");
                    sw.Write(parameterSymbol.Type);
                    sw.Write(".TryParse(request.get,out ");
                    sw.Write(parameterSymbol.Name);
                    sw.WriteLine(");");
                }
            }
            sw.Write("\t\t\t");
            sw.Write("this.");
            sw.Write(methodSymbol.Name);
            sw.Write("(");
            bool isFirst = true;
            foreach (var parameter in methodSyntax.ParameterList.Parameters)
            {
                parameterSymbol = model.GetDeclaredSymbol(parameter);
                if (parameterSymbol.IsDefinition)
                {
                    if (isFirst)
                    {
                        isFirst = false;
                    }
                    else
                    {
                        sw.Write(", ");
                    } 
                    sw.Write(parameterSymbol.Name);
                }
            }
            sw.WriteLine(");");
            sw.Write("\t\t\t");
            sw.WriteLine("this.ResponseFilter(response);");
            sw.WriteLine("\t\t}");
        }
    }
}
