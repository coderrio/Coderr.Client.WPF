using System;
using System.Collections.Generic;
using System.Windows;

namespace Coderr.Client.Wpf.Utils
{
    /// <summary>
    /// Generates names for all windows.
    /// </summary>
    public static class WindowNameGenerator
    {
        /// <summary>
        /// We don't want some stinking anonymous names.
        /// </summary>
        /// <param name="window"></param>
        /// <param name="collection"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static string GenerateName(this Window window, IDictionary<string, string> collection, int index)
        {
            if (window == null)
            {
                throw new ArgumentNullException(nameof(window));
            }

            if (collection == null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            if (index <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }

            var name = window.Name;
            if (string.IsNullOrEmpty(name))
                name = window.Title;
            if (string.IsNullOrEmpty(name))
                name = $"Form{index}";

            if (!collection.ContainsKey(name))
                return name;


            for (var i = 0; i < 10; i++)
                if (!collection.ContainsKey(name + i))
                    return name + i;

            throw new InvalidOperationException("Failed to generate name: " + name);
        }
    }
}