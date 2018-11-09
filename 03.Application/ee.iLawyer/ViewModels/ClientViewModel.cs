using ee.Framework;
using ee.iLawyer.Domain;
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
using System.Windows.Input;

namespace ee.iLawyer.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class ClientViewModel
    {

        public ObservableCollection<Client> Clients { get; protected set; }

        public Client SelectedItem { get; set; }


        public ICommand QueryCommand => new CommandImpl(ExecuteQueryCommand);
        public ICommand NewCommand => new CommandImpl(ExecuteNewCommand);
        public ICommand EditCommand => new CommandImpl(ExecuteEditCommand);
        public ICommand DeleteCommand => new CommandImpl(ExecuteDeleteCommand);

        public ClientViewModel()
        {
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
                        var response = server.QueryClient(new QueryClientRequest());
                        if (response.Code == ErrorCodes.Ok && response.QueryList != null)
                        {
                            Clients = new ObservableCollection<Client>();
                            response.QueryList.ToList().ForEach(x => Clients.Add(new Client()
                            {
                                Id = x.Id,
                                Name = x.Name,
                                //TODO:
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
                var response = server.AddClient(new AddClientRequest()
                {
                    Name = SelectedItem.Name,

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
                var response = server.UpdateClient(new UpdateClientRequest()
                {
                    Id = SelectedItem.Id,
                    Name = SelectedItem.Name,

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
                var response = server.RemoveClient(new RemoveClientRequest()
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
            Query();
        }
        private async void ExecuteNewCommand(object o)
        {
            //let's set up a little MVVM, cos that's what the cool kids are doing:
            var view = new NewEditClientInfo()
            {
                DataContext = this,
            };

            //show the dialog
            var result = await DialogHost.Show(view, "RootDialog", ExtendedOpenedEventHandler, ExtendedClosingEventHandler);
        }
        private async void ExecuteEditCommand(object o)
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
        private void ExecuteDeleteCommand(object o)
        {
            var Client = ((Button)o).DataContext as Client;
            this.SelectedItem = Client;
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
            if (eventArgs.Session.Content is NewEditClient)
            {
                var content = eventArgs.Session.Content as NewEditClient;
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
                var client = ((FrameworkElement)eventArgs.Session.Content).DataContext as Client;

                SelectedItem = client;
                Delete();
                Query();
            }

        }
    }
}
