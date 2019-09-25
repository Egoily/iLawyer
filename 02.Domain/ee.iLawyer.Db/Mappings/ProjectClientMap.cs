using FluentNHibernate.Mapping;
using ee.iLawyer.Db.Entities;
namespace ee.iLawyer.Db.Mappings
{
    public class ProjectClientMap : ClassMap<ProjectClient>
    {
        public ProjectClientMap()
        {
            Table("ProjectClients");
            LazyLoad();

            Id(x => x.Id);
            //.GeneratedBy.Assigned();
            Map(x => x.OrderNo);
            Map(x => x.CreateTime);
            References(x => x.InProject).Column("ProjectId").NotFound.Ignore();
            References(x => x.Client).Column("ClientId").NotFound.Ignore();

        }
    }
}
