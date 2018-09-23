using Cinema.Domain.Models;
using System;
using System.Collections.Generic;

namespace Cinema.Domain
{
    public interface ICinemaProvider
    {
        event EventHandler<FilmEventArgs> FilmAdded;
        IEnumerable<Film> GetAllFilms();
    }
}