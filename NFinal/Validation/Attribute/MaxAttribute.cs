using System;
using System.Collections.Generic;
using System.Text;

namespace NFinal.Validation
{
    public class MaxAttribute : ValidateAttribte
    {
        private double number;
        public MaxAttribute(double number)
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
                return $"{name} 最大不能超过{number}";
            }
        }
    }
}
