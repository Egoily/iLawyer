using ee.iLawyer.ViewModels;
using MaterialDesignThemes.Wpf;
using System;
using System.Windows;
using System.Windows.Controls;

namespace ee.iLawyer.Modules
{
    /// <summary>
    /// Interaction logic for ManageCourt.xaml
    /// </summary>
    public partial class ManageCourt : UserControl
    {
        private CourtViewModel viewModel;

        public ManageCourt()
        {
            InitializeComponent();
            viewModel = new CourtViewModel();
            DataContext = viewModel;
        }

        private void DialogHost_OnDialogClosing_DeleteItem(object sender, DialogClosingEventArgs eventArgs)
        {
            viewModel.DeleteItem(sender, eventArgs);
        }
    }
}
