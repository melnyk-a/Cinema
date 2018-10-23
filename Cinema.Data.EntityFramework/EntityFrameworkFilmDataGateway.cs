using Cinema.Data.EntityFramework.Dtos;
using Cinema.Domain.Models;
using Cinema.Domain.Models.JobTitles;
using Cinemas;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Cinema.Data.EntityFramework
{
    internal sealed class EntityFrameworkFilmDataGateway : DisposableObject, IFilmDataGateway
    {
        private readonly Lazy<FilmDbContext> filmContext;

        public EntityFrameworkFilmDataGateway()
        {
            filmContext = new Lazy<FilmDbContext>(() => new FilmDbContext());
        }

        private FilmDbContext FilmContext => filmContext.Value;

        public void AddFilm(Film film)
        {
            FilmDto filmDto = new FilmDto()
            {
                HasBlurayRelease = film.HasBlurayRelease,
                Language = film.Language,
                ReleaseDate = film.ReleaseDate,
                Title = film.Title
            };

            DirectorDto directorDto = new DirectorDto()
            {
                HumanDto = new HumanDto()
                {
                    Name = film.FilmCrew.Director.Human.Name,
                    Surname = film.FilmCrew.Director.Human.Surname
                }
            };

            ICollection<ActorDto> actorDtos = new List<ActorDto>();
            foreach (Actor actor in film.FilmCrew.Actors)
            {
                actorDtos.Add(new ActorDto()
                {
                    HumanDto = new HumanDto()
                    {
                        Name = actor.Human.Name,
                        Surname = actor.Human.Surname
                    }
                }
                );
            }
            FilmCrewDto filmCrewDto = new FilmCrewDto()
            {
                DirectorDto = directorDto,
                ActorDtos = actorDtos,
                FilmDto = filmDto
            };

            FilmContext.FilmCrews.Add(filmCrewDto);
            FilmContext.Films.Add(filmDto);
        }

        private IEnumerable<Actor> CreateActors(FilmDto filmDto)
        {
            var filmCrew = FilmContext.FilmCrews
               .Include(currnetFilmCrew => currnetFilmCrew.ActorDtos.Select(actor => actor.HumanDto))
               .Include(currnetFilmCrew => currnetFilmCrew.ActorDtos)
               .Where(currnetFilmCrew => currnetFilmCrew.Id.Equals(filmDto.Id))
               .First();

            ICollection<Actor> actors = new List<Actor>();

            foreach (var actorDto in filmCrew.ActorDtos)
            {
                actors.Add(new Actor(
                    new Human(actorDto.HumanDto.Name, actorDto.HumanDto.Surname)
                    )
                );
            }

            return actors;
        }

        private Director CreateDirector(FilmDto filmDto)
        {
            var filmCrew = FilmContext.FilmCrews
                .Include(currnetFilmCrew => currnetFilmCrew.DirectorDto.HumanDto)
                .Include(currnetFilmCrew => currnetFilmCrew.DirectorDto)
                .Where(currnetFilmCrew => currnetFilmCrew.Id.Equals(filmDto.Id))
                .First();

            return new Director(
                        new Human(
                            filmCrew.DirectorDto.HumanDto.Name,
                            filmCrew.DirectorDto.HumanDto.Surname
                        )
            );
        }

        private Film CreateFilm(FilmDto filmDto)
        {
            Director director = CreateDirector(filmDto);
            IEnumerable<Actor> actors = CreateActors(filmDto);

            return new Film(
                filmDto.Title, 
                filmDto.ReleaseDate, 
                filmDto.Language, 
                new FilmCrew(director, actors)
                )
            {
                HasBlurayRelease = filmDto.HasBlurayRelease
            };
        }

        protected override void Dispose(bool disposing)
        {
            if (filmContext.IsValueCreated)
            {
                filmContext.Value.SaveChanges();
            }
        }

        public IEnumerable<Film> GetAllFilms()
        {
            var films = new List<Film>();

            ICollection<FilmDto> filmDtos = FilmContext.Films.ToList();

            foreach (FilmDto filmDto in filmDtos)
            {
                Film film = CreateFilm(filmDto);
                films.Add(film);
            }

            return films;
        }
    }
}