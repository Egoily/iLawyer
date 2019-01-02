using System.Linq;
using ee.Framework;
using System;
using System.Collections.Generic;
using ee.iLawyer.Ops.Contact.Interfaces;
using ee.SessionFactory;
using ee.iLawyer.SessionFactoryBuilder.Sqlite;
using ee.iLawyer.Ops.Contact.Args;
using ee.SessionFactory.Repository;
using ee.iLawyer.Ops.Contact.AutoMapper;

namespace ee.iLawyer.Ops
{
    public class CtsService : ICtsService
    {

        private static bool IsInit = false;
        public static void Build()
        {
            if (IsInit) return;
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

        public BaseResponse AddCourt(AddCourtRequest request)
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

        public BaseResponse AddJudge(AddJudgeRequest request)
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

                        var entity = repo.Query<Db.Entity.Judge>(x => x.Name == req.Name && x.PhoneNo == req.PhoneNo).FirstOrDefault();
                        if (entity != null)
                        {
                            throw new EeException(ErrorCodes.Existed, "Object is existed.");
                        }
                        var court = repo.GetById<Db.Entity.Court>(req.InCourtId);
                        entity = new Db.Entity.Judge()
                        {
                            Name = req.Name,
                            PhoneNo = req.PhoneNo,
                            Gender = req.Gender,
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
                        entity.PhoneNo = req.PhoneNo;
                        entity.Gender = req.Gender;
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

        public BaseResponse AddClient(AddClientRequest request)
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

                         var entity = repo.Query<Db.Entity.Client>(x => x.Name == req.Name).FirstOrDefault();
                         if (entity != null)
                         {
                             throw new EeException(ErrorCodes.Existed, "Object is existed.");
                         }
                         entity = new Db.Entity.Client()
                         {
                             Name = req.Name,
                             //TODO:
                             CreateTime = DateTime.Now,

                         };
                         repo.Create(entity);
                     }
                     return response;
                 }
             );

        }

        public BaseResponse UpdateClient(UpdateClientRequest request)
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
                         var entity = repo.GetById(req.Id);
                         if (entity == null)
                         {
                             throw new EeException(ErrorCodes.NotFound, "Object is not found.");
                         }

                         entity.Name = req.Name;
                         //TODO:

                         repo.Update(entity);
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
                        var query = repo.Query();

                        response.Total = query.Count();
                        response.QueryList = query.ToList().Select(DtoConverter.Convert).ToList();
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

                        var entity = repo.Query<Db.Entity.Project>(x => x.Name == req.Name).FirstOrDefault();
                        if (entity != null)
                        {
                            throw new EeException(ErrorCodes.Existed, "Object is existed.");
                        }
                        var involvedClients = new List<Db.Entity.ProjectClient>();
                        var clients = new List<Db.Entity.Client>();
                        if (req.Clients.Any())
                        {
                            foreach (var client in req.Clients)
                            {
                                if (client.Id > 0)
                                {
                                    var c = repo.GetById<Db.Entity.Client>(client.Id);
                                    if (c != null)
                                    {
                                        clients.Add(c);
                                    }
                                }
                                else
                                {
                                    var properties = new List<Db.Entity.ClientPropertyItem>();
                                    if (client.Properties != null && client.Properties.Any())
                                    {
                                        int orderNo = 0;
                                        foreach (var property in client.Properties)
                                        {
                                            var categorty = repo.GetById<Db.Entity.PropertyItemCategory>(property.Key);
                                            properties.Add(new Db.Entity.ClientPropertyItem()
                                            {
                                                Id = 0,
                                                Category = categorty,
                                                Value = property.Value,
                                                CreateTime = now,
                                                OrderNo = orderNo++,
                                            });
                                        }

                                    }
                                    clients.Add(new Db.Entity.Client()
                                    {
                                        Id = 0,
                                        Abbreviation = client.Abbreviation,
                                        IsNP = client.IsNP,
                                        Name = client.Name,
                                        Properties = properties,
                                        CreateTime = now,
                                    });
                                }
                            }
                        }
                        var owner = repo.GetById<Db.Entity.SysUser>(req.OwnerId);
                        entity = new Db.Entity.Project()
                        {
                            Name = req.Name,
                            Code = req.Code,
                            Level = req.Level,
                            InvolvedClients = involvedClients,
                            Owner = owner,
                            CreateTime = DateTime.Now,
                        };
                        repo.Create(entity);
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
    }
}
