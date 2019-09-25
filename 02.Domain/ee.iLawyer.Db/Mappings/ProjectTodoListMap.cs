using ee.iLawyer.Db.Entities;
using FluentNHibernate.Mapping;
namespace ee.iLawyer.Db.Mappings
{
    public class ProjectTodoListMap : ClassMap<ProjectTodoItem>
    {
        public ProjectTodoListMap()
        {
            Table("ProjectTodoList");
            LazyLoad();

            Id(x => x.Id)
            .GeneratedBy.Assigned();
            Map(x => x.Name);
            Map(x => x.Priority);
            Map(x => x.IsSetRemind);
            Map(x => x.RemindTime);
            Map(x => x.ExpiredTime);
            Map(x => x.Content);
            Map(x => x.Status);
            Map(x => x.CompletedTime);
            Map(x => x.CreateTime);
            References(x => x.InProject).Column("ProjectId").NotFound.Ignore();

        }
    }
}
