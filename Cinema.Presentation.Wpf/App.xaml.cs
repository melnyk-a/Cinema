using Cinema.Presentation.Wpf.Views;
using StructureMap;
using System.Windows;

namespace Cinema.Presentation.Wpf
{
    internal partial class App : Application
    {
        private Container CreateContainer()
        {
            var container = new Container(
                configurator =>
                {
                    configurator.Scan(
                        scanner =>
                        {
                            scanner.AssembliesAndExecutablesFromApplicationBaseDirectory();
                            scanner.SingleImplementationsOfInterface();
                            scanner.WithDefaultConventions();
                        });
                }
            );

            return container;
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            Container container = CreateContainer();
            MainWindowView mainView = container.GetInstance<MainWindowView>();
            mainView.Show();
        }
    }
}