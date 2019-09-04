using ee.Framework.Schema;
using ee.iLawyer.Ops.Contact.DTO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ee.iLawyer.Ops.Tests
{
    [TestClass]
    public class UtilityTests
    {

        [TestInitialize()]
        public void Initialize()
        {

        }

        [TestMethod()]
        public void Test()
        {
            var a = new BaseObjectResponse<Court>()
            {
                Code = 0,
                Message = "this is message.",
                Object = new Court(),
            };

            object obj = a;

            var typeA = a.GetType();
            var typeObj = obj.GetType();

            var dataRes = obj.ToBaseDataResponse();
        }

     
    }
}
