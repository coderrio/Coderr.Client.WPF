using System;
using System.Windows.Input;

namespace OneTrueError.Client.Wpf.Presenters
{
    public class ActionCommand : ICommand
    {
        private readonly Action _what;
        private bool _executed;

        public ActionCommand(Action what)
        {
            _what = what;
        }
        public bool CanExecute(object parameter)
        {
            return !_executed;
        }

        public void Execute(object parameter)
        {
            _executed = true;
            _what();
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler CanExecuteChanged;
    }
}