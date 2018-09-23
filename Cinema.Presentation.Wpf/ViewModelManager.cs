using Cinema.Presentation.Wpf.ViewModels;
using Cinema.Presentation.Wpf.ViewModels.Factories;
using Cinema.Utilities.Wpf.ViewModels;
using System;

namespace Cinema.Presentation.Wpf
{
    public sealed class ViewModelManager
    {
        private readonly SwitchContentViewModel addFilmViewModel;
        private readonly SwitchContentViewModel filmListViewModel;

        private ViewModel currentViewModel;

        public ViewModelManager(IViewModelFactory viewModelFactory)
        {
            addFilmViewModel = viewModelFactory.CreateAddFilmViewModel();
            addFilmViewModel.ViewModelManager = this;
            filmListViewModel = viewModelFactory.CreateFilmListViewModel();
            filmListViewModel.ViewModelManager = this;

            currentViewModel = filmListViewModel;
        }

        public ViewModel CurrentViewModel
        {
            get => currentViewModel;
            set
            {
                if (currentViewModel != value)
                {
                    currentViewModel = value;
                    OnCurrentViewModelChanged(new EventArgs());
                }
            }
        }

        public event EventHandler CurrentViewModelChanged;

        public void OnCurrentViewModelChanged(EventArgs e)
        {
            CurrentViewModelChanged?.Invoke(this, e);
        }

        public void SetAddFilmViewModel()
        {
            CurrentViewModel = addFilmViewModel;
        }
    }
}