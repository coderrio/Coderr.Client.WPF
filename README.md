Integration library WPF
=======================

[![VSTS](https://1tcompany.visualstudio.com/_apis/public/build/definitions/75570083-b1ef-4e78-88e2-5db4982f756c/17/badge)]() [![NuGet](https://img.shields.io/nuget/dt/codeRR.Client.Wpf.svg?style=flat-square)]()

This library will detect all unhandled exceptions in WPF based applications and report them to your codeRR server (or your account at https://coderrapp.com).

For more information about codeRR, visit our [homepage](https://coderrapp.com).

# Installation

1. Download and install the [codeRR server](https://github.com/coderrapp/coderr.server) or create an account at [coderrapp.com](https://coderrapp.com)
2. Install this client library (using nuget `coderr.client.wpf`)
3. Configure the credentials from your codeRR account in your `App.xaml.cs`.

```csharp
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

![](https://coderrapp.com/images/features/custom-context.png)

[Read more...](https://coderrapp.com/features/)

# Requirements

You need to either install [codeRR Community Server](https://github.com/coderrapp/coderr.server) or use [codeRR Live](https://coderrapp.com/live).

# More information

* [Questions/Help](http://discuss.coderrapp.com)
* [Documentation](https://coderrapp.com/documentation/client/libraries/wpf/)

