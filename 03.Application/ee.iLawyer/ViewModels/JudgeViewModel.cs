using ee.Framework;
using ee.Framework.Schema;
using ee.iLawyer.Modules;
using ee.iLawyer.Ops;
using ee.iLawyer.Ops.Contact.Args;
using ee.iLawyer.Ops.Contact.DTO;
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
    public class JudgeViewModel : AbstractViewModel
    {

        public ObservableCollection<Judge> Judges { get; protected set; }

        public Judge SelectedItem { get; set; }

        public ObservableCollection<Court> Courts { get; protected set; }


        public JudgeViewModel()
        {
            Courts = Cacher.Courts;
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
                        var response = server.QueryJudge(new QueryJudgeRequest());
                        if (response.Code == ErrorCodes.Ok && response.QueryList != null)
                        {
                            Judges = new ObservableCollection<Judge>();
                            response.QueryList.ToList().ForEach(x => Judges.Add(x));
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
                var response = server.CreateJudge(new CreateJudgeRequest()
                {
                    Name = SelectedItem.Name,
                    Gender = SelectedItem.Gender,
                    InCourtId = SelectedItem.InCourtId,
                    Grade = SelectedItem.Grade,
                    Duty = SelectedItem.Duty,
                    ContactNo = SelectedItem.ContactNo,

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
                var response = server.UpdateJudge(new UpdateJudgeRequest()
                {
                    Id = SelectedItem.Id,
                    Name = SelectedItem.Name,
                    Gender = SelectedItem.Gender,
                    InCourtId = SelectedItem.InCourtId,
                    Grade = SelectedItem.Grade,
                    Duty = SelectedItem.Duty,
                    ContactNo = SelectedItem.ContactNo,
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
                var response = server.RemoveJudge(new RemoveJudgeRequest()
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


        public override void ExecuteQueryCommand(object o)
        {
            Courts = Cacher.Courts;
            Query();
        }
        public override async void ExecuteNewCommandAsync(object o)
        {
            Courts = Cacher.Courts;
            //let's set up a little MVVM, cos that's what the cool kids are doing:
            var view = new NewEditJudge()
            {
                DataContext = this,
            };

            //show the dialog
            var result = await DialogHost.Show(view, "RootDialog", ExtendedOpenedEventHandler, ExtendedClosingEventHandler);
        }
        public override async void ExecuteEditCommandAsync(object o)
        {
            Courts = Cacher.Courts;
            var judge = ((Button)o).DataContext as Judge;
            this.SelectedItem = judge;
            var view = new NewEditJudge(judge)
            {
                DataContext = this,
            };

            //show the dialog
            var result = await DialogHost.Show(view, "RootDialog", ExtendedOpenedEventHandler, ExtendedClosingEventHandler);
        }
        public override void ExecuteDeleteCommand(object o)
        {
            var judge = ((Button)o).DataContext as Judge;
            this.SelectedItem = judge;
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
                var judge = ((FrameworkElement)eventArgs.Session.Content).DataContext as Judge;

                SelectedItem = judge;
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
            if (eventArgs.Session.Content is NewEditJudge)
            {
                var content = eventArgs.Session.Content as NewEditJudge;
                SelectedItem = content.TreatedObject;
                if (content.IsNew)
                {
                    var task = new Task<BaseResponse>(Create);
                    task.Start();

                    var taskResult = task.Result;

                    if (taskResult != null && taskResult.Code == ErrorCodes.Ok)
                    {
                        task.ContinueWith((t) => { Query(); Cacher.UpdateCourts(); }, TaskContinuationOptions.OnlyOnRanToCompletion)
                        .ContinueWith((t, _) => eventArgs.Session.Close(false), null, TaskScheduler.FromCurrentSynchronizationContext());
                    }
                }
                else
                {
                    var task = new Task<BaseResponse>(Update);
                    task.Start();

                    var taskResult = task.Result;

                    if (taskResult != null && taskResult.Code == ErrorCodes.Ok)
                    {
                        task.ContinueWith((t) => { Query(); Cacher.UpdateCourts(); }, TaskContinuationOptions.OnlyOnRanToCompletion)
                        .ContinueWith((t, _) => eventArgs.Session.Close(false), null, TaskScheduler.FromCurrentSynchronizationContext());
                    }
                }
            }

            eventArgs.Cancel();

            //TODO:Show message here


        }

    }
}
