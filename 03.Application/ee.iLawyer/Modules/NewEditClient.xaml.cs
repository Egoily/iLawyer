using ee.iLawyer.Ops.Contact.DTO;
using PropertyChanged;
using System.Collections.ObjectModel;
using System.Windows.Controls;

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
        public ObservableCollection<CategoryValue> CategoryValues { get; set; }


        public NewEditClient(Client Client = null)
        {
            isInit = false;
            Init(Client);
            isInit = true;
        }
        private void Init(Client Client)
        {
            InitializeComponent();

            this.grid.DataContext = this;
            TreatedObject = Client?.Clone() as Client;
            if (TreatedObject == null)
            {
                CategoryValues = new ObservableCollection<CategoryValue>();
            }
            else
            {
                CategoryValues = new ObservableCollection<CategoryValue>(TreatedObject.Properties);
            }

            if (TreatedObject != null && TreatedObject.Id > 0)
            {
                Title = objectName + "编辑";
                IsNew = false;
                clientType.IsEnabled = false;



            }
            else
            {
                Title = objectName + "新增";
                IsNew = true;
                clientType.IsEnabled = true;
                TreatedObject = new Client
                {

                };
             
            }
            if (TreatedObject.IsNP)
            {
                txtName.Text = "姓名";

            }
            else
            {
                txtName.Text = "机构名称";
            }


        }



        private void tab_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.Source is ListBox)
            {
                if (IsNew)
                {
                    ;
                }

            }

        }

        private void Accept_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (isInit)
            {
                TreatedObject.Properties = new System.Collections.Generic.List<CategoryValue>(CategoryValues);
            }
        }

        private void ClientTypeControl_TypeChanged(object sender, UserControls.TypeRoutedEventArge e)
        {
            if (e.IsNaturalPerson)
            {
                txtName.Text = "姓名";

            }
            else
            {
                txtName.Text = "机构名称";
            }
        }


    }


}
