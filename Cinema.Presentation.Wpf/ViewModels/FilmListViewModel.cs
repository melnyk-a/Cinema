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
        private readonly ICommand addFilmCommand;
        private readonly ICollection<FilmViewModel> films = new ObservableCollection<FilmViewModel>();
        private readonly IViewModelFactory viewModelFactory;

        public FilmListViewModel(IFilmProvider filmProvider, IViewModelFactory viewModelFactory)
        {
            this.viewModelFactory = viewModelFactory;
            addFilmCommand = new DelegateCommand(() => ViewModelManager.SetAddFilmViewModel());
            foreach (Film film in filmProvider.GetAllFilms())
            {
                var viewModel = viewModelFactory.CreateFilmViewModel(film);
                films.Add(viewModel);
            }

            filmProvider.FilmAdded += (sender, e) =>
             {
                 var viewModel = viewModelFactory.CreateFilmViewModel(e.Film);
                 films.Add(viewModel);
             };
        }
        public IEnumerable<FilmViewModel> Films => films;

        public ICommand AddFilmCommand =>  addFilmCommand;
    }
}