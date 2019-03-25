using ee.Framework;
using ee.iLawyer.Ops;
using ee.iLawyer.Ops.Contact.DTO;
using ee.iLawyer.ViewModels;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

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


        public ClientSearchProvider ClientSearchProvider { get { return ClientSearchProvider.Instance; } }
        public ObservableCollection<MultiItemSelectorItemInfo> SelectedItems { get; set; }

        public NewEditProject()
        {
            isInit = false;
            Init(null);
            isInit = true;
        }
        public NewEditProject(Project project)
        {
            isInit = false;
            Init(project);
            isInit = true;
        }
        private void Init(Project project)
        {
            InitializeComponent();
            var now = DateTime.Now;
            this.grid.DataContext = this;
            TreatedObject = project?.Clone() as Project;
            if (TreatedObject?.InvolvedClients != null)
            {
                //SelectedItems = new ObservableCollection<MultiItemSelectorItemInfo>();
                //TreatedObject.Clients.ForEach(x => SelectedItems.AddIfNotContains(new MultiItemSelectorItemInfo()
                //{
                //    Id = x.Id,
                //    DisplayText = x.Name,
                //    Description = x.Abbreviation + x.Impression,
                //}));

                SelectedItems = new ObservableCollection<MultiItemSelectorItemInfo>(ClientSearchProvider.DataSource.Where(x => x.Id.In(TreatedObject.InvolvedClients.Select(o => o.Id).ToArray())).ToList());
            }
            else
            {
                SelectedItems = new ObservableCollection<MultiItemSelectorItemInfo>();
            }


            if (TreatedObject != null && TreatedObject.Id > 0)
            {
                Title = objectName + "编辑";
                IsNew = false;

            }
            else
            {
                Title = objectName + "新增";
                IsNew = true;
                TreatedObject = new Project()
                {
                    CreateTime = now,
                    DealDate = now,
                };
            }


        }


        private void UpdateTodoItem(ProjectTodoItem item)
        {
            if (TreatedObject.TodoList == null)
            {
                return;
            }

            var editObj = TreatedObject.TodoList.FirstOrDefault(x => x.Id == item.Id);
            if (editObj != null)
            {
                editObj.Content = item.Content;
                editObj.CreateTime = item.CreateTime;
                editObj.ExpiredTime = item.ExpiredTime;
                editObj.CompletedTime = item.CompletedTime;
                editObj.IsSetRemind = item.IsSetRemind;
                editObj.Name = item.Name;
                editObj.Priority = item.Priority;
                editObj.RemindTime = item.RemindTime;
                editObj.Status = item.Status;

                dataGridTodoList.ItemsSource = null;
                dataGridTodoList.ItemsSource = TreatedObject.TodoList;
            }
        }

        private void RemoveTodoItem(ProjectTodoItem item)
        {
            if (item != null)
            {
                TreatedObject.TodoList.Remove(item);
                dataGridTodoList.ItemsSource = null;
                dataGridTodoList.ItemsSource = TreatedObject.TodoList;
            }
        }
        private void UpdateProgress(ProjectProgress item)
        {
            if (TreatedObject.Progresses == null)
            {
                return;
            }

            var editObj = TreatedObject.Progresses.FirstOrDefault(x => x.Id == item.Id);
            if (editObj != null)
            {
                editObj.HandleTime = item.HandleTime;
                editObj.Content = item.Content;
                editObj.CreateTime = item.CreateTime;

                dataGridProgress.ItemsSource = null;
                dataGridProgress.ItemsSource = TreatedObject.Progresses;
            }
        }
        private void RemoveProgress(ProjectProgress item)
        {
            if (item != null)
            {
                TreatedObject.Progresses.Remove(item);
                dataGridProgress.ItemsSource = null;
                dataGridProgress.ItemsSource = TreatedObject.Progresses;
            }
        }
        private void Accept_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (isInit)
            {
                if (SelectedItems != null && SelectedItems.Any())
                {
                    var server = new CtsService();
                    var response = server.QueryClient(new Ops.Contact.Args.QueryClientRequest() { Keys = SelectedItems.Select(x => x.Id).ToArray() });
                    if (response.Code == ErrorCodes.Ok && (response.QueryList?.Any() ?? false))
                    {
                        TreatedObject.InvolvedClients = response.QueryList;
                    }
                }
            }
        }

        private void BtnAddTodoItem_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var popup = new NewEditTodoItem();

            popup.ShowDialog();
            if (popup.OpResult == System.Windows.Forms.DialogResult.OK)
            {
                if (TreatedObject.TodoList == null)
                {
                    TreatedObject.TodoList = new List<ProjectTodoItem>();
                }
                TreatedObject.TodoList.Add(popup.TreatedObject);
                dataGridTodoList.ItemsSource = null;
                dataGridTodoList.ItemsSource = TreatedObject.TodoList;
            }

        }
        private void BtnEditTodoItem_Click(object sender, RoutedEventArgs e)
        {
            if (TreatedObject.TodoList != null)
            {
                var todoItem = ((FrameworkElement)sender).DataContext as ProjectTodoItem;
                if (todoItem != null)
                {
                    var popup = new NewEditTodoItem(todoItem);

                    popup.ShowDialog();
                    if (popup.OpResult == System.Windows.Forms.DialogResult.OK)
                    {
                        UpdateTodoItem(popup.TreatedObject);
                    }
                }

            }
        }
        private void BtnRemoveTodoItem_Click(object sender, RoutedEventArgs e)
        {
            if (TreatedObject.TodoList != null)
            {
                var todoItem = ((FrameworkElement)sender).DataContext as ProjectTodoItem;
                RemoveTodoItem(todoItem);

            }
        }

        private void BtnAddProgress_Click(object sender, RoutedEventArgs e)
        {
            var popup = new NewEditProgress();

            popup.ShowDialog();
            if (popup.OpResult == System.Windows.Forms.DialogResult.OK)
            {
                if (TreatedObject.Progresses == null)
                {
                    TreatedObject.Progresses = new List<ProjectProgress>();
                }
                TreatedObject.Progresses.Add(popup.TreatedObject);
                dataGridProgress.ItemsSource = null;
                dataGridProgress.ItemsSource = TreatedObject.Progresses;
            }
        }

        private void BtnEditProgress_Click(object sender, RoutedEventArgs e)
        {
            if (TreatedObject.Progresses != null)
            {
                var progress = ((FrameworkElement)sender).DataContext as ProjectProgress;
                if (progress != null)
                {
                    var popup = new NewEditProgress(progress);

                    popup.ShowDialog();
                    if (popup.OpResult == System.Windows.Forms.DialogResult.OK)
                    {
                        UpdateProgress(popup.TreatedObject);
                    }
                }

            }
        }

        private void BtnRemoveProgress_Click(object sender, RoutedEventArgs e)
        {
            if (TreatedObject.Progresses != null)
            {
                var progress = ((FrameworkElement)sender).DataContext as ProjectProgress;
                RemoveProgress(progress);

            }
        }
    }


}
