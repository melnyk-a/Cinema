namespace Cinema.Data.EntityFramework
{
    public sealed class EntityFrameworkFilmDataService : IFilmDataService
    {
        public IFilmDataGateway OpenDataGateway()
        {
            return new EntityFrameworkFilmDataGateway();
        }
    }
}