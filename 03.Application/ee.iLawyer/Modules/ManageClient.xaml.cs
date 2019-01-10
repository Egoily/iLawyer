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
    /// Interaction logic for ManageClient.xaml
    /// </summary>
    public partial class ManageClient : UserControl
    {
        private ClientViewModel viewModel;

        public ManageClient()
        {
            InitializeComponent();
            viewModel = new ClientViewModel();
            DataContext = viewModel;
        }

        private void DialogHost_OnDialogClosing_DeleteItem(object sender, DialogClosingEventArgs eventArgs)
        {
            viewModel.DeleteItem(sender, eventArgs);
        }
    }

    public class IsNPToType : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((bool)value == true)
                return "自然人";
            else
                return "机构";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((string)value == "自然人")
                return true;
            else
                return false;
        }
    }
}
