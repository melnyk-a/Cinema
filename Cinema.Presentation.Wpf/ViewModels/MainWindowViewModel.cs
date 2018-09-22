namespace Cinema.Presentation.Wpf.ViewModels
{
    internal sealed class MainWindowViewModel
    {
        private readonly FilmListViewModel listViewModel;
        private readonly AddFilmViewModel addFilmViewModel;
        private object current;

        public MainWindowViewModel(FilmListViewModel listViewModel, AddFilmViewModel addFilmViewModel)
        {
            this.addFilmViewModel = addFilmViewModel;
            this.listViewModel = listViewModel;

            current = listViewModel;
        }

        public object Current => current;
    }
}