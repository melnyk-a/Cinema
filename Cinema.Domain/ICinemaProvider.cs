using Cinema.Domain.Models;
using System.Collections.Generic;

namespace Cinema.Domain
{
    public interface ICinemaProvider
    {
        IEnumerable<Film> GetAllFilms();
    }
}