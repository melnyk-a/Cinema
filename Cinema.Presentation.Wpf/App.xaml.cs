using Cinema.Presentation.Wpf.Views;
using System.Windows;

namespace Cinema.Presentation.Wpf
{
    internal partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var view = new MainView();
            view.Show();
        }
    }
}