using ee.Framework;
using ee.Framework.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ee.iLawyer.Ops.Utiltity
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
