using FluentNHibernate.Mapping;

namespace ee.iLawyer.Db.Entity.Mapping
{
    public class ProjectCauseMap : ClassMap<ProjectCause>
    {
        public ProjectCauseMap()
        {
            Table("ProjectCauses");
            LazyLoad();
            Id(x => x.Id);
            //.GeneratedBy.Assigned();
            Map(x => x.Name);
            Map(x => x.OrderNo);
        }
    }
}
