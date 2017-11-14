using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;
using System.Windows.Threading;
using codeRR.Client.Contracts;
using codeRR.Client.Wpf.Contexts;
using codeRR.Client.Wpf.Utils;

// ReSharper disable UseStringInterpolation

namespace codeRR.Client.Wpf
{
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
        ///     Activate this library.
        /// </summary>
        public static void Activate()
        {
            if (_activated)
                return;
            _activated = true;

            Application.Current.DispatcherUnhandledException += OnException;
            Application.Current.NavigationFailed += OnNavigationFailed;

            AppDomain.CurrentDomain.UnhandledException += OnAppDomainException;

            TaskScheduler.UnobservedTaskException += (s, e) =>
            {
                Err.Report(e.Exception, new{ErrTags= "UnobservedTaskException,unhandled-exception" });
                if (ConfigExtensions.MarkAsHandled)
                    e.SetObserved();
            };

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