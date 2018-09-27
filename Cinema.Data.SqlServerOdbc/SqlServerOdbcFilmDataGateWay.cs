using System;
using System.Collections.Generic;
using System.Data.Odbc;
using Cinema.Data.SqlServerOdbc.Dtos;
using Cinema.Domain.Models;
using Cinema.Domain.Models.JobTitles;
using Cinemas;

namespace Cinema.Data.SqlServerOdbc
{
    internal sealed class SqlServerOdbcFilmDataGateway : DisposableObject, IFilmDataGateway
    {
        private readonly Lazy<OdbcConnection> connection;

        public SqlServerOdbcFilmDataGateway()
        {
            connection = new Lazy<OdbcConnection>(() =>
            {
                var connection = new OdbcConnection($"Driver={{SQL Server}};Server={Environment.MachineName};Database=Films;Trusted_Security=Yes;");
                connection.Open();

                return connection;
            });
        }

        private OdbcConnection Connection => connection.Value;

        public void AddFilm(Film film)
        {
            throw new System.NotImplementedException();
        }

        private IEnumerable<Actor> CreateActorsList(FilmDto filmDto)
        {
            var command = new OdbcCommand(
                $@"select Humans.Name, Humans.Surname
                  from ActorsFilms inner join Actors on ActorId = Actors.Id
                  inner join Humans on HumanId = Humans.Id
                  where FilmId = {filmDto.Id}",
                Connection
             );

            ICollection<Actor> actors = new List<Actor>();

            using (OdbcDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var name = (string)reader["Name"];
                    var surname = (string)reader["Surname"];
                    Human human = new Human(name, surname);
                    actors.Add(new Actor(human));
                }
            }

            return actors;
        }

        private Director CreateDirector(FilmDto filmDto)
        {
            var command = new OdbcCommand(
               $@"select Humans.Name, Humans.Surname
                  from DirectorsFilms inner join Directors on DirectorId = Directors.Id
                  inner join Humans on HumanId = Humans.Id
                  where FilmId = {filmDto.Id}",
               Connection
            );

            Human human = null;
            using (OdbcDataReader reader = command.ExecuteReader())
            {
                reader.Read();
                var name = (string)reader["Name"];
                var surname = (string)reader["Surname"];
                human = new Human(name, surname);
            }

            return new Director(human);
        }

        private Film CreateFilm(FilmDto filmDto)
        {
            Director director = CreateDirector(filmDto);
            IEnumerable<Actor> actors = CreateActorsList(filmDto);

            return new Film(filmDto.Title, filmDto.ReleaseDate, filmDto.Language, new FilmCrew(director, actors));
        }

        public IEnumerable<Film> GetAllFilms()
        {
            var filmsDtos = new List<FilmDto>();

            var command = new OdbcCommand(
                @"select Films.Id, Title, Languages.Name as Language, ReleaseDate
                  from Films inner join Languages on Films.LanguageId = Languages.Id",
                Connection
            );

            using (OdbcDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var filmDto = new FilmDto
                    {
                        Id = (int)reader["Id"],
                        Language = (Language)Enum.Parse(typeof(Language), (string)reader["Language"]),
                        ReleaseDate = DateTime.Parse((string)reader["ReleaseDate"]),
                        Title = (string)reader["Title"]
                    };
                    filmsDtos.Add(filmDto);
                }
            }

            var films = new List<Film>();

            foreach (FilmDto filmDto in filmsDtos)
            {
                Film film = CreateFilm(filmDto);
                films.Add(film);
            }

            return films;
        }

        protected override void Dispose(bool disposing)
        {
            if (connection.IsValueCreated)
            {
                connection.Value.Close();
            }
        }
    }
}