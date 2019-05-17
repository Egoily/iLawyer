using FluentNHibernate.Mapping;
namespace ee.iLawyer.Db.Entity.Mapping
{
    public class ProjectProgressMap : ClassMap<ProjectProgress>
    {
        public ProjectProgressMap()
        {
            Table("ProjectProgresss");
            LazyLoad();
            Id(x => x.Id)
            .GeneratedBy.Assigned();
            Map(x => x.HandleTime);
            Map(x => x.Content);
            Map(x => x.CreateTime);
            References(x => x.InProject).Column("ProjectId").NotFound.Ignore();

        }
    }
}
