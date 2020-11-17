using System;
using System.Windows.Input;
using Coderr.Client.Wpf.Demo.Helpers;
using log4net;

namespace Coderr.Client.Wpf.Demo.ViewModels
{
    public class CommentViewModel : ObservableObject
    {
        private string _comment = "Hello world";
        private string _emailAddress = "hello@coderrapp.com";
        private readonly ILog _logger = LogManager.GetLogger(typeof(CommentViewModel));
        private string _name = "jgauffin";

        public string Comment
        {
            get => _comment;
            set
            {
                _comment = value;
                _logger.Debug("Changed Comment to " + value);
                RaisePropertyChangedEvent();
            }
        }

        public string EmailAddress
        {
            get => _emailAddress;
            set
            {
                _emailAddress = value;
                _logger.Debug("Changed EmailAddress to " + value);
                RaisePropertyChangedEvent();
            }
        }

        public ICommand SendCommentCommand => new ActionCommand(SendComment);

        public string UserName
        {
            get => _name;
            set
            {
                _name = value;
                _logger.Debug("Changed UserName to " + value);
                RaisePropertyChangedEvent();
            }
        }

        private void SendComment()
        {
            // Press F5 if Visual Studio breaks here ("First-chance exceptions")
            throw new Exception("Demo exception");
        }
    }
}