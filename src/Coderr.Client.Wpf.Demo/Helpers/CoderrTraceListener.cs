using System.Diagnostics;
using System.Text;
using log4net;

namespace Coderr.Client.Wpf.Demo.Helpers
{
    internal class CoderrTraceListener : TraceListener
    {
        private readonly ILog _logger = LogManager.GetLogger(typeof(CoderrTraceListener));
        private readonly StringBuilder _sb = new StringBuilder();

        public override void Write(string message)
        {
            _sb.Append(message);
        }

        public override void WriteLine(string message)
        {
            _sb.Append(message);
            _logger.Debug(_sb.ToString());
            _sb.Clear();
        }

        public static void Activate()
        {
            Trace.Listeners.Add(new CoderrTraceListener());
        }
    }
}