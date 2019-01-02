using AutoMapper;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ee.iLawyer.Ops.Contact.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ee.iLawyer.Db.Entity.PropertyItemCategory, Contact.DTO.PropertyItemCategory>();
            CreateMap<ee.iLawyer.Db.Entity.Area, Contact.DTO.Area>();
            CreateMap<ee.iLawyer.Db.Entity.Court, Contact.DTO.Court>();
            CreateMap<ee.iLawyer.Db.Entity.Judge, Contact.DTO.Judge>();
            CreateMap<ee.iLawyer.Db.Entity.Client, Contact.DTO.Client>();
        }
    }
}
