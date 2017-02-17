using System;
using System.Collections.Generic;
using System.Text;

namespace NFinal.Validation
{
    public class ValidObject
    {
        public string name;
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
