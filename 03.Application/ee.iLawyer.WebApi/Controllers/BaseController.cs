using ee.Framework.Schema;
using ee.iLawyer.Ops;
using ee.iLawyer.Ops.Contact.Args;
using ee.iLawyer.Ops.Contact.DTO;
using System.Web.Http;

namespace ee.iLawyer.WebApi.Controllers
{

    public class BaseController : ApiController
    {
        protected static ILawyerService Service = new ILawyerService();
    }
}