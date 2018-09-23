using Cinema.Presentation.Wpf.ViewModels.Factories;
using Cinema.Utilities.Wpf.ViewModels;
using System.ComponentModel;

namespace Cinema.Presentation.Wpf.ViewModels
{
    internal sealed class MainWindowViewModel : ViewModel
    {
        private object current;

        public MainWindowViewModel(ViewModelManager viewModelManager)
        {
            viewModelManager.CurrentViewModelChanged += (sender, e) =>
                 {
                     Current = viewModelManager.CurrentViewModel;
                 };

            current = viewModelManager.CurrentViewModel;
        }

        public object Current
        {
            get => current;
            set
            {
                current = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(Current)));
            }
            
            //private set => SetProperty(ref current, value);
        }
    }
}