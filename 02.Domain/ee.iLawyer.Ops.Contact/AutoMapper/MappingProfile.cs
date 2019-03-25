using AutoMapper;
using System.Collections.Generic;

namespace ee.iLawyer.Ops.Contact.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Db.Entity.Area, DTO.Area>();
            CreateMap<Db.Entity.ProjectCategory, DTO.ProjectCategory>();
            CreateMap<Db.Entity.ProjectCause, DTO.ProjectCause>();
            CreateMap<Db.Entity.PropertyItemCategory, DTO.PropertyItemCategory>();
            CreateMap<Db.Entity.Court, DTO.Court>();
            CreateMap<Db.Entity.Judge, DTO.Judge>();
            CreateMap<Db.Entity.Client, DTO.Client>();

            CreateMap<Db.Entity.Project, DTO.Project>()
                .ForMember(dest => dest.Account, m => m.Ignore())
                .ForMember(dest => dest.TodoList, m => m.Ignore())
                .ForMember(dest => dest.Progresses, m => m.Ignore())
                .ForMember(dest => dest.InvolvedClients, m => m.Ignore());
            CreateMap<Db.Entity.ProjectAccount, DTO.ProjectAccount>();
            CreateMap<Db.Entity.ProjectProgress, DTO.ProjectProgress>();
            CreateMap<Db.Entity.ProjectTodoItem, DTO.ProjectTodoItem>();




            CreateMap<DTO.ProjectAccount, Db.Entity.ProjectAccount>();
            CreateMap<DTO.ProjectProgress, Db.Entity.ProjectProgress>();
            CreateMap<DTO.ProjectTodoItem, Db.Entity.ProjectTodoItem>();








            CreateMap<IList<Db.Entity.ClientPropertyItem>, List<DTO.CategoryValue>>().ConvertUsing<ClientPropertyItemTypeConverter>();
            CreateMap<DTO.PropertyItemCategory, DTO.Category>();



        }
    }
}
