using ee.Framework.Schema;
using ee.iLawyer.Ops.Contact.DTO;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Web.Http;

namespace ee.iLawyer.WebApi.Controllers
{
    [RoutePrefix("api/judge")]
    public class JudgeController : BaseController
    {
        // GET api/Judge/5
        public BaseObjectResponse<Judge> Get(int id)
        {
            return Service.GetJudge(new BaseIdRequest() { Id = id });
        }
        // POST api/Judge
        public BaseResponse Post( [FromBody]dynamic para)
        {
            var request = JsonConvert.DeserializeObject<Ops.Contact.Args.CreateJudgeRequest>(para.ToString());
            return Service.CreateJudge(request);
        }

        // PUT api/Judge/{id}
        public BaseResponse Put(int id, [FromBody]dynamic para)
        {
            var request = JsonConvert.DeserializeObject<Ops.Contact.Args.UpdateJudgeRequest>(para.ToString());
            request.Id = id;
            return Service.UpdateJudge(request);
        }

        // DELETE api/Judge/{id}
        public BaseResponse Delete(int id)
        {
            return Service.RemoveJudge(new Ops.Contact.Args.RemoveJudgeRequest() { Ids = new List<int>() { id } });
        }
        [Route("query"), HttpGet]
        public BaseQueryResponse<Judge> Query(string grade, string name, int? inCourtId, int pageIndex, int pageSize)
        {

            return Service.QueryJudge(
                new Ops.Contact.Args.QueryJudgeRequest()
                {
                    Grade = grade,
                    Name = name,
                    InCourtId = inCourtId,
                    PageIndex = pageIndex,
                    PageSize = pageSize,
                });
        }
    }
}