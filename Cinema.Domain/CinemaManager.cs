using Cinema.Data;
using Cinema.Domain.Models;
using System;
using System.Collections.Generic;

namespace Cinema.Domain
{
    public sealed class FilmManager : IFilmProvider, IFilmManager
    {
        private readonly IFilmDataService dataService;

        public FilmManager(IFilmDataService dataService)
        {
            this.dataService = dataService;
        }

        public event EventHandler<FilmEventArgs> FilmAdded;

        public void AddFilm(Film film)
        {
            IFilmDataGateway dataGateway = dataService.OpenDataGateway();

            dataGateway.AddFilm(film);
            OnFilmAdded(new FilmEventArgs(film));

            dataService.CloseDataGateway(dataGateway);
        }

        public IEnumerable<Film> GetAllFilms()
        {
            IFilmDataGateway dataGateway = dataService.OpenDataGateway();

            IEnumerable<Film> films = dataGateway.GetAllFilms();

            dataService.CloseDataGateway(dataGateway);

            return films;
        }

        private void OnFilmAdded(FilmEventArgs e)
        {
            FilmAdded?.Invoke(this, e);
        }
    }
}