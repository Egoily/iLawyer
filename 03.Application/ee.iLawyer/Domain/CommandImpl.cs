﻿using System;
using System.Windows.Input;

namespace ee.iLawyer.Domain
{
    public class CommandImpl : ICommand
    {
        private readonly Action<object> _execute;
        private readonly Func<object, bool> _canExecute;

        public CommandImpl(Action<object> execute) : this(execute, null)
        {
        }

        public CommandImpl(Action<object> execute, Func<object, bool> canExecute)
        {
            if (execute == null) throw new ArgumentNullException(nameof(execute));

            _execute = execute;
            _canExecute = canExecute ?? (x => true);
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            _execute(parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }

        public void Refresh()
        {
            CommandManager.InvalidateRequerySuggested();
        }
    }
}
