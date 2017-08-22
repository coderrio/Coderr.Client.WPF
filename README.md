Integration library for WPF applications
==========================

This library will detect all unhandled exceptions in WPF-based applications and report them to your OneTrueError server (or your account at https://onetrueerror.com).

To report exceptions manually, use `OneTrue.Report(exception)` to allow OneTrueError to include context data.

# Context collections

This library includes the following context collections for every reported exceptions:

* All in the [core library](https://github.com/onetrueerror/onetrueerror.client)
* Information about all open windows

# Getting started

1. Download and install the [OneTrueError server](https://github.com/onetrueerror/onetrueerror.server) or create an account at [OneTrueError.com](https://onetrueerror.com)
2. Install this client library (using nuget `onetrueerror.client.wpf`)
3. Configure the credentials from your OneTrueError account in your `App.xaml.cs`
4. Add `OneTrue.Configuration.CatchWpfExceptions()` in your `App.xaml.cs`

```csharp
public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        var url = new Uri("http://localhost/onetrueerror/");
        OneTrue.Configuration.Credentials(url, "yourAppKey", "yourSharedSecret");
        OneTrue.Configuration.CatchWpfExceptions();
		
		//extra config options
        OneTrue.Configuration.UserInteraction.AskUserForDetails = true;
        OneTrue.Configuration.UserInteraction.AskUserForPermission = true;
        OneTrue.Configuration.UserInteraction.AskForEmailAddress = true;
		
        base.OnStartup(e);
    }
}
```

Done.