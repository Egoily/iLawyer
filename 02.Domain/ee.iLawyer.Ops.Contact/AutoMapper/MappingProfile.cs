using AutoMapper;
using System.Collections.Generic;

namespace ee.iLawyer.Ops.Contact.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Db.Entity.PropertyItemCategory, DTO.PropertyItemCategory>();
            CreateMap<Db.Entity.Area, DTO.Area>();
            CreateMap<Db.Entity.Court, DTO.Court>();
            CreateMap<Db.Entity.Judge, DTO.Judge>();
            CreateMap<Db.Entity.Client, DTO.Client>();
            CreateMap<DTO.PropertyItemCategory, DTO.Category>();
            CreateMap<IList<Db.Entity.ClientPropertyItem>, List<DTO.CategoryValue>>().ConvertUsing<ClientPropertyItemTypeConverter>();
        }
    }
}
