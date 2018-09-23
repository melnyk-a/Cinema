using Cinema.Utilities.Wpf.ViewModels;

namespace Cinema.Presentation.Wpf.ViewModels
{
    public sealed class FilmCrewViewModel : ViewModel
    {
        private readonly string name;
        private readonly string surname;
        private readonly string position;

        public FilmCrewViewModel(string name, string surname, string position)
        {
            this.name = name;
            this.surname = surname;
            this.position = position;
        }

        public string Name => name;

        public string Surname => surname;

        public string Position => position;
    }
}