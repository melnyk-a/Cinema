using Cinema.Domain.Models;

namespace Cinema.Presentation.Wpf.ViewModels.Factories
{
    public interface IViewModelFactory
    {
        AddFilmCrewViewModel CreateAddFilmCrewViewModel();
        AddFilmViewModel CreateAddFilmViewModel();
        FilmViewModel CreateFilmViewModel(Film film);
        FilmListViewModel CreateFilmListViewModel();
    }
}