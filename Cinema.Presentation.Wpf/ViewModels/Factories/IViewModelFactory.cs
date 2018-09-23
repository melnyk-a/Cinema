using Cinema.Domain.Models;

namespace Cinema.Presentation.Wpf.ViewModels.Factories
{
    public interface IViewModelFactory
    {
        AddFilmCrewViewModel CreateAddFilmCrewViewModel();
        AddFilmViewModel CreateAddFilmViewModel();
        FilmCrewViewModel CreateFilmCrewViewModel(string name, string surname, string position);
        FilmListViewModel CreateFilmListViewModel();
        FilmViewModel CreateFilmViewModel(Film film);
    }
}