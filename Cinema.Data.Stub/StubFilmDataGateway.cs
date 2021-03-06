﻿using Cinema.Domain.Models;
using Cinema.Domain.Models.JobTitles;
using Cinemas;
using System;
using System.Collections.Generic;

namespace Cinema.Data.Stub
{
    internal sealed class StubFilmDataGateway : DisposableObject, IFilmDataGateway
    {
        private readonly ICollection<Film> films = new List<Film>();

        public void AddFilm(Film film)
        {
            films.Add(film);
        }
        protected override void Dispose(bool disposing)
        {
            // Do nothing.
        }

        public IEnumerable<Film> GetAllFilms()
        {
            films.Add(new Film("The House with a Clock in Its Walls",
                DateTime.Parse("27 September 2018"),
                Language.English,
                new FilmCrew(
                    new Director(new Human("Eli", "Roth")),
                    new List<Actor>()
                    {
                       new Actor(new Human("Jack", "Black")),
                       new Actor(new Human("Cate", "Blanchett")),
                       new Actor(new Human("Owen", "Vaccaro"))
                    })
             )
            {
                HasBlurayRelease = true
            });
            films.Add(new Film("Fahrenheit 11/9",
                DateTime.Parse("21 September 2018"),
                Language.English,
                new FilmCrew(
                    new Director(new Human("Michael", "Moore")),
                    new List<Actor>()
                    {
                       new Actor(new Human("David", "Hogg")),
                       new Actor(new Human("Michael", "Moore")),
                       new Actor(new Human("Alexandria", "Ocasio-Cortez"))
                    })
             )
            {
                HasBlurayRelease = false
            });
            films.Add(new Film("Life Itself",
               DateTime.Parse("21 September 2018"),
               Language.Spanish,
               new FilmCrew(
                   new Director(new Human("Dan", "Fogelman")),
                   new List<Actor>()
                   {
                       new Actor(new Human("Oscar", "Isaac")),
                       new Actor(new Human("Olivia", "Wilde")),
                       new Actor(new Human("Annette", "Bening"))
                   })
            ));

            return films;
        }
    }
}