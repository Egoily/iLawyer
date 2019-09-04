using ee.iLawyer.Ops.Contact.Args;
using ee.iLawyer.Ops.Contact.DTO;
using ee.iLawyer.SessionFactoryBuilder.Sqlite;
using ee.SessionFactory;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace ee.iLawyer.Ops.Tests
{
    [TestClass]
    public class CtsServerTests
    {
        private ILawyerService service;

        private static void Build()
        {
            SessionManager.Builder = new SqliteSessionFactoryBuilder();
            SessionManager.Builder.Build(@"..\..\..\..\03.Application\ee.iLawyer\bin\Debug\database.db");
        }
        [TestInitialize()]
        public void Initialize()
        {
            log4net.Config.XmlConfigurator.Configure();
            Build();
            service = new ILawyerService();
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
        public void CreateClientTest()
        {
            var properties = new List<CategoryValue>
            {
               new CategoryValue(11,"个人手机", "13610142196" ),
                new CategoryValue( 21,"邮箱", "egoily@hotmail.com" ),
                new CategoryValue(30,"地址","番禺市桥" )
            };


            var request = new CreateClientRequest()
            {
                Abbreviation = "Ego2",
                Impression = "good",
                IsNP = true,
                Name = "Ego Huang",
                Properties = properties,
            };
            var response = service.CreateClient(request);

            Assert.AreEqual(0, response.Code);
        }

        [TestMethod()]
        public void QueryClientTest()
        {


            var request = new QueryClientRequest()
            {
                Keys = new int[] { 1, 2, 3 }
            };
            var response = service.QueryClient(request);

            Assert.AreEqual(0, response.Code);
        }


        [TestMethod()]
        public void CreateJudgeTest()
        {


            var request = new CreateJudgeRequest()
            {
                Name = "Ego",
                ContactNo = "13610142196",
                Gender = Contact.Gender.Male,
                Grade = Contact.JudgeGrade.FirstClassJustice,
                Duty = "This is duty.",
                InCourtId = 1,
            };

            var json = JsonConvert.SerializeObject(request);


            var obj = JsonConvert.DeserializeObject<CreateJudgeRequest>(json);
            //var response = service.CreateJudge(request);
            //Assert.AreEqual(0, response.Code);
        }

    }
}
