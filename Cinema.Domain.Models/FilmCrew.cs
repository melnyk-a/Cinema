using Cinema.Domain.Models.JobTitles;
using System.Collections.Generic;

namespace Cinema.Domain.Models
{
    public sealed class FilmCrew
    {
        private readonly IEnumerable<Actor> actors;
        private readonly Director director;

        public FilmCrew(Director director, IEnumerable<Actor> actors)
        {
            this.actors = actors;
            this.director = director;
        }

        public IEnumerable<Actor> Actors => actors;

        public Director Director => director;
    }
}