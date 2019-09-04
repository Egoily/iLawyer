using ee.Framework.Schema;
using ee.iLawyer.Ops;
using ee.iLawyer.Ops.Contact.Args;
using System.Reflection;
using System.Web.Http;

namespace ee.iLawyer.WebApi.Controllers
{
    [RoutePrefix("api/lawyer")]
    public class LawyerController : BaseController
    {
        #region * Infr
        [Route("infr/areas"), HttpPost]
        public BaseDataResponse GetAreas([FromBody] GetAreasRequest request)
        {
            return ServiceProcessor.CreateProcessor<GetAreasRequest, BaseDataResponse>(MethodBase.GetCurrentMethod())
                 .Input(request, true)
                 .Process(req => { return Service.GetAreas(req); })
                 .UsingResponseConverter(new Framework.Processor.ResponseConverter<BaseDataResponse>(Converters.ToBaseDataResponse))
                 .Build();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Route("infr/projectcategories"), HttpPost]
        public BaseDataResponse GetProjectCategories()
        {
            return ServiceProcessor.CreateProcessor<GetProjectCategoriesRequest, BaseDataResponse>(MethodBase.GetCurrentMethod())
                  .Input(new GetProjectCategoriesRequest(), true)
                  .Process(req => { return Service.GetProjectCategories(req); })
                  .UsingResponseConverter(new Framework.Processor.ResponseConverter<BaseDataResponse>(Converters.ToBaseDataResponse))
                  .Build();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Route("infr/projectcauses"), HttpPost]
        public BaseDataResponse GetProjectCauses()
        {
            return ServiceProcessor.CreateProcessor<GetProjectCausesRequest, BaseDataResponse>(MethodBase.GetCurrentMethod())
                  .Input(new GetProjectCausesRequest(), true)
                  .Process(req => { return Service.GetProjectCauses(req); })
                  .UsingResponseConverter(new Framework.Processor.ResponseConverter<BaseDataResponse>(Converters.ToBaseDataResponse))
                  .Build();

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Route("infr/propertyCategories"), HttpPost]
        public BaseDataResponse GetPropertyCategories()
        {
            return ServiceProcessor.CreateProcessor<GetPropertyCategoriesRequest, BaseDataResponse>(MethodBase.GetCurrentMethod())
                 .Input(new GetPropertyCategoriesRequest(), true)
                 .Process(req => { return Service.GetPropertyCategories(req); })
                 .UsingResponseConverter(new Framework.Processor.ResponseConverter<BaseDataResponse>(Converters.ToBaseDataResponse))
                 .Build();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Route("infr/propertyItemCategories"), HttpPost]
        public BaseDataResponse GetPropertyItemCategories()
        {
            return ServiceProcessor.CreateProcessor<GetPropertyItemCategoriesRequest, BaseDataResponse>(MethodBase.GetCurrentMethod())
                  .Input(new GetPropertyItemCategoriesRequest(), true)
                  .Process(req => { return Service.GetPropertyItemCategories(req); })
                  .UsingResponseConverter(new Framework.Processor.ResponseConverter<BaseDataResponse>(Converters.ToBaseDataResponse))
                  .Build();
        }

        #endregion

        #region * Court
        [Route("court/get"), HttpPost]
        public BaseDataResponse GetCourt(BaseIdRequest request)
        {
            return ServiceProcessor.CreateProcessor<BaseIdRequest, BaseDataResponse>(MethodBase.GetCurrentMethod())
                  .Input(request, true)
                  .Process(req => { return Service.GetCourt(req); })
                  .UsingResponseConverter(new Framework.Processor.ResponseConverter<BaseDataResponse>(Converters.ToBaseDataResponse))
                  .Build();

        }
        [Route("court/create"), HttpPost]
        public BaseDataResponse CreateCourt([FromBody]CreateCourtRequest request)
        {
            return ServiceProcessor.CreateProcessor<CreateCourtRequest, BaseDataResponse>(MethodBase.GetCurrentMethod())
                  .Input(request, true)
                  .Process(req => { return Service.CreateCourt(req); })
                  .UsingResponseConverter(new Framework.Processor.ResponseConverter<BaseDataResponse>(Converters.ToBaseDataResponse))
                  .Build();
        }
        [Route("court/update"), HttpPost]
        public BaseDataResponse UpdateCourt([FromBody]UpdateCourtRequest request)
        {
            return ServiceProcessor.CreateProcessor<UpdateCourtRequest, BaseDataResponse>(MethodBase.GetCurrentMethod())
                  .Input(request, true)
                  .Process(req => { return Service.UpdateCourt(req); })
                  .UsingResponseConverter(new Framework.Processor.ResponseConverter<BaseDataResponse>(Converters.ToBaseDataResponse))
                  .Build();
        }

        [Route("court/remove"), HttpPost]
        public BaseDataResponse DeleteCourt(RemoveCourtRequest request)
        {
            return ServiceProcessor.CreateProcessor<RemoveCourtRequest, BaseDataResponse>(MethodBase.GetCurrentMethod())
                  .Input(request, true)
                  .Process(req => { return Service.RemoveCourt(req); })
                  .UsingResponseConverter(new Framework.Processor.ResponseConverter<BaseDataResponse>(Converters.ToBaseDataResponse))
                  .Build();
        }
        [Route("court/query"), HttpPost]
        public BaseDataResponse QueryCourt(QueryCourtRequest request)
        {
            return ServiceProcessor.CreateProcessor<QueryCourtRequest, BaseDataResponse>(MethodBase.GetCurrentMethod())
                  .Input(request, true)
                  .Process(req => { return Service.QueryCourt(req); })
                  .UsingResponseConverter(new Framework.Processor.ResponseConverter<BaseDataResponse>(Converters.ToBaseDataResponse))
                  .Build();

        }
        #endregion

        #region * Judge
        [Route("judge/get"), HttpPost]
        public BaseDataResponse GetJudge(BaseIdRequest request)
        {
            return ServiceProcessor.CreateProcessor<BaseIdRequest, BaseDataResponse>(MethodBase.GetCurrentMethod())
                  .Input(request, true)
                  .Process(req => { return Service.GetJudge(req); })
                  .UsingResponseConverter(new Framework.Processor.ResponseConverter<BaseDataResponse>(Converters.ToBaseDataResponse))
                  .Build();

        }
        [Route("judge/create"), HttpPost]
        public BaseDataResponse CreateJudge([FromBody]CreateJudgeRequest request)
        {
            return ServiceProcessor.CreateProcessor<CreateJudgeRequest, BaseDataResponse>(MethodBase.GetCurrentMethod())
                  .Input(request, true)
                  .Process(req => { return Service.CreateJudge(req); })
                  .UsingResponseConverter(new Framework.Processor.ResponseConverter<BaseDataResponse>(Converters.ToBaseDataResponse))
                  .Build();
        }
        [Route("judge/update"), HttpPost]
        public BaseDataResponse UpdateJudge([FromBody]UpdateJudgeRequest request)
        {
            return ServiceProcessor.CreateProcessor<UpdateJudgeRequest, BaseDataResponse>(MethodBase.GetCurrentMethod())
                  .Input(request, true)
                  .Process(req => { return Service.UpdateJudge(req); })
                  .UsingResponseConverter(new Framework.Processor.ResponseConverter<BaseDataResponse>(Converters.ToBaseDataResponse))
                  .Build();
        }

        [Route("judge/remove"), HttpPost]
        public BaseDataResponse DeleteJudge(RemoveJudgeRequest request)
        {
            return ServiceProcessor.CreateProcessor<RemoveJudgeRequest, BaseDataResponse>(MethodBase.GetCurrentMethod())
                  .Input(request, true)
                  .Process(req => { return Service.RemoveJudge(req); })
                  .UsingResponseConverter(new Framework.Processor.ResponseConverter<BaseDataResponse>(Converters.ToBaseDataResponse))
                  .Build();
        }
        [Route("judge/query"), HttpPost]
        public BaseDataResponse QueryJudge(QueryJudgeRequest request)
        {
            return ServiceProcessor.CreateProcessor<QueryJudgeRequest, BaseDataResponse>(MethodBase.GetCurrentMethod())
                  .Input(request, true)
                  .Process(req => { return Service.QueryJudge(req); })
                  .UsingResponseConverter(new Framework.Processor.ResponseConverter<BaseDataResponse>(Converters.ToBaseDataResponse))
                  .Build();

        }
        #endregion

        #region * Client
        [Route("client/get"), HttpPost]
        public BaseDataResponse GetClient(BaseIdRequest request)
        {
            return ServiceProcessor.CreateProcessor<BaseIdRequest, BaseDataResponse>(MethodBase.GetCurrentMethod())
                  .Input(request, true)
                  .Process(req => { return Service.GetClient(req); })
                  .UsingResponseConverter(new Framework.Processor.ResponseConverter<BaseDataResponse>(Converters.ToBaseDataResponse))
                  .Build();

        }
        [Route("client/create"), HttpPost]
        public BaseDataResponse CreateClient([FromBody]CreateClientRequest request)
        {
            return ServiceProcessor.CreateProcessor<CreateClientRequest, BaseDataResponse>(MethodBase.GetCurrentMethod())
                  .Input(request, true)
                  .Process(req => { return Service.CreateClient(req); })
                  .UsingResponseConverter(new Framework.Processor.ResponseConverter<BaseDataResponse>(Converters.ToBaseDataResponse))
                  .Build();
        }
        [Route("client/update"), HttpPost]
        public BaseDataResponse UpdateClient([FromBody]UpdateClientRequest request)
        {
            return ServiceProcessor.CreateProcessor<UpdateClientRequest, BaseDataResponse>(MethodBase.GetCurrentMethod())
                  .Input(request, true)
                  .Process(req => { return Service.UpdateClient(req); })
                  .UsingResponseConverter(new Framework.Processor.ResponseConverter<BaseDataResponse>(Converters.ToBaseDataResponse))
                  .Build();
        }

        [Route("client/remove"), HttpPost]
        public BaseDataResponse DeleteClient(RemoveClientRequest request)
        {
            return ServiceProcessor.CreateProcessor<RemoveClientRequest, BaseDataResponse>(MethodBase.GetCurrentMethod())
                  .Input(request, true)
                  .Process(req => { return Service.RemoveClient(req); })
                  .UsingResponseConverter(new Framework.Processor.ResponseConverter<BaseDataResponse>(Converters.ToBaseDataResponse))
                  .Build();
        }
        [Route("client/query"), HttpPost]
        public BaseDataResponse QueryClient(QueryClientRequest request)
        {
            return ServiceProcessor.CreateProcessor<QueryClientRequest, BaseDataResponse>(MethodBase.GetCurrentMethod())
                  .Input(request, true)
                  .Process(req => { return Service.QueryClient(req); })
                  .UsingResponseConverter(new Framework.Processor.ResponseConverter<BaseDataResponse>(Converters.ToBaseDataResponse))
                  .Build();

        }
        #endregion

        #region * Project
        [Route("project/get"), HttpPost]
        public BaseDataResponse GetProject(BaseIdRequest request)
        {
            return ServiceProcessor.CreateProcessor<BaseIdRequest, BaseDataResponse>(MethodBase.GetCurrentMethod())
                  .Input(request, true)
                  .Process(req => { return Service.GetProject(req); })
                  .UsingResponseConverter(new Framework.Processor.ResponseConverter<BaseDataResponse>(Converters.ToBaseDataResponse))
                  .Build();

        }
        [Route("project/create"), HttpPost]
        public BaseDataResponse CreateProject([FromBody]CreateProjectRequest request)
        {
            return ServiceProcessor.CreateProcessor<CreateProjectRequest, BaseDataResponse>(MethodBase.GetCurrentMethod())
                  .Input(request, true)
                  .Process(req => { return Service.CreateProject(req); })
                  .UsingResponseConverter(new Framework.Processor.ResponseConverter<BaseDataResponse>(Converters.ToBaseDataResponse))
                  .Build();
        }
        [Route("project/update"), HttpPost]
        public BaseDataResponse UpdateProject([FromBody]UpdateProjectRequest request)
        {
            return ServiceProcessor.CreateProcessor<UpdateProjectRequest, BaseDataResponse>(MethodBase.GetCurrentMethod())
                  .Input(request, true)
                  .Process(req => { return Service.UpdateProject(req); })
                  .UsingResponseConverter(new Framework.Processor.ResponseConverter<BaseDataResponse>(Converters.ToBaseDataResponse))
                  .Build();
        }

        [Route("project/remove"), HttpPost]
        public BaseDataResponse DeleteProject(RemoveProjectRequest request)
        {
            return ServiceProcessor.CreateProcessor<RemoveProjectRequest, BaseDataResponse>(MethodBase.GetCurrentMethod())
                  .Input(request, true)
                  .Process(req => { return Service.RemoveProject(req); })
                  .UsingResponseConverter(new Framework.Processor.ResponseConverter<BaseDataResponse>(Converters.ToBaseDataResponse))
                  .Build();
        }
        [Route("project/query"), HttpPost]
        public BaseDataResponse QueryProject(QueryProjectRequest request)
        {
            return ServiceProcessor.CreateProcessor<QueryProjectRequest, BaseDataResponse>(MethodBase.GetCurrentMethod())
                  .Input(request, true)
                  .Process(req => { return Service.QueryProject(req); })
                  .UsingResponseConverter(new Framework.Processor.ResponseConverter<BaseDataResponse>(Converters.ToBaseDataResponse))
                  .Build();

        }
        #endregion

    }
}