using System;

namespace ee.Framework.Attributes
{
    public abstract class ValidateAttibute : Attribute
    {

        public string ErrorMessage { get; set; }
        public object InputValue { get; set; }
        public string PropertyName { get; set; }

        public ValidateAttibute(string errorMessage)
        {
            this.ErrorMessage = errorMessage;
        }

        public abstract bool Validate();
    }
}
