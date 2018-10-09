using Cinema.Domain;
using Cinema.Presentation.Wpf.Views;
using Ninject;
using Ninject.Extensions.Conventions;
using System.Windows;

namespace Cinema.Presentation.Wpf
{
    internal partial class App : Application
    {
        private StandardKernel CreateContainer()
        {
            var container = new StandardKernel();
            
            container.Bind(
                configurator => configurator
                .From("Cinema.Data.EntityFramework", "Cinema.Domain", "Cinema.Presentation.Wpf")
                .SelectAllClasses() 
                .BindAllInterfaces()
                .ConfigureFor<ViewModelManager>(config => config.InSingletonScope())
                .ConfigureFor<FilmManager>(config => config.InSingletonScope())
                );

            container.Bind(
                configurator => configurator
                .From("Cinema.Presentation.Wpf")
                .IncludingNonPublicTypes()
                .SelectAllInterfaces()
                .EndingWith("Factory")
                .BindToFactory()
                );

            return container;
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var container = CreateContainer();
            MainWindowView mainView = container.Get<MainWindowView>();
            mainView.Show();
        }
    }
}