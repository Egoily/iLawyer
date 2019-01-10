using AutoMapper;
using ee.iLawyer.Db.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    public class ClientPropertyItemTypeConverter : ITypeConverter<IList<ClientPropertyItem>, List<DTO.KeyValue>>
    {
        public List<DTO.KeyValue> Convert(IList<ClientPropertyItem> source, List<DTO.KeyValue> destination, ResolutionContext context)
        {
            if (destination == null)
                destination = new List<DTO.KeyValue>();
            if (source != null)
            {
                foreach (var item in source)
                {
                    if (item.Category != null)
                    {
                        destination.Add(new DTO.KeyValue(item.Category.Id, item.Value, item.Id));
                    }
                }
            }
            return destination;
        }
    }
}
