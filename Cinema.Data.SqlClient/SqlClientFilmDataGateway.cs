using System;
using System.Collections.Generic;
using Cinema.Domain.Models;
using Cinemas;
using System.Data.SqlClient;
using Cinema.Utilities.Data.Dtos;
using Cinema.Domain.Models.JobTitles;
using System.Data;

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
            SqlTransaction transaction = Connection.BeginTransaction();
            SqlCommand command = Connection.CreateCommand();
            command.Transaction = transaction;

            try
            {
                int langageId = GetLanguageId(command, film.Language);
                int filmId = FillFilmsTable(command, film.Title, film.ReleaseDate, langageId);
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

        private void FillActorsTable(SqlCommand command, Actor actor, int filmId)
        {
            command.CommandText = "AddActor";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Clear();

            command.Parameters.Add(new SqlParameter("@name", SqlDbType.NVarChar, 100)
            {
                Value = actor.Human.Name
            });
            command.Parameters.Add(new SqlParameter("@surname", SqlDbType.NVarChar, 100)
            {
                Value = actor.Human.Surname
            });
            command.Parameters.Add(new SqlParameter("@filmId", SqlDbType.Int)
            {
                Value = filmId
            });

            command.ExecuteNonQuery();
        }

        private void FillDirectorsTable(SqlCommand command, Director director, int filmId)
        {
            command.CommandText = "AddDirector";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Clear();

            command.Parameters.Add(new SqlParameter("@name", SqlDbType.NVarChar, 100)
            {
                Value = director.Human.Name
            });
            command.Parameters.Add(new SqlParameter("@surname", SqlDbType.NVarChar, 100)
            {
                Value = director.Human.Surname
            });
            command.Parameters.Add(new SqlParameter("@filmId", SqlDbType.Int)
            {
                Value = filmId
            });

            command.ExecuteNonQuery();
        }

        private int FillFilmsTable(SqlCommand command, string title, DateTime releaseDate, int langageId)
        {
            command.CommandText = "AddFilm";
            command.CommandType = CommandType.StoredProcedure;

            SqlParameter titleParameter = command.Parameters.Add("@title", SqlDbType.NVarChar, 100);
            titleParameter.Value = title;

            SqlParameter releaseDateParameter = command.Parameters.Add("@releaseDate", SqlDbType.DateTime);
            releaseDateParameter.Value = releaseDate.ToString();

            SqlParameter languageParameter = command.Parameters.Add("@languageId", SqlDbType.Int);
            languageParameter.Value = langageId;

            SqlParameter filmId = command.Parameters.AddWithValue("@return_value", SqlDbType.Int);
            filmId.Direction = ParameterDirection.ReturnValue;

            command.ExecuteNonQuery();

            return (int)filmId.Value;
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

        private int GetLanguageId(SqlCommand command, Language language)
        {
            command.CommandText = $"select Id from Languages where Languages.Name = N'{language.ToString()}'";
            int result = (int)command.ExecuteScalar();
            return result;
        }
    }
}