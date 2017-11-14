using System;
using System.Windows.Input;

namespace codeRR.Client.Wpf.Utils
{
    public class ActionCommand : ICommand
    {
        private readonly Action _action;

        public ActionCommand(Action action)
        {
            _action = action;
            CanExecuteFlag = true;
        }

        public bool CanExecuteFlag { get; set; }

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