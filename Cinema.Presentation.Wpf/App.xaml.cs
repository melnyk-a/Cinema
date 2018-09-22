using Cinema.Data.Stub;
using Cinema.Domain;
using Cinema.Presentation.Wpf.ViewModels;
using Cinema.Presentation.Wpf.Views;
using System.Windows;

namespace Cinema.Presentation.Wpf
{
    internal partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var viewModel = new MainWindowViewModel(new CinemaManager(new StubCinemaDataService()));
            var view = new MainWindowView(viewModel);
            view.Show();
        }
    }
}