namespace Cinema.Data.Stub
{
    internal sealed class StubCinemaDataService : ICinemaDataService
    {
        public void CloseDataGateway(ICinemaDataGateway dataGateway)
        {
            // Do nothing.
        }

        public ICinemaDataGateway OpenDataGateway()
        {
            return new StubCinemaDataGateway();
        }
    }
}