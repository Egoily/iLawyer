using FluentNHibernate.Mapping;
using ee.iLawyer.Db.Entity;

namespace ee.iLawyer.Db.Entity.Mapping
{
    public class ProjectCategortyMap : ClassMap<ProjectCategory>
    {
        public ProjectCategortyMap()
        {
            Table("ProjectCategories");
            Id(x => x.Code).GeneratedBy.Assigned();
            Map(x => x.Name);
            Map(x => x.OrderNo);
            References(x => x.Parent).Column("ParentId").LazyLoad(Laziness.False).NotFound.Ignore();
            HasMany(x => x.Children).KeyColumn("ParentId").Not.LazyLoad();
        }
    }
}
