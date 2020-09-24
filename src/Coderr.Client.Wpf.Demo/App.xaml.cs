using System;
using System.IO;
using System.Windows;
using Coderr.Client.Wpf.Demo.Helpers;
using log4net.Config;

namespace Coderr.Client.Wpf.Demo
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            ConfigureLogging();

            var url = new Uri("http://localhost:60473/");
            Err.Configuration.Credentials(url,
                "1a68bc3e123c48a3887877561b0982e2",
                "bd73436e965c4f3bb0578f57c21fde69");

            Err.Configuration.CatchWpfExceptions();
            Err.Configuration.TakeScreenshots();
            Err.Configuration.QueueReports = true;
            //Err.Configuration.DoNotMarkExceptionsAsHandled();

            Err.Configuration.UserInteraction.AskUserForDetails = true;
            Err.Configuration.UserInteraction.AskUserForPermission = true;
            Err.Configuration.UserInteraction.AskForEmailAddress = true;

            base.OnStartup(e);
        }

        private static void ConfigureLogging()
        {
            CoderrTraceListener.Activate();
            XmlConfigurator.Configure(new FileInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                "log4net.config")));
            Err.Configuration.CatchLog4NetExceptions();
        }
    }
}
