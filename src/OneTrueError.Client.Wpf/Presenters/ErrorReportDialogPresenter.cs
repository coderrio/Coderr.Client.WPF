using System;
using System.Windows.Input;
using OneTrueError.Client.ContextCollections;
using OneTrueError.Client.Contracts;
using OneTrueError.Client.Wpf.Utils;

// ReSharper disable UseNullPropagation

// ReSharper disable ConvertPropertyToExpressionBody

namespace OneTrueError.Client.Wpf.Presenters
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

        public bool AskForEmailAddress
        {
            get { return OneTrue.Configuration.UserInteraction.AskForEmailAddress; }
        }

        public bool AskForUserDetails
        {
            get { return OneTrue.Configuration.UserInteraction.AskUserForDetails; }
        }

        public bool AskForUserPermission
        {
            get { return OneTrue.Configuration.UserInteraction.AskUserForPermission; }
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

        private void ReportToOneTrue()
        {
            var info = UserErrorDescription.UserDescription;
            var email = NotificationControl.Email;

            // only upload it if the flag is set, it have already been uploaded otherwise.
            if (AskForUserPermission)
                OneTrue.UploadReport(_dto);

            if (!string.IsNullOrEmpty(info) || !string.IsNullOrEmpty(email))
                OneTrue.LeaveFeedback(_dto.ReportId, new UserSuppliedInformation(info, email));
        }

        private void SubmitReport()
        {
            ActionWrapper.SafeActionExecution(ReportToOneTrue);
            PublishFinishedReporting();
        }
    }
}