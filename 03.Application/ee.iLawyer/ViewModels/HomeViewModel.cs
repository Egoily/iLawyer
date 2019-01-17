using ee.iLawyer.Ops.Contact;
using ee.iLawyer.Ops.Contact.DTO;
using ee.iLawyer.UserControls;
using PropertyChanged;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ee.iLawyer.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class HomeViewModel
    {
        public PropertyListItem PhonePropertyListItem { get; set; }
        public int MyProperty { get; set; }

        public ObservableCollection<PropertyListItem> PersonProperties { get; set; }

        public ProjectLevel ProjectLevel { get; set; }

        public MaterialDesignThemes.Wpf.PackIconKind Icon { get; set; }
        public string Text { get; set; }
        public ObservableCollection<Category> CategorySource { get; set; }

        public CategoryValue CategoryValue { get; set; }
    }
}
