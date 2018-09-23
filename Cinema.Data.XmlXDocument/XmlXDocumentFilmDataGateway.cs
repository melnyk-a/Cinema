using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Cinema.Domain.Models;
using Cinemas;

namespace Cinema.Data.XmlXDocument
{
    internal sealed class XmlXDocumentFilmDataGateway : DisposableObject, IFilmDataGateway
    {
        private const string FilePath = @"..\..\..\DataSources\Films.xml";

        private readonly Lazy<XDocument> document;
        private bool isSaveRequired;

        public XmlXDocumentFilmDataGateway()
        {
            document = new Lazy<XDocument>(() => XDocument.Load(FilePath));
        }

        private XDocument Document => document.Value;

        public void AddFilm(Film film)
        {
            throw new System.NotImplementedException();
        }

        protected override void Dispose(bool disposing)
        {
            if(isSaveRequired)
            {
                Document.Save(FilePath);
            }
        }

        public IEnumerable<Film> GetAllFilms()
        {
            throw new System.NotImplementedException();
        }
    }
}