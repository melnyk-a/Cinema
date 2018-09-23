using Cinema.Presentation.Wpf.ViewModels;
using Cinema.Presentation.Wpf.ViewModels.Factories;
using Cinema.Presentation.Wpf.Views;
using StructureMap;
using StructureMap.AutoFactory;
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
                    configurator.For<IViewModelFactory>().CreateFactory();
                    configurator.For<AddFilmViewModel>().Use<AddFilmViewModel>();
                    configurator.For<FilmListViewModel>().Use<FilmListViewModel>();
                    configurator.For<FilmViewModel>().Use<FilmViewModel>();
                    configurator.For<ViewModelManager>().Use<ViewModelManager>().Singleton();
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