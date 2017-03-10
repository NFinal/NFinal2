using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnvDTE;
using EnvDTE80;
using EnvDTE90;
using System.IO;
using System.Xml;
using System.Xml.Linq;

namespace NFinalCompiler.Helper
{
    public class NestedHelper
    {
        public XmlNode GetItemGrounp(ProjectItem projectItem,out XmlDocument doc,out bool hasItemGroup)
        {
            string projectFileName = projectItem.ContainingProject.FileName;
            doc = null;
            hasItemGroup = false;
            if (File.Exists(projectFileName))
            {
                doc = new System.Xml.XmlDocument();
                doc.Load(projectFileName);
                XmlNode compileNode = doc.SelectSingleNode("/Project/ItemGroup/Compile");
                XmlNode itemGroupNode = null;
                if (compileNode == null)
                {
                    itemGroupNode = doc.CreateElement("ItemGroup");
                    doc.DocumentElement.AppendChild(itemGroupNode);
                }
                else
                {
                    itemGroupNode = compileNode.ParentNode;
                }
                return itemGroupNode;
            }
            return null;
        }
        public void NestedFile(ProjectItem projectItem,string itemType,string fileName,string subItemType)
        {
            string projectFileName= projectItem.ContainingProject.FileName;
            var property= projectItem.ContainingProject.Properties;
            bool hasModify = false;
            if (File.Exists(projectFileName))
            {
                XmlDocument doc = new System.Xml.XmlDocument();
                doc.Load(projectFileName);
                XmlNode compileNode = doc.SelectSingleNode("/Project/ItemGroup/Compile");
                XmlNode itemGroupNode = null;
                bool hasItemGroup = false;
                if (compileNode == null)
                {
                    hasModify = true;
                    hasItemGroup = false;
                    itemGroupNode = doc.CreateElement("ItemGroup");
                }
                else
                {
                    hasItemGroup = true;
                    itemGroupNode = compileNode.ParentNode;
                }

                string parentFileName = projectItem.FileNames[0];
                string parentRelativeFileName = parentFileName.Substring(Path.GetDirectoryName(projectFileName).Length+1);
                string parentNodePath = string.Format("//{0}[@Update='{1}']",itemType, parentRelativeFileName);
                XmlNode parentNode = doc.SelectSingleNode(parentNodePath);
                if (parentNode == null)
                {
                    hasModify = true;
                    parentNode = doc.CreateElement(itemType);
                    var updateAttribute = doc.CreateAttribute("Update");
                    updateAttribute.Value = parentRelativeFileName;
                    parentNode.Attributes.Append(updateAttribute);
                    var generatorNode= doc.CreateElement("Generator");
                    parentNode.AppendChild(generatorNode);
                    itemGroupNode.AppendChild(parentNode);
                }
                
                string relativeFileName= fileName.Substring(Path.GetDirectoryName(projectFileName).Length+1);
                string NeestedNodePath = string.Format("//{0}[@Update='{1}']",subItemType, relativeFileName);
                XmlNode NestedNode = itemGroupNode.SelectSingleNode(NeestedNodePath);
                if (NestedNode == null)
                {
                    hasModify = true;
                        var manager = projectItem.ContainingProject.ConfigurationManager;
                        NestedNode = doc.CreateElement(subItemType);
                        var updateAttribute = doc.CreateAttribute("Update");
                        updateAttribute.Value = relativeFileName;
                    NestedNode.Attributes.Append(updateAttribute);
                        NestedNode.InnerXml = string.Format("<DependentUpon>{0}</DependentUpon>", Path.GetFileName(parentFileName));
                    itemGroupNode.AppendChild(NestedNode);
                }
                else
                {
                    XmlNode dependentUponNode = null;
                    dependentUponNode = NestedNode.SelectSingleNode("./DependentUpon");
                    if (dependentUponNode == null)
                    {
                        hasModify = true;
                        dependentUponNode = doc.CreateElement("DependentUpon");
                        dependentUponNode.InnerText = Path.GetFileName(parentFileName);
                        NestedNode.AppendChild(dependentUponNode);
                    }
                    else
                    {
                        if (dependentUponNode.InnerText != Path.GetFileName(parentFileName))
                        {
                            hasModify = true;
                            dependentUponNode.InnerText = Path.GetFileName(parentFileName);
                        }
                    }
                }
     

                if (!hasItemGroup)
                {
                    doc.DocumentElement.AppendChild(itemGroupNode);
                }
                if (hasModify)
                {
                    doc.Save(projectFileName);
                }
            }
        }
    }
}
