using System;
using System.IO;
using System.Windows;
using Coderr.Client.Wpf.Demo.Helpers;
using log4net;
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
                "3c15309ae70d45b194b08fdbc76b84ca",
                "8bd1be1762a8425592775e1a19c99812");
            
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
            var log = LogManager.GetLogger(typeof(App));
            Err.Configuration.CatchLog4NetExceptions();
        }
    }
}
