using Cinema.Utilities.Wpf.Commands;
using Cinema.Utilities.Wpf.ViewModels;
using System.Windows.Input;

namespace Cinema.Presentation.Wpf.ViewModels
{
    public sealed class AddFilmViewModel : SwitchContentViewModel
    {
        private ICommand cancelCommand;

        public AddFilmViewModel()
        {
            cancelCommand = new DelegateCommand(() => ViewModelManager.SetFilmListViewModel());
        }

        public ICommand CancelCommand
        {
            get => cancelCommand;
        }
    }
}