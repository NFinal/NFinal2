using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;
using System.Diagnostics;
using System.Reflection;

namespace NFinal.AutoConfig
{
    /// <summary>
    /// 初始化模块时修改*.proj文件类
    /// 修改完成后一定要选择放弃，否则会修改失败
    /// </summary>
    public class Project
    {
        private string projectFile=null;
        private XmlNamespaceManager namespaceManager = null;
        private XmlDocument doc = null;
        private string root;
        //private bool hasChanged=false;
        private string nameSpace = null;
        public Project(string projectFile)
        {
            this.projectFile = projectFile;
            this.root= new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory).Parent.FullName + "\\";
            //修改工程文件,加入dll引入
            doc = new System.Xml.XmlDocument();
            doc.Load(projectFile);
            nameSpace = doc.DocumentElement.Attributes["xmlns"].Value;
            namespaceManager = new XmlNamespaceManager(doc.NameTable);
            namespaceManager.AddNamespace("x", nameSpace);
        }
        public void Save()
        {
            doc.Save(projectFile);
        }
        //将相对根目录的路径转为绝对路径
        public string MapPath(string url)
        {
            return root + url.Trim('/').Replace('/', '\\');
        }
        public string MPath(string url)
        {
            return  url.Replace(root,"")+"\\";
        }

        public static string GetFileName(string fileName)
        {
            string dir = Path.GetDirectoryName(fileName);
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            return fileName;
        }
        public bool hasAdded(string fileName)
        {
            return true;
        }
        /// <summary>
        /// 添加文件夹
        /// </summary>
        /// <param name="folderName"></param>
        public void AddFolder(string folderName)
        {
            //<Folder Include="AA\Models\BLL" />
            string relativeFileName = folderName.Substring(root.Length);
            string folderInclude = string.Format("x:ItemGroup/x:Folder[@Include='{0}']", relativeFileName);
            XmlNode node = doc.DocumentElement.SelectSingleNode(folderInclude,namespaceManager);
            if (node == null)
            {
                XmlNode GroupInclude= doc.DocumentElement.SelectSingleNode("x:ItemGroup[2]", namespaceManager);
                XmlNode FolderNode = doc.CreateElement("Folder", nameSpace);
                XmlAttribute FolderNodeIncludeAttr = doc.CreateAttribute("Include");
                FolderNodeIncludeAttr.Value = relativeFileName;
                FolderNode.Attributes.Append(FolderNodeIncludeAttr);
                GroupInclude.AppendChild(FolderNode);
            }
        }
        /// <summary>
        /// 添加文件
        /// </summary>
        /// <param name="fileName"></param>
        public  void AddFile(string fileName)
        {
            XmlNode GroupInclude = doc.DocumentElement.SelectSingleNode("x:ItemGroup[2]", namespaceManager);
            string relativeFileName = fileName.Substring(root.Length);
            string shortFileName = Path.GetFileName(fileName);
            if (fileName.EndsWith(".cs"))
            {
                //截取Include值
                string compileInclude = string.Format("x:ItemGroup/x:Compile[@Include='{0}']", relativeFileName);
                XmlNode node = doc.DocumentElement.SelectSingleNode(compileInclude, namespaceManager);
                if (node == null)
                {
                    //<Compile Include="App\Handler1.ashx.cs">
                    //</Compile>
                    XmlNode CompileNode = doc.CreateElement("Compile", nameSpace);
                    XmlAttribute CompileNodeIncludeAttr = doc.CreateAttribute("Include");
                    CompileNodeIncludeAttr.Value = relativeFileName;
                    CompileNode.Attributes.Append(CompileNodeIncludeAttr);
                    //<Compile Include="App\Handler1.ashx.cs">
                    //< DependentUpon > Handler1.ashx </ DependentUpon >
                    //</ Compile >
                    if (fileName.EndsWith(".ashx.cs"))
                    {
                        XmlNode DependentUponNode = doc.CreateElement("DependentUpon", nameSpace);
                        DependentUponNode.InnerText =shortFileName.Substring(0, shortFileName.Length - 3);
                        CompileNode.AppendChild(DependentUponNode);
                    }
                    //<Compile Include="App\Views\Default\Common\Public\Success.aspx.cs">
                    //< DependentUpon > Success.aspx </ DependentUpon >
                    //< SubType > ASPXCodeBehind </ SubType >
                    //</ Compile >
                    else if (fileName.EndsWith(".aspx.cs"))
                    {
                        XmlNode DependentUponNode = doc.CreateElement("DependentUpon", nameSpace);
                        DependentUponNode.InnerText = shortFileName.Substring(0, shortFileName.Length - 3);
                        XmlNode SubTypeNode = doc.CreateElement("SubType", nameSpace);
                        SubTypeNode.InnerText = "ASPXCodeBehind";
                        CompileNode.AppendChild(DependentUponNode);
                        CompileNode.AppendChild(SubTypeNode);
                    }
                    //<Compile Include="App\Views\Default\Common\Public\Success.aspx.designer.cs">
                    //< DependentUpon > Success.aspx </ DependentUpon >
                    //</ Compile >
                    else if (fileName.EndsWith(".designer.cs"))
                    {
                        XmlNode DependentUponNode = doc.CreateElement("DependentUpon", nameSpace);
                        DependentUponNode.InnerText = shortFileName.Substring(0, shortFileName.Length - 12);
                        CompileNode.AppendChild(DependentUponNode);
                    }
                    //<Compile Include="App\Views\Default\Common\Public\Footer.ascx.designer.cs">
                    //< DependentUpon > Footer.ascx </ DependentUpon >
                    //</ Compile >
                    else if (fileName.EndsWith(".ascx.cs"))
                    {
                        XmlNode DependentUponNode = doc.CreateElement("DependentUpon", nameSpace);
                        DependentUponNode.InnerText = shortFileName.Substring(0, shortFileName.Length - 3);
                        XmlNode SubTypeNode = doc.CreateElement("SubType", nameSpace);
                        SubTypeNode.InnerText = "ASPXCodeBehind";
                        CompileNode.AppendChild(DependentUponNode);
                        CompileNode.AppendChild(SubTypeNode);
                    }
                    //<Compile Include="App\Models\Tips\Common\users.cs" />
                    else
                    {
                        XmlNode SubTypeNode = doc.CreateElement("SubType", nameSpace);
                        SubTypeNode.InnerText = "Code";
                        CompileNode.AppendChild(SubTypeNode);
                    }
                    GroupInclude.AppendChild(CompileNode);
                }
            }
            else if (fileName.EndsWith(".Debug.config") || fileName.EndsWith(".Release.config"))
            {
            }
            else
            {
                string fileInclude = string.Format("x:ItemGroup/x:Content[@Include='{0}']", relativeFileName);
                XmlNode node = doc.DocumentElement.SelectSingleNode(fileInclude, namespaceManager);
                if (node == null)
                {
                    XmlNode ContentNode = doc.CreateElement("Content", nameSpace);
                    XmlAttribute ContentNodeIncludeAttr = doc.CreateAttribute("Include");
                    ContentNodeIncludeAttr.Value = relativeFileName;
                    ContentNode.Attributes.Append(ContentNodeIncludeAttr);
                    GroupInclude.AppendChild(ContentNode);
                }
            }
        }
        /// <summary>
        /// 添加文件夹下所有文件
        /// </summary>
        /// <param name="folder"></param>
        public void AddFiles(string folder)
        {
            AddFolder(folder);
            string[] fileNames = Directory.GetFiles(folder);//查找指定目录下文件名
            foreach (string fileName in fileNames)
            {
                AddFile(fileName);
            }
            string[] folderNames = Directory.GetDirectories(folder);//查询指定路径下的子目录
            foreach (string folderName in folderNames)
            {
                AddFiles(folderName);
            }
        }
        /// <summary>
        /// 添加模块
        /// </summary>
        /// <param name="folder"></param>
        public void AddModule(string folder)
        {
            AddFiles(folder);
            
        }
    }
}
