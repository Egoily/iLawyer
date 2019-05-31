using ee.iLawyer.Models;
using ee.iLawyer.Ops.Contact.DTO;
using ee.iLawyer.UserControls;
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
                
            };
        }

        private void DialogHost_OnDialogClosing(object sender, DialogClosingEventArgs eventArgs)
        {

            if (!Equals(eventArgs.Parameter, true))
            {
                return;
            }

            Console.Write("Do Delete.");

        }



        private void btnOK_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var vm = (this.DataContext as HomeViewModel);
        }

        private void DisplayMonthChanged(UserControls.Agenda.MonthChangedEventArgs e)
        {

        }
    }
}
