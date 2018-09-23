using System;

namespace Cinema.Data.XmlXDocument
{
    internal sealed class XmlXDocumentFilmDataService : IFilmDataService
    {
        public IFilmDataGateway OpenDataGateway()
        {
            return new XmlXDocumentFilmDataGateway();
        }
    }
}