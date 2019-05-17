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
    public class CourtViewModel : AbstractViewModel
    {
        public ObservableCollection<Court> Courts { get; set; }

        public Court SelectedItem { get; set; } = new Court();


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
                        var response = server.QueryCourt(new QueryCourtRequest());
                        if (response.Code == ErrorCodes.Ok && response.QueryList != null)
                        {
                            Courts = new ObservableCollection<Court>();
                            response.QueryList.ToList().ForEach(x => Courts.Add(x));
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
                var response = server.CreateCourt(new CreateCourtRequest()
                {
                    Rank = SelectedItem.Rank.ToString(),
                    Name = SelectedItem.Name,
                    Province = SelectedItem.Province,
                    City = SelectedItem.City,
                    County = SelectedItem.County,
                    Address = SelectedItem.Address,
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
                var response = server.UpdateCourt(new UpdateCourtRequest()
                {
                    Id = SelectedItem.Id,
                    Rank = SelectedItem.Rank.ToString(),
                    Name = SelectedItem.Name,
                    Province = SelectedItem.Province,
                    City = SelectedItem.City,
                    County = SelectedItem.County,
                    Address = SelectedItem.Address,
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
                var response = server.RemoveCourt(new RemoveCourtRequest()
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
            var view = new NewEditCourt()
            {
                DataContext = this,
            };

            //show the dialog
            var result = await DialogHost.Show(view, "RootDialog", ExtendedOpenedEventHandler, ExtendedClosingEventHandler);
        }
        public override async void ExecuteEditCommandAsync(object o)
        {
            var court = ((Button)o).DataContext as Court;
            this.SelectedItem = court;
            var view = new NewEditCourt(court)
            {
                DataContext = this,
            };

            //show the dialog
            var result = await DialogHost.Show(view, "RootDialog", ExtendedOpenedEventHandler, ExtendedClosingEventHandler);
        }
        public override void ExecuteDeleteCommand(object o)
        {
            var court = ((Button)o).DataContext as Court;
            this.SelectedItem = court;
            Delete();
            SelectedItem = null;
            Query();
            Cacher.UpdateCourts();
        }

        public override void DeleteItem(object sender, DialogClosingEventArgs eventArgs)
        {

            if (!Equals(eventArgs.Parameter, true))
            {
                return;
            }

            if (eventArgs.Session.Content != null && ((FrameworkElement)eventArgs.Session.Content).DataContext != null)
            {
                var court = ((FrameworkElement)eventArgs.Session.Content).DataContext as Court;

                SelectedItem = court;
                Delete();
                Query();
                Cacher.UpdateCourts();
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
            if (eventArgs.Session.Content is NewEditCourt)
            {
                var content = eventArgs.Session.Content as NewEditCourt;
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
