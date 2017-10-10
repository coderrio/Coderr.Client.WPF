using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
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
            // configuring the connection to the codeRR server.
            var uri = new Uri("http://localhost:50473/");
            Err.Configuration.Credentials(uri,
                "5f219f356daa40b3b31dfc67514df6d6",
                "22612e4444f347d1bb3d841d64c9750a");

            // for automated handling
            Err.Configuration.CatchWpfExceptions();

            // configuring the error window
            Err.Configuration.UserInteraction.AskUserForDetails = true;
            Err.Configuration.UserInteraction.AskUserForPermission = true;
            Err.Configuration.UserInteraction.AskForEmailAddress = true;

            base.OnStartup(e);
        }
    }
}
