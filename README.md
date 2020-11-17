Integration library WPF
=======================

[![VSTS](https://1tcompany.visualstudio.com/_apis/public/build/definitions/75570083-b1ef-4e78-88e2-5db4982f756c/17/badge)]() [![NuGet](https://img.shields.io/nuget/dt/Coderr.Client.Wpf.svg?style=flat-square)]()

This library will detect all unhandled exceptions in WPF based applications and report them to the Coderr service (or your account at https://coderr.io).

For more information about Coderr, visit our [homepage](https://coderr.io).

# Installation

1. Download and install the [Coderr Community Server](https://github.com/coderrio/coderr.server), use our feaure complete [Coderr Premise Server](https://coderr.io/try/), or use our [Cloud Service](https://app.coderr.io) (free up 1000 error reports / month).
2. Install this client library (using nuget `coderr.client.wpf`)
3. Configure the credentials from your Coderr account in your `App.xaml.cs`.

```csharp
public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {

        // Coderr configuration
        var uri = new Uri("https://report.coderr.io/");
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
```

# Getting started

Simply catch an exception and report it:

```csharp
public void UpdatePost(int uid, ForumPost post)
{
    try
    {
        _service.Update(uid, post);
    }
    catch (Exception ex)
    {
        Err.Report(ex, new{ UserId = uid, ForumPost = post });
    }
}
```

The context information will be attached as:

![](https://coderr.io/images/features/custom-context.png)

[Read more...](https://coderr.io/features/)

# More information

* [Questions/Help](http://discuss.coderr.io)
* [Documentation](https://coderr.io/documentation/client/libraries/wpf/)

