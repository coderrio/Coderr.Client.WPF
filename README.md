Integration library WPF
=======================

[![VSTS](https://1tcompany.visualstudio.com/_apis/public/build/definitions/75570083-b1ef-4e78-88e2-5db4982f756c/17/badge)]() [![NuGet](https://img.shields.io/nuget/dt/Coderr.Client.Wpf.svg?style=flat-square)]()

This library will detect all unhandled exceptions in WPF based applications and report them to the Coderr service (or your account at https://coderr.io).

For more information about Coderr, visit our [homepage](https://coderr.io).

# Reporting the first error

First, follow [this guide](https://coderr.io/documentation/getting-started/).

Then activate this library:

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

And finally try to report an error:

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

