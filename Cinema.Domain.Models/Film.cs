using System;

namespace Cinema.Domain.Models
{
    public sealed class Film
    {
        private readonly FilmCrew filmCrew;
        private readonly Language language;
        private readonly DateTime releaseDate;
        private readonly string title;
       
        public Film(string title, DateTime releaseDate, Language language, FilmCrew filmCrew)
        {
            this.filmCrew = filmCrew;
            this.language = language;
            this.releaseDate = releaseDate;
            this.title = title;        
        }

        public FilmCrew FilmCrew => filmCrew;

        public Language Language => language;

        public DateTime ReleaseDate => releaseDate;

        public string Title => title;
    }
}