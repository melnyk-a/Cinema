using Cinema.Domain.Models;
using Cinema.Presentation.Wpf.ViewModels.Factories;
using Cinema.Utilities.Wpf.Commands;
using System;
using System.Windows.Input;

namespace Cinema.Presentation.Wpf.ViewModels
{
    public sealed class AddFilmViewModel : SwitchContentViewModel
    {
        private readonly AddFilmCrewViewModel addFilmCrewViewModel;
        private readonly ICommand cancelCommand;

        private Language selectedLanguage;
        private DateTime selectedDate = DateTime.Now;
        private string title;

        public AddFilmViewModel(IViewModelFactory viewModelFactory)
        {
            addFilmCrewViewModel = viewModelFactory.CreateAddFilmCrewViewModel();
            cancelCommand = new DelegateCommand(() => ViewModelManager.SetFilmListViewModel());
        }

        public object AddFilmCrewViewModel => addFilmCrewViewModel;

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
    }
}