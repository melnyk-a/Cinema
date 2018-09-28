using Cinema.Domain.Models;
using System;

namespace Cinema.Domain
{
    public sealed class FilmEventArgs : EventArgs
    {
        private readonly Film film;

        public FilmEventArgs(Film film)
        {
            this.film = film;
        }

        public Film Film => film;
    }
}