using System;
using Coderr.Client.Contracts;
using Coderr.Client.Wpf.Utils;

namespace Coderr.Client.Wpf
{
    /// <summary>
    ///     Interaction logic for ReportDialog.xaml
    /// </summary>
    public partial class ReportDialog
    {
        /// <summary>
        ///     Creates a new instance of the <see cref="ReportDialog" /> class.
        /// </summary>
        /// <param name="dto">Report to send.</param>
        /// <param name="exceptionMessage">Error message to show.</param>
        public ReportDialog(ErrorReportDTO dto, string exceptionMessage)
        {
            ErrorReportDetailsProvider.DtoReport = dto ?? throw new ArgumentNullException(nameof(dto));
            ErrorReportDetailsProvider.ExceptionMessage = exceptionMessage;
            InitializeComponent();
            var height = CalculateFormHeight();
            Height = height;
        }

        /// <summary>
        ///     Message to show in the error window.
        /// </summary>
        public string ExceptionMessage { get; set; }

        private int CalculateFormHeight()
        {
            var height = 0;
            if (Err.Configuration.UserInteraction.AskUserForDetails)
            {
                height += 200;
            }

            if (Err.Configuration.UserInteraction.AskForEmailAddress)
            {
                height += 100;
            }

            height += 100;
            height += 100;
            return height;
        }

        private void DialogPresenterFinishedReporting(object sender, EventArgs eventArgs)
        {
            Close();
        }
    }
}