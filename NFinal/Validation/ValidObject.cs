//======================================================================
//
//        Copyright : Zhengzhou Strawberry Computer Technology Co.,LTD.
//        All rights reserved
//        
//        Application:NFinal MVC framework
//        Filename : ValidObject.cs
//        Description :验证对象
//
//        created by Lucas at  2015-5-31
//     
//        WebSite:http://www.nfinal.com
//
//======================================================================
using System;
using System.Collections.Generic;
using System.Text;

namespace NFinal.Validation
{
    /// <summary>
    /// 验证对象
    /// </summary>
    public class ValidObject
    {  
        /// <summary>
        /// 名称
        /// </summary>
        public string name;
        /// <summary>
        /// 值
        /// </summary>
        public string value;
        private bool isDirty;
        public ValidResult validResult;
        public bool IsValid
        {
            get
            {
                return !isDirty;
            }
            set
            {
                isDirty = !value;
            }
        }
        internal ValidObject(string name, string value)
        {
            this.name = name;
            this.value = value;
            this.validResult = new ValidResult();
            this.isDirty = false;
        }
        public static implicit operator ValidObject(string value)
        {
            if (value == null)
            {
                return default(ValidObject);
            }
            return new ValidObject("",value);
        }
    }
}
