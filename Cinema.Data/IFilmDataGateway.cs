using Cinema.Domain.Models;
using System.Collections.Generic;

namespace Cinema.Data
{
    public interface IFilmDataGateway
    {
        void AddFilm(Film film);
        IEnumerable<Film> GetAllFilms();
    }
}