using System;
using System.Windows;

namespace OneTrueError.Client.Wpf.Demo
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
            throw new Exception("Demo exception");
        }
    }
}
