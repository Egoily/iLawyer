using FluentNHibernate.Mapping;

namespace ee.iLawyer.Db.Entity.Mapping
{
    public class AreaMap : ClassMap<Area>
    {
        public AreaMap()
        {
            Table("Areas");

            //read - only是只读型，缓存不更新，适用于不发生改变的数据，效率最高，事务隔离级别最低，
            //read - write读写型，缓存在数据变化时触发更新，适用于变化的数据，
            //nonstrict - read - write不严格读写型，缓存不定期更新，适用于变化频率低的数据，
            //transactional事务型，缓存在数据变化时更新，并且支持事务，效率最低，事务隔离级别最高。

            Cache.ReadOnly();
            LazyLoad();
            Id(x => x.AreaCode);
            //.GeneratedBy.Assigned();
            Map(x => x.Name);
            References(x => x.Parent).Column("ParentId").LazyLoad(Laziness.False).NotFound.Ignore();
            HasMany(x => x.Children).KeyColumn("ParentId").Not.LazyLoad();
        }
    }
}
