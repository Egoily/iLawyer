using ee.iLawyer.Db.Entities;
using FluentNHibernate.Mapping;

namespace ee.iLawyer.Db.Mappings
{
    public class JudgeMap : ClassMap<Judge>
    {
        public JudgeMap()
        {
            Table("Judges");
            LazyLoad();

            Id(x => x.Id);
            //.GeneratedBy.Assigned();
            Map(x => x.ContactNo);
            Map(x => x.Name);
            Map(x => x.Gender);
            Map(x => x.Duty);
            Map(x => x.Grade);
            References(x => x.InCourt).Column("CourtId").NotFound.Ignore();
        }
    }
}
