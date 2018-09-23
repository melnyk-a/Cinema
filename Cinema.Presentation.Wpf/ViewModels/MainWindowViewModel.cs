using Cinema.Utilities.Wpf.ViewModels;

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
            private set => SetProperty(ref current, value);
        }
    }
}