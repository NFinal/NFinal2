//------------------------------------------------------------------------------
// <copyright file="ControllerGenerator.cs" company="Microsoft">
//     Copyright (c) Microsoft.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------
using System;
using System.Runtime.InteropServices;
using Microsoft.Win32;
using Microsoft.VisualStudio.Shell;
using VSLangProj80;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.MSBuild;
using System.Collections.Generic;
using Microsoft.VisualStudio.LanguageServices;
using System.IO;
using System.Composition;
using System.Linq;
using System.Diagnostics.CodeAnalysis;
using Microsoft.VisualStudio.ComponentModelHost;

namespace NFinalAspx
{
    /// <summary>
    /// This class implements the tool window exposed by this package and hosts a user control.
    /// </summary>
    /// <remarks>
    /// In Visual Studio tool windows are composed of a frame (implemented by the shell) and a pane,
    /// usually implemented by the package implementer.
    /// <para>
    /// This class derives from the ToolWindowPane class provided from the MPF in order to use its
    /// implementation of the IVsUIElementPane interface.
    /// </para>
    /// </remarks>
    [ComVisible(true)]
    [Guid(NFinalAspxGenerator.PackageGuidString)]
    [CodeGeneratorRegistration(typeof(NFinalAspxGenerator), "NFinalAspxGenerator", vsContextGuids.vsContextGuidVCSProject, GeneratesDesignTimeSource = true)]
    [ProvideObject(typeof(NFinalAspxGenerator))]
    public class NFinalAspxGenerator : BaseCodeGeneratorWithSite
    {
        public const string PackageGuidString = "4746CF7A-8129-4443-9A87-AA122BBB8C98";
        internal static string name = "NFinalAspxGenerator";
        protected override byte[] GenerateCode(string inputFileContent)
        {
            IComponentModel componentModel =
        (IComponentModel)Microsoft.VisualStudio.Shell.Package.GetGlobalService(typeof(SComponentModel));

            var visualStuioWorkspace = componentModel.GetService<VisualStudioWorkspace>();
           
            var project = GetProject();
            //var currentDoc = visualStuioWorkspace.CurrentSolution.GetDocumentIdsWithFilePath(this.InputFilePath).FirstOrDefault();
            var proj = visualStuioWorkspace.CurrentSolution.Projects.Where(doc => doc.FilePath == project.FileName).FirstOrDefault();
            StringWriter sw = new StringWriter();
            if (InputFilePath.EndsWith(".aspx") || InputFilePath.EndsWith(".ascx"))
            {
                CSharpCompilation cSharpCompilation = null;
                Compilation compilation = null;
                if (cSharpCompilation == null)
                {
                    if (proj.TryGetCompilation(out compilation))
                    {
                        cSharpCompilation = (CSharpCompilation)compilation;
                    }
                    else
                    {
                        cSharpCompilation = (CSharpCompilation)proj.GetCompilationAsync().Result;
                    }
                }
                var compileFilePath = this.InputFilePath + ".cs";
                var document = proj.Documents.Single(doc => { return doc.FilePath == compileFilePath; });
                SyntaxTree tree = null;
                string viewBagType = "dynamic";
                string viewBagName = "ViewBag";
                if (document != null)
                {
                    tree = document.GetSyntaxTreeAsync().Result;
                    SyntaxNode root = tree.GetRoot();
                    SemanticModel model = cSharpCompilation.GetSemanticModel(tree);
                    var page = root.DescendantNodes().OfType<ClassDeclarationSyntax>().Single();
                    var pageSymbol = model.GetDeclaredSymbol(page);
                    var field = page.ChildNodes().OfType<FieldDeclarationSyntax>().Single();
                    viewBagType = field.Declaration.Type.ToString();
                    viewBagName = field.Declaration.Variables[0].ToString();
                    var rootUnit = (CompilationUnitSyntax)root;
                    foreach (var us in rootUnit.Usings)
                    {
                        sw.Write(us.ToFullString());
                    }
                    sw.WriteLine("//此代码由NFinalAspxGenerator生成。");
                    sw.WriteLine("//http://bbs.nfinal.com");
                    sw.Write("namespace ");
                    sw.WriteLine(pageSymbol.ContainingNamespace);
                    sw.WriteLine("{");
                    sw.Write("\tpublic static class ");
                    sw.Write(pageSymbol.Name);
                    sw.WriteLine("_Extend");
                    sw.WriteLine("\t{");
                    sw.WriteLine("\t\t//如果此处报错，请添加NFinal引用");
                    sw.WriteLine("\t\t//PMC命令为：Install-Package NFinal");
                    sw.Write("\t\tpublic static void View(");
                    sw.Write("this NFinal.IAction action,");
                    sw.Write(viewBagType);
                    sw.Write(" ");
                    sw.Write(viewBagName);
                    sw.WriteLine(")");
                    sw.WriteLine("\t\t{");
                }
                string templateString = inputFileContent;
                ASPX aspx = new ASPX(Path.GetDirectoryName(project.FileName));
                StringWriter contentWriter = new StringWriter();
                string content= aspx.Render(contentWriter, templateString, 3, false);
                sw.Write(content);
                if (document != null)
                {
                    sw.WriteLine("\t\t}");
                    sw.WriteLine("\t}");
                    sw.WriteLine("}");
                }
            }
            return System.Text.Encoding.UTF8.GetBytes(sw.ToString());
        }
    }
}
