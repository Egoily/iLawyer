using ee.iLawyer.Db.Entity;
using ee.iLawyer.SessionFactoryBuilder.Sqlite;
using ee.SessionFactory;
using ee.SessionFactory.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace ee.iLawyer.SessionFactory.Sqlite.Tests
{
    [TestClass()]
    public class SessionFactoryTests
    {
        static void Build()
        {
            SessionManager.Builder = new SqliteSessionFactoryBuilder();
            SessionManager.Builder.Build(@"..\..\..\..\03.Application\ee.iLawyer\bin\Debug\database.db");
        }
        [TestInitialize()]
        public void Initialize()
        {
            log4net.Config.XmlConfigurator.Configure();
            Build();
        }

        [TestMethod()]
        public void QueryAreaTest()
        {
            using (var session = SessionManager.GetConnection())
            {
                using (var repo = new NhRepository<Area>())
                {
                    var query = repo.Query(x => x.AreaCode == "440000");

                    var list = query.ToList();
                }
            }
        }

    }
}