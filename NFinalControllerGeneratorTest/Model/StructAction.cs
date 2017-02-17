using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace NFinalControllerGenerator.Model
{
    public class StructAction
    {
        public string filePath;
        public string fileName;
        public string defaultNameSpace;
        public IMethodSymbol methodSymbol;
        public SemanticModel model;
        public MethodDeclarationSyntax syntax;
        public string url;
        public string actionUrl;
        public string GetUrlFunction;
        public string ExecuteFunction;
        public string MainMethod;
    }
}
