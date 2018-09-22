using Cinema.Domain.Models;
using System.Collections.Generic;

namespace Cinema.Data
{
    public interface ICinemaDataGateway
    {
        void AddFilm();
        IEnumerable<Film> GetAllFilms();
    }
}