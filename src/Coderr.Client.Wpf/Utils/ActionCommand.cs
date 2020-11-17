using System;
using System.Windows.Input;

namespace Coderr.Client.Wpf.Utils
{
    /// <summary>
    /// Delagate command
    /// </summary>
    public class ActionCommand : ICommand
    {
        private readonly Action _action;
        private bool _canExecuteFlag;

        /// <summary>
        /// Some constructor
        /// </summary>
        /// <param name="action">.. that want some action ...</param>
        public ActionCommand(Action action)
        {
            _action = action;
            CanExecuteFlag = true;
        }

        /// <summary>
        /// May I?
        /// </summary>
        public bool CanExecuteFlag
        {
            get => _canExecuteFlag;
            set
            {
                _canExecuteFlag = value;
                CanExecuteChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Can I?
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public bool CanExecute(object parameter)
        {
            return CanExecuteFlag;
        }

        /// <summary>
        /// I will!
        /// </summary>
        /// <param name="parameter"></param>
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