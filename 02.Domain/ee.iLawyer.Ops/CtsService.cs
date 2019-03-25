using ee.Framework;
using ee.iLawyer.Db.Entity;
using ee.iLawyer.Ops.Contact.Args;
using ee.iLawyer.Ops.Contact.AutoMapper;
using ee.iLawyer.Ops.Contact.Interfaces;
using ee.iLawyer.SessionFactoryBuilder.Sqlite;
using ee.SessionFactory;
using ee.SessionFactory.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ee.iLawyer.Ops
{
    public class CtsService : ICtsService
    {

        private static bool IsInit = false;
        public static void Build()
        {
            if (IsInit)
            {
                return;
            }
            // create our  session factory
            SessionManager.Builder = new SqliteSessionFactoryBuilder();
            AutoMapperConfiguration.Configure();
            IsInit = true;

        }
        public CtsService()
        {
            Build();

        }
        public BaseQueryResponse<Contact.DTO.Area> GetAreas(GetAreasRequest request)
        {
            return ServiceProcessor.ProcessRequest(request,
                //inbound.do validate or do something here
                () =>
                {
                },

                req =>
                {
                    var response = new BaseQueryResponse<Contact.DTO.Area>();
                    using (var repo = new NhRepository<Db.Entity.Area>())
                    {
                        var query = repo.Query(x => x.Parent == null);

                        response.Total = query.Count();
                        response.QueryList = query.ToList().Select(DtoConverter.Convert).ToList();
                    }
                    return response;
                }
             );
        }

        public BaseQueryResponse<Contact.DTO.ProjectCategory> GetProjectCategories(GetProjectCategoriesRequest request)
        {
            return ServiceProcessor.ProcessRequest(request,
               //inbound.do validate or do something here
               () =>
               {
               },

               req =>
               {
                   var response = new BaseQueryResponse<Contact.DTO.ProjectCategory>();
                   using (var repo = new NhRepository<Db.Entity.ProjectCategory>())
                   {
                       var query = repo.Query(x => x.Parent == null);

                       response.Total = query.Count();
                       response.QueryList = query.ToList().Select(DtoConverter.Convert).ToList();
                   }
                   return response;
               }
            );
        }

        public BaseQueryResponse<Contact.DTO.ProjectCause> GetProjectCauses(GetProjectCausesRequest request)
        {
            return ServiceProcessor.ProcessRequest(request,
               //inbound.do validate or do something here
               () =>
               {
               },

               req =>
               {
                   var response = new BaseQueryResponse<Contact.DTO.ProjectCause>();
                   using (var repo = new NhRepository<Db.Entity.ProjectCause>())
                   {
                       var query = repo.Query();
                       response.Total = query.Count();
                       response.QueryList = query.ToList().Select(DtoConverter.Convert).ToList();
                   }
                   return response;
               }
            );
        }

        public BaseResponse CreateCourt(CreateCourtRequest request)
        {
            return ServiceProcessor.ProcessRequest(request,
                //inbound.do validate or do something here
                () =>
                {
                },

                req =>
                {
                    var response = new BaseResponse();
                    using (var repo = new NhRepository<Db.Entity.Court>())
                    {

                        var entity = repo.Query(x => x.Name == req.Name).FirstOrDefault();
                        if (entity != null)
                        {
                            throw new EeException(ErrorCodes.Existed, "Object is existed.");
                        }
                        entity = new Db.Entity.Court()
                        {
                            Name = req.Name,
                            Province = req.Province,
                            City = req.City,
                            County = req.County,
                            Address = req.Address,
                            Rank = req.Rank,
                            ContactNo = req.ContactNo,

                        };
                        repo.Create(entity);
                    }
                    return response;
                }
            );

        }

        public BaseResponse UpdateCourt(UpdateCourtRequest request)
        {
            return ServiceProcessor.ProcessRequest(request,
                //inbound.do validate or do something here
                () =>
                {
                },

                req =>
                {
                    var response = new BaseResponse();
                    using (var repo = new NhRepository<Db.Entity.Court>())
                    {
                        var entity = repo.GetById(req.Id);
                        if (entity == null)
                        {
                            throw new EeException(ErrorCodes.NotFound, "Object is not found.");
                        }
                        entity.Name = req.Name;
                        entity.Rank = req.Rank;
                        entity.Province = req.Province;
                        entity.City = req.City;
                        entity.County = req.County;
                        entity.Address = req.Address;
                        entity.ContactNo = req.ContactNo;
                        repo.Update(entity);
                    }
                    return response;
                }
            );

        }

        public BaseResponse RemoveCourt(RemoveCourtRequest request)
        {
            return ServiceProcessor.ProcessRequest(request,
                //inbound.do validate or do something here
                () =>
                {
                },

                req =>
                {
                    var response = new BaseResponse();
                    using (var repo = new NhRepository<Db.Entity.Court>())
                    {
                        foreach (var id in req.Ids)
                        {
                            var entity = repo.GetById(id);
                            if (entity != null)
                            {
                                repo.Delete(entity);
                            }
                        }
                    }
                    return response;
                }
            );

        }

        public BaseQueryResponse<Contact.DTO.Court> QueryCourt(QueryCourtRequest request)
        {
            return ServiceProcessor.ProcessRequest(request,
                //inbound.do validate or do something here
                () =>
                {
                },

                req =>
                {
                    var response = new BaseQueryResponse<Contact.DTO.Court>();
                    using (var repo = new NhRepository<Db.Entity.Court>())
                    {
                        var query = repo.Query();

                        response.Total = query.Count();
                        response.QueryList = query.ToList().Select(DtoConverter.Convert).ToList();
                    }
                    return response;
                }
             );
        }

        public BaseResponse CreateJudge(CreateJudgeRequest request)
        {
            return ServiceProcessor.ProcessRequest(request,
                //inbound.do validate or do something here
                () =>
                {
                },

                req =>
                {
                    var response = new BaseResponse();
                    using (var repo = new NhGlobalRepository())
                    {

                        var entity = repo.Query<Db.Entity.Judge>(x => x.Name == req.Name && x.ContactNo == req.ContactNo).FirstOrDefault();
                        if (entity != null)
                        {
                            throw new EeException(ErrorCodes.Existed, "Object is existed.");
                        }
                        var court = repo.GetById<Db.Entity.Court>(req.InCourtId);
                        entity = new Db.Entity.Judge()
                        {
                            Name = req.Name,
                            ContactNo = req.ContactNo,
                            Gender = (int)req.Gender,
                            Grade = req.Grade.ToString(),
                            Duty = req.Duty,
                            InCourt = court,
                        };
                        repo.Create(entity);
                    }
                    return response;
                }
            );

        }

        public BaseResponse UpdateJudge(UpdateJudgeRequest request)
        {
            return ServiceProcessor.ProcessRequest(request,
                //inbound.do validate or do something here
                () =>
                {
                },

                req =>
                {
                    var response = new BaseResponse();
                    using (var repo = new NhGlobalRepository())
                    {
                        var entity = repo.GetById<Db.Entity.Judge>(req.Id);
                        if (entity == null)
                        {
                            throw new EeException(ErrorCodes.NotFound, "Object is not found.");
                        }

                        entity.Name = req.Name;
                        entity.ContactNo = req.ContactNo;
                        entity.Gender = (int)req.Gender;
                        entity.Grade = req.Grade.ToString();
                        entity.Duty = req.Duty;
                        entity.InCourt = repo.GetById<Db.Entity.Court>(req.InCourtId) ?? throw new EeException(ErrorCodes.NotFound, "Court is not found.");

                        repo.Update(entity);
                    }
                    return response;
                }
            );

        }

        public BaseResponse RemoveJudge(RemoveJudgeRequest request)
        {
            return ServiceProcessor.ProcessRequest(request,
                //inbound.do validate or do something here
                () =>
                {
                },

                req =>
                {
                    var response = new BaseResponse();
                    using (var repo = new NhRepository<Db.Entity.Judge>())
                    {
                        foreach (var id in req.Ids)
                        {
                            var entity = repo.GetById(id);
                            if (entity != null)
                            {
                                repo.Delete(entity);
                            }
                        }
                    }
                    return response;
                }
            );

        }

        public BaseQueryResponse<Contact.DTO.Judge> QueryJudge(QueryJudgeRequest request)
        {
            return ServiceProcessor.ProcessRequest(request,
                //inbound.do validate or do something here
                () =>
                {
                },

                req =>
                {
                    var response = new BaseQueryResponse<Contact.DTO.Judge>();
                    using (var repo = new NhRepository<Db.Entity.Judge>())
                    {
                        var query = repo.Query();

                        response.Total = query.Count();
                        response.QueryList = query.ToList().Select(DtoConverter.Convert).ToList();
                    }
                    return response;
                }
             );
        }

        public BaseResponse CreateClient(CreateClientRequest request)
        {
            var now = DateTime.Now;
            return ServiceProcessor.ProcessRequest(request,
                //inbound.do validate or do something here
                () =>
                {
                },

                 req =>
                 {
                     var response = new BaseResponse();
                     using (var repo = new NhGlobalRepository())
                     {

                         var entity = repo.Query<Db.Entity.Client>(x => x.Name == req.Name && x.Abbreviation == req.Abbreviation).FirstOrDefault();
                         if (entity != null)
                         {
                             throw new EeException(ErrorCodes.Existed, "Object is existed.");
                         }

                         entity = new Db.Entity.Client()
                         {
                             Name = req.Name,
                             ContactNo = req.ContactNo,
                             Abbreviation = req.Abbreviation,
                             Impression = req.Impression,
                             IsNP = req.IsNP,

                             CreateTime = now,

                         };
                         var properties = new List<ClientPropertyItem>();
                         foreach (var p in req.Properties)
                         {
                             var clientPropertyItem = new ClientPropertyItem()
                             {
                                 CreateTime = now,
                                 Value = p.Value,
                                 Category = repo.GetById<Db.Entity.PropertyItemCategory>(p.CategoryId),
                                 Client = entity,
                             };
                             repo.Create(clientPropertyItem);
                             properties.Add(clientPropertyItem);
                         }
                         entity.Properties = properties;
                         repo.Create(entity);

                     }
                     return response;
                 }
             );

        }

        public BaseResponse UpdateClient(UpdateClientRequest request)
        {
            var now = DateTime.Now;
            return ServiceProcessor.ProcessRequest(request,
                //inbound.do validate or do something here
                () =>
                {
                },

                 req =>
                 {
                     var response = new BaseResponse();
                     using (var repo = new NhGlobalRepository())
                     {
                         var client = repo.GetById<Db.Entity.Client>(req.Id);
                         if (client == null)
                         {
                             throw new EeException(ErrorCodes.NotFound, "Object is not found.");
                         }

                         client.Name = req.Name;
                         client.ContactNo = req.ContactNo;
                         client.Abbreviation = req.Abbreviation;
                         client.Impression = req.Impression;
                         client.IsNP = req.IsNP;

                         //remove
                         var toRemove = client.Properties.Where(x => !req.Properties.Select(o => o.Id).Contains(x.Id));
                         if (toRemove != null && toRemove.Any())
                         {
                             foreach (var item in toRemove.ToList())
                             {
                                 client.Properties.Remove(item);
                                 var clientPropertyItem = repo.GetById<ClientPropertyItem>(item.Id);
                                 if (clientPropertyItem != null)
                                 {
                                     repo.Delete(clientPropertyItem);
                                 }
                             }
                         }

                         foreach (var p in req.Properties)
                         {
                             var existedObj = client.Properties.FirstOrDefault(x => x.Id == p.Id);
                             //update
                             if (existedObj != null)
                             {
                                 var hasUpdated = false;
                                 if (existedObj.Value != p.Value)
                                 {
                                     existedObj.Value = p.Value;
                                     hasUpdated = true;
                                 }
                                 if (existedObj.Category.Id != p.CategoryId && p.CategoryId > 0)
                                 {
                                     var category = repo.GetById<Db.Entity.PropertyItemCategory>(p.CategoryId);
                                     existedObj.Category = category;
                                     hasUpdated = true;
                                 }
                                 if (hasUpdated)
                                 {
                                     existedObj.UpdateTime = now;
                                 }
                             }
                             //add
                             else
                             {
                                 var clientPropertyItem = new ClientPropertyItem()
                                 {
                                     CreateTime = now,
                                     Value = p.Value,
                                     Category = repo.GetById<Db.Entity.PropertyItemCategory>(p.CategoryId),
                                     Client = client,
                                 };
                                 client.Properties.Add(clientPropertyItem);
                             }
                         }


                         repo.Update(client);
                     }
                     return response;
                 }
             );

        }

        public BaseResponse RemoveClient(RemoveClientRequest request)
        {
            return ServiceProcessor.ProcessRequest(request,
                //inbound.do validate or do something here
                () =>
                {
                },

                req =>
                {
                    var response = new BaseResponse();
                    using (var repo = new NhRepository<Db.Entity.Client>())
                    {
                        foreach (var id in req.Ids)
                        {
                            var entity = repo.GetById(id);
                            if (entity != null)
                            {
                                repo.Delete(entity);
                            }
                        }
                    }
                    return response;
                }
         );
        }

        public BaseQueryResponse<Contact.DTO.Client> QueryClient(QueryClientRequest request)
        {
            return ServiceProcessor.ProcessRequest(request,
                //inbound.do validate or do something here
                () =>
                {
                },

                req =>
                {
                    var response = new BaseQueryResponse<Contact.DTO.Client>();
                    using (var repo = new NhRepository<Db.Entity.Client>())
                    {
                        IEnumerable<Db.Entity.Client> query;

                        if (req.Keys != null && req.Keys.Any())
                        {
                            var queryOver = repo.GetQuery().WhereRestrictionOn(x => x.Id).IsIn(req.Keys);
                            query = queryOver.List();
                        }
                        else
                        {
                            query = repo.Query();
                        }
                        response.Total = query.Count();
                        response.QueryList = query.ToList().Select(DtoConverter.Convert).ToList();
                    }
                    return response;
                }
             );
        }


        public BaseQueryResponse<Contact.DTO.PropertyItemCategory> GetPropertyCategories(GetPropertyCategoriesRequest request)
        {
            return ServiceProcessor.ProcessRequest(request,
               //inbound.do validate or do something here
               () => { },

               req =>
               {
                   var response = new BaseQueryResponse<Contact.DTO.PropertyItemCategory>();
                   using (var repo = new NhRepository<Db.Entity.PropertyItemCategory>())
                   {
                       var query = repo.Query().Where(x => x.Parent == null);

                       response.Total = query.Count();
                       response.QueryList = query.ToList().Select(DtoConverter.Convert).ToList();
                   }
                   return response;
               }
            );
        }

        public BaseQueryResponse<Contact.DTO.PropertyItemCategory> GetPropertyItemCategories(GetPropertyItemCategoriesRequest request)
        {
            return ServiceProcessor.ProcessRequest(request,
             //inbound.do validate or do something here
             () => { },

             req =>
             {
                 var response = new BaseQueryResponse<Contact.DTO.PropertyItemCategory>();
                 using (var repo = new NhRepository<Db.Entity.PropertyItemCategory>())
                 {
                     if (string.IsNullOrEmpty(req.Code))
                     {
                         var query = repo.Query().Where(x => x.Parent == null && x.IsEnabled);

                         response.Total = query.Count();
                         response.QueryList = query.ToList().Select(DtoConverter.Convert).ToList();
                     }
                     else
                     {
                         var query = repo.Query().Where(x => x.Parent != null && x.Parent.Code == req.Code && x.IsEnabled);

                         response.Total = query.Count();
                         response.QueryList = query.ToList().Select(DtoConverter.Convert).ToList();
                     }
                 }
                 return response;
             }
          );
        }



        public BaseResponse CreateProject(CreateProjectRequest request)
        {
            var now = DateTime.Now;
            return ServiceProcessor.ProcessRequest(request,
                //inbound.do validate or do something here
                () => { },

                req =>
                {
                    var response = new BaseResponse();
                    using (var repo = new NhGlobalRepository())
                    {

                        var entity = repo.Query<Db.Entity.Project>(x => x.Name == req.Name && x.Code == req.Code).FirstOrDefault();
                        if (entity != null)
                        {
                            throw new EeException(ErrorCodes.Existed, "Object is existed.");
                        }

                        entity = new Db.Entity.Project()
                        {
                            Category = repo.GetById<Db.Entity.ProjectCategory>(req.CategoryCode),
                            Name = req.Name,
                            Code = req.Code,
                            Level = req.Level,
                            Details = req.Details,
                            OtherLitigant = req.OtherLitigant,
                            InterestedParty = req.InterestedParty,
                            DealDate = req.DealDate,
                            Owner = repo.GetById<Db.Entity.SysUser>(req.OwnerId),
                            CreateTime = now,
                        };
                        entity.AddAccount(DtoConverter.Convert(req.Account));

                        var involvedClients = new List<Db.Entity.ProjectClient>();
                        var todoList = new List<Db.Entity.ProjectTodoItem>();
                        var progresses = new List<Db.Entity.ProjectProgress>();

                        if (req.InvolvedClientIds?.Any() ?? false)
                        {
                            int projectClientOrderNo = 0;
                            foreach (var id in req.InvolvedClientIds)
                            {
                                if (id > 0)
                                {
                                    var existedClient = repo.GetById<Db.Entity.Client>(id);
                                    if (existedClient != null)
                                    {
                                        var projectClient = new ProjectClient()
                                        {
                                            Id = 0,
                                            InProject = entity,
                                            Client = existedClient,
                                            CreateTime = now,
                                            OrderNo = projectClientOrderNo++,
                                        };
                                        involvedClients.Add(projectClient);
                                    }
                                }

                            }
                        }

                        req.TodoList?.ToList().ForEach(x => todoList.Add(DtoConverter.Convert(x)));
                        req.Progresses?.ToList().ForEach(x => progresses.Add(DtoConverter.Convert(x)));

                        entity.AddInvolvedClients(involvedClients);
                        entity.AddTodoList(todoList);
                        entity.AddProgresses(progresses);
                        repo.Create(entity);
                    }
                    return response;
                }
            );

        }

        public BaseResponse UpdateProject(UpdateProjectRequest request)
        {
            return ServiceProcessor.ProcessRequest(request,
        //inbound.do validate or do something here
        () =>
        {
        },

        req =>
        {
            var response = new BaseResponse();
            using (var repo = new NhGlobalRepository())
            {
                var entity = repo.GetById<Db.Entity.Project>(req.Id);
                if (entity == null)
                {
                    throw new EeException(ErrorCodes.NotFound, "Object is not found.");
                }
                entity.Name = req.Name;

                entity.Code = req.Code;
                entity.Details = req.Details;
                entity.Level = req.Level;
                entity.OtherLitigant = req.OtherLitigant;
                entity.InterestedParty = req.InterestedParty;
                entity.DealDate = req.DealDate;
                entity.UpdateTime = DateTime.Now;


                entity.Category = repo.GetById<ProjectCategory>(req.CategoryCode);
                entity.Owner = repo.GetById<Db.Entity.SysUser>(req.OwnerId);


                //TODO:

                repo.Update(entity);
            }
            return response;
        }
        );

        }

        public BaseResponse RemoveProject(RemoveProjectRequest request)
        {
            return ServiceProcessor.ProcessRequest(request,
               //inbound.do validate or do something here
               () =>
               {
               },

               req =>
               {
                   var response = new BaseResponse();
                   using (var repo = new NhRepository<Db.Entity.Project>())
                   {
                       foreach (var id in req.Ids)
                       {
                           var entity = repo.GetById(id);
                           if (entity != null)
                           {
                               repo.Delete(entity);
                           }
                       }
                   }
                   return response;
               }
        );
        }

        public BaseQueryResponse<Contact.DTO.Project> QueryProject(QueryProjectRequest request)
        {
            return ServiceProcessor.ProcessRequest(request,
            //inbound.do validate or do something here
            () =>
            {
            },

            req =>
            {
                var response = new BaseQueryResponse<Contact.DTO.Project>();
                using (var repo = new NhRepository<Db.Entity.Project>())
                {
                    IEnumerable<Db.Entity.Project> query;

                    if (req.Keys != null && req.Keys.Any())
                    {
                        var queryOver = repo.GetQuery().WhereRestrictionOn(x => x.Id).IsIn(req.Keys);
                        query = queryOver.List();
                    }
                    else
                    {
                        query = repo.Query();
                    }
                    response.Total = query.Count();
                    response.QueryList = query.ToList().Select(DtoConverter.Convert).ToList();
                }
                return response;
            }
         );
        }


    }
}
