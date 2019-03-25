using ee.iLawyer.Ops.Contact;
using ee.iLawyer.Ops.Contact.DTO;
using PropertyChanged;
using System;
using System.Windows;
using System.Windows.Forms;

namespace ee.iLawyer.Modules
{
    /// <summary>
    /// Interaction logic for NewEditTodoItem.xaml
    /// </summary>
    [AddINotifyPropertyChangedInterface]
    public partial class NewEditTodoItem : Window
    {
        private DialogResult opResult = System.Windows.Forms.DialogResult.Cancel;

        public DialogResult OpResult => opResult;

        public NewEditTodoItem(ProjectTodoItem todoItem = null)
        {
            Init(todoItem);
        }
        private string objectName = "待办事项-";

        public bool IsNew { get; protected set; }

        public ProjectTodoItem TreatedObject { get; set; }



        private void Init(ProjectTodoItem todoItem)
        {
            InitializeComponent();
            this.grid.DataContext = this;
            TreatedObject = todoItem?.Clone() as ProjectTodoItem;

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
                TreatedObject = new ProjectTodoItem()
                {
                    Id = Guid.NewGuid().ToString(),
                    CreateTime = now,
                    ExpiredTime = DateTime.Now.AddDays(1),
                    CompletedTime = null,
                    RemindTime = null,
                };
            }

        }
        private void btnAccept_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            opResult = System.Windows.Forms.DialogResult.OK;
            if (cbbStatus.SelectedItem != null)
            {
                switch (cbbStatus.SelectedItem)
                {
                    case StatusOfTodoItem.Pending:
                        TreatedObject.CompletedTime = null;
                        break;
                    case StatusOfTodoItem.Completed:
                        if (TreatedObject.CompletedTime == null)
                        {
                            TreatedObject.CompletedTime = DateTime.Now;
                        }
                        break;
                    case StatusOfTodoItem.Canceled:
                        TreatedObject.CompletedTime = null;
                        break;
                    default:
                        break;
                }
            }
            this.Close();
        }
        private void BtnCancel_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            opResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        private void CbbStatus_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (cbbStatus.SelectedItem != null )
            {
                switch (cbbStatus.SelectedItem)
                {
                    case StatusOfTodoItem.Pending:
                        dpCompletedTime.Text = "";
                        break;
                    case StatusOfTodoItem.Completed:
                        if(TreatedObject.CompletedTime==null)
                        {
                            dpCompletedTime.SelectedDate = DateTime.Now;
                        }
                            break;
                    case StatusOfTodoItem.Canceled:
                        dpCompletedTime.Text = "";
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
