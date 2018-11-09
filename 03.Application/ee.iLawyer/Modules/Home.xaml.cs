using ee.iLawyer.Models;
using ee.iLawyer.ViewModels;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            this.DataContext = new HomeViewModel()
            {
                PhoneItems = new ObservableCollection<PropertyPickerItem>
                {
                    new PropertyPickerItem()
                    {
                        Guid=Guid.NewGuid(),
                        IsDefault=true,
                        KeyValue=new KeyValue() { Key=1,Value="13610142196"}
                    },
                    new PropertyPickerItem()
                    {
                        Guid=Guid.NewGuid(),
                        KeyValue=new KeyValue() { Key=2,Value="13763357288"}
                    }
                },
             
                Items=new ObservableCollection<PropertyItem>
                {
                   new PropertyItem()
                   {
                       PropertyName="电话",
                        Icon="Phone",
                         
                   },
                    new PropertyItem()
                   {
                       PropertyName="证件",
                        Icon="Phone",

                   },
                },

            };
        }

        private void DialogHost_OnDialogClosing(object sender, DialogClosingEventArgs eventArgs)
        {

            if (!Equals(eventArgs.Parameter, true)) return;

            Console.Write("Do Delete.");

        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var items = (this.DataContext as HomeViewModel).PhoneItems;
        }
    }
}
