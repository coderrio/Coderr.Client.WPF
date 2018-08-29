using System;
using System.Windows;
using Coderr.Client;
using Coderr.Client.Wpf;

namespace codeRR.Client.Wpf.Demo
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            var url = new Uri("http://localhost/coderr.oss/");
            Err.Configuration.Credentials(url, 
                "fda942de02b645db9d6788300fa56655", 
                "5d8691c60768457e965502a833f2257d");

            Err.Configuration.CatchWpfExceptions();
            Err.Configuration.TakeScreenshots();
            //Err.Configuration.DoNotMarkExceptionsAsHandled();

            Err.Configuration.UserInteraction.AskUserForDetails = true;
            Err.Configuration.UserInteraction.AskUserForPermission = true;
            Err.Configuration.UserInteraction.AskForEmailAddress = true;

            base.OnStartup(e);
        }
    }
}
