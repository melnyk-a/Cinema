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
        private const string ActorTag = "Actor";
        private const string DirectorTag = "Director";

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

            addActorCommand = new DelegateCommand(() =>
                crews.Add(viewModelFactory.CreateFilmCrewViewModel(actorName, actorSurname, ActorTag)),
                () => CanAddActor
            );
            addDirectorCommand = new DelegateCommand(() =>
                crews.Add(viewModelFactory.CreateFilmCrewViewModel(directorName, directorSurname, DirectorTag)),
                () => CanAddDirector
            );

            PropertyChanged += (sender, e) =>
                 {
                     if (e.PropertyName == nameof(IsFilmCrewReadyForSetUp))
                     {
                         if (IsFilmCrewReadyForSetUp)
                         {
                             OnFilmCrewPrepared(EventArgs.Empty);
                         }
                     }
                 };
        }

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

        [RaiseCanExecuteDependsUpon(nameof(CanAddActor))]
        public ICommand AddActorCommand => addActorCommand;

        [RaiseCanExecuteDependsUpon(nameof(CanAddDirector))]
        public ICommand AddDirectorCommand => addDirectorCommand;

        [DependsUponProperty(nameof(ActorName))]
        [DependsUponProperty(nameof(ActorSurname))]
        public bool CanAddActor => ActorName.Length > 0 && ActorSurname.Length > 0;

        [DependsUponProperty(nameof(DirectorName))]
        [DependsUponProperty(nameof(DirectorSurname))]
        public bool CanAddDirector => DirectorName.Length > 0 && DirectorSurname.Length > 0;

        public IEnumerable<FilmCrewViewModel> Crews => crews;

        public Director Director => FindDirector();

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

        public event EventHandler FilmCrewPrepared;

        private ICollection<Actor> FindAllActors()
        {
            ICollection<Actor> actors = new List<Actor>();

            foreach (FilmCrewViewModel filmCrew in crews)
            {
                if (filmCrew.Position == ActorTag)
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
                if (filmCrew.Position == DirectorTag)
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