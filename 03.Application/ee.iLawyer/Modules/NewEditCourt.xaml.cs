using System.Windows.Controls;
using PropertyChanged;
using ee.iLawyer.Ops.Contact.DTO;
using ee.iLawyer.ViewModels;

namespace ee.iLawyer.Modules
{
    /// <summary>
    /// Interaction logic for NewEditCourt.xaml
    /// </summary>
    [AddINotifyPropertyChangedInterface]
    public partial class NewEditCourt : UserControl
    {
        public NewEditCourt(Court court = null)
        {
            Init(court);
        }
        private string objectName = "法院信息-";

        public string Title { get; set; }

        public bool IsNew { get; protected set; }

        public Court TreatedObject { get; set; }
    


        private void Init(Court court)
        {
            InitializeComponent();
            this.grid.DataContext = this;
            TreatedObject = court?.Clone() as Court;

            if (TreatedObject != null && TreatedObject.Id > 0)
            {
                Title = objectName + "编辑";
                IsNew = false;
            }
            else
            {
                Title = objectName + "新增";
                IsNew = true;
                TreatedObject = new Court();
            }
            areaSelector.Provinces = WebApi.Invoker.Cacher.Provinces;
        }



    }
}
