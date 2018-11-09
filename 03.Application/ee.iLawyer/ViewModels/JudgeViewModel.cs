using PropertyChanged;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using ee.iLawyer.UserControls;
using ee.iLawyer.Modules;
using MaterialDesignThemes.Wpf;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;
using ee.iLawyer.Domain;
using ee.Framework;
using ee.iLawyer.Ops.Contact.DTO;
using ee.iLawyer.Ops.Contact.Args;
using ee.iLawyer.Ops;
namespace ee.iLawyer.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class JudgeViewModel
    {

        public ObservableCollection<Judge> Judges { get; protected set; }

        public Judge SelectedItem { get; set; }

        public ObservableCollection<Court> Courts { get; protected set; }

        public ICommand QueryCommand => new CommandImpl(ExecuteQueryCommand);
        public ICommand NewCommand => new CommandImpl(ExecuteNewCommand);
        public ICommand EditCommand => new CommandImpl(ExecuteEditCommand);
        public ICommand DeleteCommand => new CommandImpl(ExecuteDeleteCommand);

        public JudgeViewModel()
        {
            Courts = GlobalViewModel.Courts;
        }


        public void Query()
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
                        var response = server.QueryJudge(new QueryJudgeRequest());
                        if (response.Code == ErrorCodes.Ok && response.QueryList != null)
                        {
                            Judges = new ObservableCollection<Judge>();
                            response.QueryList.ToList().ForEach(x => Judges.Add(new Judge()
                            {
                                Id = x.Id,
                                Name = x.Name,
                                Gender = x.Gender,
                                InCourtId = x.InCourtId,
                                InCourtName = x.InCourtName,
                                PhoneNo = x.PhoneNo,
                            }));
                        }

                    }
                    catch (Exception ex)
                    {
                    }

                }, null);
            });
        }

        public BaseResponse Add()
        {
            if (SelectedItem == null) return new BaseResponse() { Code = ErrorCodes.NullParameter, Message = "新增的对象为空." };

            try
            {
                var server = new CtsService();
                var response = server.AddJudge(new AddJudgeRequest()
                {
                    Name = SelectedItem.Name,
                    Gender = SelectedItem.Gender,
                    InCourtId = SelectedItem.InCourtId,
                    PhoneNo = SelectedItem.PhoneNo,

                });
                return response;
            }
            catch (Exception ex)
            {
                return new BaseResponse() { Code = ErrorCodes.UnknownError, Message = "Unknown Error" };
            }

        }
        public BaseResponse Update()
        {
            if (SelectedItem == null) return new BaseResponse() { Code = ErrorCodes.NullParameter, Message = "更新的对象为空." };

            try
            {
                var server = new CtsService();
                var response = server.UpdateJudge(new UpdateJudgeRequest()
                {
                    Id = SelectedItem.Id,
                    Name = SelectedItem.Name,
                    Gender = SelectedItem.Gender,
                    InCourtId = SelectedItem.InCourtId,
                    PhoneNo = SelectedItem.PhoneNo,
                });
                return response;
            }
            catch (Exception ex)
            {
                return new BaseResponse() { Code = ErrorCodes.UnknownError, Message = "Unknown Error" };
            }

        }

        public BaseResponse Delete()
        {
            if (SelectedItem == null) return new BaseResponse() { Code = ErrorCodes.NullParameter, Message = "删除的对象为空." };

            try
            {
                var server = new CtsService();
                var response = server.RemoveJudge(new RemoveJudgeRequest()
                {
                    Ids = new int[] { SelectedItem.Id },

                });
                return response;
            }
            catch (Exception ex)
            {
                return new BaseResponse() { Code = ErrorCodes.UnknownError, Message = "Unknown Error" };
            }

        }


        private void ExecuteQueryCommand(object o)
        {
            Courts = GlobalViewModel.Courts;
            Query();
        }
        private async void ExecuteNewCommand(object o)
        {
            Courts = GlobalViewModel.Courts;
            //let's set up a little MVVM, cos that's what the cool kids are doing:
            var view = new NewEditJudge()
            {
                DataContext = this,
            };

            //show the dialog
            var result = await DialogHost.Show(view, "RootDialog", ExtendedOpenedEventHandler, ExtendedClosingEventHandler);
        }
        private async void ExecuteEditCommand(object o)
        {
            Courts = GlobalViewModel.Courts;
            var judge = ((Button)o).DataContext as Judge;
            this.SelectedItem = judge;
            var view = new NewEditJudge(judge)
            {
                DataContext = this,
            };

            //show the dialog
            var result = await DialogHost.Show(view, "RootDialog", ExtendedOpenedEventHandler, ExtendedClosingEventHandler);
        }
        private void ExecuteDeleteCommand(object o)
        {
            var judge = ((Button)o).DataContext as Judge;
            this.SelectedItem = judge;
            Delete();
            SelectedItem = null;
            Query();
        }
        private void ExtendedOpenedEventHandler(object sender, DialogOpenedEventArgs eventargs)
        {
            Console.WriteLine("You could intercept the open and affect the dialog using eventArgs.Session.");
        }

        private void ExtendedClosingEventHandler(object sender, DialogClosingEventArgs eventArgs)
        {
            if ((bool)eventArgs.Parameter == false) return;

            //note, you can also grab the session when the dialog opens via the DialogOpenedEventHandler
            if (eventArgs.Session.Content is NewEditJudge)
            {
                var content = eventArgs.Session.Content as NewEditJudge;
                if (content.IsNew)
                {
                    SelectedItem = content.TreatedObject;
                    Task.Run(() => Add())
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
        public void DeleteItem(object sender, DialogClosingEventArgs eventArgs)
        {

            if (!Equals(eventArgs.Parameter, true)) return;
            if (eventArgs.Session.Content != null && ((FrameworkElement)eventArgs.Session.Content).DataContext != null)
            {
                var judge = ((FrameworkElement)eventArgs.Session.Content).DataContext as Judge;

                SelectedItem = judge;
                Delete();
                Query();
            }

        }

    }
}
