//------------------------------------------------------------------------------
// <copyright file="ControllerGenerator.cs" company="Microsoft">
//     Copyright (c) Microsoft.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

namespace NFinalControllerGenerator
{
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
    [Guid("32606d70-cbcb-4e62-aa77-a483d86077db")]
    //[PackageRegistration(UseManagedResourcesOnly = true)]
    //[InstalledProductRegistration("#110", "#112", "1.0", IconResourceID = 400)] // Info on this package for Help/About
    //[SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "pkgdef, VS and vsixmanifest are valid VS terms")]
    [CodeGeneratorRegistration(typeof(NFinalControllerGenerator),"C# Controller Generator",vsContextGuids.vsContextGuidVCSProject,GeneratesDesignTimeSource =true)]
    [ProvideObject(typeof(NFinalControllerGenerator))]
    
    public class NFinalControllerGenerator :BaseCodeGeneratorWithSite
    {
        internal static string name = "NFinalControllerGenerator";
        public static MSBuildWorkspace msbw = null;
        protected override byte[] GenerateCode(string inputFileContent)
        {
            IComponentModel componentModel =
        (IComponentModel)Microsoft.VisualStudio.Shell.Package.GetGlobalService(typeof(SComponentModel));

            var visualStuioWorkspace = componentModel.GetService<VisualStudioWorkspace>();
            //var componentModel = (IComponentModel)this.GetService(typeof(SComponentModel));
            //if (componentModel != null)
            //{
            //    visualStuioWorkspace = componentModel.GetService<Microsoft.VisualStudio.LanguageServices.VisualStudioWorkspace>();
            //}
            var project = GetProject();
          
            //var currentDoc = visualStuioWorkspace.CurrentSolution.GetDocumentIdsWithFilePath(this.InputFilePath).FirstOrDefault();
            var proj = visualStuioWorkspace.CurrentSolution.Projects.Where(doc=>doc.FilePath==project.FileName).FirstOrDefault();
          
            
            //if (msbw == null)
            //{
            //    msbw = MSBuildWorkspace.Create(new Dictionary<string, string> { { "DesignTimeBuild", "true" },
            //    { "IntelliSenseBuild", "true" },
            //    { "BuildingInsideVisualStudio", "true" }});
            //}
            //if (proj == null)
            //{
            //    proj = msbw.OpenProjectAsync(project.FileName).Result;
            //}
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
            StringWriter sw = new StringWriter();
            var document = proj.Documents.Single(doc => { return doc.FilePath == this.InputFilePath; });
            StructModel model = new StructModel();
            var tree = document.GetSyntaxTreeAsync().Result;
            SyntaxNode root = tree.GetRoot();
            SemanticModel semanticModel = cSharpCompilation.GetSemanticModel(tree);
            model.WriteDocument(sw, tree, semanticModel);
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(sw.ToString());
            sw.Dispose();
            return buffer;
        }
 
    }
  
}
