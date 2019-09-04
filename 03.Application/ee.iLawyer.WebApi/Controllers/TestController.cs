using System.Web.Http;

namespace ee.iLawyer.WebApi.Controllers
{
    [RoutePrefix("api/test")]
    public class TestController : BaseController
    {
        /// <summary>
        /// This is a test for web api 
        /// </summary>
        /// <returns></returns>
        [Route(""), HttpGet]
        public string Test()
        {
            Service.Test(new Framework.Schema.BaseRequest());
            return $"Thread Id:{ System.Threading.Thread.CurrentThread.ManagedThreadId.ToString()}";
        }
    }
}