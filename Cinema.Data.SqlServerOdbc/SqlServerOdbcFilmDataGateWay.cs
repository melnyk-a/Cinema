using Cinema.Domain.Models;
using Cinema.Domain.Models.JobTitles;
using Cinema.Utilities.Data.Dtos;
using Cinemas;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;

namespace Cinema.Data.SqlServerOdbc
{
    internal sealed class SqlServerOdbcFilmDataGateway : DisposableObject, IFilmDataGateway
    {
        private readonly Lazy<OdbcConnection> connection;

        public SqlServerOdbcFilmDataGateway()
        {
            connection = new Lazy<OdbcConnection>(() =>
            {
                var connection = new OdbcConnection($"Driver={{SQL Server}};Server=(local);Database=Films;Trusted_Security=Yes;");
                connection.Open();

                return connection;
            });
        }

        private OdbcConnection Connection => connection.Value;

        private void AddBlurayRelease(OdbcCommand command, int filmId, bool hasBlurayRelease)
        {
            string value = hasBlurayRelease? "1" : "0";
            command.CommandText = $@"update Films 
                                    set HasBlurayRelease = {value} 
                                    where Id = {filmId}";
            command.ExecuteNonQuery();
        }

        public void AddFilm(Film film)
        {
            OdbcTransaction transaction = Connection.BeginTransaction();
            OdbcCommand command = Connection.CreateCommand();
            command.Transaction = transaction;

            try
            {
                int langageId = GetLanguageId(command, film.Language);
                int filmId = FillFilmsTable(command, film.Title, film.ReleaseDate, langageId);
                if (film.HasBlurayRelease != null)
                {
                    AddBlurayRelease(command, filmId, film.HasBlurayRelease.Value);
                }
                FillDirectorsTable(command, film.FilmCrew.Director, filmId);
                foreach (Actor actor in film.FilmCrew.Actors)
                {
                    FillActorsTable(command, actor, filmId);
                }
                transaction.Commit();
            }
            catch (Exception e)
            {
                transaction.Rollback();
            }
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

            return new Film(filmDto.Title, filmDto.ReleaseDate, filmDto.Language, new FilmCrew(director, actors)) { HasBlurayRelease = filmDto.HasBlurayRelease };
        }

        protected override void Dispose(bool disposing)
        {
            if (connection.IsValueCreated)
            {
                connection.Value.Close();
            }
        }

        private void FillActorsTable(OdbcCommand command, Actor actor, int filmId)
        {
            command.CommandText = "{call AddActor(?, ?, ?)}";
            command.Parameters.Clear();

            command.Parameters.Add(new OdbcParameter("@name", OdbcType.NVarChar, 100)
            {
                Value = actor.Human.Name
            });
            command.Parameters.Add(new OdbcParameter("@surname", OdbcType.NVarChar, 100)
            {
                Value = actor.Human.Surname
            });
            command.Parameters.Add(new OdbcParameter("@filmId", OdbcType.Int)
            {
                Value = filmId
            });

            command.ExecuteNonQuery();
        }

        private void FillDirectorsTable(OdbcCommand command, Director director, int filmId)
        {
            command.CommandText = "{call AddDirector(?, ?, ?)}";
            command.Parameters.Clear();

            command.Parameters.Add(new OdbcParameter("@name", OdbcType.NVarChar, 100)
            {
                Value = director.Human.Name
            });
            command.Parameters.Add(new OdbcParameter("@surname", OdbcType.NVarChar, 100)
            {
                Value = director.Human.Surname
            });
            command.Parameters.Add(new OdbcParameter("@filmId", OdbcType.Int)
            {
                Value = filmId
            });

            command.ExecuteNonQuery();
        }

        private int FillFilmsTable(OdbcCommand command, string title, DateTime releaseDate, int langageId)
        {
            command.CommandText = "{? = call AddFilm(?, ?, ?)}";

            OdbcParameter filmId = command.Parameters.AddWithValue("@return_value", OdbcType.Int);
            filmId.Direction = ParameterDirection.ReturnValue;

            OdbcParameter titleParameter = command.Parameters.Add("@title", OdbcType.NVarChar, 100);
            titleParameter.Value = title;

            OdbcParameter releaseDateParameter = command.Parameters.Add("@releaseDate", OdbcType.DateTime);
            releaseDateParameter.Value = releaseDate.ToString();

            OdbcParameter languageParameter = command.Parameters.Add("@languageId", OdbcType.Int);
            languageParameter.Value = langageId;

            command.ExecuteNonQuery();

            return (int)filmId.Value;
        }

        public IEnumerable<Film> GetAllFilms()
        {
            var filmsDtos = new List<FilmDto>();

            var command = new OdbcCommand(
                @"select Films.Id, Title, Languages.Name as Language, ReleaseDate, HasBlurayRelease
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
                        ReleaseDate = (DateTime)reader["ReleaseDate"],
                        Title = (string)reader["Title"],
                    };
                    object blurayRelease = reader["HasBlurayRelease"];
                    filmDto.HasBlurayRelease = Convert.IsDBNull(blurayRelease) ? null : (bool?)blurayRelease;

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

        private int GetLanguageId(OdbcCommand command, Language language)
        {
            command.CommandText = $"select Id from Languages where Languages.Name = N'{language.ToString()}'";
            int result = (int)command.ExecuteScalar();
            return result;
        }
    }
}