using System;
using System.ComponentModel;

namespace Coderr.Client.Wpf.Presenters
{
    /// <summary>
    ///     Displays the error message in a window.
    /// </summary>
    public class ErrorMessagePresenter : INotifyPropertyChanged
    {
        private string _exceptionMessage;

        /// <summary>
        ///     Creates a new instance of the <see cref="ErrorMessagePresenter" /> class.
        /// </summary>
        /// <param name="exceptionMessage">Exception message</param>
        public ErrorMessagePresenter(string exceptionMessage)
        {
            ExceptionMessage = exceptionMessage ?? throw new ArgumentNullException(nameof(exceptionMessage));
        }

        /// <summary>
        ///     Exception message
        /// </summary>
        public string ExceptionMessage
        {
            get => _exceptionMessage;
            set
            {
                _exceptionMessage = value;
                OnPropertyChanged("ExceptionMessage");
            }
        }

        /// <inheritdoc />
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Property changed trigger.
        /// </summary>
        /// <param name="propertyName"></param>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (propertyName == null)
            {
                throw new ArgumentNullException(nameof(propertyName));
            }

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}