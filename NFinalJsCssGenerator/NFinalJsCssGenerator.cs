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
using System.Linq;
using System.Diagnostics.CodeAnalysis;
using Microsoft.VisualStudio.ComponentModelHost;
using Yahoo.Yui.Compressor;

namespace NFinalJsCssGenerator
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
    [Guid(NFinalJsCssGenerator.PackageGuidString)]
    [CodeGeneratorRegistration(typeof(NFinalJsCssGenerator), "NFinalJsCssGenerator", vsContextGuids.vsContextGuidVCSProject, GeneratesDesignTimeSource = true)]
    [ProvideObject(typeof(NFinalJsCssGenerator))]
    public class NFinalJsCssGenerator : BaseCodeGeneratorWithSite
    {
        public const string PackageGuidString = "9B400A44-CE15-44E1-BC35-24A2CE750941";
        internal static string name = "NFinalJsonGenerator";
        protected override byte[] GenerateCode(string inputFileContent)
        {
            IComponentModel componentModel =
                (IComponentModel)Microsoft.VisualStudio.Shell.Package.GetGlobalService(typeof(SComponentModel));

            var project = GetProject();
            string compressContent = string.Empty;
            if (InputFilePath.EndsWith(".js"))
            {
                JavaScriptCompressor javaScriptCompressor = new JavaScriptCompressor();
                compressContent=javaScriptCompressor.Compress(inputFileContent);
            }
            else if (InputFilePath.EndsWith(".css"))
            {
                CssCompressor cssCompressor = new CssCompressor();
                compressContent= cssCompressor.Compress(inputFileContent);
            }
            return System.Text.Encoding.UTF8.GetBytes(compressContent);
        }
    }
}
