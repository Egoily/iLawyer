using ee.iLawyer.Ops.Contact.DTO;
using System.Windows.Controls;

namespace ee.iLawyer.Modules
{
    /// <summary>
    /// Interaction logic for NewEditClient.xaml
    /// </summary>
    public partial class NewEditClientInfo : UserControl
    {

        private string objectName = "客户信息-";
        public string Title { get; set; }

        public bool IsNew { get; protected set; }

        public Client TreatedObject { get; set; }


        public NewEditClientInfo(Client Client = null)
        {
            Init(Client);
        }
        private void Init(Client Client)
        {
            InitializeComponent();

            this.Content.DataContext = this;
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
            txtTitle.Text = Title;


        }
    }
}
