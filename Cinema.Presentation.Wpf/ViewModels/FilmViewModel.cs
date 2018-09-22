using Cinema.Domain.Models;

namespace Cinema.Presentation.Wpf.ViewModels
{
    internal sealed class FilmViewModel
    {
        private readonly Film film;

        public FilmViewModel(Film film)
        {
            this.film = film;
        }

        public string Title => film.Title;
    }
}