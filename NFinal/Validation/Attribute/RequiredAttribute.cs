using System;
using System.Collections.Generic;
using System.Text;

namespace NFinal.Validation
{
    public class RequiredAttribute :ValidateAttribte
    {
        public RequiredAttribute()
        {
        }
        public override bool Validate {
            get {
                return value.value!=null;
            }
        }
        public override string ErrorMessage
        {
            get {
                return $"{name} 输出不能为 null";
            }
        }
    }
}
