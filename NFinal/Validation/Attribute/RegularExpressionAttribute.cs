using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace NFinal.Validation.Attribute
{
    public class RegularExpressionAttribute:ValidateAttribte 
    {
        private string pattern;
        public RegularExpressionAttribute(string pattern)
        {
            this.pattern = pattern;
        }
        public override bool Validate {
            get {
                Regex reg = new Regex(this.pattern);
                return reg.IsMatch(value.value);
            }
        }
        public override string ErrorMessage
        {
            get {
                return $"{name}输入有误";
            }
        }
    }
}
