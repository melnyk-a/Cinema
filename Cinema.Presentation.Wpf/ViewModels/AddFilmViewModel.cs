using Cinema.Domain;
using Cinema.Domain.Models;
using Cinema.Presentation.Wpf.ViewModels.Factories;
using Cinema.Utilities.Wpf.Attributes;
using Cinema.Utilities.Wpf.Commands;
using System;
using System.ComponentModel;
using System.Windows.Input;

namespace Cinema.Presentation.Wpf.ViewModels
{
    public sealed class AddFilmViewModel : SwitchContentViewModel
    {
        private readonly ICommand addFilmCommand;
        private readonly AddFilmCrewViewModel addFilmCrewViewModel;
        private readonly ICommand cancelCommand;
        private readonly IFilmManager cinemaManager;

        private DateTime selectedDate;
        private Language selectedLanguage;
        private string title;

        public AddFilmViewModel(IFilmManager cinemaManager, IViewModelFactory viewModelFactory)
        {
            this.cinemaManager = cinemaManager;
            addFilmCommand = new DelegateCommand(AddFilm, () => CanAddFilm);
            addFilmCrewViewModel = viewModelFactory.CreateAddFilmCrewViewModel();
            addFilmCrewViewModel.FilmCrewPrepared += (sender, e) =>
             {
                 OnPropertyChanged(new PropertyChangedEventArgs(nameof(FilmCrewPrepared)));
             };
            cancelCommand = new DelegateCommand(() => ViewModelManager.SetFilmListViewModel());
            ResetValues();
        }

        [RaiseCanExecuteDependsUpon(nameof(CanAddFilm))]
        public ICommand AddFilmCommand => addFilmCommand;

        public object AddFilmCrewViewModel => addFilmCrewViewModel;

        [DependsUponProperty(nameof(Title))]
        [DependsUponProperty(nameof(SelectedDate))]
        [DependsUponProperty(nameof(SelectedLanguage))]
        [DependsUponProperty(nameof(FilmCrewPrepared))]

        public bool CanAddFilm
        {
            get
            {
                return Title.Length > 0 && SelectedDate != DateTime.MinValue && 
                    SelectedLanguage != Language.Unspecified && FilmCrewPrepared;
            }
        }

        public ICommand CancelCommand => cancelCommand;

        public bool FilmCrewPrepared => addFilmCrewViewModel.IsFilmCrewReadyForSetUp;

        public DateTime SelectedDate
        {
            get => selectedDate;
            set => SetProperty(ref selectedDate, value);
        }

        public Language SelectedLanguage
        {
            get => selectedLanguage;
            set => SetProperty(ref selectedLanguage, value);
        }

        public string Title
        {
            get => title;
            set => SetProperty(ref title, value);
        }

        public void AddFilm()
        {
            cinemaManager.AddFilm(new Film(Title, 
                SelectedDate, 
                SelectedLanguage, 
                new FilmCrew(addFilmCrewViewModel.Director, 
                    addFilmCrewViewModel.Actors))
            );
            ResetValues();
            ViewModelManager.SetFilmListViewModel();
        }

        public void ResetValues()
        {
            SelectedLanguage = Language.Unspecified;
            SelectedDate = DateTime.MinValue;
            Title = string.Empty;
            addFilmCrewViewModel.ResetValues();
        }
    }
}