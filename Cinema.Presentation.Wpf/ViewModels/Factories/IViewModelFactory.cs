using Cinema.Domain.Models;

namespace Cinema.Presentation.Wpf.ViewModels.Factories
{
    public interface IViewModelFactory
    {
        AddFilmViewModel CreateAddFilmViewModel();
        FilmViewModel CreateFilmViewModel(Film film);
        FilmListViewModel CreateFilmListViewModel();
    }
}