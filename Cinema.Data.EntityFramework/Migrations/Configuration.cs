using Cinema.Data.EntityFramework.Dtos;
using Cinema.Domain.Models;
using System;
using System.Data.Entity.Migrations;

namespace Cinema.Data.EntityFramework.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<FilmDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(FilmDbContext context)
        {
            FilmDto filmDto = new FilmDto()
            {
                Language = Language.English,
                ReleaseDate = new DateTime(2018, 10, 19),
                Title = "Halloween"
            };

            context.Films.Add(filmDto);

            context.FilmCrews.Add(
                new FilmCrewDto()
                {
                    DirectorDto = new DirectorDto()
                    {
                        HumanDto = new HumanDto()
                        {
                            Name = "David",
                            Surname = "Gordon Green"
                        }
                    },
                    ActorDtos = new ActorDto[] {
                        new ActorDto()
                        {
                            HumanDto = new HumanDto()
                            {
                                Name = "Judi", Surname = "Greer"
                            }
                        },
                        new ActorDto()
                        {
                            HumanDto = new HumanDto()
                            {
                                Name = "Andi", Surname = "Matichak"
                            }
                        }
                    },
                    FilmDto = filmDto
                }
                );
        }
    }
}