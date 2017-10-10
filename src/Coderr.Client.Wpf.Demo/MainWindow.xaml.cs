using System;
using System.Windows;

namespace codeRR.Client.Wpf.Demo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ThrowException(object sender, RoutedEventArgs e)
        {
            // Press F5 if Visual Studio breaks here ("First-chance exceptions")
            throw new Exception("Demo exception");
        }
    }
}
