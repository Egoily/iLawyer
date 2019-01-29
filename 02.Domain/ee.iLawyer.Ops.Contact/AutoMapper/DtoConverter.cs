using AutoMapper;

namespace ee.iLawyer.Ops.Contact.AutoMapper
{
    public static class DtoConverter
    {
        public static DTO.PropertyItemCategory Convert(Db.Entity.PropertyItemCategory source)
        {
            return Mapper.Map<Contact.DTO.PropertyItemCategory>(source);
        }
        public static DTO.Category Convert(DTO.PropertyItemCategory source)
        {
            return Mapper.Map<DTO.Category>(source);
        }
        public static DTO.Area Convert(Db.Entity.Area source)
        {
            return Mapper.Map<DTO.Area>(source);
        }
        public static DTO.Court Convert(Db.Entity.Court source)
        {
            return Mapper.Map<DTO.Court>(source);
        }
        public static DTO.Judge Convert(Db.Entity.Judge source)
        {
            return Mapper.Map<DTO.Judge>(source);
        }
        public static DTO.Client Convert(Db.Entity.Client source)
        {
            return Mapper.Map<DTO.Client>(source);
        }
    }
}
