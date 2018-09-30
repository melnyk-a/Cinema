namespace Cinema.Data.SqlClient
{
    public sealed class SqlClientFilmDataService : IFilmDataService
    {
        public IFilmDataGateway OpenDataGateway()
        {
            return new SqlClientFilmDatGateway();
        }
    }
}