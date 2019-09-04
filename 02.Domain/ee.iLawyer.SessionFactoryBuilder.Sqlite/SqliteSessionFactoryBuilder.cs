using ee.SessionFactory;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using System.IO;

namespace ee.iLawyer.SessionFactoryBuilder.Sqlite
{
    public class SqliteSessionFactoryBuilder : ISessionFactoryBuilder
    {
        private static string DbFile = @"database.db";

        private static ISessionFactory sessionFactory;
        public static ISessionFactory GetFactory()
        {
            return sessionFactory;
        }

        public void Build(string para)
        {
            if (!string.IsNullOrEmpty(para))
            {
                DbFile = para;
            }
            sessionFactory = CreateSessionFactory();
        }


        public ISessionFactory CreateSessionFactory()
        {
            return Fluently.Configure()
                .Database(SQLiteConfiguration.Standard
                    .UsingFile(DbFile))
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<ee.iLawyer.Db.Entity.Mapping.Factory.RealAssembly>())
                .ExposeConfiguration(BuildSchema)
                .BuildSessionFactory();
        }

        private static void BuildSchema(NHibernate.Cfg.Configuration config)
        {
            if (!File.Exists(DbFile))
            {
                new SchemaExport(config).Create(true, true);
            }
        }
    }
}
