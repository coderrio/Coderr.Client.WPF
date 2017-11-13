using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using codeRR.Client.ContextProviders;
using codeRR.Client.Contracts;
using codeRR.Client.Reporters;
using codeRR.Client.Wpf.Contexts;
using codeRR.Client.Wpf.Utils;

namespace codeRR.Client.Wpf.ContextProviders
{
    public class ScreenshotCollector : IContextInfoProvider
    {
        public ContextCollectionDTO Collect(IErrorReporterContext context)
        {
            var ctx = context as WpfErrorReportContext;
            if (ctx?.Windows == null)
                return null;

            var index = 0;
            var forms = new Dictionary<string, string>();

            foreach (var window in ctx.Windows)
            {
                var ms = TakeScreenshot(window);
                if (ms == null)
                    continue;

                index++;
                var name = window.GenerateName(forms, index);
                forms[name] = ToBase64(ms);
            }


            return forms.Count == 0 ? null : new ContextCollectionDTO(Name, forms);
        }

        /// <summary>
        ///     "Screenshots"
        /// </summary>
        public string Name => "Screenshots";

        private MemoryStream TakeScreenshot(Window window)
        {
            var source = PresentationSource.FromVisual(window);
            if (source?.CompositionTarget == null)
                return null;

            var transformToDevice = source.CompositionTarget.TransformToDevice;
            var pixelSize = (Size) transformToDevice.Transform((Vector) window.RenderSize);

            var renderTargetBitmap =
                new RenderTargetBitmap((int) pixelSize.Width, (int) pixelSize.Height, 96, 96, PixelFormats.Pbgra32);
            renderTargetBitmap.Render(window);
            var pngImage = new PngBitmapEncoder();
            pngImage.Frames.Add(BitmapFrame.Create(renderTargetBitmap));

            var ms = new MemoryStream();
            pngImage.Save(ms);
            ms.Position = 0;
            return ms;
        }

        private string ToBase64(MemoryStream ms)
        {
            return Convert.ToBase64String(ms.GetBuffer(), 0, (int) ms.Length);
        }
    }
}