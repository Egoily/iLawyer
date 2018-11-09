using ee.iLawyer.ViewModels;
using MaterialDesignThemes.Wpf;
using System.Windows;
using System.Windows.Controls;

namespace ee.iLawyer.Modules
{
    /// <summary>
    /// Interaction logic for JudgeManagement.xaml
    /// </summary>
    public partial class JudgeManagement : UserControl
    {
        private JudgeViewModel viewModel;

        public JudgeManagement()
        {
            InitializeComponent();
            viewModel = new JudgeViewModel();
            DataContext = viewModel;
        }

        private void DialogHost_OnDialogClosing_DeleteItem(object sender, DialogClosingEventArgs eventArgs)
        {
            viewModel.DeleteItem(sender, eventArgs);
        }
    }
}
