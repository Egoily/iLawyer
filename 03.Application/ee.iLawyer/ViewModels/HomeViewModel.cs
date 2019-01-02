using ee.iLawyer.Models;
using ee.iLawyer.UserControls;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ee.iLawyer.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class HomeViewModel
    {
        public PropertyListItem PhonePropertyListItem { get; set; }
        public int MyProperty { get; set; }

        public ObservableCollection<PropertyListItem> PersonProperties { get; set; }
    }
}
