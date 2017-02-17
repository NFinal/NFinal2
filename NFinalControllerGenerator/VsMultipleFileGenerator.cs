using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.OLE.Interop;
using Microsoft.VisualStudio.Shell;
using System.Runtime.InteropServices;
using System.IO;
using System.ComponentModel;

namespace NFinalControllerGenerator
{
    public abstract class VsMultipleFileGenerator<IterativeElementType> : IEnumerable<IterativeElementType>, IVsSingleFileGenerator, IObjectWithSite
    {
        #region Visual Studio Specific Fields
        private object site;
        private ServiceProvider serviceProvider = null;
        #endregion

        #region Our Fields
        private string bstrInputFileContents;
        private string wszInputFilePath;
        private EnvDTE.Project project;

        private List<string> newFileNames;
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

        public VsMultipleFileGenerator()
        {
            EnvDTE.DTE dte = (EnvDTE.DTE)Package.GetGlobalService(typeof(EnvDTE.DTE));
            Array ary = (Array)dte.ActiveSolutionProjects;
            if (ary.Length > 0)
            {
                project = (EnvDTE.Project)ary.GetValue(0);

            }
            newFileNames = new List<string>();
        }
        public abstract IEnumerator<IterativeElementType> GetEnumerator();

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        protected abstract string GetFileName(IterativeElementType element);
        public abstract byte[] GenerateContent(IterativeElementType element);


        public abstract byte[] GenerateSummaryContent();

        //public int Generate(string wszInputFilePath, string bstrInputFileContents, string wszDefaultNamespace, IntPtr[] rgbOutputFileContents, out uint pcbOutput, IVsGeneratorProgress pGenerateProgress)
        //{
        //    throw new NotImplementedException();
        //}
        public int Generate(string wszInputFilePath, string bstrInputFileContents, string wszDefaultNamespace, IntPtr[] rgbOutputFileContents, out int pcbOutput, IVsGeneratorProgress pGenerateProgress)
        {
            this.bstrInputFileContents = bstrInputFileContents;
            this.wszInputFilePath = wszInputFilePath;
            this.newFileNames.Clear();

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

            // now we can start our work, iterate across all the 'elements' in our source file 
            foreach (IterativeElementType element in this)
            {
                try
                {
                    // obtain a name for this target file
                    string fileName = GetFileName(element);
                    // add it to the tracking cache
                    newFileNames.Add(fileName);
                    // fully qualify the file on the filesystem
                    string strFile = Path.Combine(wszInputFilePath.Substring(0, wszInputFilePath.LastIndexOf(Path.DirectorySeparatorChar)), fileName);
                    // create the file
                    FileStream fs = File.Create(strFile);

                    try
                    {
                        // generate our target file content
                        byte[] data = GenerateContent(element);

                        // write it out to the stream
                        fs.Write(data, 0, data.Length);

                        fs.Close();

                        // add the newly generated file to the solution, as a child of the source file...
                        EnvDTE.ProjectItem itm = item.ProjectItems.AddFromFile(strFile);
                        /*
                         * Here you may wish to perform some addition logic
                         * such as, setting a custom tool for the target file if it
                         * is intented to perform its own generation process.
                         * Or, set the target file as an 'Embedded Resource' so that
                         * it is embedded into the final Assembly.
                         
                        EnvDTE.Property prop = itm.Properties.Item("CustomTool");
                        //// set to embedded resource
                        itm.Properties.Item("BuildAction").Value = 3;
                        if (String.IsNullOrEmpty((string)prop.Value) || !String.Equals((string)prop.Value, typeof(AnotherCustomTool).Name))
                        {
                            prop.Value = typeof(AnotherCustomTool).Name;
                        }
                        */
                    }
                    catch (Exception)
                    {
                        fs.Close();
                        if (File.Exists(strFile))
                            File.Delete(strFile);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            // perform some clean-up, making sure we delete any old (stale) target-files
            foreach (EnvDTE.ProjectItem childItem in item.ProjectItems)
            {
                string extension;
                DefaultExtension(out extension);
                if (!(childItem.Name.EndsWith(extension) || newFileNames.Contains(childItem.Name)))
                    // then delete it
                    childItem.Delete();
            }

            // generate our summary content for our 'single' file
            byte[] summaryData = GenerateSummaryContent();

            if (summaryData == null)
            {
                rgbOutputFileContents[0] = IntPtr.Zero;

                pcbOutput = 0;
            }
            else
            {
                // return our summary data, so that Visual Studio may write it to disk.
                rgbOutputFileContents[0] = Marshal.AllocCoTaskMem(summaryData.Length);

                Marshal.Copy(summaryData, 0, rgbOutputFileContents[0], summaryData.Length);

                pcbOutput = summaryData.Length;
            }
            return Microsoft.VisualStudio.VSConstants.S_OK;
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

        public abstract int DefaultExtension(out string pbstrDefaultExtension);

        public int Generate(string wszInputFilePath, string bstrInputFileContents, string wszDefaultNamespace, IntPtr[] rgbOutputFileContents, out uint pcbOutput, IVsGeneratorProgress pGenerateProgress)
        {
            throw new NotImplementedException();
        }



        #endregion

    }
}

