using System;
using System.Windows.Input;
using codeRR.Client.Wpf.Utils;
using codeRR.Client.ContextCollections;
using codeRR.Client.Contracts;
// ReSharper disable UseNullPropagation
// ReSharper disable ConvertPropertyToExpressionBody

namespace codeRR.Client.Wpf.Presenters
{
    /// <summary>
    /// Presenter for the error report dialog
    /// </summary>
    public class ErrorReportDialogPresenter
    {
        private readonly ErrorReportDTO _dto;

        /// <inheritdoc />
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
        /// Let user be able to enter email address for status updates.
        /// </summary>
        public bool AskForEmailAddress
        {
            get { return Err.Configuration.UserInteraction.AskForEmailAddress; }
        }

        /// <summary>
        /// Ask user for details about what he/she did when the exception was thrown.
        /// </summary>
        public bool AskForUserDetails
        {
            get { return Err.Configuration.UserInteraction.AskUserForDetails; }
        }

        /// <summary>
        /// Ask user if it's OK to upload the error report (otherwise you typically regulate that with a "Terms Of Service" or similar)
        /// </summary>
        public bool AskForUserPermission
        {
            get { return Err.Configuration.UserInteraction.AskUserForPermission; }
        }

        public ICommand CancelCommand { get; set; }

        public ErrorMessagePresenter ErrorMessage { get; set; }

        public NotificationControlPresenter NotificationControl { get; set; }

        public ICommand SubmitCommand { get; set; }

        public UserErrorDescriptionPresenter UserErrorDescription { get; set; }

        public event EventHandler<EventArgs> FinishedReporting;

        private void CancelReport()
        {
            PublishFinishedReporting();
        }

        private void PublishFinishedReporting()
        {
            if (FinishedReporting != null)
                FinishedReporting(this, new EventArgs());
        }

        private void ReportToCoderr()
        {
            var info = UserErrorDescription.UserDescription;
            var email = NotificationControl.Email;

            // only upload it if the flag is set, it have already been uploaded otherwise.
            if (AskForUserPermission)
                Err.UploadReport(_dto);

            if (!string.IsNullOrEmpty(info) || !string.IsNullOrEmpty(email))
                Err.LeaveFeedback(_dto.ReportId, new UserSuppliedInformation(info, email));
        }

        private void SubmitReport()
        {
            ActionWrapper.SafeActionExecution(ReportToCoderr);
            PublishFinishedReporting();
        }
    }
}