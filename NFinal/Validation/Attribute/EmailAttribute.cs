using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace NFinal.Validation.Attribute
{
    /// <summary>
    /// Email验证
    /// </summary>
    public class EmailAttribute : ValidateAttribte
    {
        /// <summary>
        /// 验证
        /// </summary>
        public override bool Validate {
            get {
                Regex reg = new Regex(Pattern.email);
                return reg.IsMatch(value.value);
            }
        }
        /// <summary>
        /// 验证失败信息
        /// </summary>
        public override string ErrorMessage {
            get {
                return $"{name}必须为邮件地址";
            }
        }
    }
}
