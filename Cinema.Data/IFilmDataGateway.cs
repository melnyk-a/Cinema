using Cinema.Domain.Models;
using System;
using System.Collections.Generic;

namespace Cinema.Data
{
    public interface IFilmDataGateway : IDisposable
    {
        void AddFilm(Film film);
        IEnumerable<Film> GetAllFilms();
    }
}