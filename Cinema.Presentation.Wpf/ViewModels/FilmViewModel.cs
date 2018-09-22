using Cinema.Domain.Models;
using Cinema.Utilities.Wpf.ViewModels;

namespace Cinema.Presentation.Wpf.ViewModels
{
    public sealed class FilmViewModel : ViewModel
    {
        private readonly Film film;

        public FilmViewModel(Film film)
        {
            this.film = film;
        }

        public string Title => film.Title;
    }
}