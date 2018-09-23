using System.Collections.Generic;
using Cinema.Domain.Models;
using Cinemas;

namespace Cinema.Data.XmlXDocument
{
    internal sealed class XmlXDocumentFilmDataGateway : DisposableObject, IFilmDataGateway
    {
        public void AddFilm(Film film)
        {
            throw new System.NotImplementedException();
        }

        protected override void Dispose(bool disposing)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Film> GetAllFilms()
        {
            throw new System.NotImplementedException();
        }
    }
}