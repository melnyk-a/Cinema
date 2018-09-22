using System;
using System.Windows.Input;

namespace Cinema.Utilities.Wpf.Commands
{
    public abstract class Command : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public virtual bool CanExecute() => true;

        public abstract void Execute();

        protected virtual void OnCanExecuteChanged(EventArgs e)
        {
            CanExecuteChanged?.Invoke(this, e);
        }

        bool ICommand.CanExecute(object parameter)
        {
            return CanExecute();
        }

        void ICommand.Execute(object parameter)
        {
            Execute();
        }
    }
}