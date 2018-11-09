using ee.iLawyer.Models;
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
        public Dictionary<int, string> PhoneCategories
        {
            get
            {
                return new Dictionary<int, string>
                {
                    { 1, "重要" },
                    { 2, "手机" },
                    { 3, "办公" },
                    { 4, "家庭" },
                };
            }
        }

        public ObservableCollection<PropertyPickerItem> PhoneItems { get; set; }

        public ObservableCollection<PropertyItem> Items { get; set; }


    }
}
