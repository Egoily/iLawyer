using FluentNHibernate.Mapping;
using ee.iLawyer.Db.Entity;

namespace ee.iLawyer.Db.Entity.Mapping
{
    public class ClientPropertyItemMap : ClassMap<ClientPropertyItem>
    {
        public ClientPropertyItemMap()
        {
            Table("ClientPropertyItems");
            Id(x => x.Id);
            //.GeneratedBy.Assigned();   
            Map(x => x.Value);
            Map(x => x.OrderNo);
            Map(x => x.CreateTime);
            Map(x => x.UpdateTime);

            References(x => x.Client).Column("ClientId").Cascade.SaveUpdate().NotFound.Ignore();
            References(x => x.Category).Column("PropertyItemCategoryId").Cascade.SaveUpdate().NotFound.Ignore();
        }
    }
}
