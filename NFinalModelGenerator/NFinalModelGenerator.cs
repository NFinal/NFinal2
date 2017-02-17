using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TextTemplating.VSHost;
using Microsoft.VisualStudio.OLE.Interop;
using Microsoft.VisualStudio.Shell;
using System.Runtime.InteropServices;
using System.IO;
using System.ComponentModel;
using Microsoft.VisualStudio.Shell.Interop;
using VSLangProj80;
using Microsoft.VisualStudio;

namespace NFinalModelGenerator
{
    [ComVisible(true)]
    [Guid("B33A8899-0F9F-4895-9875-7A5FB71741FD")]
    [CodeGeneratorRegistration(typeof(NFinalModelGenerator), "NFinalModelGenerator", vsContextGuids.vsContextGuidVCSProject, GeneratesDesignTimeSource = true)]
    [ProvideObject(typeof(NFinalModelGenerator))]
    public class NFinalModelGenerator :  IVsSingleFileGenerator, IObjectWithSite
    {
        #region Visual Studio Specific Fields
        private object site;
        private ServiceProvider serviceProvider = null;
        #endregion

        #region Our Fields
        private string bstrInputFileContents;
        private string wszInputFilePath;
        private EnvDTE.Project project;
        #endregion

        protected EnvDTE.Project Project
        {
            get
            {
                return project;
            }
        }

        protected string InputFileContents
        {
            get
            {
                return bstrInputFileContents;
            }
        }

        protected string InputFilePath
        {
            get
            {
                return wszInputFilePath;
            }
        }

        private ServiceProvider SiteServiceProvider
        {
            get
            {
                if (serviceProvider == null)
                {
                    Microsoft.VisualStudio.OLE.Interop.IServiceProvider oleServiceProvider = site as Microsoft.VisualStudio.OLE.Interop.IServiceProvider;
                    serviceProvider = new ServiceProvider(oleServiceProvider);
                }
                return serviceProvider;
            }
        }

        public NFinalModelGenerator()
        {
            EnvDTE.DTE dte = (EnvDTE.DTE)Package.GetGlobalService(typeof(EnvDTE.DTE));
            Array ary = (Array)dte.ActiveSolutionProjects;
            if (ary.Length > 0)
            {
                project = (EnvDTE.Project)ary.GetValue(0);
            }
        }
        public string GetDefaultExtension()
        {
            return null;
        }


        public int Generate(string wszInputFilePath, string bstrInputFileContents, string wszDefaultNamespace,IntPtr[] rgbOutputFileContents, out uint pcbOutput, IVsGeneratorProgress pGenerateProgress)
        {
            this.bstrInputFileContents = bstrInputFileContents;
            this.wszInputFilePath = wszInputFilePath;

            int iFound = 0;
            uint itemId = 0;
            EnvDTE.ProjectItem item;
            Microsoft.VisualStudio.Shell.Interop.VSDOCUMENTPRIORITY[] pdwPriority = new Microsoft.VisualStudio.Shell.Interop.VSDOCUMENTPRIORITY[1];

            // obtain a reference to the current project as an IVsProject type
            Microsoft.VisualStudio.Shell.Interop.IVsProject VsProject = VsHelper.ToVsProject(project);
            // this locates, and returns a handle to our source file, as a ProjectItem
            VsProject.IsDocumentInProject(InputFilePath, out iFound, pdwPriority, out itemId);

            // if our source file was found in the project (which it should have been)
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

            SqlDocument sqlDocument = new SqlDocument(InputFilePath, wszDefaultNamespace, InputFileContents);

            // perform some clean-up, making sure we delete any old (stale) target-files
            //foreach (EnvDTE.ProjectItem childItem in item.ProjectItems)
            //{
            //    if (childItem.Name.EndsWith(".cs"))
            //        // then delete it
            //        childItem.Delete();
            //}

            foreach (var model in sqlDocument.modelFileDataList)
            {
                try
                {
                    StreamWriter sw = new StreamWriter(model.fileName, false, System.Text.Encoding.UTF8);
                    sw.Write(model.content);
                    sw.Close();
                    EnvDTE.ProjectItem itm = item.ProjectItems.AddFromFile(model.fileName);
                }
                catch
                {
                    if (File.Exists(model.fileName))
                    {
                        File.Delete(model.fileName);
                    }
                }
            }


            // generate our summary content for our 'single' file
            byte[] summaryData = null;

            if (summaryData == null)
            {
                rgbOutputFileContents =new IntPtr[] { IntPtr.Zero };

                pcbOutput = 0;
                return VSConstants.E_FAIL;
            }
            else
            {
                // return our summary data, so that Visual Studio may write it to disk.
                rgbOutputFileContents[0] = Marshal.AllocCoTaskMem(summaryData.Length);

                Marshal.Copy(summaryData, 0, rgbOutputFileContents[0], summaryData.Length);

                pcbOutput =(uint) summaryData.Length;
                return VSConstants.S_OK;
            }
            return 0;
        }

        #region IObjectWithSite Members

        public void GetSite(ref Guid riid, out IntPtr ppvSite)
        {
            if (this.site == null)
            {
                throw new Win32Exception(-2147467259);
            }

            IntPtr objectPointer = Marshal.GetIUnknownForObject(this.site);

            try
            {
                Marshal.QueryInterface(objectPointer, ref riid, out ppvSite);
                if (ppvSite == IntPtr.Zero)
                {
                    throw new Win32Exception(-2147467262);
                }
            }
            finally
            {
                if (objectPointer != IntPtr.Zero)
                {
                    Marshal.Release(objectPointer);
                    objectPointer = IntPtr.Zero;
                }
            }
        }

        public void SetSite(object pUnkSite)
        {
            this.site = pUnkSite;
        }

        public int DefaultExtension(out string pbstrDefaultExtension)
        {
            pbstrDefaultExtension = ".cc";
            return 0;
        }

        //public int Generate(string wszInputFilePath, string bstrInputFileContents, string wszDefaultNamespace, IntPtr[] rgbOutputFileContents, out uint pcbOutput, IVsGeneratorProgress pGenerateProgress)
        //{
        //    throw new NotImplementedException();
        //}

        #endregion

    }
}

