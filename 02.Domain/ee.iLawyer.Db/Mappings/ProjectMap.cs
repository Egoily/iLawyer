using ee.iLawyer.Db.Entities;
using FluentNHibernate.Mapping;

namespace ee.iLawyer.Db.Mappings
{
    public class ProjectMap : ClassMap<Project>
    {
        public ProjectMap()
        {
            Table("Projects");
            LazyLoad();

            Id(x => x.Id);
            //.GeneratedBy.Assigned();
            Map(x => x.Name);
            Map(x => x.Code);
            Map(x => x.Level);
            Map(x => x.Details);
            Map(x => x.OtherLitigant);
            Map(x => x.InterestedParty);
            Map(x => x.Contacts);
            Map(x => x.DealDate);
            Map(x => x.CreateTime);
            Map(x => x.UpdateTime);

            HasOne(x => x.Account).Cascade.All();

            HasMany(x => x.InvolvedClients).Cascade.All().Inverse();
            HasMany(x => x.TodoList).Cascade.All().Inverse();
            HasMany(x => x.Progresses).Cascade.All().Inverse();
            References(x => x.Category).Column("CategoryId").NotFound.Ignore();
            References(x => x.Cause).Column("CauseId").NotFound.Ignore();
            References(x => x.Owner).Column("OwnerId").NotFound.Ignore();


        }
    }
}
