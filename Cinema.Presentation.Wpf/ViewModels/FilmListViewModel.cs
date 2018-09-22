using Cinema.Domain;
using Cinema.Domain.Models;
using Cinema.Presentation.Wpf.ViewModels.Factories;
using Cinema.Utilities.Wpf.ViewModels;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Cinema.Presentation.Wpf.ViewModels
{
    internal sealed class FilmListViewModel : ViewModel
    {
        private readonly ICollection<FilmViewModel> films = new ObservableCollection<FilmViewModel>();

        public FilmListViewModel(ICinemaProvider cinemaProvider, IViewModelFactory viewModelFactory)
        {
            foreach (Film film in cinemaProvider.GetAllFilms())
            {
                var viewModel = viewModelFactory.CreateFilmViewModel(film);
                films.Add(viewModel);
            }
        }

        public IEnumerable<FilmViewModel> Films => films;
    }
}