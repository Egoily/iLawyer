using ee.iLawyer.Ops.Contact.Args;
using ee.iLawyer.Ops.Contact.Args.SystemManagement;
using ee.iLawyer.Ops.Contact.DTO;
using ee.SessionFactory;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace ee.iLawyer.Ops.Tests
{
    [TestClass]
    public class iLawyerServerTests
    {
        private ILawyerService service;

        private static void Build()
        {
            SessionManager.Builder = new SessionFactoryBuilder.SqlServer.SqlServerSessionFactoryBuilder();
            SessionManager.Builder.Build();
        }
        [TestInitialize()]
        public void Initialize()
        {
            ee.Framework.Logging.Logger.Configure("ee.iLawyer.Ops.Tests");
            Build();
            service = new ILawyerService();
        }



        [TestMethod()]
        public void GetAreas()
        {
            var request = new GetAreasRequest()
            {

            };
            var response = service.GetAreas(request);

            Assert.AreEqual(0, response.Code);
        }

        [TestMethod()]
        public void GetPropertyItemCategoriesTest_Main()
        {
            var request = new GetPropertyPicksRequest()
            {

            };
            var response = service.GetPropertyPicks(request);

            Assert.AreEqual(0, response.Code);
        }

        [TestMethod()]
        public void GetPropertyItemCategoriesTest_Item()
        {
            var request = new GetPropertyPicksRequest()
            {
                Code = "Phone"
            };
            var response = service.GetPropertyPicks(request);

            Assert.AreEqual(0, response.Code);
        }

        [TestMethod()]
        public void Login()
        {
            var request = new LoginRequest()
            {
                LoginName = "Elise",
                Password = "Elise"
            };
            var response = service.Login(request);

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
