using System.Diagnostics;
using System.Reflection;
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
            var listener = new CoderrTraceListener();
            Trace.Listeners.Add(listener);


            PresentationTraceSources.Refresh();

            // enable all WPF Trace sources (change this if you only want DataBindingSource)
            foreach (var pi in typeof(PresentationTraceSources).GetProperties(BindingFlags.Static | BindingFlags.Public))
            {
                if (!typeof(TraceSource).IsAssignableFrom(pi.PropertyType))
                {
                    continue;
                }

                var ts = (TraceSource)pi.GetValue(null, null);
                ts.Listeners.Add(listener);
                ts.Switch.Level = SourceLevels.All;
            }
        }
    }
}