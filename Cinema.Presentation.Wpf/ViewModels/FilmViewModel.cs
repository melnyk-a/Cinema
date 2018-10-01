using Cinema.Domain.Models;
using Cinema.Utilities.Wpf.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace Cinema.Presentation.Wpf.ViewModels
{
    public sealed class FilmViewModel : ViewModel
    {
        private readonly Film film;

        public FilmViewModel(Film film)
        {
            this.film = film;
        }

        public IEnumerable<string> Actors
        {
            get => film.FilmCrew.Actors.Select(actor => $"{actor.Human.Name} {actor.Human.Surname}");
        }


        public string Director
        {
            get
            {
                Human director = film.FilmCrew.Director.Human;
                return $"{director.Name} {director.Surname}";
            }
        }

        public string HasBluray
        {
            get
            {
                string hasBluray;
                if (film.HasBlurayRelease.HasValue)
                {
                    hasBluray = film.HasBlurayRelease.Value ? "Yes" : "No";
                }
                else
                {
                    hasBluray = "N/A";
                }
                return hasBluray;
            }
        }

        public string Language => film.Language.ToString();

        public string ReleaseDate => film.ReleaseDate.ToShortDateString();

        public string Title => film.Title;
    }
}