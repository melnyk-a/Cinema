using System;

namespace Cinema.Domain.Models
{
    public sealed class Film
    {
        private readonly FilmCrew filmCrew;
        private readonly string language;
        private readonly DateTime releaseDate;
        private readonly string title;
       
        public Film(string title, DateTime releaseDate, string language, FilmCrew filmCrew)
        {
            this.filmCrew = filmCrew;
            this.language = language;
            this.releaseDate = releaseDate;
            this.title = title;        
        }

        public FilmCrew FilmCrew => filmCrew;
        public string Language => language;

        public DateTime ReleaseDate => releaseDate;

        public string Title => title;
    }
}