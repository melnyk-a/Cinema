namespace Cinema.Data.SqlServerOdbc
{
    public sealed class SqlServerOdbcFilmDataService : IFilmDataService
    {
        public IFilmDataGateway OpenDataGateway()
        {
            return new SqlServerOdbcFilmDataGateway();
        }
    }
}