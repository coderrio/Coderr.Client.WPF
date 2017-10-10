using System;
using System.Windows;
using System.Windows.Threading;
using codeRR.Client.Wpf.Contexts;
using codeRR.Client.Wpf.Utils;
using codeRR.Client;

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
        }

        private static void OnException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            var context = new WpfErrorReportContext(Instance, e.Exception);

            var dto = Err.GenerateReport(context);
            if (!Err.Configuration.UserInteraction.AskUserForPermission)
            {
                ActionWrapper.SafeActionExecution(() => Err.UploadReport(dto));
            }

            var ctx = new WindowFactoryContext {Context = context, Report = dto};
            var dialog = WindowFactory(ctx);
            dialog.ShowDialog();
            e.Handled = true;
        }

    }
}
