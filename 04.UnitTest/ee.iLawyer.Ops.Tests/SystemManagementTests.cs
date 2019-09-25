using ee.iLawyer.Ops.Contact.Args.SystemManagement;
using ee.SessionFactory;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace ee.iLawyer.Ops.Tests
{
    [TestClass]
    public class SystemManagementTests
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
        public void Register()
        {
            var request = new RegisterRequest()
            {
                UserName = "Test",
                Password = "Test",
                PhoneNo = "123456789",
                RoleIds = new List<int> { 2 },
            };
            var response = service.Register(request);

            Assert.AreEqual(0, response.Code);
        }


        [TestMethod()]
        public void Login()
        {
            var request = new LoginRequest()
            {
                LoginName = "Test",
                Password = "Test"
            };
            var response = service.Login(request);

            Assert.AreEqual(0, response.Code);
        }

        [TestMethod()]
        public void Grant_Increase()
        {
            var request = new GrantRequest()
            {
                UserId = 5,
                Pattern = Contact.OperatePattern.Increase,
                RoleIds = new List<int> { 3 },
                PermissionGroupIds = new List<int> { 1, 2 },
                PermissionIds = new List<int> { 1, 2, 3, 4 },
                PermissionRestrictionIds = new List<int> { 1, 2, 3 },

            };
            var response = service.Grant(request);

            Assert.AreEqual(0, response.Code);
        }
        [TestMethod()]
        public void Grant_Decrease()
        {
            var request = new GrantRequest()
            {
                UserId = 5,
                Pattern = Contact.OperatePattern.Decrease,
                RoleIds = new List<int> { 3 },
                PermissionGroupIds = new List<int> { 2 },
                PermissionIds = new List<int> { 2, 3, 4 },
                PermissionRestrictionIds = new List<int> { 2, 3 },

            };
            var response = service.Grant(request);

            Assert.AreEqual(0, response.Code);
        }
        [TestMethod()]
        public void Grant_Hybrid()
        {
            var request = new GrantRequest()
            {
                UserId = 5,
                Pattern = Contact.OperatePattern.Hybrid,
                RoleIds = new List<int> { 3 },
                PermissionGroupIds = new List<int> { 1, 3, 5, 7 },
                PermissionIds = new List<int> { 2, 4, 6, 8 },
                PermissionRestrictionIds = new List<int> { 1, 3, 4, 5 },

            };
            var response = service.Grant(request);

            Assert.AreEqual(0, response.Code);
        }
        [TestMethod()]
        public void Grant_Hybrid_Clear()
        {
            var request = new GrantRequest()
            {
                UserId = 5,
                Pattern = Contact.OperatePattern.Hybrid,
            };
            var response = service.Grant(request);

            Assert.AreEqual(0, response.Code);
        }
    }
}
