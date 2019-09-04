using ee.Framework.Schema;
using ee.iLawyer.Ops.Contact.DTO;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Web.Http;

namespace ee.iLawyer.WebApi.Controllers
{
    [RoutePrefix("api/project")]
    public class ProjectController : BaseController
    {
        // GET api/Client/5
        public BaseObjectResponse<Project> Get(int id)
        {
            return Service.GetProject(new BaseIdRequest() { Id = id });
        }
        // POST api/Client
        public BaseResponse Post([FromBody]dynamic para)
        {
            var request = JsonConvert.DeserializeObject<Ops.Contact.Args.CreateProjectRequest>(para.ToString());
            return Service.CreateProject(request);
        }

        // PUT api/Client/{id}
        public BaseResponse Put(int id, [FromBody]dynamic para)
        {
            var request = JsonConvert.DeserializeObject<Ops.Contact.Args.UpdateProjectRequest>(para.ToString());
            request.Id = id;
            return Service.UpdateProject(request);
        }

        // DELETE api/Client/{id}
        public BaseResponse Delete(int id)
        {
            return Service.RemoveProject(new Ops.Contact.Args.RemoveProjectRequest() { Ids = new List<int>() { id } });
        }
        [Route("query"), HttpGet]
        public BaseQueryResponse<Project> Query(string categoryCode, string code, string name, string level, string dealDateFrom, string dealDateTo, int? ownerId, int pageIndex, int pageSize)
        {
            return Service.QueryProject(
                new Ops.Contact.Args.QueryProjectRequest()
                {
                    CategoryCode = categoryCode,
                    Code = code,
                    Name = name,
                    Level = level,
                    DealDateFrom = dealDateFrom,
                    DealDateTo = dealDateTo,
                    OwnerId = ownerId,
                    PageIndex = pageIndex,
                    PageSize = pageSize,
                });
        }
    }
}