using System.Collections.Generic;
using Cinema.Data;
using Cinema.Domain.Models;

namespace Cinema.Domain
{
    public sealed class CinemaManager : ICinemaProvider, ICinemaManager
    {
        private readonly ICinemaDataService dataService;

        public CinemaManager(ICinemaDataService dataService)
        {
            this.dataService = dataService;
        }

        public IEnumerable<Film> GetAllFilms()
        {
            ICinemaDataGateway dataGateway = dataService.OpenDataGateway();

            IEnumerable<Film> films = dataGateway.GetAllFilms();

            dataService.CloseDataGateway(dataGateway);

            return films;
        }
    }
}