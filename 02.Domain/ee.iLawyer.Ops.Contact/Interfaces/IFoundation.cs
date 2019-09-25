using ee.Framework.Schema;
using ee.iLawyer.Ops.Contact.Args;
using ee.iLawyer.Ops.Contact.DTO;

namespace ee.iLawyer.Ops.Contact.Interfaces
{
    public interface IFoundation
    {
        BaseQueryResponse<Area> GetAreas(GetAreasRequest request);
        BaseQueryResponse<ProjectCategory> GetProjectCategories(GetProjectCategoriesRequest request);
        BaseQueryResponse<ProjectCause> GetProjectCauses(GetProjectCausesRequest request);
        BaseQueryResponse<PropertyPicker> GetPropertyPicks(GetPropertyPicksRequest request);

    }
}
