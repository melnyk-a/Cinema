using Cinema.Domain;
using Cinema.Domain.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Cinema.Presentation.Wpf.ViewModels
{
    internal sealed class MainWindowViewModel
    {
        private readonly ICollection<FilmViewModel> films = new ObservableCollection<FilmViewModel>();

        public MainWindowViewModel(ICinemaProvider cinemaProvider)
        {
            foreach(Film film in cinemaProvider.GetAllFilms())
            {
                films.Add(new FilmViewModel(film));
            }
        }

        public IEnumerable<FilmViewModel> Films => films;
    }
}