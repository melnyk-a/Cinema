using Cinema.Domain.Models;
using Cinema.Domain.Models.JobTitles;
using Cinemas;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;

namespace Cinema.Data.XmlXDocument
{
    internal sealed class XmlXDocumentFilmDataGateway : DisposableObject, IFilmDataGateway
    {
        private const string ActorElementName = "Actor";
        private const string ActorsElementName = "Actors";
        private const string DirectorElementName = "Director";
        private const string FilePath = @"..\..\..\DataSources\Films.xml";
        private const string FilmCrewElementName = "FilmCrew";
        private const string FilmElementName = "Film";
        private const string FilmsElementName = "Films";
        private const string LanguageAttribute = "Language";
        private const string NameAttribute = "Name";
        private const string ReleaseDateAttribute = "ReleaseDate";
        private const string SurnameAttribute = "Surname";
        private const string TitleAttribute = "Title";

        private readonly Lazy<XDocument> document;
        private bool isSaveRequired;

        public XmlXDocumentFilmDataGateway()
        {
            document = new Lazy<XDocument>(OpenXmlFile); 
        }

        private XDocument Document => document.Value;

        public void AddFilm(Film film)
        {
            XElement actors = new XElement(ActorsElementName);

            foreach(Actor actor in film.FilmCrew.Actors)
            {
                actors.Add(
                    new XElement(
                        ActorElementName, 
                        new XAttribute(NameAttribute, actor.Human.Name),
                        new XAttribute(SurnameAttribute, actor.Human.Surname)));
            }
            XElement filmCrew = new XElement(
                FilmCrewElementName, 
                new XElement(
                    DirectorElementName, 
                     new XAttribute(NameAttribute, film.FilmCrew.Director.Human.Name),
                     new XAttribute(SurnameAttribute, film.FilmCrew.Director.Human.Surname)),
                actors);

            Document.Element(FilmsElementName).Add(
                new XElement(
                    FilmElementName,
                     filmCrew,
                     new XAttribute(TitleAttribute, film.Title),
                     new XAttribute(LanguageAttribute, film.Language.ToString()),
                     new XAttribute(ReleaseDateAttribute, film.ReleaseDate.ToShortDateString())));
                             
            isSaveRequired = true;
        }

        protected override void Dispose(bool disposing)
        {
            if (isSaveRequired)
            {
                Document.Save(FilePath);
            }
        }

        public IEnumerable<Film> GetAllFilms()
        {
            ICollection<Film> films = new List<Film>();

            string title;
            DateTime releaseDate;
            Language language;
            Director director;
            ICollection<Actor> actors = new List<Actor>();
            FilmCrew filmCrew;

            IEnumerable<XElement> filmElements = Document.Element(FilmsElementName).Elements(FilmElementName);
            foreach (XElement filmElement in filmElements)
            {
                title = filmElement.Attribute(TitleAttribute).Value;
                releaseDate = DateTime.Parse(filmElement.Attribute(ReleaseDateAttribute).Value);
                language = (Language)Enum.Parse(typeof(Language), filmElement.Attribute(LanguageAttribute).Value);

                XElement directorElement = filmElement.Element(FilmCrewElementName).Element(DirectorElementName);
                director = new Director(new Human(directorElement.Attribute(NameAttribute).Value, directorElement.Attribute(SurnameAttribute).Value));
                IEnumerable<XElement> actorElements = filmElement.Element(FilmCrewElementName).Element(ActorsElementName).Elements(ActorElementName);
                foreach (XElement actorElement in actorElements)
                {
                    actors.Add(new Actor(new Human(actorElement.Attribute(NameAttribute).Value, actorElement.Attribute(SurnameAttribute).Value)));
                }
                filmCrew = new FilmCrew(director, actors);
                films.Add(new Film(title, releaseDate, language, filmCrew));
                actors.Clear();
            }

            return films;
        }

        private XDocument OpenXmlFile()
        {
            XDocument result;

            if (File.Exists(FilePath))
            {
                result = XDocument.Load(FilePath);
            }
            else
            {
                result = new XDocument();
                result.Add(new XElement("Films"));
            }

            return result;
        }
    }
}