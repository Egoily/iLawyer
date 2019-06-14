using ee.iLawyer.Ops.Contact.DTO;
using PropertyChanged;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace ee.iLawyer.UserControls
{
    /// <summary>
    /// Interaction logic for ClientCard.xaml
    /// </summary>
    [AddINotifyPropertyChangedInterface]
    public partial class ClientCard : UserControl
    {
        private bool isInit = false;

        public bool IsNew { get; protected set; }
        public Ops.Contact.DTO.Client TreatedObject { get; set; }
        public ObservableCollection<CategoryValue> CategoryValues { get; set; }



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
                IsNew = false;
                clientType.IsEnabled = false;

            }
            else
            {
                IsNew = true;
                clientType.IsEnabled = true;
                TreatedObject = new Client();
            }

            txtName.Text = TreatedObject.IsNP ? "姓名" : "机构名称";



        }

        public ClientCard()
        {
            InitializeComponent();
        }
    }
}
