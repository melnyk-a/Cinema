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
            using (IFilmDataGateway dataGateway = dataService.OpenDataGateway())
            {
                dataGateway.AddFilm(film);
                OnFilmAdded(new FilmEventArgs(film));
            }
        }

        public IEnumerable<Film> GetAllFilms()
        {
            using (IFilmDataGateway dataGateway = dataService.OpenDataGateway())
            {
                return dataGateway.GetAllFilms();
            }
        }

        private void OnFilmAdded(FilmEventArgs e)
        {
            FilmAdded?.Invoke(this, e);
        }
    }
}