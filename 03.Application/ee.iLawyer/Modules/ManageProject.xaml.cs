using ee.iLawyer.ViewModels;
using MaterialDesignThemes.Wpf;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace ee.iLawyer.Modules
{
    /// <summary>
    /// Interaction logic for ManageProject.xaml
    /// </summary>
    public partial class ManageProject : UserControl
    {
        private ProjectViewModel viewModel;

        public ManageProject()
        {
            InitializeComponent();
            viewModel = new ProjectViewModel();
            DataContext = viewModel;
        }

        private void DialogHost_OnDialogClosing_DeleteItem(object sender, DialogClosingEventArgs eventArgs)
        {
            viewModel.DeleteItem(sender, eventArgs);
        }
    }


}
