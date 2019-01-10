using FluentNHibernate.Mapping;
using ee.iLawyer.Db.Entity;

namespace ee.iLawyer.Db.Entity.Mapping
{
    public class ClientMap : ClassMap<Client>
    {
        public ClientMap()
        {
            Table("Clients");
            Id(x => x.Id);
            //.GeneratedBy.Assigned();   
            Map(x => x.IsNP);
            Map(x => x.Name);
            Map(x => x.Abbreviation);
            Map(x => x.Impression);
            Map(x => x.CreateTime);
            Map(x => x.UpdateTime);

            HasMany(x => x.Properties).Cascade.All().Inverse();
        }
    }
}
