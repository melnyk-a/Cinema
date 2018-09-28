namespace Cinema.Data.Stub
{
    public sealed class StubFilmDataService : IFilmDataService
    {
        public IFilmDataGateway OpenDataGateway()
        {
            return new StubFilmDataGateway();
        }
    }
}