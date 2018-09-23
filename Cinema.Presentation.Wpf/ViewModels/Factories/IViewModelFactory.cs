using Cinema.Domain.Models;

namespace Cinema.Presentation.Wpf.ViewModels.Factories
{
    public interface IViewModelFactory
    {
        AddFilmCrewViewModel CreateAddFilmCrewViewModel();
        AddFilmViewModel CreateAddFilmViewModel();
        FilmCrewViewModel CreateFilmCrewViewModel(string name, string surname, string position);
        FilmViewModel CreateFilmViewModel(Film film);
        FilmListViewModel CreateFilmListViewModel();
    }
}