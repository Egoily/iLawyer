﻿using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using CodeDisplayer;
using MaterialDesignThemes.Wpf;
using System.IO;
using ee.iLawyer.ViewModels;

namespace ee.iLawyer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static Snackbar Snackbar;
        public MainWindow()
        {
            InitializeComponent();
            var sourceLocation = File.Exists(@"..\..\MainWindow.xaml") ? XamlDisplayerPanel.SourceEnum.LoadFromLocal : XamlDisplayerPanel.SourceEnum.LoadFromRemote;

            XamlDisplayerPanel.Initialize(
                source: sourceLocation,
                defaultLocalPath: $@"..\..\",
                defaultRemotePath: @"https://raw.githubusercontent.com/ButchersBoy/MaterialDesignInXamlToolkit/master/MainDemo.Wpf/",
                attributesToBeRemoved:
                new List<string>()
                {
                    "xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\"",
                    "xmlns:materialDesign=\"http://materialdesigninxaml.net/winfx/xaml/themes\"",
                    "xmlns:x=\"http://schemas.microsoft.com/winfx/2006/xaml\""
                });
            Task.Factory.StartNew(() =>
            {
                WebApi.Invoker.Cacher.Load();
            }).ContinueWith(t =>
            {
                //note you can use the message queue from any thread, but just for the demo here we 
                //need to get the message queue from the snackbar, so need to be on the dispatcher
                MainSnackbar.MessageQueue.Enqueue("Welcome to use iLawyer");
            }, TaskScheduler.FromCurrentSynchronizationContext());

            DataContext = new MainWindowViewModel(MainSnackbar.MessageQueue);

            Snackbar = this.MainSnackbar;

 
        }
     
        private void UIElement_OnPreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //until we had a StaysOpen glag to Drawer, this will help with scroll bars
            var dependencyObject = Mouse.Captured as DependencyObject;
            while (dependencyObject != null)
            {
                if (dependencyObject is ScrollBar) return;
                dependencyObject = VisualTreeHelper.GetParent(dependencyObject);
            }

            MenuToggleButton.IsChecked = false;
        }

        private void MenuPopupButton_OnClick(object sender, RoutedEventArgs e)
        {

        }
    }
}
