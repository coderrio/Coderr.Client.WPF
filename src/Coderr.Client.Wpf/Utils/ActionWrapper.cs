using System;
using System.Windows;

// ReSharper disable UseStringInterpolation

namespace Coderr.Client.Wpf.Utils
{
    /// <summary>
    /// Extension methods for doing something.
    /// </summary>
    public static class ActionWrapper
    {
        /// <summary>
        /// Let's do it!
        /// </summary>
        /// <param name="action"></param>
        public static void SafeActionExecution(Action action)
        {
            try
            {
                action();
            }
            catch (Exception e)
            {
                //.. or not ..
                var message = string.Format("An error occurred with this message:{0}{1}", Environment.NewLine,
                    e.Message);
                MessageBox.Show(message);
            }
        }
    }
}