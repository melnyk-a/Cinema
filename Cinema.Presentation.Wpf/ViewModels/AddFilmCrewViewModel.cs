using Cinema.Presentation.Wpf.ViewModels.Factories;
using Cinema.Utilities.Wpf.Commands;
using Cinema.Utilities.Wpf.ViewModels;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Cinema.Presentation.Wpf.ViewModels
{
    public sealed class AddFilmCrewViewModel : ViewModel
    {
        private readonly ICommand addActorCommand;
        private readonly ICommand addDirectorCommand;
        private readonly ICollection<FilmCrewViewModel> crews = new ObservableCollection<FilmCrewViewModel>();
        private readonly IViewModelFactory viewModelFactory;

        private string actorName;
        private string actorSurname;
        private string directorName;
        private string directorSurname;

        public AddFilmCrewViewModel(IViewModelFactory viewModelFactory)
        {
            this.viewModelFactory = viewModelFactory;

            addActorCommand = new DelegateCommand(() => crews.Add(viewModelFactory.CreateFilmCrewViewModel(actorName, actorSurname, "Actor")));
            addDirectorCommand = new DelegateCommand(() => crews.Add(viewModelFactory.CreateFilmCrewViewModel(directorName, directorSurname, "Director")));
        }

        public ICommand AddActorCommand => addActorCommand;

        public ICommand AddDirectorCommand => addDirectorCommand;

        public string ActorName
        {
            get => actorName;
            set => SetProperty(ref actorName, value);
        }

        public string ActorSurname
        {
            get => actorSurname;
            set => SetProperty(ref actorSurname, value);
        }

        public IEnumerable<FilmCrewViewModel> Crews => crews;

        public string DirectorName
        {
            get => directorName;
            set => SetProperty(ref directorName, value);
        }

        public string DirectorSurname
        {
            get => directorSurname;
            set => SetProperty(ref directorSurname, value);
        }
    }
}