using Coderr.Client.Contracts;

namespace Coderr.Client.Wpf.Utils
{
    public static class ErrorReportDetailsProvider
    {
        public static ErrorReportDTO DtoReport { get; set; }

        public static string ExceptionMessage { get; set; }
    }
}
