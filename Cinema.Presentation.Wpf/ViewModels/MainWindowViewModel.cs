using Cinema.Presentation.Wpf.ViewModels.Factories;
using Cinema.Utilities.Wpf.ViewModels;

namespace Cinema.Presentation.Wpf.ViewModels
{
    internal sealed class MainWindowViewModel : ViewModel
    {
        private readonly AddFilmViewModel addFilmViewModel;
        private readonly FilmListViewModel listViewModel;
        
        private object current;

        public MainWindowViewModel(IViewModelFactory viewModelFactory)
        {
            addFilmViewModel = viewModelFactory.CreateAddFilmViewModel();
            listViewModel = viewModelFactory.CreateFilmListViewModel();

            current = listViewModel;
        }

        public object Current => current;
    }
}