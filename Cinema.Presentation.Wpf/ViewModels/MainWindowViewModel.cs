using Cinema.Presentation.Wpf.ViewModels.Factories;
using Cinema.Utilities.Wpf.Commands;
using Cinema.Utilities.Wpf.ViewModels;
using System.Windows.Input;

namespace Cinema.Presentation.Wpf.ViewModels
{
    internal sealed class MainWindowViewModel : ViewModel
    {
        private readonly ICommand addFilmCommand;
        private readonly ViewModel addFilmViewModel;
        private readonly ViewModel filmListViewModel;
        private readonly ICommand showAllFilmsCommand;

        private object current;

        public MainWindowViewModel(IViewModelFactory viewModelFactory)
        {
            addFilmViewModel = viewModelFactory.CreateAddFilmViewModel();
            filmListViewModel = viewModelFactory.CreateFilmListViewModel();

            addFilmCommand = new DelegateCommand(() => Current = addFilmCommand);
            showAllFilmsCommand = new DelegateCommand(() => Current = showAllFilmsCommand);
            current = filmListViewModel;
        }

        public object Current
        {
            get => current;
            private set => SetProperty(ref current, value);
        }

        public ICommand AddFilmCommand => addFilmCommand;
        public ICommand ShowAllFilmsCommand => showAllFilmsCommand;
    }
}