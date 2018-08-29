using System;
using Coderr.Client.Config;
using Coderr.Client.Wpf.ContextProviders;

// Keeps in the root namespace to get intellisense
// ReSharper disable once CheckNamespace
namespace Coderr.Client.Wpf
{
    /// <summary>
    ///     Use <c>Err.Configuration.CatchWpfExceptions()</c> to get started.
    /// </summary>
    public static class ConfigExtensions
    {
        /// <summary>
        ///     Catch all uncaught wpf exceptions.
        /// </summary>
        /// <param name="configurator">codeRR configurator (accessed through <see cref="Err.Configuration" />).</param>
        public static void CatchWpfExceptions(this CoderrConfiguration configurator)
        {
            if (configurator == null) throw new ArgumentNullException("configurator");
            Err.Configuration.ContextProviders.Add(new OpenWindowsCollector());
            Err.Configuration.ContextProviders.Add(new ViewModelProvider());
            MarkAsHandled = true;
            WpfErrorReporter.Activate();
        }

        public static void TakeScreenshots(this CoderrConfiguration configurator)
        {
            Err.Configuration.ContextProviders.Add(new ScreenshotCollector());
        }

        /// <summary>
        /// Will NOT tell WPF that the exception is handled, i.e. will crash the application.
        /// </summary>
        public static void DoNotMarkExceptionsAsHandled(this CoderrConfiguration configurator)
        {
            MarkAsHandled = false;
        }

        internal static bool MarkAsHandled { get; set; }
    }
}
