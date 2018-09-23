using Cinema.Domain.Models;
using Cinema.Domain.Models.JobTitles;
using Cinema.Presentation.Wpf.ViewModels.Factories;
using Cinema.Utilities.Wpf.Attributes;
using Cinema.Utilities.Wpf.Commands;
using Cinema.Utilities.Wpf.ViewModels;
using System;
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

        private string actorName = string.Empty;
        private string actorSurname = string.Empty;
        private string directorName = string.Empty;
        private string directorSurname = string.Empty;

        public AddFilmCrewViewModel(IViewModelFactory viewModelFactory)
        {
            this.viewModelFactory = viewModelFactory;

            addActorCommand = new DelegateCommand(() => crews.Add(viewModelFactory.CreateFilmCrewViewModel(actorName, actorSurname, "Actor")), () => CanAddActor);
            addDirectorCommand = new DelegateCommand(() => crews.Add(viewModelFactory.CreateFilmCrewViewModel(directorName, directorSurname, "Director")), () => CanAddDirector);

            PropertyChanged += (sender, e) =>
                 {
                     if (e.PropertyName == nameof(IsFilmCrewReadyForSetUp))
                     {
                         if(IsFilmCrewReadyForSetUp)
                         {
                             OnFilmCrewPrepared(EventArgs.Empty);
                         }
                     }
                 };
        }

        [RaiseCanExecuteDependsUpon(nameof(CanAddActor))]
        public ICommand AddActorCommand => addActorCommand;

        [RaiseCanExecuteDependsUpon(nameof(CanAddDirector))]
        public ICommand AddDirectorCommand => addDirectorCommand;

        public ICollection<Actor> Actors => FindAllActors();

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

        [DependsUponProperty(nameof(ActorName))]
        [DependsUponProperty(nameof(ActorSurname))]
        public bool CanAddActor => ActorName.Length > 0 && ActorSurname.Length > 0;

        [DependsUponProperty(nameof(DirectorName))]
        [DependsUponProperty(nameof(DirectorSurname))]
        public bool CanAddDirector => DirectorName.Length > 0 && DirectorSurname.Length > 0;

        public Director Director => FindDirector();

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

        public event EventHandler FilmCrewPrepared;

        [DependsUponCollection(nameof(Crews))]
        public bool IsFilmCrewReadyForSetUp
        {
            get
            {
                bool hasActor = FindAllActors().Count != 0;
                bool hasDirector = FindDirector() != null;
                return hasActor & hasDirector;
            }
        }

        private ICollection<Actor> FindAllActors()
        {
            ICollection<Actor> actors = new List<Actor>();

            foreach (FilmCrewViewModel filmCrew in crews)
            {
                if (filmCrew.Position == "Actor")
                {
                    actors.Add(new Actor(new Human(filmCrew.Name, filmCrew.Surname)));
                    break;
                }
            }

            return actors;
        }

        private Director FindDirector()
        {
            Director director = null;

            foreach (FilmCrewViewModel filmCrew in crews)
            {
                if (filmCrew.Position == "Director")
                {
                    director = new Director(new Human(filmCrew.Name, filmCrew.Surname));
                    break;
                }
            }

            return director;
        }

        public void OnFilmCrewPrepared(EventArgs e)
        {
            FilmCrewPrepared?.Invoke(this, e);
        }

        public void ResetValues()
        {
            ActorName = string.Empty;
            ActorSurname = string.Empty;
            DirectorName = string.Empty;
            DirectorSurname = string.Empty;
            crews.Clear();
        }
    }
}