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
using System.Collections.Generic;
using System.IO;
using System.Composition;
using System.Linq;
using System.Diagnostics.CodeAnalysis;
using Microsoft.VisualStudio.ComponentModelHost;

namespace NFinalJsonGenerator
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
    [Guid(NFinalJsonGenerator.PackageGuidString)]
    [CodeGeneratorRegistration(typeof(NFinalJsonGenerator), "NFinalJsonGenerator", vsContextGuids.vsContextGuidVCSProject, GeneratesDesignTimeSource = true)]
    [ProvideObject(typeof(NFinalJsonGenerator))]
    public class NFinalJsonGenerator : BaseCodeGeneratorWithSite
    {
        public const string PackageGuidString = "1c450b36-a366-41d8-a651-dfb39c48045d";
        internal static string name = "NFinalJsonGenerator";
        protected override byte[] GenerateCode(string inputFileContent)
        {
            IComponentModel componentModel =
        (IComponentModel)Microsoft.VisualStudio.Shell.Package.GetGlobalService(typeof(SComponentModel));

            var project = GetProject();
            StringWriter sw = new StringWriter();
            if (InputFilePath.EndsWith(".json"))
            {
                Xamasoft.JsonClassGenerator.JsonClassGenerator gen = new Xamasoft.JsonClassGenerator.JsonClassGenerator();
                gen.Example = inputFileContent;
                gen.InternalVisibility = false;
                gen.CodeWriter =new Xamasoft.JsonClassGenerator.CodeWriters.CSharpCodeWriter();
                gen.ExplicitDeserialization = false;
                gen.Namespace =this.FileNameSpace;
                gen.NoHelperClass =true;
                gen.SecondaryNamespace = this.FileNameSpace;
                gen.TargetFolder = null;
                gen.UseProperties = false;
                gen.MainClass = Path.GetFileNameWithoutExtension(this.InputFilePath);
                gen.UsePascalCase = false;
                gen.UseNestedClasses = false;
                gen.ApplyObfuscationAttributes = false;
                gen.SingleFile = true;
                gen.ExamplesInDocumentation = true;
                gen.OutputStream = sw;
                gen.GenerateClasses();
            }
            return System.Text.Encoding.UTF8.GetBytes(sw.ToString());
        }
    }
}
