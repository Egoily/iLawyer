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
    }
    public class KeyValue
    {
        public int Key { get; set; }
        public string Value { get; set; }
    }
}
