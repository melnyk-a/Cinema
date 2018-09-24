using System;

namespace Cinema.Data.XmlXDocument
{
    public sealed class XmlXDocumentFilmDataService : IFilmDataService
    {
        public IFilmDataGateway OpenDataGateway()
        {
            return new XmlXDocumentFilmDataGateway();
        }
    }
}