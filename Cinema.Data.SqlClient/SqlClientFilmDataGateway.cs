using System;
using System.Collections.Generic;
using Cinema.Domain.Models;
using Cinemas;
using System.Data.SqlClient;
using Cinema.Utilities.Data.Dtos;
using Cinema.Domain.Models.JobTitles;

namespace Cinema.Data.SqlClient
{
    internal sealed class SqlClientFilmDataGateway : DisposableObject, IFilmDataGateway
    {
        private readonly Lazy<SqlConnection> connection;

        public SqlClientFilmDataGateway()
        {
            connection = new Lazy<SqlConnection>(() =>
            {
                var connection = new SqlConnection("Server = (local); Database = Films; Trusted_Connection = True");
                connection.Open();

                return connection;
            });
        }

        private SqlConnection Connection => connection.Value;

        public void AddFilm(Film film)
        {
            throw new System.NotImplementedException();
        }

        private IEnumerable<Actor> CreateActorsList(FilmDto filmDto)
        {
            var command = new SqlCommand(
                $@"select Humans.Name, Humans.Surname
                  from ActorsFilms inner join Actors on ActorId = Actors.Id
                  inner join Humans on HumanId = Humans.Id
                  where FilmId = {filmDto.Id}",
                Connection
             );

            ICollection<Actor> actors = new List<Actor>();

            using (SqlDataReader reader = command.ExecuteReader())
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
            var command = new SqlCommand(
               $@"select Humans.Name, Humans.Surname
                  from DirectorsFilms inner join Directors on DirectorId = Directors.Id
                  inner join Humans on HumanId = Humans.Id
                  where FilmId = {filmDto.Id}",
               Connection
            );

            Human human = null;
            using (SqlDataReader reader = command.ExecuteReader())
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

        protected override void Dispose(bool disposing)
        {
            if(connection.IsValueCreated)
            {
                connection.Value.Close();
            }
        }

        public IEnumerable<Film> GetAllFilms()
        {
            var filmDtos = new List<FilmDto>();

            var command = new SqlCommand(
                @"select Films.Id, Title, Languages.Name as Language, ReleaseDate
                  from Films inner join Languages on Films.LanguageId = Languages.Id",
                Connection
            );

            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var filmDto = new FilmDto
                    {
                        Id = (int)reader["Id"],
                        Language = (Language)Enum.Parse(typeof(Language), (string)reader["Language"]),
                        ReleaseDate = (DateTime)reader["ReleaseDate"],
                        Title = (string)reader["Title"]
                    };
                    filmDtos.Add(filmDto);
                }
            }

            var films = new List<Film>();

            foreach (FilmDto filmDto in filmDtos)
            {
                Film film = CreateFilm(filmDto);
                films.Add(film);
            }

            return films;
        }
    }
}