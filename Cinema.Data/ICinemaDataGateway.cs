using Cinema.Domain.Models;
using System.Collections.Generic;

namespace Cinema.Data
{
    public interface ICinemaDataGateway
    {
        IEnumerable<Film> GetAllFilms();
    }
}