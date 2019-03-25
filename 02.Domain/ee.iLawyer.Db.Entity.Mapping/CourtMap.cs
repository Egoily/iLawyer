using FluentNHibernate.Mapping;
using ee.iLawyer.Db.Entity;

namespace ee.iLawyer.Db.Entity.Mapping
{
    public class CourtMap : ClassMap<Court>
    {
        public CourtMap()
        {
            Table("Courts");
            LazyLoad();
            Id(x => x.Id);
                //.GeneratedBy.Assigned();
            Map(x => x.Name);
            Map(x => x.Rank);
            Map(x => x.Province);
            Map(x => x.City);
            Map(x => x.County);
            Map(x => x.Address);
            Map(x => x.ContactNo);
            HasMany(x => x.Judges);
        }
    }
}
