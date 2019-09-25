using ee.Framework.Schema;
using ee.iLawyer.Ops;
using ee.iLawyer.Ops.Contact.Args;
using ee.iLawyer.Ops.Contact.DTO;
using System.Web.Http;

namespace ee.iLawyer.WebApi.Controllers
{
    [RoutePrefix("api/infr")]
    public class InfrastructureController : BaseController
    {
        /// <summary>
        /// Gets the list of area infos
        /// </summary>
        /// <param name="pageIndex">page index</param>
        /// <param name="pageSize">page size</param>
        /// <returns></returns>
        [Route("Areas"), HttpGet]
        public BaseQueryResponse<Area> GetAreas(int pageIndex, int pageSize)
        {

            var areas = Service.GetAreas(new GetAreasRequest()
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
            });

            return areas;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Route("ProjectCategories"), HttpGet]
        public BaseQueryResponse<ProjectCategory> GetProjectCategories()
        {
            return Service.GetProjectCategories(new GetProjectCategoriesRequest());
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Route("ProjectCauses"), HttpGet]
        public BaseQueryResponse<ProjectCause> GetProjectCauses()
        {
            return Service.GetProjectCauses(new GetProjectCausesRequest());
        }
  
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Route("PropertyPicks"), HttpGet]
        public BaseQueryResponse<PropertyPicker> GetPropertyPicks()
        {
            return Service.GetPropertyPicks(new GetPropertyPicksRequest());
        }
    }
}