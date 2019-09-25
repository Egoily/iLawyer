using ee.Framework.Schema;
using ee.iLawyer.Domain;
using MaterialDesignThemes.Wpf;
using System.Windows.Input;

namespace ee.iLawyer.ViewModels
{
    public abstract class AbstractViewModel
    {
        public virtual ICommand QueryCommand => new CommandImpl(ExecuteQueryCommand);
        public virtual ICommand NewCommand => new CommandImpl(ExecuteNewCommandAsync);
        public virtual ICommand EditCommand => new CommandImpl(ExecuteEditCommandAsync);
        public virtual ICommand DeleteCommand => new CommandImpl(ExecuteDeleteCommand);

        public AbstractViewModel()
        {
        }


        public abstract void Query();
        public abstract BaseResponse Create();
        public abstract BaseResponse Update();
        public abstract BaseResponse Delete();

        public abstract void ExecuteNewCommandAsync(object o);
        public abstract void ExecuteEditCommandAsync(object o);
        public abstract void ExecuteDeleteCommand(object o);

        public abstract void DeleteItem(object sender, DialogClosingEventArgs eventArgs);

        public virtual void ExecuteQueryCommand(object o)
        {
            Query();
        }


    }
}
