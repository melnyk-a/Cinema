namespace Cinema.Data
{
    public interface IFilmDataService
    {
        IFilmDataGateway OpenDataGateway();
    }
}