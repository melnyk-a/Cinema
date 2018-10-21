using System.Collections.Generic;

namespace Cinema.Data.EntityFramework.Dtos
{
    internal sealed class FilmCrewDto
    {
        public ICollection<ActorDto> ActorDtos { get; set; }

        public DirectorDto DirectorDto { get; set; }

        public int Id { get; set; }

        public FilmDto FilmDto { get; set; }
    }
}