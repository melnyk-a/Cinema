using System;
using System.Collections.Generic;
using System.Data.Odbc;
using Cinema.Domain.Models;
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
                var connection = new OdbcConnection($"Driver={{SQL Server}};Server ={Environment.MachineName};Database=Films;Trusted_Security=Yes;");
                connection.Open();

                return connection;
            });
        }

        private OdbcConnection Connection => connection.Value;

        public void AddFilm(Film film)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Film> GetAllFilms()
        {
            throw new System.NotImplementedException();
        }

        protected override void Dispose(bool disposing)
        {
            if(connection.IsValueCreated)
            {
                connection.Value.Close();
            }
        }
    }
}