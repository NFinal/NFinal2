using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NFinal.Validation
{
    public class ValidateStatus
    {
        public string message;
        public bool isDirty;
        public ValidateStatus(string message, bool isDirty)
        {
            this.message = message;
            this.isDirty = isDirty;
        }
    }
}
