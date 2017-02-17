//======================================================================
//
//        Copyright : Zhengzhou Strawberry Computer Technology Co.,LTD.
//        All rights reserved
//
//        Application:NFinal MVC framework
//        Filename :GenConfig.cs
//        Description :创建配置类
//
//        created by Lucas at  2015-6-30`
//     
//        WebSite:http://www.nfinal.com
//
//======================================================================
using System;
using System.Collections.Generic;
using System.Web;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace NFinal.Compile
{
    public class GenConfig
    {
        public System.Collections.Generic.List<string> controllerFiles = new System.Collections.Generic.List<string>();
        public System.Collections.Generic.List<string> bllFiles = new System.Collections.Generic.List<string>();

        public static GenConfig Load(string fileName)
        {
            GenConfig config=null;
            if (File.Exists(fileName))
            {
                XmlSerializer ser = new XmlSerializer(typeof(GenConfig));
                StreamReader sr = new StreamReader(fileName, System.Text.Encoding.UTF8);
                config = (GenConfig)ser.Deserialize(sr);
                sr.Close();
            }
            return config;
        }
    }
}