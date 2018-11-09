using ee.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ee.iLawyer.Ops.Contact.Args
{

    public class GetPropertyCategoryRequest : BasePageRequest
    {
    }
    public class GetPropertyItemCategoryRequest : BasePageRequest
    {
        public virtual string Code { get; set; }
    }

}
