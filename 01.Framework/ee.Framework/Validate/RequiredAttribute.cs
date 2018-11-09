using System;

namespace ee.Framework
{
    [AttributeUsage(AttributeTargets.Property)]
    public class RequiredAttribute : ValidateAttibute
    {
        public RequiredAttribute() : base("")
        {

        }

        public RequiredAttribute(string errorMessage) : base(errorMessage)
        {
        }

        public override bool Validate()
        {
            if (string.IsNullOrEmpty(ErrorMessage))
            {
                ErrorMessage = $"{PropertyName} is required.";
            }

            return InputValue != null && !string.IsNullOrEmpty(InputValue.ToString());
        }
    }
}
