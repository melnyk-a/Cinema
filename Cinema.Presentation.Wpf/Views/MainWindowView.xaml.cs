using Cinema.Presentation.Wpf.ViewModels;
using System.Windows;

namespace Cinema.Presentation.Wpf.Views
{
    internal partial class MainWindowView : Window
    {
        public MainWindowView(MainWindowViewModel viewModel)
        {
            InitializeComponent();

            DataContext = viewModel;
        }
    }
}