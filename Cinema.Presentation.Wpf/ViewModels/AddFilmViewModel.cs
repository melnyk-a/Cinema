using Cinema.Domain.Models;
using Cinema.Presentation.Wpf.ViewModels.Factories;
using Cinema.Utilities.Wpf.Attributes;
using Cinema.Utilities.Wpf.Commands;
using System;
using System.Windows.Input;

namespace Cinema.Presentation.Wpf.ViewModels
{
    public sealed class AddFilmViewModel : SwitchContentViewModel
    {
        private readonly AddFilmCrewViewModel addFilmCrewViewModel;
        private readonly ICommand addFilmCommand;
        private readonly ICommand cancelCommand;

        private Language selectedLanguage = Language.Unspecified;
        private DateTime selectedDate = DateTime.MinValue;
        private string title = string.Empty;

        public AddFilmViewModel(IViewModelFactory viewModelFactory)
        {
            addFilmCommand = new DelegateCommand(AddFilm, () => CanAddFilm);
            addFilmCrewViewModel = viewModelFactory.CreateAddFilmCrewViewModel();
            cancelCommand = new DelegateCommand(() => ViewModelManager.SetFilmListViewModel());
        }

        [RaiseCanExecuteDependsUpon(nameof(CanAddFilm))]
        public ICommand AddFilmCommand => addFilmCommand;
        public object AddFilmCrewViewModel => addFilmCrewViewModel;

        [DependsUpon(nameof(Title))]
        [DependsUpon(nameof(SelectedDate))]
        [DependsUpon(nameof(SelectedLanguage))]
        public bool CanAddFilm => Title.Length > 0 && SelectedDate != DateTime.MinValue && SelectedLanguage != Language.Unspecified && addFilmCrewViewModel.FilmCrewSetUp;

        public ICommand CancelCommand
        {
            get => cancelCommand;
        }

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

        }
    }
}