﻿using ee.iLawyer.Ops.Contact.DTO;
using PropertyChanged;
using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;

namespace ee.iLawyer.Modules
{
    /// <summary>
    /// Interaction logic for NewEditClient.xaml
    /// </summary>
    [AddINotifyPropertyChangedInterface]
    public partial class NewEditClient : UserControl
    {

        private string objectName = "客户信息-";
        public string Title { get; set; }

        public bool IsNew { get; protected set; }

        public Client TreatedObject { get; set; }


        public NewEditClient(Client Client = null)
        {
            Init(Client);
        }
        private void Init(Client Client)
        {
            InitializeComponent();

            this.grid.DataContext = this;
            TreatedObject = Client?.Clone() as Client;

            if (TreatedObject != null && TreatedObject.Id > 0)
            {
                Title = objectName + "编辑";
                IsNew = false;
            }
            else
            {
                Title = objectName + "新增";
                IsNew = true;
                TreatedObject = new Client();
            }
            groupBox.Header = Title;



        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var a = ViewModels.GlobalViewModel.PropertyListItemOfPhone;
        }
    }


}
