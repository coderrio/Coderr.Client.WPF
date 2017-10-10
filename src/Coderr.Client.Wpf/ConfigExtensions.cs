// Keeps in the root namespace to get IntelliSense
// ReSharper disable CheckNamespace

using System;
using codeRR.Client.Config;
using codeRR.Client.Wpf;
using codeRR.Client.Wpf.ContextProviders;

namespace codeRR.Client
{
    /// <summary>
    ///     Use <c>Err.Configuration.CatchWpfExceptions()</c> to get started.
    /// </summary>
    public static class ConfigExtensions
    {
        /// <summary>
        ///     Catch all uncaught WPF exceptions.
        /// </summary>
        /// <param name="configurator">codeRR configurator (accessed through <see cref="Err.Configuration" />).</param>
        public static void CatchWpfExceptions(this CoderrConfiguration configurator)
        {
            if (configurator == null) throw new ArgumentNullException(nameof(configurator));
            WpfErrorReporter.Activate();
            Err.Configuration.ContextProviders.Add(new OpenWindowsCollector());
        }
    }
}
