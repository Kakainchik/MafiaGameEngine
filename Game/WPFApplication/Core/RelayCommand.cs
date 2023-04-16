using System;
using System.Windows.Input;

#pragma warning disable CS8625

namespace WPFApplication.Core
{
    public class RelayCommand : ICommand
    {
        #region ImplementInterface

        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object? parameter)
        {
            return this.canExecute == null || canExecute(parameter);
        }

        public void Execute(object? parameter)
        {
            this.execute(parameter);
        }

        #endregion

        private Action<object?> execute;
        private Func<object?, bool> canExecute;

        public RelayCommand(Action<object?> execute, Func<object?, bool> canExecute = null)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }
    }
}