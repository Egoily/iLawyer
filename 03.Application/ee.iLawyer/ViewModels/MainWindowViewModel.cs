using MaterialDesignThemes.Wpf;
using System;
using ee.iLawyer.Modules;
using ee.iLawyer.Models;

namespace ee.iLawyer.ViewModels
{
    public class MainWindowViewModel
    {
        public MainWindowViewModel(ISnackbarMessageQueue snackbarMessageQueue)
        {
            if (snackbarMessageQueue == null) throw new ArgumentNullException(nameof(snackbarMessageQueue));

            ModuleItems = new[]
            {
                new ModuleItem("Home", new Home()),
                new ModuleItem("法院管理", new ManageCourt { } ),
                new ModuleItem("法官管理", new ManageJudge { } ),
                new ModuleItem("客户管理", new ManageClient { } ),
                new ModuleItem("案件管理", new ManageProject { } ),
            };
        }

        public ModuleItem[] ModuleItems { get; }
    }
}