using ee.Framework;
using ee.Framework.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ee.iLawyer.Ops.Contact.Args
{

    public class GetPropertyCategoriesRequest : BasePageRequest
    {
    }
    public class GetPropertyItemCategoriesRequest : BasePageRequest
    {
        public virtual string Code { get; set; }
    }

}
