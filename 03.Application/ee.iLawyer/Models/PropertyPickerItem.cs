using ee.iLawyer.Ops.Contact.DTO;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ee.iLawyer.Models
{
    [AddINotifyPropertyChangedInterface]
    public class PropertyPickerItem
    {
        public Guid Guid { get; set; }
        public bool IsDefault { get; set; }
        public KeyValue KeyValue { get; set; }

        public int OrderNo { get; set; }
    }
    
}
