/***************************************************************************

Copyright (c) Microsoft Corporation. All rights reserved.
This code is licensed under the Visual Studio SDK license terms.
THIS CODE IS PROVIDED *AS IS* WITHOUT WARRANTY OF
ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING ANY
IMPLIED WARRANTIES OF FITNESS FOR A PARTICULAR
PURPOSE, MERCHANTABILITY, OR NON-INFRINGEMENT.

***************************************************************************/

using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.LanguageServices;
using Microsoft.VisualStudio.ComponentModelHost;
using System.Linq;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using Microsoft.CodeAnalysis.Formatting;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.MSBuild;

namespace NFinalRazorGenerator
{
    /// <summary>
    /// This is the generator class. 
    /// When setting the 'Custom Tool' property of a C#, VB, or J# project item to "RazorGenerator", 
    /// the GenerateCode function will get called and will return the contents of the generated file 
    /// to the project system
    /// </summary>
    [ComVisible(true)]
    [Guid(NFinalRazorGenerator.PackageGuidString)]
    [CodeGeneratorRegistration(typeof(NFinalRazorGenerator), "C# Razor Generator", "{FAE04EC1-301F-11D3-BF4B-00C04F79EFBC}", GeneratesDesignTimeSource = true)]
    [ProvideObject(typeof(NFinalRazorGenerator))]
    public class NFinalRazorGenerator : BaseCodeGeneratorWithSite
    {
        public const string PackageGuidString = "1dc69a9f-cdec-4d09-a606-cd48936d96c9";
#pragma warning disable 0414
        //The name of this generator (use for 'Custom Tool' property of project item)
        public static string name = "NFinalRazorGenerator";
#pragma warning restore 0414

        /// <summary>
        /// Function that builds the contents of the generated file based on the contents of the input file
        /// </summary>
        /// <param name="inputFileContent">Content of the input file</param>
        /// <returns>Generated file as a byte array</returns>
        protected override byte[] GenerateCode(string inputFileContent)
        {
            
            //var componentModel = (IComponentModel)this.GetService(typeof(SComponentModel));
            //if (componentModel != null)
            //{
            //    visualStuioWorkspace = componentModel.GetService<Microsoft.VisualStudio.LanguageServices.VisualStudioWorkspace>();
            //}
            var project = GetProject();
           
            string fileName = Path.Combine(Path.GetDirectoryName(InputFilePath), Path.GetFileNameWithoutExtension(InputFilePath))+ ".template.cs";
            StringWriter sw = new StringWriter();
            if (InputFilePath.EndsWith(".cshtml"))
            {
                System.Web.Razor.Parser.RazorParser parser = new System.Web.Razor.Parser.RazorParser(
                new System.Web.Razor.Parser.CSharpCodeParser()
                , new System.Web.Razor.Parser.HtmlMarkupParser());
                parser.DesignTimeMode = false;

                StringReader sr = new StringReader(inputFileContent);

                var results = parser.Parse(sr); ;
                string nameSpace = this.FileNameSpace;
                string className = Path.GetFileNameWithoutExtension(this.InputFilePath);
                NFinalRazorGeneratorTest.RazorWriter writer = new NFinalRazorGeneratorTest.RazorWriter(inputFileContent);
                writer.WriteTemplate(sw, nameSpace, className);
                string template = sw.ToString();
                sw.Dispose();
                //添加文件
                int iFound = 0;
                uint itemId = 0;
                EnvDTE.ProjectItem item;
                Microsoft.VisualStudio.Shell.Interop.VSDOCUMENTPRIORITY[] pdwPriority = new Microsoft.VisualStudio.Shell.Interop.VSDOCUMENTPRIORITY[1];
                Microsoft.VisualStudio.Shell.Interop.IVsProject VsProject = VsHelper.ToVsProject(project);
                VsProject.IsDocumentInProject(InputFilePath, out iFound, pdwPriority, out itemId);
                if (iFound != 0 && itemId != 0)
                {
                    Microsoft.VisualStudio.OLE.Interop.IServiceProvider oleSp = null;
                    VsProject.GetItemContext(itemId, out oleSp);
                    if (oleSp != null)
                    {
                        ServiceProvider sp = new ServiceProvider(oleSp);
                        // convert our handle to a ProjectItem
                        item = sp.GetService(typeof(EnvDTE.ProjectItem)) as EnvDTE.ProjectItem;
                    }
                    else
                        throw new ApplicationException("Unable to retrieve Visual Studio ProjectItem");
                }
                else
                    throw new ApplicationException("Unable to retrieve Visual Studio ProjectItem");
                foreach (EnvDTE.ProjectItem childItem in item.ProjectItems)
                {
                    if (childItem.Name.EndsWith(".cs"))
                        // then delete it
                        childItem.Delete();
                }
                EnvDTE.ProjectItem newItem = null;
                try
                {
                    StreamWriter fileWriter = new StreamWriter(fileName, false, System.Text.Encoding.UTF8);
                    fileWriter.Write(template);
                    fileWriter.Dispose();
                    newItem=item.ProjectItems.AddFromFile(fileName);
                }
                catch
                {
                    if (File.Exists(fileName))
                    {
                        File.Delete(fileName);
                    }
                }
                //代码结构
                IComponentModel componentModel =
        (IComponentModel)Microsoft.VisualStudio.Shell.Package.GetGlobalService(typeof(SComponentModel));

                var visualStuioWorkspace = componentModel.GetService<VisualStudioWorkspace>();
                var proj = visualStuioWorkspace.CurrentSolution.Projects.Where(x => x.FilePath == project.FileName).FirstOrDefault();
                Document codeDocument= proj.Documents.FirstOrDefault((doc => { if (doc.FilePath == fileName) { return true; } else { return false; } }));
                //格式化
                Document formatDocument= Formatter.FormatAsync(codeDocument).Result;
                //更改
                visualStuioWorkspace.TryApplyChanges(formatDocument.Project.Solution);
                return null;
            }
            return null;
        }
    }
}