using ee.Framework.Schema;
using ee.iLawyer.Ops.Contact.DTO;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Web.Http;

namespace ee.iLawyer.WebApi.Controllers
{
    [RoutePrefix("api/client")]
    public class ClientController : BaseController
    {
        // GET api/Client/5
        public BaseObjectResponse<Client> Get(int id)
        {
            return Service.GetClient(new BaseIdRequest() { Id = id });
        }
        // POST api/Client
        public BaseResponse Post([FromBody]dynamic para)
        {
            var request = JsonConvert.DeserializeObject<Ops.Contact.Args.CreateClientRequest>(para.ToString());
            return Service.CreateClient(request);
        }

        // PUT api/Client/{id}
        public BaseResponse Put(int id, [FromBody]dynamic para)
        {
            var request = JsonConvert.DeserializeObject<Ops.Contact.Args.UpdateClientRequest>(para.ToString());
            request.Id = id;
            return Service.UpdateClient(request);
        }

        // DELETE api/Client/{id}
        public BaseResponse Delete(int id)
        {
            return Service.RemoveClient(new Ops.Contact.Args.RemoveClientRequest() { Ids = new List<int>() { id } });
        }
        [Route("query"), HttpGet]
        public BaseQueryResponse<Client> Query(string name, bool? isnp, int pageIndex, int pageSize)
        {

            return Service.QueryClient(
                new Ops.Contact.Args.QueryClientRequest()
                {
                    Name = name,
                    IsNP = isnp,
                    PageIndex = pageIndex,
                    PageSize = pageSize,
                });
        }
    }
}