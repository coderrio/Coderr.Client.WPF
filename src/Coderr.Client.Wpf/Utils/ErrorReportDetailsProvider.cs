using Coderr.Client.Contracts;

namespace Coderr.Client.Wpf.Utils
{
    /// <summary>
    /// Details about the current error.
    /// </summary>
    public static class ErrorReportDetailsProvider
    {
        /// <summary>
        /// Report to send.
        /// </summary>
        public static ErrorReportDTO DtoReport { get; set; }

        /// <summary>
        /// Message to display.
        /// </summary>
        public static string ExceptionMessage { get; set; }
    }
}
