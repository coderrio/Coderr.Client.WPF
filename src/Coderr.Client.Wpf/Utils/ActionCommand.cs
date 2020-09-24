using System;
using System.Windows.Input;

namespace Coderr.Client.Wpf.Utils
{
    public class ActionCommand : ICommand
    {
        private readonly Action _action;
        private bool _canExecuteFlag;

        public ActionCommand(Action action)
        {
            _action = action;
            CanExecuteFlag = true;
        }

        public bool CanExecuteFlag
        {
            get => _canExecuteFlag;
            set
            {
                _canExecuteFlag = value;
                CanExecuteChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public bool CanExecute(object parameter)
        {
            return CanExecuteFlag;
        }

        public void Execute(object parameter)
        {
            _action();
        }

        /// <summary>
        /// Use the <see cref="CanExecuteFlag"/> to change internal state
        /// </summary>
        public event EventHandler CanExecuteChanged;
    }
}