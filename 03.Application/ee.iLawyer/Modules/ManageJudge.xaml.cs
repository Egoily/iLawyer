using ee.iLawyer.ViewModels;
using MaterialDesignThemes.Wpf;
using System.Windows;
using System.Windows.Controls;

namespace ee.iLawyer.Modules
{
    /// <summary>
    /// Interaction logic for ManageJudge.xaml
    /// </summary>
    public partial class ManageJudge : UserControl
    {
        private JudgeViewModel viewModel;

        public ManageJudge()
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
