using System;
using System.Windows.Input;
using codeRR.Client.ContextCollections;
using codeRR.Client.Contracts;
using codeRR.Client.Wpf.Utils;

// ReSharper disable UseNullPropagation

// ReSharper disable ConvertPropertyToExpressionBody

namespace codeRR.Client.Wpf.Presenters
{
    public class ErrorReportDialogPresenter
    {
        private readonly ErrorReportDTO _dto;

        public ErrorReportDialogPresenter()
        {
            _dto = ErrorReportDetailsProvider.DtoReport;
            SubmitCommand = new ActionCommand(SubmitReport);
            CancelCommand = new ActionCommand(CancelReport);
            ErrorMessage = new ErrorMessagePresenter(ErrorReportDetailsProvider.ExceptionMessage);
            UserErrorDescription = new UserErrorDescriptionPresenter();
            NotificationControl = new NotificationControlPresenter();
        }

        public bool AskForUserDetails
        {
            get { return Err.Configuration.UserInteraction.AskUserForDetails; }
        }

        public bool AskForEmailAddress
        {
            get { return Err.Configuration.UserInteraction.AskForEmailAddress; }
        }

        public bool AskForUserPermission
        {
            get { return Err.Configuration.UserInteraction.AskUserForPermission; }
        }

        public ErrorMessagePresenter ErrorMessage { get; set; }

        public UserErrorDescriptionPresenter UserErrorDescription { get; set; }

        public NotificationControlPresenter NotificationControl { get; set; }

        public event EventHandler<EventArgs> FinishedReporting;

        public ICommand SubmitCommand { get; set; }

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
                Err.LeaveFeedback(_dto.ReportId, new UserSuppliedInformation(info, email));
            }
        }

        private void CancelReport()
        {
            PublishFinishedReporting();
        }

        private void PublishFinishedReporting()
        {
            if (FinishedReporting != null)
            {
                FinishedReporting(this, new EventArgs());
            }
        }
    }
}
