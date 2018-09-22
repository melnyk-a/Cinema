namespace Cinema.Data
{
    public interface ICinemaDataService
    {
        void CloseDataGateway(ICinemaDataGateway dataGateway);
        ICinemaDataGateway OpenDataGateway();
    }
}