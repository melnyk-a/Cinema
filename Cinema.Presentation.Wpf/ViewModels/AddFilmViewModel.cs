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
        private readonly AddFilmCrewViewModel addFilmCrewViewModel;
        private readonly ICommand addFilmCommand;
        private readonly ICommand cancelCommand;

        private Language selectedLanguage;
        private DateTime selectedDate;
        private string title;

        public AddFilmViewModel(IViewModelFactory viewModelFactory)
        {
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
        public bool CanAddFilm => Title.Length > 0 && SelectedDate != DateTime.MinValue && SelectedLanguage != Language.Unspecified && FilmCrewPrepared;

        public ICommand CancelCommand
        {
            get => cancelCommand;
        }

        public bool FilmCrewPrepared => addFilmCrewViewModel.IsFilmCrewReadyForSetUp;

        public Language SelectedLanguage
        {
            get => selectedLanguage;
            set => SetProperty(ref selectedLanguage, value);
        }

        public DateTime SelectedDate
        {
            get => selectedDate;
            set => SetProperty(ref selectedDate, value);
        }

        public string Title
        {
            get => title;
            set => SetProperty(ref title, value);
        }

        public void AddFilm()
        {
            ResetValues();
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