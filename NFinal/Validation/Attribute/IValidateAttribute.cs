using System;
using System.Collections.Generic;
using System.Text;

namespace NFinal.Validation
{
    public interface IValidateAttribute
    {
        void Init(string name, StringContainer value);
        string Error { get; }
        bool Validate { get; }
    }
    [AttributeUsage(AttributeTargets.Field|AttributeTargets.Property)]
    public abstract class ValidateAttribte:System.Attribute
    {
        public void Init(string name, StringContainer value)
        {
            this.name = name;
            this.value = value;
        }
        protected string name;
        protected StringContainer value;
        public abstract bool Validate { get; }
        public virtual string ErrorMessage { get { return $"input {name} error!"; } }
    }
}
