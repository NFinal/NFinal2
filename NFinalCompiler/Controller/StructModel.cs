using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace NFinalCompiler.Controller
{
    public class StructModel
    {
        public bool IsNotController(string actionName)
        {
            string[] SpecailNames = { "Before", "After", "EnvironmentFilter", "RequestFilter", "ResponseFilter" };
            bool hasName = false;
            foreach (string sn in SpecailNames)
            {
                if (sn == actionName)
                {
                    hasName = true;
                }
            }
            return hasName;
        }
        public void WriteDocument(TextWriter sw, SyntaxTree tree, SemanticModel model)
        {
            SyntaxNode root = tree.GetRoot();
            var controllers = root.DescendantNodes().OfType<ClassDeclarationSyntax>();

            if (controllers.Count() > 0)
            {
                var controller = controllers.First();
                var controllerSymbol = model.GetDeclaredSymbol(controller);
                Dictionary<string, DeclareData> filesAndPropertys = new Dictionary<string, DeclareData>();
                int level = 0;
                GetFieldsUtility.GetAllFieldsAndPropertys(controllerSymbol, level, ref filesAndPropertys);
                var rootUnit = (CompilationUnitSyntax)root;
                foreach (var us in rootUnit.Usings)
                {
                    sw.Write(us.ToFullString());
                }
                sw.WriteLine("//此代码由NFinalCompiler生成。");
                sw.WriteLine("//http://bbs.nfinal.com");
                sw.Write("namespace ");
                sw.Write(controllerSymbol.ContainingNamespace.ToString());
                sw.Write(".");
                sw.Write(controllerSymbol.Name);
                sw.WriteLine("_Model");
                //sw.WriteLine();
                sw.WriteLine("{");

                var actions = root.DescendantNodes().OfType<MethodDeclarationSyntax>();
                string viewBagTypeName = null;
                string structName = null;
                foreach (var action in actions)
                {
                    var actionSymbol = model.GetDeclaredSymbol(action);
                    if (actionSymbol.IsStatic || actionSymbol.IsVirtual
                        || actionSymbol.IsOverride || actionSymbol.IsSealed
                        || actionSymbol.DeclaredAccessibility != Accessibility.Public)
                    {
                        continue;
                    }
                    Dictionary<string, DeclareData> methodFields = new Dictionary<string, DeclareData>();
                    Dictionary<string, DeclareData> ViewBagFields = new Dictionary<string, DeclareData>();
                    foreach (var fap in filesAndPropertys)
                    {
                        ViewBagFields.Add(fap.Key, fap.Value);
                    }

                    structName = actionSymbol.Name;


                    GetFieldsUtility.GetLocalVars(action, model, ref methodFields);
                    foreach (var methodField in methodFields)
                    {
                        if (!ViewBagFields.ContainsKey(methodField.Key))
                        {
                            ViewBagFields.Add(methodField.Key, methodField.Value);
                        }
                    }
                    viewBagTypeName =
                        controllerSymbol.Name + "_Model." + actionSymbol.Name;
                    var ViewBagFieldList = ViewBagFields.ToList();
                    for (int i = ViewBagFieldList.Count - 1; i >= 0; i--)
                    {
                        if (ViewBagFieldList[i].Value.Type.Contains(viewBagTypeName))
                        {
                            ViewBagFieldList.RemoveAt(i);
                        }
                    }
                    sw.Write("\tpublic class ");
                    sw.WriteLine(structName);
                    sw.WriteLine("\t{");
                    GetFieldsUtility.BuildStruct(sw, ViewBagFieldList);
                    sw.WriteLine("\t}");
                }
                sw.WriteLine("}");
            }
        }
    }
}