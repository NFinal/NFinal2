using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NFinal
{
    public class EmptyMasterPageModel : MasterPageModel
    {
        public EmptyMasterPageModel(string masterPageTemplatePath, string templatePath, object model) : base(masterPageTemplatePath, templatePath, model)
        {
        }
    }
    /// <summary>
    /// 母页模板实体类
    /// </summary>
    public class MasterPageModel
    {
        /// <summary>
        /// 母页模板路径
        /// </summary>
        public string MasterPageTemplatePath { get; set; }
        /// <summary>
        /// 当前模板数据，包括模板和数据
        /// </summary>
        public ViewData ViewData { get; set; }
        /// <summary>
        /// 母页模板数据初始化
        /// </summary>
        /// <param name="masterPageTemplatePath">母页模板路径</param>
        /// <param name="templatePath">当前模板路径</param>
        /// <param name="model">当前模板数据</param>
        public MasterPageModel(string masterPageTemplatePath,string templatePath, object model)
        {
            this.MasterPageTemplatePath = masterPageTemplatePath;
            this.ViewData = new NFinal.ViewData(templatePath, model);
        }
    }
}
