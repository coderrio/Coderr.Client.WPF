using System;
using System.Collections.Generic;
using System.Windows;

namespace Coderr.Client.Wpf.Utils
{
    public static class WindowNameGenerator
    {
        public static string GenerateName(this Window window, IDictionary<string, string> collection, int index)
        {
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