using Cinema.Domain;
using Cinema.Domain.Models;
using Cinema.Presentation.Wpf.ViewModels.Factories;
using Cinema.Utilities.Wpf.Commands;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Cinema.Presentation.Wpf.ViewModels
{
    public sealed class FilmListViewModel : SwitchContentViewModel
    {
        private ICommand addFilmCommand;
        private readonly ICollection<FilmViewModel> films = new ObservableCollection<FilmViewModel>();

        public FilmListViewModel(ICinemaProvider cinemaProvider, IViewModelFactory viewModelFactory)
        {
            addFilmCommand = new DelegateCommand(() => ViewModelManager.SetAddFilmViewModel());
            foreach (Film film in cinemaProvider.GetAllFilms())
            {
                var viewModel = viewModelFactory.CreateFilmViewModel(film);
                films.Add(viewModel); films.Add(viewModel);
            }
        }

        public IEnumerable<FilmViewModel> Films => films;

        public ICommand AddFilmCommand
        {
            get => addFilmCommand;
        }
    }
}