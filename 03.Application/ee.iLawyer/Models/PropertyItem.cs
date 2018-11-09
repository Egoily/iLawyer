using ee.iLawyer.UserControls;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ee.iLawyer.Models
{
    [AddINotifyPropertyChangedInterface]
    public class PropertyItem
    {
        public string PropertyName { get; set; }
        public string Icon { get; set; }
        public PickerType Type { get; set; }
        public Dictionary<int, string> CategorySource { get; set; }
        public ObservableCollection<PropertyPickerItem> Items { get; set; }
    }

}
