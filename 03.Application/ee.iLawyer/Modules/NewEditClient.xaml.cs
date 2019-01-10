using ee.iLawyer.Ops.Contact.DTO;
using PropertyChanged;
using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
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

        private bool isInit = false;

        public string Title { get; set; }

        public bool IsNew { get; protected set; }

        public Client TreatedObject { get; set; }
        public ObservableCollection<UserControls.PropertyListItem> PersonProperties { get; set; }


        public NewEditClient(Client Client = null, ObservableCollection<UserControls.PropertyListItem> personProperties = null)
        {
            isInit = false;
            Init(Client, personProperties);
            isInit = true;
        }
        private void Init(Client Client, ObservableCollection<UserControls.PropertyListItem> personProperties = null)
        {
            InitializeComponent();

            this.grid.DataContext = this;
            TreatedObject = Client?.Clone() as Client;

            PersonProperties = personProperties;
            if (PersonProperties == null || !PersonProperties.Any())
            {
                PersonProperties = ViewModels.GlobalViewModel.GetPropertyListItems();
            }



            if (TreatedObject != null && TreatedObject.Id > 0)
            {
                Title = objectName + "编辑";
                IsNew = false;



                if (TreatedObject.IsNP)
                {
                    tab.SelectedIndex = 1;
                    tabItemOrganization.Visibility = System.Windows.Visibility.Hidden;
                    tabItemNaturalPerson.Visibility = System.Windows.Visibility.Visible;
                }
                else
                {
                    tab.SelectedIndex = 0;
                    tabItemOrganization.Visibility = System.Windows.Visibility.Visible;
                    tabItemNaturalPerson.Visibility = System.Windows.Visibility.Hidden;

                }

            }
            else
            {
                Title = objectName + "新增";
                IsNew = true;
                TreatedObject = new Client();
            }

            KeyValueToPropertyListItem();

        }

        private void KeyValueToPropertyListItem()
        {
            if (TreatedObject==null||TreatedObject.Properties == null)
                return;
            foreach (var property in TreatedObject.Properties)
            {
                foreach (var item in PersonProperties)
                {
                    if (item.PickerProperty.CategorySource.Any(x => x.Id == property.Key))
                    {
                        var nullItem = item.Items.FirstOrDefault(x => x.KeyValue == null);
                        if (nullItem == null)
                        {
                            item.Items.Add(new Models.PropertyPickerItem()
                            {
                                Guid = Guid.NewGuid(),
                                KeyValue = property,
                            });
                        }
                        else
                        {
                            nullItem.KeyValue = property;
                        }
                    }
                }
            }
        }

        private void PropertyListItemToKeyValue()
        {
            TreatedObject.Properties.Clear();
            var items = PersonProperties.SelectMany(x => x.Items).ToList();
            foreach (var item in items)
            {
                if (item.KeyValue != null && item.KeyValue.Key > 0 && !string.IsNullOrEmpty(item.KeyValue.Value))
                {
                    TreatedObject.Properties.Add(item.KeyValue);
                }

            }

        }

        private void tab_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.Source is TabControl)
            {
                if (IsNew)
                {
                    TreatedObject.IsNP = tab.SelectedIndex == 1;
                }
            }

        }

        private void Accept_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (isInit)
                PropertyListItemToKeyValue();
        }






    }


}
