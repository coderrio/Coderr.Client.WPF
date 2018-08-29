Coderr WPF integration package
==============================

This library reports all unhandled exceptions in WPF to Coderr.
You also need to use a Coderr server.

https://coderr.io/documentation/getting-started/


Reporting exceptions manually
=============================

This is one of many examples:

    public void SomeMethod(PostViewModel model)
    {
        try
        {
            _somService.Execute(model);
        }
        catch (Exception ex)
        {
            Err.Report(ex, model);

            //some custom handling
        }

        // some other code here...
    }


Questions:
http://discuss.coderr.io

Guides and support
https://coderr.io/guides-and-support/
