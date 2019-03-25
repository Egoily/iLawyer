using ee.Framework;
using ee.iLawyer.Ops.Contact.Args;
using ee.iLawyer.Ops.Contact.DTO;

namespace ee.iLawyer.Ops.Contact.Interfaces
{
    public interface ICtsService
    {
        BaseQueryResponse<Area> GetAreas(GetAreasRequest request);
        BaseQueryResponse<ProjectCategory> GetProjectCategories(GetProjectCategoriesRequest request);
        BaseQueryResponse<ProjectCause> GetProjectCauses(GetProjectCausesRequest request);
        BaseQueryResponse<PropertyItemCategory> GetPropertyCategories(GetPropertyCategoriesRequest request);
        BaseQueryResponse<PropertyItemCategory> GetPropertyItemCategories(GetPropertyItemCategoriesRequest request);




        BaseResponse CreateCourt(CreateCourtRequest request);
        BaseResponse UpdateCourt(UpdateCourtRequest request);
        BaseResponse RemoveCourt(RemoveCourtRequest request);
        BaseQueryResponse<Court> QueryCourt(QueryCourtRequest request);

        BaseResponse CreateJudge(CreateJudgeRequest request);
        BaseResponse UpdateJudge(UpdateJudgeRequest request);
        BaseResponse RemoveJudge(RemoveJudgeRequest request);
        BaseQueryResponse<Judge> QueryJudge(QueryJudgeRequest request);

        BaseResponse CreateClient(CreateClientRequest request);
        BaseResponse UpdateClient(UpdateClientRequest request);
        BaseResponse RemoveClient(RemoveClientRequest request);
        BaseQueryResponse<Client> QueryClient(QueryClientRequest request);

        BaseResponse CreateProject(CreateProjectRequest request);
        BaseResponse UpdateProject(UpdateProjectRequest request);
        BaseResponse RemoveProject(RemoveProjectRequest request);
        BaseQueryResponse<Project> QueryProject(QueryProjectRequest request);

    }
}
