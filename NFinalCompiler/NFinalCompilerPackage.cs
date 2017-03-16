//------------------------------------------------------------------------------
// <copyright file="NFinalCompiler.cs" company="Microsoft">
//     Copyright (c) Microsoft.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.OLE.Interop;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.Win32;
using EnvDTE;
using EnvDTE80;
using EnvDTE90;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.MSBuild;
using Microsoft.VisualStudio.LanguageServices;
using Microsoft.VisualStudio.ComponentModelHost;
using System.Linq;
using System.IO;
using NFinalCompiler.Helper;

namespace NFinalCompiler
{
    /// <summary>
    /// This is the class that implements the package exposed by this assembly.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The minimum requirement for a class to be considered a valid package for Visual Studio
    /// is to implement the IVsPackage interface and register itself with the shell.
    /// This package uses the helper classes defined inside the Managed Package Framework (MPF)
    /// to do it: it derives from the Package class that provides the implementation of the
    /// IVsPackage interface and uses the registration attributes defined in the framework to
    /// register itself and its components with the shell. These attributes tell the pkgdef creation
    /// utility what data to put into .pkgdef file.
    /// </para>
    /// <para>
    /// To get loaded into VS, the package must be referred by &lt;Asset Type="Microsoft.VisualStudio.VsPackage" ...&gt; in .vsixmanifest file.
    /// </para>
    /// </remarks>
    [PackageRegistration(UseManagedResourcesOnly = true)]
    [InstalledProductRegistration("#110", "#112", "1.0", IconResourceID = 400)] // Info on this package for Help/About
    [Guid(NFinalCompilerPackage.PackageGuidString)]
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "pkgdef, VS and vsixmanifest are valid VS terms")]
    [ProvideAutoLoad(UIContextGuids80.SolutionExists)]
    public sealed class NFinalCompilerPackage : Package
    {
        /// <summary>
        /// NFinalCompiler GUID string.
        /// </summary>
        public const string PackageGuidString = "33cd9153-3847-49d8-b887-823444a2a1c3";
        public static DTE2 _dte;
        //public static Dispatcher _dispatcher;
        public static Package Package;
        public static NFinalCompilerPackage _instance;
        public static MSBuildWorkspace msbw = null;
        public static IComponentModel componentModel = null;
        public VisualStudioWorkspace workspace;
        public static EnvDTE.SolutionEvents _solutionEvents;
        public static EnvDTE.DocumentEvents _documentEvents;
        /// <summary>
        /// Initializes a new instance of the <see cref="NFinalCompiler"/> class.
        /// </summary>
        public NFinalCompilerPackage()
        {
            // Inside this method you can place any initialization code that does not require
            // any Visual Studio service because at this point the package object is created but
            // not sited yet inside Visual Studio environment. The place to do all the other
            // initialization is the Initialize method.
        }

        #region Package Members

        /// <summary>
        /// Initialization of the package; this method is called right after the package is sited, so this is the place
        /// where you can put all the initialization code that rely on services provided by VisualStudio.
        /// </summary>
        protected override void Initialize()
        {
            _instance = this;
            _dte = GetService(typeof(DTE)) as DTE2;
            Package = this;
            EnvDTE80.Events2 events = _dte.Events as Events2;
            Helper.Logger.Initialize(this, "NFinalCompiler");
            _documentEvents = events.DocumentEvents;
            _solutionEvents = events.SolutionEvents;
            _documentEvents.DocumentSaved += DocumentEvents_DocumentSaved;
            _solutionEvents.AfterClosing += _solutionEvents_AfterClosing;
            _solutionEvents.ProjectRemoved += _solutionEvents_ProjectRemoved;
            componentModel =
        (IComponentModel)Microsoft.VisualStudio.Shell.Package.GetGlobalService(typeof(SComponentModel));
            
            base.Initialize();
        }

        private void _solutionEvents_ProjectRemoved(EnvDTE.Project Project)
        {
           
        }

        private void _solutionEvents_AfterClosing()
        {
            
        }

        private void DocumentEvents_DocumentSaved(EnvDTE.Document Document)
        {
            EnvDTE.Project project;
            Microsoft.CodeAnalysis.Project proj;
            //string fileName = null;
            if (Document.Name.EndsWith("Controller.cs",StringComparison.OrdinalIgnoreCase))
            {
                project = Document.ProjectItem.ContainingProject;
                string parentName = Document.ProjectItem.Name;
                string parentFileName = Document.ProjectItem.FileNames[0];
                string parentPath = Path.GetDirectoryName(parentFileName);

                workspace = componentModel.GetService<VisualStudioWorkspace>();
                proj = workspace.CurrentSolution.Projects.Where(doc => doc.FilePath == project.FileName).FirstOrDefault();
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
                string structFileName = Path.Combine(parentPath, Path.GetFileNameWithoutExtension(parentName) + ".model.cs");
                ProjectItem projectItem = Helper.ProjectHelpers.FindInProject(structFileName);
                projectItem?.Document?.Close(vsSaveChanges.vsSaveChangesNo);
                using (StreamWriter sw = new StreamWriter(structFileName, false, System.Text.Encoding.UTF8))
                {
                    var document = proj.Documents.Single(doc => { return doc.FilePath == parentFileName; });
                    Controller.StructModel model = new Controller.StructModel();
                    var tree = document.GetSyntaxTreeAsync().Result;
                    SyntaxNode root = tree.GetRoot();
                    SemanticModel semanticModel = cSharpCompilation.GetSemanticModel(tree);
                    model.WriteDocument(sw, tree, semanticModel);
                    sw.Close();
                }
                if (File.Exists(structFileName))
                {
                    Helper.ProjectHelpers.AddNestedFile(Document.ProjectItem, structFileName, "Compile", false);
                }
            }
            else if (Document.Name.EndsWith(".cshtml"))
            {
                project = Document.ProjectItem.ContainingProject;
                string parentName = Document.ProjectItem.Name;
                string parentFileName = Document.ProjectItem.FileNames[0];
                string parentPath = Path.GetDirectoryName(parentFileName);

                workspace = componentModel.GetService<VisualStudioWorkspace>();
                proj = workspace.CurrentSolution.Projects.Where(doc => doc.FilePath == project.FileName).FirstOrDefault();
                string fileName = Path.Combine(parentPath,Path.GetFileNameWithoutExtension(parentName) + ".template.cs");
                ProjectItem projectItem = Helper.ProjectHelpers.FindInProject(fileName);
                projectItem?.Document?.Close(vsSaveChanges.vsSaveChangesNo);
                using (StreamWriter sw = new StreamWriter(fileName, false, System.Text.Encoding.UTF8))
                {
                    string nameSpace = GetNameSpace(Document);
                    string className = Path.GetFileNameWithoutExtension(parentName);
                    Razor.RazorWriter writer = new Razor.RazorWriter(parentFileName);
                    writer.WriteTemplate(sw, nameSpace, className);
                    sw.Close();
                }
                if (File.Exists(fileName))
                {
                    projectItem= Helper.ProjectHelpers.AddNestedFile(Document.ProjectItem, fileName, "Compile", false);
                    //添加<Content Include="Views\Index.cshtml">BrowseToURL
                    
                    //代码结构
                    projectItem?.Document?.NewWindow();
                    workspace = componentModel.GetService<VisualStudioWorkspace>();
                    proj = workspace.CurrentSolution.Projects.Where(x => x.FilePath == project.FileName).FirstOrDefault();
                    Microsoft.CodeAnalysis.Document codeDocument = proj.Documents.FirstOrDefault((doc => { if (doc.FilePath == fileName) { return true; } else { return false; } }));
                    //格式化
                    Microsoft.CodeAnalysis.Document formatDocument = Microsoft.CodeAnalysis.Formatting.Formatter.FormatAsync(codeDocument).Result;
                    //更改
                    workspace.TryApplyChanges(formatDocument.Project.Solution);
                }
                   
            }
            else if (Document.Name.EndsWith(".nf.json"))
            {
                project = Document.ProjectItem.ContainingProject;
                string parentName = Document.ProjectItem.Name;
                string parentFileName = Document.ProjectItem.FileNames[0];
                string inputFileContent=null;
                using (StreamReader sr = new StreamReader(parentFileName, System.Text.Encoding.UTF8))
                {
                    inputFileContent = sr.ReadToEnd();
                }
                string nameSpace = GetNameSpace(Document);
                string className = parentName.TrimSuffix(".nf.json").Replace('-','_');
                string outPutFileName = parentFileName.TrimSuffix(".nf.json")+".cs";
                ProjectItem projectItem = Helper.ProjectHelpers.FindInProject(outPutFileName);
                projectItem?.Document?.Close(vsSaveChanges.vsSaveChangesNo);
                if (inputFileContent != null)
                {
                    using (StreamWriter sw = new StreamWriter(outPutFileName, false, System.Text.Encoding.UTF8))
                    {
                        Xamasoft.JsonClassGenerator.JsonClassGenerator gen = new Xamasoft.JsonClassGenerator.JsonClassGenerator();
                        gen.Example = inputFileContent;
                        gen.InternalVisibility = false;
                        gen.CodeWriter = new Xamasoft.JsonClassGenerator.CodeWriters.CSharpCodeWriter();
                        gen.ExplicitDeserialization = false;
                        gen.Namespace = nameSpace;
                        gen.NoHelperClass = true;
                        gen.SecondaryNamespace = nameSpace;
                        gen.TargetFolder = null;
                        gen.UseProperties = false;
                        gen.MainClass = className;
                        gen.UsePascalCase = false;
                        gen.UseNestedClasses = false;
                        gen.ApplyObfuscationAttributes = false;
                        gen.SingleFile = true;
                        gen.ExamplesInDocumentation = true;
                        gen.OutputStream = sw;
                        gen.GenerateClasses();
                    }
                }
                if (File.Exists(outPutFileName))
                {

                    Helper.ProjectHelpers.AddNestedFile(Document.ProjectItem, outPutFileName, "Compile", false);
                }
            }
            else if (Document.Name.EndsWith(".nf.sql"))
            {
                string parentName = Document.ProjectItem.Name;
                string parentFileName = Document.ProjectItem.FileNames[0];
                string nameSpace = GetNameSpace(Document);
                string fileContent = null;
                using (StreamReader sr = new StreamReader(parentFileName, System.Text.Encoding.UTF8))
                {
                    fileContent = sr.ReadToEnd();
                }
                if (fileContent != null)
                {
                    Sql.SqlDocument sqlDocument = new Sql.SqlDocument(parentFileName, nameSpace, fileContent);
                    foreach (var model in sqlDocument.modelFileDataList)
                    {
                        ProjectItem projectItem = Helper.ProjectHelpers.FindInProject(model.fileName);
                        projectItem?.Document?.Close(vsSaveChanges.vsSaveChangesNo);
                        using (StreamWriter sw = new StreamWriter(model.fileName, false, System.Text.Encoding.UTF8))
                        {
                            sw.Write(model.content);
                            sw.Close();
                        }
                        if (File.Exists(model.fileName))
                        {
                            Helper.ProjectHelpers.AddNestedFile(Document.ProjectItem, model.fileName, "Compile", false);
                        }
                    }
                }
            }

        }
        public static string GetNameSpace(EnvDTE.Document Document)
        {
            var project = Document.ProjectItem.ContainingProject;
            return Path.Combine(project.Name) + "." + 
                string.Join(".", Path.GetDirectoryName(Document.ProjectItem.FileNames[0]).Substring(Path.GetDirectoryName(project.FullName).Length).Split(Path.DirectorySeparatorChar)).Trim('.');
        }
        #endregion
    }
}
