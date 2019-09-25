using ee.SessionFactory;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;

namespace ee.iLawyer.SessionFactoryBuilder.SqlServer
{
    public class SqlServerSessionFactoryBuilder : ISessionFactoryBuilder
    {

        private static ISessionFactory sessionFactory;
        public static ISessionFactory GetFactory()
        {
            return sessionFactory;
        }

        public void Build(params string[] args)
        {

            sessionFactory = CreateSessionFactory();
        }

        public ISessionFactory CreateSessionFactory()
        {
            return Fluently.Configure()
                    .Database(MsSqlConfiguration.MsSql2012.ConnectionString(c => c.FromConnectionStringWithKey("ConnectionString")).ShowSql())
                    .Cache(c =>
                           c.ProviderClass<NHibernate.Caches.SysCache2.SysCacheProvider>()
                            //.ProviderClass<NHibernate.Cache.HashtableCacheProvider>()//<--Cache Level Two
                            .UseSecondLevelCache()
                            .UseQueryCache()
                            .UseMinimalPuts()
                           )
                    .Mappings(m => m.FluentMappings.AddFromAssemblyOf<ee.iLawyer.Db.Factory.RealAssembly>())
                    //.ExposeConfiguration(f => f.SetInterceptor(new SqlStatementInterceptor()))
                    .BuildSessionFactory();
        }


    }
}
