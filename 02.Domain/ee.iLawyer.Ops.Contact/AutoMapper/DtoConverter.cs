using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ee.iLawyer.Ops.Contact.AutoMapper
{
    public static class DtoConverter
    {
        public static Contact.DTO.PropertyItemCategory Convert(ee.iLawyer.Db.Entity.PropertyItemCategory source)
        {
            return Mapper.Map<Contact.DTO.PropertyItemCategory>(source);
        }
        public static Contact.DTO.Area Convert(ee.iLawyer.Db.Entity.Area source)
        {
            return Mapper.Map<Contact.DTO.Area>(source);
        }
        public static Contact.DTO.Court Convert(ee.iLawyer.Db.Entity.Court source)
        {
            return Mapper.Map<Contact.DTO.Court>(source);
        }
        public static Contact.DTO.Judge Convert(ee.iLawyer.Db.Entity.Judge source)
        {
            return Mapper.Map<Contact.DTO.Judge>(source);
        }
        public static Contact.DTO.Client Convert(ee.iLawyer.Db.Entity.Client source)
        {
            return Mapper.Map<Contact.DTO.Client>(source);
        }
    }
}
