namespace Cinema.Data
{
    public interface IFilmDataService
    {
        void CloseDataGateway(IFilmDataGateway dataGateway);
        IFilmDataGateway OpenDataGateway();
    }
}