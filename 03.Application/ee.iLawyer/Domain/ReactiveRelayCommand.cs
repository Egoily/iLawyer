﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.Diagnostics;
using System.Reactive.Subjects;


namespace ee.iLawyer.Domain
{
    public class ReactiveRelayCommand : ICommand
    {
        private readonly Action<object> execute;
        private readonly Predicate<object> canExecute;

        public ReactiveRelayCommand(Action<object> execute)
            : this(execute, null)
        {
        }

        public ReactiveRelayCommand(Action<object> execute, Predicate<object> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");

            this.execute = execute;
            this.canExecute = canExecute;
        }

        [DebuggerStepThrough]
        public bool CanExecute(object parameter)
        {
            return canExecute == null || canExecute(parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void Execute(object parameter)
        {
            execute(parameter);
            executed.OnNext(parameter);
        }

        private readonly Subject<object> executed = new Subject<object>();

        public IObservable<object> Executed
        {
            get { return executed; }
        }
    }
}
