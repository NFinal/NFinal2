using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.Shell;

namespace NFinalCompiler.Helper
{
    public static class ProjectHelpers
    {
        private static DTE2 _dte = NFinalCompilerPackage._dte;

        public static void CheckFileOutOfSourceControl(string file)
        {
            if (!File.Exists(file) || _dte.Solution.FindProjectItem(file) == null)
                return;

            if (_dte.SourceControl.IsItemUnderSCC(file) && !_dte.SourceControl.IsItemCheckedOut(file))
                _dte.SourceControl.CheckOutItem(file);

            FileInfo info = new FileInfo(file);
            info.IsReadOnly = false;
        }

        public static IEnumerable<ProjectItem> GetSelectedItems()
        {
            var items = (Array)_dte.ToolWindows.SolutionExplorer.SelectedItems;

            foreach (UIHierarchyItem selItem in items)
            {
                ProjectItem item = selItem.Object as ProjectItem;

                if (item != null)
                    yield return item;
            }
        }

        public static IEnumerable<string> GetSelectedItemPaths()
        {
            foreach (ProjectItem item in GetSelectedItems())
            {
                if (item != null && item.Properties != null)
                    yield return item.Properties.Item("FullPath").Value.ToString();
            }
        }

        public static string GetConfigFile(this Project project)
        {
            string folder = project.GetRootFolder();

            if (string.IsNullOrEmpty(folder))
                return null;

            return Path.Combine(folder, Constants.CONFIG_FILENAME);
        }

        public static string GetRootFolder(this Project project)
        {
            if (string.IsNullOrEmpty(project.FullName))
                return null;

            string fullPath;

            try
            {
                fullPath = project.Properties.Item("FullPath").Value as string;
            }
            catch (ArgumentException)
            {
                try
                {
                    // MFC projects don't have FullPath, and there seems to be no way to query existence
                    fullPath = project.Properties.Item("ProjectDirectory").Value as string;
                }
                catch (ArgumentException)
                {
                    // Installer projects have a ProjectPath.
                    fullPath = project.Properties.Item("ProjectPath").Value as string;
                }
            }

            if (string.IsNullOrEmpty(fullPath))
                return File.Exists(project.FullName) ? Path.GetDirectoryName(project.FullName) : null;

            if (Directory.Exists(fullPath))
                return fullPath;

            if (File.Exists(fullPath))
                return Path.GetDirectoryName(fullPath);

            return null;
        }
        public static ProjectItem FindInProject(string file)
        {
            return _dte.Solution.FindProjectItem(file);
        }
        public static void AddFileToProject(this Project project, string file, string itemType = null)
        {
            if (project.IsKind(ProjectTypes.ASPNET_5, ProjectTypes.DOTNET_Core, ProjectTypes.SSDT))
                return;

            try
            {
                if (_dte.Solution.FindProjectItem(file) == null)
                {
                    ProjectItem item = project.ProjectItems.AddFromFile(file);

                    if (string.IsNullOrEmpty(itemType)
                        || project.IsKind(ProjectTypes.WEBSITE_PROJECT)
                        || project.IsKind(ProjectTypes.UNIVERSAL_APP))
                        return;

                    item.Properties.Item("ItemType").Value = "None";
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
            }
        }
        public static bool ContainsProperty(this ProjectItem projectItem, string propertyName)
        {
            if (projectItem.Properties != null)
            {
                foreach (Property item in projectItem.Properties)
                {
                    if (item != null && item.Name == propertyName)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
        public static ProjectItem AddNestedFile(ProjectItem item, string newFile,string itemType, bool force = false)
        {

            try
            {
                if (item == null
                    || item.ContainingProject == null)
                    return null;
                ProjectItem newItem = null;
                bool mayNeedAttributeSet = item.ContainingProject.IsKind(ProjectTypes.DOTNET_Core, ProjectTypes.UNIVERSAL_APP);
                
                if (item.ProjectItems == null)
                {
                    item.ContainingProject.AddFileToProject(newFile, itemType);
                }
                else if ((newItem=_dte.Solution.FindProjectItem(newFile)) == null || force)
                {
                    newItem= item.ProjectItems.AddFromFile(newFile);
                }
                bool isVisualStudioRTM = false;
                if (mayNeedAttributeSet)
                {
                    if (newItem != null)
                    {
                        if (newItem.ContainsProperty("DependentUpon"))
                        {
                            isVisualStudioRTM = true;
                            newItem.Properties.Item("DependentUpon").Value = item.FileNames[0];
                        }
                    }
                    //如果是免费版VS，则要手动修改项目文件。
                    if (!isVisualStudioRTM)
                    {
                        Helper.NestedHelper helper = new NestedHelper();
                        string parentItemType = item.Properties.Item("ItemType").Value.ToString();
                        if (string.IsNullOrEmpty(parentItemType))
                        {
                            parentItemType = "None";
                        }
                        //防止项目未保存，而本地的项目文件却已经修改。
                        item.ContainingProject.Save();
                        helper.NestedFile(item, parentItemType, newFile, itemType);
                    }
                }
                return newItem;
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
                return null;
            }
        }

        public static ProjectItem FindFileInProject(this Project project,string fileName)
        {
            int iFound = 0;
            uint itemId = 0;
            EnvDTE.ProjectItem item=null;
            Microsoft.VisualStudio.Shell.Interop.VSDOCUMENTPRIORITY[] pdwPriority = new Microsoft.VisualStudio.Shell.Interop.VSDOCUMENTPRIORITY[1];

            // obtain a reference to the current project as an IVsProject type
            Microsoft.VisualStudio.Shell.Interop.IVsProject VsProject = VsHelper.ToVsProject(project);
            // this locates, and returns a handle to our source file, as a ProjectItem
            VsProject.IsDocumentInProject(fileName, out iFound, pdwPriority, out itemId);

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
            return item;
        }
        public static void AddNestedFile(this Project project, string parentFile, string newFile, string ItemType, bool force = false)
        {
            ProjectItem parentItem= project.FindFileInProject(parentFile);
            if (parentItem != null)
            {
                ProjectItem newItem = project.FindFileInProject(newFile);
                if (newItem == null)
                {
                    newItem = parentItem.ProjectItems.AddFromFile(newFile);
                }
                newItem.Properties.Item("ItemType").Value = ItemType;
            }
        }
        public static bool IsKind(this Project project, params string[] kindGuids)
        {
            foreach (var guid in kindGuids)
            {
                if (project.Kind.Equals(guid, StringComparison.OrdinalIgnoreCase))
                    return true;
            }

            return false;
        }

        public static IEnumerable<Project> GetAllProjects()
        {
            return _dte.Solution.Projects
                  .Cast<Project>()
                  .SelectMany(GetChildProjects)
                  .Union(_dte.Solution.Projects.Cast<Project>())
                  .Where(p => { try { return !string.IsNullOrEmpty(p.FullName); } catch { return false; } });
        }

        private static IEnumerable<Project> GetChildProjects(Project parent)
        {
            try
            {
                if (parent.Kind != ProjectKinds.vsProjectKindSolutionFolder && parent.Collection == null)  // Unloaded
                    return Enumerable.Empty<Project>();

                if (!string.IsNullOrEmpty(parent.FullName))
                    return new[] { parent };
            }
            catch (COMException)
            {
                return Enumerable.Empty<Project>();
            }

            return parent.ProjectItems
                    .Cast<ProjectItem>()
                    .Where(p => p.SubProject != null)
                    .SelectMany(p => GetChildProjects(p.SubProject));
        }

        public static bool IsSolutionLoaded()
        {
            if (_dte.Solution == null)
                return false;

            return GetAllProjects().Any();
        }

        public static Project GetActiveProject()
        {
            try
            {
                Array activeSolutionProjects = _dte.ActiveSolutionProjects as Array;

                if (activeSolutionProjects != null && activeSolutionProjects.Length > 0)
                    return activeSolutionProjects.GetValue(0) as Project;
            }
            catch (Exception ex)
            {
                Logger.Log("Error getting the active project" + ex);
            }

            return null;
        }

        public static bool DeleteFileFromProject(string file)
        {
            ProjectItem item = _dte.Solution.FindProjectItem(file);

            if (item == null)
                return false;

            try
            {
                item.Delete();
                return true;
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
                return false;
            }
        }
    }

    public static class ProjectTypes
    {
        public const string ASPNET_5 = "{8BB2217D-0F2D-49D1-97BC-3654ED321F3B}";
        public const string DOTNET_Core = "{9A19103F-16F7-4668-BE54-9A1E7A4F7556}";
        public const string WEBSITE_PROJECT = "{E24C65DC-7377-472B-9ABA-BC803B73C61A}";
        public const string UNIVERSAL_APP = "{262852C6-CD72-467D-83FE-5EEB1973A190}";
        public const string SSDT = "{00d1a9c2-b5f0-4af3-8072-f6c62b433612}";
    }
}
