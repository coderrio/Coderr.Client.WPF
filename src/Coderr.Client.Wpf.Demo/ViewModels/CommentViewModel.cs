using System;
using System.Windows.Input;
using codeRR.Client.Wpf.Demo.Helpers;

namespace codeRR.Client.Wpf.Demo.ViewModels
{
    public class CommentViewModel : ObservableObject
    {
        private string _comment = "Hello world";
        private string _emailAddress = "hello@coderrapp.com";
        private string _name = "jgauffin";


        public string Comment
        {
            get => _comment;
            set
            {
                _comment = value;
                RaisePropertyChangedEvent("Comment");
            }
        }

        public string EmailAddress
        {
            get => _emailAddress;
            set
            {
                _emailAddress = value;
                RaisePropertyChangedEvent("EmailAddress");
            }
        }

        public string UserName
        {
            get => _name;
            set
            {
                _name = value;
                RaisePropertyChangedEvent("UserName");
            }
        }

        public ICommand SendCommentCommand => new ActionCommand(SendComment);

        private void SendComment()
        {
            // Press F5 if Visual Studio breaks here ("First-chance exceptions")
            throw new Exception("Demo exception");
        }
    }
}