using ee.iLawyer.Models;
using ee.iLawyer.UserControls;
using ee.iLawyer.ViewModels;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;

namespace ee.iLawyer.Modules
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : UserControl
    {
        public Home()
        {
            InitializeComponent();
            var phonePropertyListItem = new PropertyListItem()
            {
                PickerProperty = GlobalViewModel.GetPickerProperty(Ops.Contact.MainPrpoertyCategory.Phone),
                Items = new ObservableCollection<PropertyPickerItem>()
                    {
                        new PropertyPickerItem()
                        {
                          Guid=Guid.NewGuid(),
                          IsDefault=true,
                          KeyValue=new KeyValue(),
                        },
                        new PropertyPickerItem()
                        {
                          Guid=Guid.NewGuid(),
                          IsDefault=false,
                          KeyValue =new KeyValue(),
                        }
                    }
            };
            this.DataContext = new HomeViewModel()
            {
                MyProperty = 11,
                PhonePropertyListItem = phonePropertyListItem,
                PersonProperties = GlobalViewModel.GetPropertyListItems(),

            };
        }

        private void DialogHost_OnDialogClosing(object sender, DialogClosingEventArgs eventArgs)
        {

            if (!Equals(eventArgs.Parameter, true)) return;

            Console.Write("Do Delete.");

        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var items = (this.DataContext as HomeViewModel).PhonePropertyListItem;
            var a = propertyListPicker;
        }
    }
}
