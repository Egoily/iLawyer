using ee.iLawyer.Db.Entities;
using FluentNHibernate.Mapping;

namespace ee.iLawyer.Db.Mappings
{
    public class ClientMap : ClassMap<Client>
    {
        public ClientMap()
        {
            Table("Clients");
            LazyLoad();

            Id(x => x.Id);
            //.GeneratedBy.Assigned();   
            Map(x => x.IsNP);
            Map(x => x.Name);
            Map(x => x.ContactNo);
            Map(x => x.Abbreviation);
            Map(x => x.Impression);
            Map(x => x.CreateTime);
            Map(x => x.UpdateTime);

            HasMany(x => x.Properties).Cascade.All().Inverse();
        }
    }
}
