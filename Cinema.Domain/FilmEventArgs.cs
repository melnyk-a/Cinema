using Cinema.Domain.Models;
using System;

namespace Cinema.Domain
{
    public sealed class FilmEventArgs : EventArgs
    {
        private readonly Film addedFilm;

        public FilmEventArgs(Film addedFilm)
        {
            this.addedFilm = addedFilm;
        }

        public Film AddedFilm => addedFilm;
    }
}