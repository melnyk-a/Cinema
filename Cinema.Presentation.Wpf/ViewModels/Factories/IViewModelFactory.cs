using Cinema.Domain.Models;

namespace Cinema.Presentation.Wpf.ViewModels.Factories
{
    public interface IViewModelFactory
    {
        FilmViewModel CreateFilmViewModel(Film film);
    }
}