namespace Cinema.Data.Stub
{
    public sealed class StubFilmDataService : IFilmDataService
    {
        public void CloseDataGateway(IFilmDataGateway dataGateway)
        {
            // Do nothing.
        }

        public IFilmDataGateway OpenDataGateway()
        {
            return new StubFilmDataGateway();
        }
    }
}