using System;
using System.Collections.Generic;
using System.Text;

namespace NFinal.Validation
{
    public class MaxLengthAttribute : ValidateAttribte
    {
        private int length { get; set; }
        public MaxLengthAttribute(int length)
        {
            this.length = length;
        }

        public override bool Validate { get {
                return this.value.value.Length < length;
            } }
        public override string ErrorMessage
        {
            get {
                return $"{name} 长度不能大于{length}";
            }
        }
    }
}
