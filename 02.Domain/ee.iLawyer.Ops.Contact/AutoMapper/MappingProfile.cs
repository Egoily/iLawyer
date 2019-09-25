using AutoMapper;
using System.Collections.Generic;

namespace ee.iLawyer.Ops.Contact.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            CreateMap<CourtRank, string>().ConvertUsing(src => src.ToString());

            CreateMap<Db.Entities.Foundation.Area, DTO.Area>()
                .ForMember(dest => dest.AreaCode, m => m.MapFrom(s => s.Id));
            CreateMap<Db.Entities.Foundation.Picklist, DTO.ProjectCategory>();
            CreateMap<Db.Entities.Foundation.Picklist, DTO.ProjectCause>();
            CreateMap<Db.Entities.Foundation.Picklist, DTO.PropertyPicker>()
                .ForMember(dest => dest.Icon, m => m.MapFrom(s => s.Value))
                .ForMember(dest => dest.PickerType, m => m.MapFrom(s => s.SubValue));

            CreateMap<Db.Entities.RBAC.SysUser, DTO.SystemManagement.User>();
            CreateMap<Db.Entities.RBAC.SysRole, DTO.SystemManagement.Role>();

            CreateMap<Db.Entities.Court, DTO.Court>();
            CreateMap<Db.Entities.Judge, DTO.Judge>();
            CreateMap<Db.Entities.Client, DTO.Client>();

            CreateMap<Db.Entities.Project, DTO.Project>()
                .ForMember(dest => dest.Account, m => m.Ignore())
                .ForMember(dest => dest.TodoList, m => m.Ignore())
                .ForMember(dest => dest.Progresses, m => m.Ignore())
                .ForMember(dest => dest.InvolvedClients, m => m.Ignore());
            CreateMap<Db.Entities.ProjectAccount, DTO.ProjectAccount>();
            CreateMap<Db.Entities.ProjectProgress, DTO.ProjectProgress>();
            CreateMap<Db.Entities.ProjectTodoItem, DTO.ProjectTodoItem>();




            CreateMap<DTO.ProjectAccount, Db.Entities.ProjectAccount>();
            CreateMap<DTO.ProjectProgress, Db.Entities.ProjectProgress>();
            CreateMap<DTO.ProjectTodoItem, Db.Entities.ProjectTodoItem>();








            CreateMap<IList<Db.Entities.ClientProperties>, List<DTO.CategoryValue>>().ConvertUsing<ClientPropertyItemTypeConverter>();
            CreateMap<DTO.PropertyPicker, DTO.Category>();




        }
    }
}
