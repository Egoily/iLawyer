using ee.Framework.Schema;

namespace ee.iLawyer.Ops.Contact.Args
{
    public class GetAreasRequest : BasePageRequest
    {
        public virtual string[] Keys { get; set; }
        public virtual string Name { get; set; }
    }

}
