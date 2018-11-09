using FluentNHibernate.Mapping;
using ee.iLawyer.Db.Entity;
namespace ee.iLawyer.Db.Entity.Mapping
{
    public class ProjectClientMap : ClassMap<ProjectClient>
    {
        public ProjectClientMap()
        {
            Table("ProjectClients");
            Id(x => x.Id);
            //.GeneratedBy.Assigned();
            Map(x => x.OrderNo);
            Map(x => x.CreateTime);
            References(x => x.Project).Column("ProjectId").NotFound.Ignore();
            References(x => x.Client).Column("ClientId").NotFound.Ignore();

        }
    }
}
