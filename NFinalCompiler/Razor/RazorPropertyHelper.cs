using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnvDTE;
using EnvDTE80;
using EnvDTE90;
using System.Xml;
using System.IO;

namespace NFinalCompiler.Razor
{
    public class RazorPropertyHelper
    {
        public static string GetDebugUrl(ProjectItem projectItem)
        {
            string debugUrl = "";
            return debugUrl;
        }
        public static void AddFileHelper(ProjectItem projectItem)
        {
            string projectFileName= projectItem.ContainingProject.FileName;
            XmlDocument doc = new XmlDocument();
            doc.Load(projectFileName);
            bool mayNeedAttributeSet= Helper.ProjectHelpers.IsKind(projectItem.ContainingProject, Helper.ProjectTypes.DOTNET_Core, Helper.ProjectTypes.UNIVERSAL_APP);
            string razorPageXmlPath = null;
            if (mayNeedAttributeSet)
            {
                razorPageXmlPath = "//Content[@Update='{0}']";
            }
            else
            {
                razorPageXmlPath = "//Content[@Include='{0}']";
            }
            string razorFileName = projectItem.FileNames[0];
            string relativeRazorFileName = razorFileName.Substring(Path.GetDirectoryName(projectFileName).Length+1);
            XmlNode razorPageNode = doc.SelectSingleNode(string.Format(razorPageXmlPath, relativeRazorFileName));
            bool hasRazorPageNode = false;
            string browseUrl = "";
            bool hasModiry = false;
            if (razorPageNode == null)
            {
                hasModiry = true;
                razorPageNode = doc.CreateElement("Content");
            }
            else
            {
                hasRazorPageNode = true;
            }
            XmlNode browseToURLNode= razorPageNode.SelectSingleNode("./BrowseToURL");
            if (browseToURLNode == null)
            {
                hasModiry = true;
                if (Helper.ProjectHelpers.ContainsProperty(projectItem, "BrowseToURL"))
                {
                    browseToURLNode = doc.CreateElement("BrowseToURL");
                    browseToURLNode.InnerText = browseUrl;
                    razorPageNode.AppendChild(browseToURLNode);
                }
            }
            else
            {
                if (browseToURLNode.InnerText != browseUrl)
                {
                    hasModiry = true;
                    razorPageNode.AppendChild(browseToURLNode);
                }
            }
            if (!hasRazorPageNode)
            {
                doc.DocumentElement.AppendChild(razorPageNode);
            }
            if (hasModiry)
            {
                projectItem.ContainingProject.Save();
                doc.Save(projectFileName);
            }
        }
    }
}
