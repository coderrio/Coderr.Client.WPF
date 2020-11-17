using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;
using System.Windows.Threading;
using Coderr.Client.Contracts;
using Coderr.Client.Wpf.Contexts;
using Coderr.Client.Wpf.Utils;

// ReSharper disable UseStringInterpolation

namespace Coderr.Client.Wpf
{
    /// <summary>
    /// Main reporter class for the WPF library.
    /// </summary>
    public class WpfErrorReporter
    {
        private static readonly WpfErrorReporter Instance = new WpfErrorReporter();
        private static bool _activated;

        static WpfErrorReporter()
        {
            WindowFactory =
                model =>
                {
                    var exceptionMessage = model.Context.Exception.Message;
                    return new ReportDialog(model.Report, exceptionMessage);
                };
        }

        internal static Func<WindowFactoryContext, Window> WindowFactory { get; set; }

        /// <summary>
        /// Report exceptions which will terminate the application.
        /// </summary>
        public static bool ReportUnhandledAppDomainExceptions { get; set; }

        /// <summary>
        /// Report exceptions from .NET Tasks (running in non-UI threads).
        /// </summary>
        public static bool ReportUnobservedTaskExceptions { get; set; }

        /// <summary>
        /// Mark exceptions in background tasks as handled (to prevent the application from crashing).
        /// </summary>
        public static bool SetUnobservedTaskExceptionsAsHandled { get; set; }

        /// <summary>
        ///     Activate this library.
        /// </summary>
        public static void Activate()
        {
            if (_activated)
                return;
            _activated = true;

            Application.Current.DispatcherUnhandledException += OnException;
            Application.Current.NavigationFailed += OnNavigationFailed;
            if(ReportUnhandledAppDomainExceptions)
                AppDomain.CurrentDomain.UnhandledException += OnAppDomainException;

            if (ReportUnobservedTaskExceptions)
            {
                TaskScheduler.UnobservedTaskException += (s, e) =>
                {
                    Err.Report(e.Exception, new { ErrTags = "UnobservedTaskException,unhandled-exception" });
                    if (SetUnobservedTaskExceptionsAsHandled)
                        e.SetObserved();
                };
            }
           

        }

        static void OnAppDomainException(object sender, UnhandledExceptionEventArgs args)
        {
            Exception e = (Exception)args.ExceptionObject;
            Err.Report(e, new {ErrTags = "unhandled-exception"});
        }


        private static void OnException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            var context = new WpfErrorReportContext(Instance, e.Exception, Application.Current);
            if (ConfigExtensions.MarkAsHandled)
                e.Handled = true;

            var dto = Err.GenerateReport(context);
            if (!Err.Configuration.UserInteraction.AskUserForPermission)
                ActionWrapper.SafeActionExecution(() => Err.UploadReport(dto));

            var ctx = new WindowFactoryContext { Context = context, Report = dto };
            var dialog = WindowFactory(ctx);
            dialog.ShowDialog();
        }

        private static void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            var context = new WpfErrorReportContext(Instance, e.Exception, Application.Current);
            var collection = new { e.Uri, e.ExtraData, e.Navigator }.ToContextCollection("NavigationData");

            var report = Err.GenerateReport(context);
            var cols = new List<ContextCollectionDTO>(report.ContextCollections) { collection };
            report.ContextCollections = cols.ToArray();
            Err.UploadReport(report);
        }
    }
}