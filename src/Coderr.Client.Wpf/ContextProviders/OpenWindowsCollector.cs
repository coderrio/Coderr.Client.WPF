using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using Coderr.Client.ContextCollections;
using Coderr.Client.Contracts;
using Coderr.Client.Reporters;
using Coderr.Client.Wpf.Contexts;
using Coderr.Client.Wpf.Utils;

namespace Coderr.Client.Wpf.ContextProviders
{
    /// <summary>
    ///     Serializes all open windows into the context collection named <c>"OpenWindows"</c>
    /// </summary>
    public class OpenWindowsCollector : IContextCollectionProvider
    {
        /// <summary>
        ///     Returns <c>OpenWindows</c>.
        /// </summary>
        public string Name => "OpenWindows";

        /// <summary>
        ///     Collect information
        /// </summary>
        /// <param name="context">Context information provided by the class which reported the error.</param>
        /// <returns>
        ///     Collection. Items with multiple values are joined using <c>";;"</c>
        /// </returns>
        public ContextCollectionDTO Collect(IErrorReporterContext context)
        {
            var ctx = context as WpfErrorReportContext;
            if (ctx?.Windows == null)
                return null;

            var values = new Dictionary<string, string>();
            var invocationRequired = false;
            foreach (Window window in ctx.Windows)
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

            try
            {
                return Collect(ctx, values);
            }
            catch (Exception exception)
            {
                return new ContextCollectionDTO(Name,
                    new Dictionary<string, string>
                    {
                        {"Error", "Collection on non-ui thread"},
                        {"Exception", exception.ToString()}
                    });
            }
        }

        private ContextCollectionDTO Collect(WpfErrorReportContext ctx, Dictionary<string, string> formCollection)
        {
            var index = 0;
            var variables = new StringBuilder();
            foreach (Window window in ctx.Windows)
            {
                if (window?.GetType().Name == "AdornerLayerWindow")
                    continue;
                index++;

                var fields =
                    window.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                foreach (var field in fields)
                {
                    if (field.FieldType.Namespace.StartsWith("Microsoft.VisualStudio.DesignTools"))
                        continue;
                    if (typeof(Control).IsAssignableFrom(field.FieldType))
                    {
                        var control = (Control) field.GetValue(window);
                        if (control != null)
                            variables.AppendFormat("{1} = {2} [{0}];;", field.FieldType, field.Name, control.Name);
                    }
                    else
                    {
                        var value = field.GetValue(window);
                        variables.AppendFormat("{1} = {2} [{0}];;", field.FieldType, field.Name, value);
                    }
                }

                var properties =
                    window.GetType()
                        .GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                foreach (var property in properties)
                {
                    if (property.PropertyType.Namespace.StartsWith("Microsoft.VisualStudio.DesignTools"))
                        continue;
                    if (!property.CanRead || property.GetIndexParameters().Length > 0)
                        continue;

                    if (typeof(Control).IsAssignableFrom(property.PropertyType))
                    {
                        var control = (Control) property.GetValue(window, null);
                        if (control != null)
                            variables.AppendFormat("{1} = {2} [{0}];;", property.PropertyType, property.Name,
                                control.Name);
                    }
                    else
                    {
                        var value = property.GetValue(window, null);
                        variables.AppendFormat("{1} = {2} [{0}];;", property.PropertyType, property.Name, value);
                    }
                }

                var name = window.GenerateName(formCollection, index);

                // Protect against windows with lots of stuff.
                if (variables.Length > 1000000)
                {
                    variables.Clear();
                    continue;
                }

                formCollection.Add(name, variables.ToString());
                variables.Clear();
            }


            return new ContextCollectionDTO(Name, formCollection);
        }
    }
}