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
        private readonly IEnumerable<FilmCrewViewModel> crews = new ObservableCollection<FilmCrewViewModel>();

        private string actorName;
        private string actorSurname;
        private string directorName;
        private string directorSurname;

        public AddFilmCrewViewModel()
        {
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

        public string DirectorName
        {
            get => directorName;
            set => SetProperty(ref directorName, value);
        }

        public string DirectorSurname
        {
            get => DirectorSurname;
            set => SetProperty(ref directorSurname, value);
        }
    }
}