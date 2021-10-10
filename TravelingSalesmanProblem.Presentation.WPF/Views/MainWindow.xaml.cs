using System;
using System.Windows;
using TravelingSalesmanProblem.Presentation.WPF.ViewModels;

namespace TravelingSalesmanProblem.Presentation.WPF.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly MainWindowViewModel viewModel_ = new();

        public MainWindow()
        {
            InitializeComponent();

            DataContext = viewModel_;
        }

        private void Window_ContentRendered(object sender, EventArgs e) => viewModel_.OnContentRendered();
    }
}