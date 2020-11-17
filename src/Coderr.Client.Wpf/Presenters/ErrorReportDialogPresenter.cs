using System;
using System.Windows.Input;
using Coderr.Client.ContextCollections;
using Coderr.Client.Contracts;
using Coderr.Client.Wpf.Utils;

// ReSharper disable UseNullPropagation
// ReSharper disable ConvertPropertyToExpressionBody

namespace Coderr.Client.Wpf.Presenters
{
    /// <summary>
    /// Used to show the built in error windows.
    /// </summary>
    public class ErrorReportDialogPresenter
    {
        private readonly ErrorReportDTO _dto;

        /// <summary>
        /// Creates a new instance of the <see cref="ErrorReportDialogPresenter"/> class.
        /// </summary>
        public ErrorReportDialogPresenter()
        {
            _dto = ErrorReportDetailsProvider.DtoReport;
            SubmitCommand = new ActionCommand(SubmitReport);
            CancelCommand = new ActionCommand(CancelReport);
            ErrorMessage = new ErrorMessagePresenter(ErrorReportDetailsProvider.ExceptionMessage);
            UserErrorDescription = new UserErrorDescriptionPresenter();
            NotificationControl = new NotificationControlPresenter();
        }

        /// <summary>
        /// Ask user for details regarding the circumstances when the error happened.
        /// </summary>
        public bool AskForUserDetails
        {
            get { return Err.Configuration.UserInteraction.AskUserForDetails; }
        }

        /// <summary>
        /// Ask if the user wants to get notified when the error have been corrected.
        /// </summary>
        public bool AskForEmailAddress
        {
            get { return Err.Configuration.UserInteraction.AskForEmailAddress; }
        }

        /// <summary>
        /// Ask user if it's OK to upload the error report.
        /// </summary>
        public bool AskForUserPermission
        {
            get { return Err.Configuration.UserInteraction.AskUserForPermission; }
        }

        /// <summary>
        /// Error message to show in the window.
        /// </summary>
        public ErrorMessagePresenter ErrorMessage { get; set; }

        /// <summary>
        /// Description written by the user.
        /// </summary>
        public UserErrorDescriptionPresenter UserErrorDescription { get; set; }

        /// <summary>
        /// Notification options
        /// </summary>
        public NotificationControlPresenter NotificationControl { get; set; }

        /// <summary>
        /// Event triggered when report has been sent.
        /// </summary>
        public event EventHandler<EventArgs> FinishedReporting;

        /// <summary>
        /// Command to invoke when user presses "Submit".
        /// </summary>
        public ICommand SubmitCommand { get; set; }

        /// <summary>
        /// Command to invoke when the user cancels the reporting.
        /// </summary>
        public ICommand CancelCommand { get; set; }

        private void SubmitReport()
        {
            ActionWrapper.SafeActionExecution(ReportToCoderr);
            PublishFinishedReporting();
        }

        private void ReportToCoderr()
        {
            var info = UserErrorDescription.UserDescription;
            var email = NotificationControl.Email;

            // only upload it if the flag is set, it have already been uploaded otherwise.
            if (AskForUserPermission)
            {
                Err.UploadReport(_dto);
            }

            if (!string.IsNullOrEmpty(info) || !string.IsNullOrEmpty(email))
            {
                Err.LeaveFeedback(_dto.ReportId, email, info);
            }
        }

        private void CancelReport()
        {
            PublishFinishedReporting();
        }

        private void PublishFinishedReporting()
        {
            FinishedReporting?.Invoke(this, new EventArgs());
        }
    }
}
