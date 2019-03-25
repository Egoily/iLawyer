using FluentNHibernate.Mapping;
using ee.iLawyer.Db.Entity;

namespace ee.iLawyer.Db.Entity.Mapping
{
    public class PropertyItemCategortyMap : ClassMap<PropertyItemCategory>
    {
        public PropertyItemCategortyMap()
        {
            Table("PropertyItemCategories");
            LazyLoad();
            Id(x => x.Id);
            //.GeneratedBy.Assigned();
            Map(x => x.Code);
            Map(x => x.Name);
            Map(x => x.Icon);
            Map(x => x.PickerType);
            Map(x => x.IsEnabled);
            References(x => x.Parent).Column("ParentId").LazyLoad(Laziness.False).NotFound.Ignore();
            HasMany(x => x.Children).KeyColumn("ParentId").Not.LazyLoad();
        }
    }
}
