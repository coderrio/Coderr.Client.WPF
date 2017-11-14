using System;
using System.Windows.Input;

namespace codeRR.Client.Wpf.Demo.Helpers
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

        public virtual bool CanExecute(object parameter)
        {
            return CanExecuteFlag;
        }

        public void Execute(object parameter)
        {
            _action();
        }

        protected void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Use the <see cref="CanExecuteFlag"/> to change internal state
        /// </summary>
        public event EventHandler CanExecuteChanged;
    }
}