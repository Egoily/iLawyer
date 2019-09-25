using ee.iLawyer.Db.Entities.Foundation;
using FluentNHibernate.Mapping;

namespace ee.iLawyer.Db.Mappings.Foundation
{
    public class PicklistMap : ClassMap<Picklist>
    {
        public PicklistMap()
        {
            Table("Sys_Picklist");
            Cache.NonStrictReadWrite();
            LazyLoad();

            Id(x => x.Id);
            Map(x => x.Category);
            Map(x => x.Name);
            Map(x => x.Code);
            Map(x => x.Value);
            Map(x => x.SubValue);
            Map(x => x.Enabled);
            Map(x => x.Description);
            Map(x => x.OrderNo);
            Map(x => x.ParentId);
            //References(x => x.Parent).Column("ParentId").NotFound.Ignore();
            //HasMany(x => x.Children).KeyColumn("ParentId").Not.LazyLoad();
        }
    }
}
