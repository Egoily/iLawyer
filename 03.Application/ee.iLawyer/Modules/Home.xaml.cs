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
            var phonePropertyListItem = new PropertyListItem()
            {
                PickerProperty = GlobalViewModel.GetPickerProperty(Ops.Contact.MainPrpoertyCategory.Phone),
                Items = new ObservableCollection<PropertyPickerItem>()
                    {
                        new PropertyPickerItem()
                        {
                          Guid=Guid.NewGuid(),
                          IsDefault=true,
                          KeyValue=new KeyValue(),
                        },
                        new PropertyPickerItem()
                        {
                          Guid=Guid.NewGuid(),
                          IsDefault=false,
                          KeyValue =new KeyValue(),
                        }
                    }
            };
            this.DataContext = new HomeViewModel()
            {
                MyProperty = 11,
                PhonePropertyListItem = phonePropertyListItem,
                PersonProperties = GlobalViewModel.GetPropertyListItems(),
                Icon = MaterialDesignThemes.Wpf.PackIconKind.Airplay,
                Text = "text",
                CategorySource = new ObservableCollection<Category>
                {
                      new Category()
                      { Id=1,Name="电话",Icon="Phone",
                      Children=new List<Category>()
                      {
                          new Category(){Id=11,Name="个人手机",Icon="MobilePhoneIphone"},
                          new Category(){Id=12,Name="工作手机",Icon="MobilePhoneBasic"},
                          new Category(){Id=13,Name="家庭电话",Icon="PhoneClassic"},
                          new Category(){Id=14,Name="办公电话",Icon="Deskphone"},
                      }

                      },
                      new Category(){Id=2,Name="邮箱",Icon="Email",
                      Children=new List<Category>()
                      {
                          new Category(){Id=21,Name="个人邮箱",Icon="Email"},
                          new Category(){Id=22,Name="注册邮箱",Icon="AlternateEmail"},
                
                      }
                      },
                      new Category(){Id=3,Name="地址",Icon="AddressMarker"},
                      new Category(){Id=4,Name="证件",Icon="Certificate"},
                      new Category(){Id=5,Name="重要人物",Icon="FaceProfile"},
                      new Category(){Id=6,Name="重要日期",Icon="Timetable"},
                },
                //CategoryValue = new CategoryValue()
                //{
                //    CategoryId = 13,
                //    CategoryName = "家庭电话",
                //    Value = "564",
                //},

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
    }
}
