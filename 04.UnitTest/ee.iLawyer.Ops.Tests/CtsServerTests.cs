using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ee.SessionFactory;
using ee.iLawyer.SessionFactoryBuilder.Sqlite;
using ee.iLawyer.Ops.Contact.Args;
using System.Collections.Generic;
using ee.iLawyer.Ops.Contact.DTO;

namespace ee.iLawyer.Ops.Tests
{
    [TestClass]
    public class CtsServerTests
    {

        CtsService service;
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
            service = new CtsService();
        }

        [TestMethod()]
        public void GetPropertyItemCategoriesTest_Main()
        {
            var request = new GetPropertyItemCategoriesRequest()
            {

            };
            var response = service.GetPropertyItemCategories(request);

            Assert.AreEqual(0, response.Code);
        }

        [TestMethod()]
        public void GetPropertyItemCategoriesTest_Item()
        {
            var request = new GetPropertyItemCategoriesRequest()
            {
                Code = "Phone"
            };
            var response = service.GetPropertyItemCategories(request);

            Assert.AreEqual(0, response.Code);
        }


        [TestMethod()]
        public void AddClientTest()
        {
            var properties = new List<CategoryValue>
            {
               new CategoryValue(11,"个人手机", "13610142196" ),
                new CategoryValue( 21,"邮箱", "egoily@hotmail.com" ),
                new CategoryValue(30,"地址","番禺市桥" )
            };


            var request = new AddClientRequest()
            {
                Abbreviation = "Ego2",
                Impression = "good",
                IsNP = true,
                Name = "Ego Huang",
                Properties = properties,
            };
            var response = service.AddClient(request);

            Assert.AreEqual(0, response.Code);
        }

    }
}
