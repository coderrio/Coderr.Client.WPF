using System.Collections.Generic;
using System.Linq;
using codeRR.Client.ContextProviders;
using codeRR.Client.Contracts;
using codeRR.Client.Reporters;
using codeRR.Client.Wpf.Contexts;
using codeRR.Client.Wpf.Utils;

namespace codeRR.Client.Wpf.ContextProviders
{
    internal class ViewModelProvider : IContextInfoProvider
    {
        public ContextCollectionDTO Collect(IErrorReporterContext context)
        {
            var ctx = context as WpfErrorReportContext;
            if (ctx?.Windows == null)
                return null;
            var invocationRequired = false;
            foreach (var window in ctx.Windows)
            {
                if (!window.CheckAccess())
                    invocationRequired = true;
            }
            if (invocationRequired)
            {
                ctx.Dispatcher.VerifyAccess();
                return new ContextCollectionDTO(Name,
                    new Dictionary<string, string> {{"Error", "Collection on non-ui thread"}});
            }

            var windowViewModels = new Dictionary<string, string>();
            var index = 0;
            foreach (var window in ctx.Windows)
            {
                if (window.DataContext == null)
                    continue;

                index++;
                var windowName = window.GenerateName(windowViewModels, index);
                var item = window.DataContext.ToContextCollection();
                var items = string.Join(";;", item.Properties.Select(x => x.Key + ": " + x.Value));
                windowViewModels.Add(windowName, items);
            }

            return new ContextCollectionDTO(Name, windowViewModels);
        }

        public string Name => "ViewModels";
    }
}