using ee.Framework;
using ee.iLawyer.Modules;
using ee.iLawyer.Ops;
using ee.iLawyer.Ops.Contact.Args;
using ee.iLawyer.Ops.Contact.DTO;
using ee.iLawyer.UserControls;
using MaterialDesignThemes.Wpf;
using PropertyChanged;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ee.iLawyer.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class ProjectViewModel : AbstractViewModel
    {

        public ObservableCollection<Project> Projects { get; protected set; }
        public Project SelectedItem { get; set; }



        public ProjectViewModel()
        {
        }


        public override void Query()
        {
            System.Threading.ThreadPool.QueueUserWorkItem(delegate
            {
                System.Threading.SynchronizationContext.SetSynchronizationContext(new
                  System.Windows.Threading.DispatcherSynchronizationContext(System.Windows.Application.Current.Dispatcher));
                System.Threading.SynchronizationContext.Current.Post(pl =>
                {
                    try
                    {
                        var server = new CtsService();
                        var response = server.QueryProject(new QueryProjectRequest());
                        if (response.Code == ErrorCodes.Ok && response.QueryList != null)
                        {
                            Projects = new ObservableCollection<Project>();
                            response.QueryList.ToList().ForEach(x => Projects.Add(x));
                        }

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }

                }, null);
            });

        }

        public override BaseResponse Create()
        {
            if (SelectedItem == null)
            {
                return new BaseResponse() { Code = ErrorCodes.NullParameter, Message = "新增的对象为空." };
            }

            try
            {
                var server = new CtsService();
                var response = server.CreateProject(new CreateProjectRequest()
                {
                    CategoryCode = SelectedItem.Category.Code,
                    Name = SelectedItem.Name,
                    Code = SelectedItem.Code,
                    Level = SelectedItem.Level.ToString(),
                    Details = SelectedItem.Details,
                    InvolvedClientIds = SelectedItem.InvolvedClients.Select(x => x.Id).ToList(),
                    OtherLitigant = SelectedItem.OtherLitigant,
                    InterestedParty = SelectedItem.InterestedParty,
                    DealDate = SelectedItem.DealDate,

                    Account = SelectedItem.Account,
                    TodoList = SelectedItem.TodoList,
                    Progresses = SelectedItem.Progresses,
                });
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new BaseResponse() { Code = ErrorCodes.UnknownError, Message = "Unknown Error" };
            }

        }
        public override BaseResponse Update()
        {
            if (SelectedItem == null)
            {
                return new BaseResponse() { Code = ErrorCodes.NullParameter, Message = "更新的对象为空." };
            }

            try
            {
                var server = new CtsService();
                var response = server.UpdateProject(new UpdateProjectRequest()
                {
                    Id = SelectedItem.Id,
                    //TODO:
                    CategoryCode = SelectedItem.Category.Code,
                    Name = SelectedItem.Name,
                    Code = SelectedItem.Code,
                    Level = SelectedItem.Level.ToString(),
                    Details = SelectedItem.Details,
                    InvolvedClientIds = SelectedItem.InvolvedClients.Select(x => x.Id).ToList(),
                    OtherLitigant = SelectedItem.OtherLitigant,
                    InterestedParty = SelectedItem.InterestedParty,
                    DealDate = SelectedItem.DealDate,

                    Account = SelectedItem.Account,

                });
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new BaseResponse() { Code = ErrorCodes.UnknownError, Message = "Unknown Error" };
            }

        }

        public override BaseResponse Delete()
        {
            if (SelectedItem == null)
            {
                return new BaseResponse() { Code = ErrorCodes.NullParameter, Message = "删除的对象为空." };
            }

            try
            {
                var server = new CtsService();
                var response = server.RemoveProject(new RemoveProjectRequest()
                {
                    Ids = new int[] { SelectedItem.Id },

                });
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new BaseResponse() { Code = ErrorCodes.UnknownError, Message = "Unknown Error" };
            }

        }

        public override async void ExecuteNewCommandAsync(object o)
        {
            //let's set up a little MVVM, cos that's what the cool kids are doing:
            var view = new NewEditProject()
            {
                DataContext = this,
            };

            //show the dialog
            var result = await DialogHost.Show(view, "RootDialog", ExtendedOpenedEventHandler, ExtendedClosingEventHandler);
        }
        public override async void ExecuteEditCommandAsync(object o)
        {
            var Project = ((Button)o).DataContext as Project;
            this.SelectedItem = Project;
            var view = new NewEditProject(Project)
            {
                DataContext = this,
            };

            //show the dialog
            var result = await DialogHost.Show(view, "RootDialog", ExtendedOpenedEventHandler, ExtendedClosingEventHandler);
        }
        public override void ExecuteDeleteCommand(object o)
        {
            var Project = ((Button)o).DataContext as Project;
            this.SelectedItem = Project;
            Delete();
            SelectedItem = null;
            Query();
        }
        public override void DeleteItem(object sender, DialogClosingEventArgs eventArgs)
        {

            if (!Equals(eventArgs.Parameter, true))
            {
                return;
            }

            if (eventArgs.Session.Content != null && ((FrameworkElement)eventArgs.Session.Content).DataContext != null)
            {
                var Project = ((FrameworkElement)eventArgs.Session.Content).DataContext as Project;

                SelectedItem = Project;
                Delete();
                Query();
            }

        }


        private void ExtendedOpenedEventHandler(object sender, DialogOpenedEventArgs eventargs)
        {
            Console.WriteLine("You could intercept the open and affect the dialog using eventArgs.Session.");
        }

        private void ExtendedClosingEventHandler(object sender, DialogClosingEventArgs eventArgs)
        {
            if ((bool)eventArgs.Parameter == false)
            {
                return;
            }

            //note, you can also grab the session when the dialog opens via the DialogOpenedEventHandler
            if (eventArgs.Session.Content is NewEditProject)
            {
                var content = eventArgs.Session.Content as NewEditProject;
                if (content.IsNew)
                {
                    SelectedItem = content.TreatedObject;
                    Task.Run(() => Create())
                        .ContinueWith((t) => Query(), TaskContinuationOptions.OnlyOnRanToCompletion)
                        .ContinueWith((t, _) => eventArgs.Session.Close(false), null, TaskScheduler.FromCurrentSynchronizationContext());
                }
                else
                {
                    SelectedItem = content.TreatedObject;
                    Task.Run(() => Update())
                        .ContinueWith((t) => Query(), TaskContinuationOptions.OnlyOnRanToCompletion)
                        .ContinueWith((t, _) => eventArgs.Session.Close(false), null, TaskScheduler.FromCurrentSynchronizationContext());
                }

            }

            //OK, lets cancel the close...
            eventArgs.Cancel();
            //...now, lets update the "session" with some new content!
            eventArgs.Session.UpdateContent(new ProgressDialog());
        }
    }
}
