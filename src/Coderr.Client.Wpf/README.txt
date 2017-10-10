WPF integration package
=======================

Welcome to codeRR! 

We try to answer questions as fast as we can at our forum: http://discuss.coderrapp.com. 
If you have any trouble at all, don't hesitate to post a message there.

This library is the client library of codeRR. What it does is to pick up unhandled exceptions
in WPF.

However, this library do not process the information but require a codeRR server for that.
You can either install the open source server from https://github.com/coderrapp/coderr.server, or
use our hosted service at https://coderrapp.com/live.


Configuration
=============

To start with, you need to configure the connection to the codeRR server, 
this code is typically added in your App.xaml.cs. The settings can be found either
in our hosted service or in your installed codeRR server.

    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {

            // codeRR configuration
            var uri = new Uri("https://report.coderrapp.com/");
            Err.Configuration.Credentials(uri,
                "yourAppKey",
                "yourSharedSecret");

            // to catch unhandled exceptions
            Err.Configuration.CatchWpfExceptions();

            // different types of configuration options
            Err.Configuration.UserInteraction.AskUserForDetails = true;
            Err.Configuration.UserInteraction.AskUserForPermission = true;
            Err.Configuration.UserInteraction.AskForEmailAddress = true;


            base.OnStartup(e);
        }
    }


Reporting exceptions
====================

All unhandled exceptions are reported directly by this library. However, sometimes you'll want to use try/catch
for some custom handling (or being able to display pretty error messages).

When doing so, simply report the exception like this:

    public void OnPostClick()
    {
        var model = CreatePostDto();

        try
        {
            _somService.Execute(model);
        }
        catch (Exception ex)
        {
            Err.Report(ex, model);

            //some custom handling
        }
    }


Questions?     http://discuss.coderrapp.com
Github:        https://github.com/coderrapp/coderr.client.wpf
Documentation: https://coderrapp.com/documentation/client/libraries/wpf/
