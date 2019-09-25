using ee.Framework.Schema;

namespace ee.Framework.Utiltity
{
    public class Validator
    {
        public static void Required(string value, string propertyName)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new EeException(ErrorCodes.InvalidParameter, $"{propertyName} is required.");
            }
        }
    }
}
