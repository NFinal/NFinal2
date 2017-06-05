using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace NFinal.Validation
{
    public class NumberAttribute:ValidateAttribte
    {
        public NumberAttribute()
        {
            
        }
        public override bool Validate {
            get {
                Regex reg = new Regex(Pattern.number);
                return reg.IsMatch(value.value);
            }
        }
        public override string ErrorMessage
        {
            get {
                return $"{name}必须为数字";
            }
        }
    }
}
