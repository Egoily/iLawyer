using ee.iLawyer.Ops.Contact.DTO;
using PropertyChanged;
using System.Windows.Controls;

namespace ee.iLawyer.Modules
{
    /// <summary>
    /// Interaction logic for NewEditJudge.xaml
    /// </summary>
    [AddINotifyPropertyChangedInterface]
    public partial class NewEditJudge : UserControl
    {

        private string objectName = "法官信息-";
        public string Title { get; set; }

        public bool IsNew { get; protected set; }

        public Judge TreatedObject { get; set; }


        public NewEditJudge(Judge judge = null)
        {
            Init(judge);
        }
        private void Init(Judge judge)
        {
            InitializeComponent();

            this.grid.DataContext = this;
            TreatedObject = judge?.Clone() as Judge;

            if (TreatedObject != null && TreatedObject.Id > 0)
            {
                Title = objectName + "编辑";
                IsNew = false;
            }
            else
            {
                Title = objectName + "新增";
                IsNew = true;
                TreatedObject = new Judge();
            }
            txtTitle.Text = Title;


        }
    }
}
