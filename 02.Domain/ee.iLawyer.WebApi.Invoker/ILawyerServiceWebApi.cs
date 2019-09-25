using ee.Framework.Schema;
using ee.iLawyer.Ops.Contact.Args;
using ee.iLawyer.Ops.Contact.DTO;
using ee.iLawyer.Ops.Contact.Interfaces;
using Newtonsoft.Json;

namespace ee.iLawyer.WebApi.Invoker
{
    public class ILawyerServiceWebApi : IILawyerService
    {
        public string EndPoint = @"http://localhost:2155";

        private BaseDataResponse Process(string resource, BaseRequest request, int? timeout = 10 * 1000)
        {
            var uri = EndPoint + resource;
            var response = WebInvoker.PostToString(uri, null, JsonConvert.SerializeObject(request), timeout);
            return JsonConvert.DeserializeObject<BaseDataResponse>(response);
        }
        public BaseResponse CreateClient(CreateClientRequest request)
        {
            var resource = @"/api/lawyer/client/create";
            return Process(resource, request);
        }

        public BaseResponse CreateCourt(CreateCourtRequest request)
        {
            var resource = @"/api/lawyer/court/create";
            return Process(resource, request);
        }

        public BaseResponse CreateJudge(CreateJudgeRequest request)
        {
            var resource = @"/api/lawyer/judge/create";
            return Process(resource, request);
        }

        public BaseResponse CreateProject(CreateProjectRequest request)
        {
            var resource = @"/api/lawyer/project/create";
            return Process(resource, request);
        }

        public BaseQueryResponse<Area> GetAreas(GetAreasRequest request)
        {
            var resource = @"/api/lawyer/infr/areas";
            return Process(resource, request, 120 * 1000).ToBaseQueryResponse<Area>();
        }

        public BaseObjectResponse<Client> GetClient(BaseIdRequest request)
        {
            var resource = @"/api/lawyer/client/get";
            return Process(resource, request).ToBaseObjectResponse<Client>();
        }

        public BaseObjectResponse<Court> GetCourt(BaseIdRequest request)
        {
            var resource = @"/api/lawyer/court/get";
            return Process(resource, request).ToBaseObjectResponse<Court>();
        }

        public BaseObjectResponse<Judge> GetJudge(BaseIdRequest request)
        {
            var resource = @"/api/lawyer/judge/get";
            return Process(resource, request).ToBaseObjectResponse<Judge>();
        }

        public BaseObjectResponse<Project> GetProject(BaseIdRequest request)
        {
            var resource = @"/api/lawyer/project/get";
            return Process(resource, request).ToBaseObjectResponse<Project>();
        }

        public BaseQueryResponse<ProjectCategory> GetProjectCategories(GetProjectCategoriesRequest request)
        {
            var resource = @"/api/lawyer/infr/ProjectCategories";
            return Process(resource, request).ToBaseQueryResponse<ProjectCategory>();
        }

        public BaseQueryResponse<ProjectCause> GetProjectCauses(GetProjectCausesRequest request)
        {
            var resource = @"/api/lawyer/infr/ProjectCauses";
            return Process(resource, request).ToBaseQueryResponse<ProjectCause>();
        }


        public BaseQueryResponse<PropertyPicker> GetPropertyPicks(GetPropertyPicksRequest request)
        {
            var resource = @"/api/lawyer/infr/PropertyPicks";
            return Process(resource, request).ToBaseQueryResponse<PropertyPicker>();
        }

        public BaseQueryResponse<Client> QueryClient(QueryClientRequest request)
        {
            var resource = @"/api/lawyer/client/query";
            return Process(resource, request).ToBaseQueryResponse<Client>();
        }

        public BaseQueryResponse<Court> QueryCourt(QueryCourtRequest request)
        {
            var resource = @"/api/lawyer/court/query";
            return Process(resource, request).ToBaseQueryResponse<Court>();
        }

        public BaseQueryResponse<Judge> QueryJudge(QueryJudgeRequest request)
        {
            var resource = @"/api/lawyer/judge/query";
            return Process(resource, request).ToBaseQueryResponse<Judge>();
        }

        public BaseQueryResponse<Project> QueryProject(QueryProjectRequest request)
        {
            var resource = @"/api/lawyer/project/query";
            return Process(resource, request).ToBaseQueryResponse<Project>();
        }

        public BaseResponse RemoveClient(RemoveClientRequest request)
        {
            var resource = @"/api/lawyer/client/remove";
            return Process(resource, request);
        }

        public BaseResponse RemoveCourt(RemoveCourtRequest request)
        {
            var resource = @"/api/lawyer/court/remove";
            return Process(resource, request);
        }

        public BaseResponse RemoveJudge(RemoveJudgeRequest request)
        {
            var resource = @"/api/lawyer/judge/remove";
            return Process(resource, request);
        }

        public BaseResponse RemoveProject(RemoveProjectRequest request)
        {
            var resource = @"/api/lawyer/project/remove";
            return Process(resource, request);
        }

        public BaseResponse SaveOrUpdateProjectProgress(SaveOrUpdateProjectProgressRequest request)
        {
            throw new System.NotImplementedException();
        }

        public BaseResponse SaveOrUpdateProjectTodoList(SaveOrUpdateProjectTodoListRequest request)
        {
            throw new System.NotImplementedException();
        }

        public BaseResponse UpdateClient(UpdateClientRequest request)
        {
            var resource = @"/api/lawyer/client/update";
            return Process(resource, request);
        }

        public BaseResponse UpdateCourt(UpdateCourtRequest request)
        {
            var resource = @"/api/lawyer/court/update";
            return Process(resource, request);
        }

        public BaseResponse UpdateJudge(UpdateJudgeRequest request)
        {
            var resource = @"/api/lawyer/judge/update";
            return Process(resource, request);
        }

        public BaseResponse UpdateProject(UpdateProjectRequest request)
        {
            var resource = @"/api/lawyer/project/update";
            return Process(resource, request);
        }
    }
}
