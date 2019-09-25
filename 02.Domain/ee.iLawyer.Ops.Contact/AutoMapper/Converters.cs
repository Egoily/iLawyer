using AutoMapper;
using ee.iLawyer.Db.Entities;
using System.Collections.Generic;

namespace ee.iLawyer.Ops.Contact.AutoMapper
{
    public class GenderTypeConverter : ITypeConverter<int, string>
    {
        public string Convert(int source, string destination, ResolutionContext context)
        {
            switch (source)
            {
                case 1:
                    destination = "男";
                    break;
                case 2:
                    destination = "女";
                    break;
                default:
                    destination = "未知";
                    break;
            }
            return destination;
        }
    }

    public class ClientPropertyItemTypeConverter : ITypeConverter<IList<ClientProperties>, List<DTO.CategoryValue>>
    {
        public List<DTO.CategoryValue> Convert(IList<ClientProperties> source, List<DTO.CategoryValue> destination, ResolutionContext context)
        {
            if (destination == null)
            {
                destination = new List<DTO.CategoryValue>();
            }

            if (source != null)
            {
                foreach (var item in source)
                {
                    if (item.Picker != null)
                    {
                        destination.Add(new DTO.CategoryValue(item.Picker.Id??0, item.Picker.Name, item.Value, item.Id));
                    }
                }
            }
            return destination;
        }
    }
}
