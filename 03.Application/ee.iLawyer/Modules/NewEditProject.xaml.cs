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
    /// Interaction logic for NewEditProject.xaml
    /// </summary>
    [AddINotifyPropertyChangedInterface]
    public partial class NewEditProject : UserControl
    {

        private string objectName = "案件信息-";

        private bool isInit = false;

        public string Title { get; set; }

        public bool IsNew { get; protected set; }

        public Project TreatedObject { get; set; }


        public NewEditProject(Project project = null)
        {
            isInit = false;
            Init(project);
            isInit = true;
        }
        private void Init(Project project)
        {
            InitializeComponent();

            this.grid.DataContext = this;
            TreatedObject = project?.Clone() as Project;

           

            if (TreatedObject != null && TreatedObject.Id > 0)
            {
                Title = objectName + "编辑";
                IsNew = false;
        
            }
            else
            {
                Title = objectName + "新增";
                IsNew = true;
                TreatedObject = new Project();
            }
            

        }



    

        private void Accept_Click(object sender, System.Windows.RoutedEventArgs e)
        {
         
        }






    }


}
