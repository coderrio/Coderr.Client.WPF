using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace OneTrueError.Client.Wpf.Demo
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        var url = new Uri("http://localhost/onetrueerror/");
        OneTrue.Configuration.Credentials(url, "yourAppKey", "yourSharedSecret");
        OneTrue.Configuration.CatchWpfExceptions();
        OneTrue.Configuration.UserInteraction.AskUserForDetails = true;
        OneTrue.Configuration.UserInteraction.AskUserForPermission = true;
        OneTrue.Configuration.UserInteraction.AskForEmailAddress = true;
        base.OnStartup(e);
    }
}
}
