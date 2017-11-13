using System;
using System.Windows;

namespace codeRR.Client.Wpf.Demo
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            var url = new Uri("http://localhost:50473/");
            Err.Configuration.Credentials(url,
                "fda370d3a4444964b52d785a9b26fe21",
                "c3f786f9205c4572b5bbe4cfb81ba4f0");

            Err.Configuration.CatchWpfExceptions();
            Err.Configuration.MarkExceptionsAsHandled();
            Err.Configuration.TakeScreenshots();

            Err.Configuration.UserInteraction.AskUserForDetails = true;
            Err.Configuration.UserInteraction.AskUserForPermission = true;
            Err.Configuration.UserInteraction.AskForEmailAddress = true;

            base.OnStartup(e);
        }
    }
}
