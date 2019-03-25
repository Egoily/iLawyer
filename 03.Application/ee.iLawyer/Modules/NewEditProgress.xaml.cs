using ee.iLawyer.Ops.Contact;
using ee.iLawyer.Ops.Contact.DTO;
using PropertyChanged;
using System;
using System.Windows;
using System.Windows.Forms;

namespace ee.iLawyer.Modules
{
    /// <summary>
    /// Interaction logic for NewEditProgress.xaml
    /// </summary>
    [AddINotifyPropertyChangedInterface]
    public partial class NewEditProgress : Window
    {
        private DialogResult opResult = System.Windows.Forms.DialogResult.Cancel;

        public DialogResult OpResult => opResult;

        public NewEditProgress(ProjectProgress progress = null)
        {
            Init(progress);
        }
        private string objectName = "项目进展-";

        public bool IsNew { get; protected set; }

        public ProjectProgress TreatedObject { get; set; }



        private void Init(ProjectProgress progress)
        {
            InitializeComponent();
            this.grid.DataContext = this;
            TreatedObject = progress?.Clone() as ProjectProgress;

            if (TreatedObject != null)
            {
                Title = objectName + "编辑";
                IsNew = false;
            }
            else
            {
                Title = objectName + "新增";
                IsNew = true;
                var now = DateTime.Now;
                TreatedObject = new ProjectProgress()
                {
                    Id = Guid.NewGuid().ToString(),
                    CreateTime = now,
                    HandleTime = now,
                };
            }

        }
        private void btnAccept_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            opResult = System.Windows.Forms.DialogResult.OK;

            this.Close();
        }
        private void BtnCancel_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            opResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

      
    }
}
