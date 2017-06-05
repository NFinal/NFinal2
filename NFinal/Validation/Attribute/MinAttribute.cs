using System;
using System.Collections.Generic;
using System.Text;

namespace NFinal.Validation.Attribute
{
    public class MinAttribute : ValidateAttribte
    {
        private double number;
        public MinAttribute(double number)
        {
            this.number = number;
        }
        public override bool Validate
        {
            get
            {
                return value < number;
            }
        }
        public override string ErrorMessage
        {
            get
            {
                return $"{name} 最小不能小于{number}";
            }
        }
    }
}
