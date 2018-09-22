using System.Collections.Generic;
using Cinema.Domain.Models;

namespace Cinema.Data.Stub
{
    internal sealed class StubCinemaDataGateway : ICinemaDataGateway
    {
        private readonly ICollection<Film> films = new List<Film>();
        public IEnumerable<Film> GetAllFilms()
        {
            films.Add(new Film("The House with a Clock in Its Walls"));
            films.Add(new Film("Fahrenheit 11/9"));
            films.Add(new Film("Life Itself"));

            return films;
        }
    }
}