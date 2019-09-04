using ee.Framework;
using ee.Framework.Schema;
using ee.iLawyer.Modules;
using ee.iLawyer.Ops.Contact.Args;
using ee.iLawyer.Ops.Contact.DTO;
using ee.iLawyer.UserControls;
using ee.iLawyer.WebApi.Invoker;
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
    public class ClientViewModel : AbstractViewModel
    {

        public ObservableCollection<Client> Clients { get; protected set; }
        public Client SelectedItem { get; set; }


        public ClientViewModel()
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
                        var server = new ILawyerServiceWebApi();
                        var response = server.QueryClient(new QueryClientRequest());
                        if (response.Code == ErrorCodes.Ok && response.QueryList != null)
                        {
                            Clients = new ObservableCollection<Client>();
                            response.QueryList.ToList().ForEach(x => Clients.Add(x));
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
                var server = new ILawyerServiceWebApi();
                var response = server.CreateClient(new CreateClientRequest()
                {
                    Name = SelectedItem.Name,
                    IsNP = SelectedItem.IsNP,
                    Properties = SelectedItem.Properties,
                    Abbreviation = SelectedItem.Abbreviation,
                    Impression = SelectedItem.Impression,

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
                var server = new ILawyerServiceWebApi();
                var response = server.UpdateClient(new UpdateClientRequest()
                {
                    Id = SelectedItem.Id,
                    IsNP = SelectedItem.IsNP,
                    Name = SelectedItem.Name,
                    ContactNo = SelectedItem.ContactNo,
                    Properties = SelectedItem.Properties,
                    Abbreviation = SelectedItem.Abbreviation,
                    Impression = SelectedItem.Impression,

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
                var server = new ILawyerServiceWebApi();
                var response = server.RemoveClient(new RemoveClientRequest()
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
            var view = new NewEditClient()
            {
                DataContext = this,
            };

            //show the dialog
            var result = await DialogHost.Show(view, "RootDialog", ExtendedOpenedEventHandler, ExtendedClosingEventHandler);
        }
        public override async void ExecuteEditCommandAsync(object o)
        {
            var Client = ((Button)o).DataContext as Client;
            this.SelectedItem = Client;
            var view = new NewEditClient(Client)
            {
                DataContext = this,
            };

            //show the dialog
            var result = await DialogHost.Show(view, "RootDialog", ExtendedOpenedEventHandler, ExtendedClosingEventHandler);
        }
        public override void ExecuteDeleteCommand(object o)
        {
            var Client = ((Button)o).DataContext as Client;
            this.SelectedItem = Client;
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
                var client = ((FrameworkElement)eventArgs.Session.Content).DataContext as Client;

                SelectedItem = client;
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
            if (eventArgs.Session.Content is NewEditClient)
            {
                var content = eventArgs.Session.Content as NewEditClient;
                SelectedItem = content.TreatedObject;
                if (content.IsNew)
                {
                    Task.Run(() => Create())
                        .ContinueWith((t) => Query(), TaskContinuationOptions.OnlyOnRanToCompletion)
                        .ContinueWith((t, _) => eventArgs.Session.Close(false), null, TaskScheduler.FromCurrentSynchronizationContext());
                }
                else
                {
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
